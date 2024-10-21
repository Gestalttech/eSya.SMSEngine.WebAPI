using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eSya.SMSEngine.DL.Entities
{
    public partial class eSyaEnterprise : DbContext
    {
        public static string _connString = "";

        public eSyaEnterprise()
        {
        }

        public eSyaEnterprise(DbContextOptions<eSyaEnterprise> options)
            : base(options)
        {
        }

        public virtual DbSet<GtEcbsln> GtEcbslns { get; set; } = null!;
        public virtual DbSet<GtEcbsmn> GtEcbsmns { get; set; } = null!;
        public virtual DbSet<GtEcfmfd> GtEcfmfds { get; set; } = null!;
        public virtual DbSet<GtEcfmpa> GtEcfmpas { get; set; } = null!;
        public virtual DbSet<GtEcmnfl> GtEcmnfls { get; set; } = null!;
        public virtual DbSet<GtEcpabl> GtEcpabls { get; set; } = null!;
        public virtual DbSet<GtEcsmsd> GtEcsmsds { get; set; } = null!;
        public virtual DbSet<GtEcsmsh> GtEcsmshes { get; set; } = null!;
        public virtual DbSet<GtEcsmsr> GtEcsmsrs { get; set; } = null!;
        public virtual DbSet<GtEcsmst> GtEcsmsts { get; set; } = null!;
        public virtual DbSet<GtEcsmsv> GtEcsmsvs { get; set; } = null!;
        public virtual DbSet<GtSmsloc> GtSmslocs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(_connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GtEcbsln>(entity =>
            {
                entity.HasKey(e => new { e.BusinessId, e.LocationId });

                entity.ToTable("GT_ECBSLN");

                entity.HasIndex(e => e.BusinessKey, "IX_GT_ECBSLN")
                    .IsUnique();

                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.BusinessName).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.DateFormat).HasMaxLength(25);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.LocationDescription).HasMaxLength(150);

                entity.Property(e => e.Lstatus).HasColumnName("LStatus");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ShortDateFormat).HasMaxLength(15);

                entity.Property(e => e.ShortDesc).HasMaxLength(15);
            });

            modelBuilder.Entity<GtEcbsmn>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.MenuKey });

                entity.ToTable("GT_ECBSMN");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEcfmfd>(entity =>
            {
                entity.HasKey(e => e.FormId);

                entity.ToTable("GT_ECFMFD");

                entity.Property(e => e.FormId)
                    .ValueGeneratedNever()
                    .HasColumnName("FormID");

                entity.Property(e => e.ControllerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FormName).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ToolTip).HasMaxLength(250);
            });

            modelBuilder.Entity<GtEcfmpa>(entity =>
            {
                entity.HasKey(e => new { e.FormId, e.ParameterId });

                entity.ToTable("GT_ECFMPA");

                entity.Property(e => e.FormId).HasColumnName("FormID");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.GtEcfmpas)
                    .HasForeignKey(d => d.FormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ECFMPA_GT_ECFMFD");
            });

            modelBuilder.Entity<GtEcmnfl>(entity =>
            {
                entity.HasKey(e => new { e.FormId, e.MainMenuId, e.MenuItemId });

                entity.ToTable("GT_ECMNFL");

                entity.Property(e => e.FormId).HasColumnName("FormID");

                entity.Property(e => e.MainMenuId).HasColumnName("MainMenuID");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormNameClient).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.GtEcmnfls)
                    .HasForeignKey(d => d.FormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ECMNFL_GT_ECFMFD");
            });

            modelBuilder.Entity<GtEcpabl>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.ParameterId });

                entity.ToTable("GT_ECPABL");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ParmDesc)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParmPerc).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.ParmValue).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.BusinessKeyNavigation)
                    .WithMany(p => p.GtEcpabls)
                    .HasPrincipalKey(p => p.BusinessKey)
                    .HasForeignKey(d => d.BusinessKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ECPABL_GT_ECBSLN");
            });

            modelBuilder.Entity<GtEcsmsd>(entity =>
            {
                entity.HasKey(e => new { e.Smsid, e.ParameterId });

                entity.ToTable("GT_ECSMSD");

                entity.Property(e => e.Smsid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SMSID");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.HasOne(d => d.Sms)
                    .WithMany(p => p.GtEcsmsds)
                    .HasForeignKey(d => d.Smsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ECSMSD_GT_ECSMSH");
            });

            modelBuilder.Entity<GtEcsmsh>(entity =>
            {
                entity.HasKey(e => e.Smsid);

                entity.ToTable("GT_ECSMSH");

                entity.Property(e => e.Smsid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SMSID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId).HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Smsdescription)
                    .HasMaxLength(100)
                    .HasColumnName("SMSDescription");

                entity.Property(e => e.Smsstatement)
                    .HasMaxLength(500)
                    .HasColumnName("SMSStatement");

                entity.Property(e => e.TeventId).HasColumnName("TEventID");
            });

            modelBuilder.Entity<GtEcsmsr>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.Smsid, e.Isdcode, e.MobileNumber });

                entity.ToTable("GT_ECSMSR");

                entity.Property(e => e.Smsid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SMSID");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.RecipientName).HasMaxLength(50);

                entity.Property(e => e.Remarks).HasMaxLength(25);

                entity.HasOne(d => d.Sms)
                    .WithMany(p => p.GtEcsmsrs)
                    .HasForeignKey(d => d.Smsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ECSMSR_GT_ECSMSH");
            });

            modelBuilder.Entity<GtEcsmst>(entity =>
            {
                entity.HasKey(e => e.TeventId);

                entity.ToTable("GT_ECSMST");

                entity.Property(e => e.TeventId)
                    .ValueGeneratedNever()
                    .HasColumnName("TEventID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.TeventDesc)
                    .HasMaxLength(150)
                    .HasColumnName("TEventDesc");
            });

            modelBuilder.Entity<GtEcsmsv>(entity =>
            {
                entity.HasKey(e => e.Smsvariable);

                entity.ToTable("GT_ECSMSV");

                entity.Property(e => e.Smsvariable)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("SMSVariable");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Smscomponent)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SMSComponent");
            });

            modelBuilder.Entity<GtSmsloc>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.FormId, e.Smsid });

                entity.ToTable("GT_SMSLOC");

                entity.Property(e => e.FormId).HasColumnName("FormID");

                entity.Property(e => e.Smsid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SMSID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId1)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID1");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
