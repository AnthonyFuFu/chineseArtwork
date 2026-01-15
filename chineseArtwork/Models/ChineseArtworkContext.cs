using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace chineseArtwork.Models;

public partial class ChineseArtworkContext : DbContext
{
    public ChineseArtworkContext()
    {
    }

    public ChineseArtworkContext(DbContextOptions<ChineseArtworkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Artwork> Artworks { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Style> Styles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:ChineseArtwork");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtId).HasName("PK__artist__FCD631072ED5B92D");

            entity.ToTable("artist");

            entity.HasIndex(e => e.ArtAccount, "UQ_ART_ACCOUNT").IsUnique();

            entity.Property(e => e.ArtId).HasColumnName("ART_ID");
            entity.Property(e => e.ArtAccount)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ART_ACCOUNT");
            entity.Property(e => e.ArtBirthday).HasColumnName("ART_BIRTHDAY");
            entity.Property(e => e.ArtCourtesyName)
                .HasMaxLength(10)
                .HasColumnName("ART_COURTESY_NAME");
            entity.Property(e => e.ArtEmail)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ART_EMAIL");
            entity.Property(e => e.ArtGender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ART_GENDER");
            entity.Property(e => e.ArtGivenName)
                .HasMaxLength(10)
                .HasColumnName("ART_GIVEN_NAME");
            entity.Property(e => e.ArtImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ART_IMAGE");
            entity.Property(e => e.ArtName)
                .HasMaxLength(20)
                .HasColumnName("ART_NAME");
            entity.Property(e => e.ArtPassword)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ART_PASSWORD");
            entity.Property(e => e.ArtPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ART_PHONE");
            entity.Property(e => e.ArtPicture)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("ART_PICTURE");
            entity.Property(e => e.ArtPseudonymName)
                .HasMaxLength(10)
                .HasColumnName("ART_PSEUDONYM_NAME");
            entity.Property(e => e.ArtStatus)
                .HasDefaultValue(1)
                .HasColumnName("ART_STATUS");
        });

        modelBuilder.Entity<Artwork>(entity =>
        {
            entity.HasKey(e => e.AwId).HasName("PK__artwork__64D812B243B5B400");

            entity.ToTable("artwork");

            entity.Property(e => e.AwId).HasColumnName("AW_ID");
            entity.Property(e => e.ArtId).HasColumnName("ART_ID");
            entity.Property(e => e.AwCreateTime)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("AW_CREATE_TIME");
            entity.Property(e => e.AwCreated)
                .HasPrecision(3)
                .HasColumnName("AW_CREATED");
            entity.Property(e => e.AwDescription)
                .HasMaxLength(500)
                .HasColumnName("AW_DESCRIPTION");
            entity.Property(e => e.AwDimension)
                .HasMaxLength(50)
                .HasColumnName("AW_DIMENSION");
            entity.Property(e => e.AwIsDel).HasColumnName("AW_IS_DEL");
            entity.Property(e => e.AwIsForSale)
                .HasDefaultValue(1)
                .HasColumnName("AW_IS_FOR_SALE");
            entity.Property(e => e.AwPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("AW_PRICE");
            entity.Property(e => e.AwSoldTime)
                .HasPrecision(3)
                .HasColumnName("AW_SOLD_TIME");
            entity.Property(e => e.AwStatus)
                .HasDefaultValue(1)
                .HasColumnName("AW_STATUS");
            entity.Property(e => e.AwTitle)
                .HasMaxLength(100)
                .HasColumnName("AW_TITLE");
            entity.Property(e => e.AwUpdateTime)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("AW_UPDATE_TIME");
            entity.Property(e => e.CatId).HasColumnName("CAT_ID");
            entity.Property(e => e.StyleId).HasColumnName("STYLE_ID");

            entity.HasOne(d => d.Art).WithMany(p => p.Artworks)
                .HasForeignKey(d => d.ArtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ARTWORK_ARTIST");

            entity.HasOne(d => d.Cat).WithMany(p => p.Artworks)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ARTWORK_CATEGORY");

            entity.HasOne(d => d.Style).WithMany(p => p.Artworks)
                .HasForeignKey(d => d.StyleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ARTWORK_STYLE");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK__category__5F8323A887752BE4");

            entity.ToTable("category");

            entity.Property(e => e.CatId).HasColumnName("CAT_ID");
            entity.Property(e => e.CatKeyword)
                .HasMaxLength(200)
                .HasColumnName("CAT_KEYWORD");
            entity.Property(e => e.CatName)
                .HasMaxLength(30)
                .HasColumnName("CAT_NAME");
            entity.Property(e => e.CatStatus)
                .HasDefaultValue(1)
                .HasColumnName("CAT_STATUS");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemId).HasName("PK__member__1B42917C7B91C0BB");

            entity.ToTable("member");

            entity.HasIndex(e => e.MemAccount, "UQ_MEMBER_ACCOUNT").IsUnique();

            entity.Property(e => e.MemId).HasColumnName("MEM_ID");
            entity.Property(e => e.MemAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MEM_ACCOUNT");
            entity.Property(e => e.MemAddress)
                .HasMaxLength(100)
                .HasColumnName("MEM_ADDRESS");
            entity.Property(e => e.MemBirthday).HasColumnName("MEM_BIRTHDAY");
            entity.Property(e => e.MemEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MEM_EMAIL");
            entity.Property(e => e.MemGender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MEM_GENDER");
            entity.Property(e => e.MemGoogleUid)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MEM_GOOGLE_UID");
            entity.Property(e => e.MemImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MEM_IMAGE");
            entity.Property(e => e.MemName)
                .HasMaxLength(45)
                .HasColumnName("MEM_NAME");
            entity.Property(e => e.MemPassword)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MEM_PASSWORD");
            entity.Property(e => e.MemPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MEM_PHONE");
            entity.Property(e => e.MemPicture)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("MEM_PICTURE");
            entity.Property(e => e.MemStatus)
                .HasDefaultValue(1)
                .HasColumnName("MEM_STATUS");
            entity.Property(e => e.MemVerificationCode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MEM_VERIFICATION_CODE");
            entity.Property(e => e.MemVerificationStatus)
                .HasDefaultValue(1)
                .HasColumnName("MEM_VERIFICATION_STATUS");
        });

        modelBuilder.Entity<Style>(entity =>
        {
            entity.HasKey(e => e.StyleId).HasName("PK__style__056BC21C639DE933");

            entity.ToTable("style");

            entity.Property(e => e.StyleId).HasColumnName("STYLE_ID");
            entity.Property(e => e.StyleKeyword)
                .HasMaxLength(200)
                .HasColumnName("STYLE_KEYWORD");
            entity.Property(e => e.StyleName)
                .HasMaxLength(30)
                .HasColumnName("STYLE_NAME");
            entity.Property(e => e.StyleStatus)
                .HasDefaultValue(1)
                .HasColumnName("STYLE_STATUS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
