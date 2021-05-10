using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApi.Models
{
    public partial class DbPoolDbContext : DbContext
    {
        public DbPoolDbContext()
        {
        }

        public DbPoolDbContext(DbContextOptions<DbPoolDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DataType> DataType { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<EmailType> EmailType { get; set; }
        public virtual DbSet<Pool> Pool { get; set; }
        public virtual DbSet<PoolAction> PoolAction { get; set; }
        public virtual DbSet<PoolActionHistory> PoolActionHistory { get; set; }
        public virtual DbSet<PoolStatusEmail> PoolStatusEmail { get; set; }
        public virtual DbSet<PoolStatusEmailHistory> PoolStatusEmailHistory { get; set; }
        public virtual DbSet<PoolStatusProperties> PoolStatusProperties { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<StatusEmailType> StatusEmailType { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=TR-207-NB\\MSSQLSERVER2017;Database=Level3_SM_WastePool;User Id=sa;Password=hola1;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataType>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsDateTime).HasDefaultValueSql("(sysdatetimeoffset())");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasIndex(e => e.EmailAddress)
                    .HasName("Unique_Email_1")
                    .IsUnique();

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.UpdDateTime).HasDefaultValueSql("(sysdatetimeoffset())");
            });

            modelBuilder.Entity<EmailType>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.EmailBody).IsUnicode(false);

                entity.Property(e => e.EmailSubject).IsUnicode(false);

                entity.Property(e => e.InsDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.UpdDateTime).HasDefaultValueSql("(sysdatetimeoffset())");
            });

            modelBuilder.Entity<Pool>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.SentEmail).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Pool)
                    .HasForeignKey(d => d.IdStatus)
                    .HasConstraintName("FK_Pool_Status_1");
            });

            modelBuilder.Entity<PoolAction>(entity =>
            {
                entity.HasKey(e => new { e.IdPool, e.IdStatus, e.IdProperty });

                entity.Property(e => e.InsDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.UpdDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.ValueDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.HasOne(d => d.IdPoolNavigation)
                    .WithMany(p => p.PoolAction)
                    .HasForeignKey(d => d.IdPool)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PoolAction_Pool_1");

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.PoolAction)
                    .HasForeignKey(d => d.IdProperty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PoolAction_Property_1");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.PoolAction)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PoolAction_Status_1");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.PoolAction)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("fk_PoolAction_User_1");
            });

            modelBuilder.Entity<PoolActionHistory>(entity =>
            {
                entity.Property(e => e.InsDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.UpdDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.ValueDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.HasOne(d => d.IdPoolNavigation)
                    .WithMany(p => p.PoolActionHistory)
                    .HasForeignKey(d => d.IdPool)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PoolActio__idPoo__23893F36");

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.PoolActionHistory)
                    .HasForeignKey(d => d.IdProperty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PoolActio__idPro__247D636F");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.PoolActionHistory)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PoolActio__idSta__257187A8");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.PoolActionHistory)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK__PoolActio__idUse__2665ABE1");
            });

            modelBuilder.Entity<PoolStatusEmail>(entity =>
            {
                entity.HasKey(e => new { e.IdPool, e.IdStatusEmailType });

                entity.HasOne(d => d.IdPoolNavigation)
                    .WithMany(p => p.PoolStatusEmail)
                    .HasForeignKey(d => d.IdPool)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PoolStatusEmail_Pool_1");

                entity.HasOne(d => d.IdStatusEmailTypeNavigation)
                    .WithMany(p => p.PoolStatusEmail)
                    .HasForeignKey(d => d.IdStatusEmailType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PoolStatusEmail_StatusEmailType_1");
            });

            modelBuilder.Entity<PoolStatusEmailHistory>(entity =>
            {
                entity.HasOne(d => d.IdPoolNavigation)
                    .WithMany(p => p.PoolStatusEmailHistory)
                    .HasForeignKey(d => d.IdPool)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PoolStatusEmailHistory_Pool_1");

                entity.HasOne(d => d.IdStatusEmailTypeNavigation)
                    .WithMany(p => p.PoolStatusEmailHistory)
                    .HasForeignKey(d => d.IdStatusEmailType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PoolStatusEmailHistory_StatusEmailType_1");
            });

            modelBuilder.Entity<PoolStatusProperties>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.IsCountDown).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.HasOne(d => d.IdPoolNavigation)
                    .WithMany(p => p.PoolStatusProperties)
                    .HasForeignKey(d => d.IdPool)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PoolStatusProperties_Pool_1");

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.PoolStatusProperties)
                    .HasForeignKey(d => d.IdProperty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PoolStatusProperties_Property_1");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.PoolStatusProperties)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PoolStatusProperties_Status_1");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.UpdDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.HasOne(d => d.IdDataTypeNavigation)
                    .WithMany(p => p.Property)
                    .HasForeignKey(d => d.IdDataType)
                    .HasConstraintName("fk_Property_DataType_1");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.EmailAlert).HasDefaultValueSql("((0))");

                entity.Property(e => e.InsDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.UpdDateTime).HasDefaultValueSql("(sysdatetimeoffset())");
            });

            modelBuilder.Entity<StatusEmailType>(entity =>
            {
                entity.HasOne(d => d.IdEmailTypeNavigation)
                    .WithMany(p => p.StatusEmailType)
                    .HasForeignKey(d => d.IdEmailType)
                    .HasConstraintName("fk_StatusEmailType_EmailType_1");

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.StatusEmailType)
                    .HasForeignKey(d => d.IdProperty)
                    .HasConstraintName("fk_StatusEmailType_Property_1");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.StatusEmailTypeIdStatusNavigation)
                    .HasForeignKey(d => d.IdStatus)
                    .HasConstraintName("fk_Email_Status_Status_1");

                entity.HasOne(d => d.IdStatusNextPoolNavigation)
                    .WithMany(p => p.StatusEmailTypeIdStatusNextPoolNavigation)
                    .HasForeignKey(d => d.IdStatusNextPool)
                    .HasConstraintName("fk_StatusEmailType_Status_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsDateTime).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.IsAdmin).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdDateTime).HasDefaultValueSql("(sysdatetimeoffset())");
            });
        }
    }
}
