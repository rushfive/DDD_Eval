using Campaigns.Domain.Entities;
using Campaigns.Domain.Enumerations;
using Campaigns.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.EFCore
{
	public class CampaignsDbContext : DbContext
	{
		private readonly ILoggerFactory _loggerFactory;

		public CampaignsDbContext(
			DbContextOptions<CampaignsDbContext> options,
			ILoggerFactory loggerFactory)
			: base(options)
			=> _loggerFactory = loggerFactory;

		public DbSet<Campaign> Campaigns { get; set; }
		public DbSet<CampaignTask> CampaignTasks { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLoggerFactory(_loggerFactory);
			optionsBuilder.EnableSensitiveDataLogging();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CampaignEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new CampaignTaskEntityTypeConfiguration());
		}
	}

	public class CampaignEntityTypeConfiguration : IEntityTypeConfiguration<Campaign>
	{
		public void Configure(EntityTypeBuilder<Campaign> builder)
		{
			builder.HasKey(x => x.Id);
			builder.OwnsOne(x => x.Id);
			//builder
			//	.Property(x => x.Id)
			//	.HasConversion(
			//		v => v.Value,
			//		v => new CampaignId(v));

			builder.OwnsOne(
				x => x.Configuration,
				c =>
				{
					c.OwnsOne(
						x => x.BasicInfo,
						bi =>
						{
							bi.Property(x => x.Name).HasColumnName("Name");
							bi.Property(x => x.Description).HasColumnName("Description");
						});

					c.OwnsOne(
						x => x.Enrollment,
						e =>
						{
							e.Property(x => x.Suspended).HasColumnName("EnrollmentSuspended");
							e.Property(x => x.AutoEnroll).HasColumnName("AutoEnroll");
						});
				});

			builder.OwnsMany(x => x.DraftCampaignTasks).HasKey(dct => dct.CampaignId);
		}
	}

	public class CampaignTaskEntityTypeConfiguration : IEntityTypeConfiguration<CampaignTask>
	{
		public void Configure(EntityTypeBuilder<CampaignTask> builder)
		{
			builder.HasKey(x => x.Id);
			builder.OwnsOne(x => x.Id);
			//builder
			//	.Property(x => x.Id)
			//	.HasConversion(
			//		v => v.Value,
			//		v => new CampaignId(v));

			builder.OwnsOne(x => x.CampaignId);

			builder.OwnsOne(
				x => x.BasicInfo,
				bi =>
				{
					bi.Property(x => x.Name).HasColumnName("Name");
					bi.Property(x => x.Description).HasColumnName("Description");
					bi.Property(x => x.Type)
						.HasColumnName("Type")
						.HasConversion(
							v => v.ToString(),
							v => Enum.Parse<TaskType>(v));
				});
		}
	}

	//public class CampaignConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<CampaignConfiguration>
	//{
	//	public void Configure(EntityTypeBuilder<CampaignConfiguration> builder)
	//	{
	//		builder.HasKey(x => x.Id);
	//		builder.OwnsOne(x => x.Id);
	//		//builder
	//		//	.Property(x => x.Id)
	//		//	.HasConversion(
	//		//		v => v.Value,
	//		//		v => new CampaignConfigurationId(v));

	//		builder.OwnsOne(x => x.CampaignId);

	//		builder.OwnsOne(x => x.BasicInfo);
	//		builder.OwnsOne(x => x.Enrollment);
	//	}
	//}

	//public class PictureEntityTypeConfiguration : IEntityTypeConfiguration<Picture>
	//{
	//	public void Configure(EntityTypeBuilder<Picture> builder)
	//	{
	//		builder.HasKey(x => x.PictureId);
	//		builder.OwnsOne(x => x.Id);
	//		builder.OwnsOne(x => x.ParentId);
	//		builder.OwnsOne(x => x.Size);
	//	}
	//}

	//public static class AppBuilderDatabaseExtensions
	//{
	//	public static void EnsureDatabase(this IApplicationBuilder app)
	//	{
	//		var context = app.ApplicationServices.GetService<ClassifiedAdDbContext>();

	//		if (!context.Database.EnsureCreated())
	//			context.Database.Migrate();
	//	}
	//}
}
