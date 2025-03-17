using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace NASPLOID.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CHART_CATEGORY_COLOR> CHART_CATEGORY_COLORs { get; set; }

    public virtual DbSet<GRID_IMPORT> GRID_IMPORTs { get; set; }

    public virtual DbSet<GRID_IMPORT_VALUE> GRID_IMPORT_VALUEs { get; set; }

    public virtual DbSet<GRID_LINE> GRID_LINEs { get; set; }

    public virtual DbSet<GRID_STORAGE> GRID_STORAGEs { get; set; }

    public virtual DbSet<LINE_CALCULATION> LINE_CALCULATIONs { get; set; }

    public virtual DbSet<LINE_CATEGORY> LINE_CATEGORies { get; set; }

    public virtual DbSet<LINE_COLOR> LINE_COLORs { get; set; }

    public virtual DbSet<LINE_FREQUENCY> LINE_FREQUENCies { get; set; }

    public virtual DbSet<LINE_STYLE> LINE_STYLEs { get; set; }

    public virtual DbSet<LINE_TYP> LINE_TYPs { get; set; }

    public virtual DbSet<MB_DEBT_LIST> MB_DEBT_LISTs { get; set; }

    public virtual DbSet<MB_DEBT_TYPE> MB_DEBT_TYPEs { get; set; }

    public virtual DbSet<MB_DEBT_USER> MB_DEBT_USERs { get; set; }

    public virtual DbSet<MB_ERROR_CODE> MB_ERROR_CODEs { get; set; }

    public virtual DbSet<MB_GLOBAL_VARIABLE> MB_GLOBAL_VARIABLEs { get; set; }

    public virtual DbSet<MB_ROLE> MB_ROLEs { get; set; }

    public virtual DbSet<MB_USER> MB_USERs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<CHART_CATEGORY_COLOR>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("CHART_CATEGORY_COLOR");

            entity.HasIndex(e => new { e.CATEGORYID, e.USERID }, "CATEGORYID").IsUnique();

            entity.HasIndex(e => e.USERID, "FK_CC_USERID");

            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.B)
                .HasDefaultValueSql("'255'")
                .HasColumnType("int(11)");
            entity.Property(e => e.CATEGORYID).HasMaxLength(50);
            entity.Property(e => e.G)
                .HasDefaultValueSql("'255'")
                .HasColumnType("int(11)");
            entity.Property(e => e.R)
                .HasDefaultValueSql("'255'")
                .HasColumnType("int(11)");
            entity.Property(e => e.USERID).HasColumnType("int(11)");

            entity.HasOne(d => d.CATEGORY).WithMany(p => p.CHART_CATEGORY_COLORs)
                .HasForeignKey(d => d.CATEGORYID)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_CC_CATEGORYID");

            entity.HasOne(d => d.USER).WithMany(p => p.CHART_CATEGORY_COLORs)
                .HasForeignKey(d => d.USERID)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_CC_USERID");
        });

        modelBuilder.Entity<GRID_IMPORT>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("GRID_IMPORT");

            entity.HasIndex(e => e.USER_ID, "FK_IMPORT_USER_ID");

            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.MONTH).HasColumnType("int(11)");
            entity.Property(e => e.STATUS).HasColumnType("int(11)");
            entity.Property(e => e.USER_ID).HasColumnType("int(11)");
            entity.Property(e => e.YEAR).HasColumnType("int(11)");

            entity.HasOne(d => d.USER).WithMany(p => p.GRID_IMPORTs)
                .HasForeignKey(d => d.USER_ID)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_IMPORT_USER_ID");
        });

        modelBuilder.Entity<GRID_IMPORT_VALUE>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("GRID_IMPORT_VALUES");

            entity.HasIndex(e => e.GZ_ID, "FK_GZ_ID");

            entity.HasIndex(e => e.USER_ID, "FK_USER_ID");

            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.GZ_ID).HasColumnType("int(11)");
            entity.Property(e => e.USER_ID).HasColumnType("int(11)");
            entity.Property(e => e.VALUE)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'");

            entity.HasOne(d => d.GZ).WithMany(p => p.GRID_IMPORT_VALUEs)
                .HasForeignKey(d => d.GZ_ID)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_GZ_ID");

            entity.HasOne(d => d.USER).WithMany(p => p.GRID_IMPORT_VALUEs)
                .HasForeignKey(d => d.USER_ID)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_USER_ID");
        });

        modelBuilder.Entity<GRID_LINE>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("GRID_LINE");

            entity.HasIndex(e => e.LINE_CALCULATIONID, "FK_LINE_CALCULATIONID_TGL");

            entity.HasIndex(e => e.LINE_CATEGORYID, "FK_LINE_CATEGORY_TGL");

            entity.HasIndex(e => e.LINE_FREQUENCYID, "FK_LINE_FREQUENCYID_TGL");

            entity.HasIndex(e => e.LINE_STYLEID, "FK_LINE_STYLEID_TGL");

            entity.HasIndex(e => e.LINE_TYPID, "FK_LINE_TYPID_TGL");

            entity.HasIndex(e => e.LINE_USERID, "FK_LINE_USERID_TGL");

            entity.HasIndex(e => e.ID, "ID");

            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.LINE_CALCULATIONID).HasColumnType("int(11)");
            entity.Property(e => e.LINE_CATEGORYID).HasMaxLength(50);
            entity.Property(e => e.LINE_COLORID).HasColumnType("int(11)");
            entity.Property(e => e.LINE_FREQUENCYID).HasMaxLength(50);
            entity.Property(e => e.LINE_NAME).HasMaxLength(250);
            entity.Property(e => e.LINE_NUMBER).HasColumnType("int(11)");
            entity.Property(e => e.LINE_STYLEID).HasColumnType("int(11)");
            entity.Property(e => e.LINE_TYPID).HasColumnType("int(11)");
            entity.Property(e => e.LINE_USERID).HasColumnType("int(11)");

            entity.HasOne(d => d.LINE_CALCULATION).WithMany(p => p.GRID_LINEs)
                .HasForeignKey(d => d.LINE_CALCULATIONID)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_LINE_CALCULATIONID_TGL");

            entity.HasOne(d => d.LINE_CATEGORY).WithMany(p => p.GRID_LINEs)
                .HasForeignKey(d => d.LINE_CATEGORYID)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_LINE_CATEGORY_TGL");

            entity.HasOne(d => d.LINE_FREQUENCY).WithMany(p => p.GRID_LINEs)
                .HasForeignKey(d => d.LINE_FREQUENCYID)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_LINE_FREQUENCYID_TGL");

            entity.HasOne(d => d.LINE_STYLE).WithMany(p => p.GRID_LINEs)
                .HasForeignKey(d => d.LINE_STYLEID)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_LINE_STYLEID_TGL");

            entity.HasOne(d => d.LINE_TYP).WithMany(p => p.GRID_LINEs)
                .HasForeignKey(d => d.LINE_TYPID)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_LINE_TYPID_TGL");

            entity.HasOne(d => d.LINE_USER).WithMany(p => p.GRID_LINEs)
                .HasForeignKey(d => d.LINE_USERID)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_LINE_USERID_TGL");
        });

        modelBuilder.Entity<GRID_STORAGE>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("GRID_STORAGE");

            entity.HasIndex(e => e.GL_ID, "FK_GL_ID_TGS");

            entity.HasIndex(e => e.LINE_USERID, "FK_LINE_USERID_TGS");

            entity.HasIndex(e => new { e.ID, e.LINE_DAY, e.LINE_MONTH, e.LINE_YEAR, e.LINE_USERID }, "ID");

            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.GL_ID).HasColumnType("int(11)");
            entity.Property(e => e.LINE_DAY).HasColumnType("int(11)");
            entity.Property(e => e.LINE_LASTEDIT).HasColumnType("timestamp");
            entity.Property(e => e.LINE_MONTH).HasColumnType("int(11)");
            entity.Property(e => e.LINE_NOTE).HasMaxLength(1024);
            entity.Property(e => e.LINE_USERID).HasColumnType("int(11)");
            entity.Property(e => e.LINE_YEAR).HasColumnType("int(11)");
            entity.Property(e => e.VALUE)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'");

            entity.HasOne(d => d.GL).WithMany(p => p.GRID_STORAGEs)
                .HasForeignKey(d => d.GL_ID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GL_ID_TGS");

            entity.HasOne(d => d.LINE_USER).WithMany(p => p.GRID_STORAGEs)
                .HasForeignKey(d => d.LINE_USERID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LINE_USERID_TGS");
        });

        modelBuilder.Entity<LINE_CALCULATION>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("LINE_CALCULATION");

            entity.Property(e => e.ID)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.CALCULATION).HasMaxLength(100);
        });

        modelBuilder.Entity<LINE_CATEGORY>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("LINE_CATEGORY");

            entity.Property(e => e.ID).HasMaxLength(50);
        });

        modelBuilder.Entity<LINE_COLOR>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("LINE_COLOR");

            entity.Property(e => e.ID)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.BACKGROUND).HasMaxLength(50);
            entity.Property(e => e.FOREGROUND).HasMaxLength(50);
        });

        modelBuilder.Entity<LINE_FREQUENCY>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("LINE_FREQUENCY");

            entity.Property(e => e.ID).HasMaxLength(50);
            entity.Property(e => e.FREQUENCY).HasColumnType("int(11)");
        });

        modelBuilder.Entity<LINE_STYLE>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("LINE_STYLE");

            entity.Property(e => e.ID)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.STYLE).HasMaxLength(10);
        });

        modelBuilder.Entity<LINE_TYP>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("LINE_TYP");

            entity.Property(e => e.ID)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.TYP).HasMaxLength(50);
        });

        modelBuilder.Entity<MB_DEBT_LIST>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("MB_DEBT_LIST");

            entity.HasIndex(e => e.NAME, "FK_MB_DLN");

            entity.HasIndex(e => e.USERID, "FK_MB_DLU");

            entity.HasIndex(e => e.DEBTTYPE, "FK_MB_DTYP");

            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.DEBTIMAGE).HasColumnType("blob");
            entity.Property(e => e.DEBTNOTE).HasMaxLength(1024);
            entity.Property(e => e.DEBTOPEN).HasDefaultValueSql("'1'");
            entity.Property(e => e.DEBTSUM)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'");
            entity.Property(e => e.DEBTTYPE).HasMaxLength(50);
            entity.Property(e => e.NAME).HasMaxLength(250);
            entity.Property(e => e.USERID).HasColumnType("int(11)");

            entity.HasOne(d => d.DEBTTYPENavigation).WithMany(p => p.MB_DEBT_LISTs)
                .HasForeignKey(d => d.DEBTTYPE)
                .HasConstraintName("FK_MB_DTYP");

            entity.HasOne(d => d.USER).WithMany(p => p.MB_DEBT_LISTs)
                .HasForeignKey(d => d.USERID)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_MB_DLU");
        });

        modelBuilder.Entity<MB_DEBT_TYPE>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("MB_DEBT_TYPE");

            entity.Property(e => e.ID).HasMaxLength(50);
        });

        modelBuilder.Entity<MB_DEBT_USER>(entity =>
        {
            entity.HasKey(e => new { e.NAME, e.USERID })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("MB_DEBT_USER");

            entity.HasIndex(e => e.USERID, "FK_MB_DU");

            entity.Property(e => e.NAME).HasMaxLength(250);
            entity.Property(e => e.USERID).HasColumnType("int(11)");

            entity.HasOne(d => d.USER).WithMany(p => p.MB_DEBT_USERs)
                .HasForeignKey(d => d.USERID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MB_DU");
        });

        modelBuilder.Entity<MB_ERROR_CODE>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("MB_ERROR_CODES");

            entity.Property(e => e.ID)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.ERROR).HasMaxLength(250);
        });

        modelBuilder.Entity<MB_GLOBAL_VARIABLE>(entity =>
        {
            entity.HasKey(e => new { e.ID, e.USERID })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("MB_GLOBAL_VARIABLES");

            entity.HasIndex(e => e.USERID, "FK_MB_GV");

            entity.Property(e => e.ID).HasMaxLength(250);
            entity.Property(e => e.USERID).HasColumnType("int(11)");
            entity.Property(e => e.URL).HasMaxLength(1024);
            entity.Property(e => e.VALUEASDECIMAL).HasPrecision(10);
            entity.Property(e => e.VALUEASSTRING).HasMaxLength(1024);

            entity.HasOne(d => d.USER).WithMany(p => p.MB_GLOBAL_VARIABLEs)
                .HasForeignKey(d => d.USERID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MB_GV");
        });

        modelBuilder.Entity<MB_ROLE>(entity =>
        {
            entity.HasKey(e => e.ROLE).HasName("PRIMARY");

            entity.ToTable("MB_ROLE");

            entity.Property(e => e.ROLE).HasMaxLength(50);
        });

        modelBuilder.Entity<MB_USER>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("MB_USER");

            entity.HasIndex(e => e.ROLE, "FK_ROLE_MBROLE");

            entity.Property(e => e.ID)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.EMAIL).HasMaxLength(75);
            entity.Property(e => e.PASSWORD).HasMaxLength(1024);
            entity.Property(e => e.ROLE).HasMaxLength(50);
            entity.Property(e => e.USERNAME).HasMaxLength(50);

            entity.HasOne(d => d.ROLENavigation).WithMany(p => p.MB_USERs)
                .HasForeignKey(d => d.ROLE)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ROLE_MBROLE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
