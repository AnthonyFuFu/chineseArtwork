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

    public virtual DbSet<ArtworkPic> ArtworkPics { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Dictionary> Dictionaries { get; set; }

    public virtual DbSet<DictionaryPic> DictionaryPics { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Radical> Radicals { get; set; }

    public virtual DbSet<RadicalPic> RadicalPics { get; set; }

    public virtual DbSet<ScriptStyle> ScriptStyles { get; set; }

    public virtual DbSet<Style> Styles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:ChineseArtwork");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtId).HasName("PK__artist__FCD63107B8F1972C");

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
            entity.HasKey(e => e.AwId).HasName("PK__artwork__64D812B2D2A6D2ED");

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
                .HasConstraintName("artwork_artist_fk");

            entity.HasOne(d => d.Cat).WithMany(p => p.Artworks)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("artwork_category_fk");

            entity.HasOne(d => d.Style).WithMany(p => p.Artworks)
                .HasForeignKey(d => d.StyleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("artwork_style_fk");
        });

        modelBuilder.Entity<ArtworkPic>(entity =>
        {
            entity.HasKey(e => e.AwPicId).HasName("PK__artwork___02B3E5F8A303946C");

            entity.ToTable("artwork_pic");

            entity.Property(e => e.AwPicId).HasColumnName("AW_PIC_ID");
            entity.Property(e => e.AwId).HasColumnName("AW_ID");
            entity.Property(e => e.AwImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("AW_IMAGE");
            entity.Property(e => e.AwPicSort).HasColumnName("AW_PIC_SORT");
            entity.Property(e => e.AwPicture)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("AW_PICTURE");

            entity.HasOne(d => d.Aw).WithMany(p => p.ArtworkPics)
                .HasForeignKey(d => d.AwId)
                .HasConstraintName("artwork_pic_artwork_fk");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK__category__5F8323A8D2114C59");

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

        modelBuilder.Entity<Dictionary>(entity =>
        {
            entity.HasKey(e => e.DictId).HasName("PK__dictiona__CB0CC840EE1B93D9");

            entity.ToTable("dictionary");

            entity.HasIndex(e => e.RadicalId, "idx_dictionary_radical_id");

            entity.HasIndex(e => e.ScriptId, "idx_dictionary_script_id");

            entity.Property(e => e.DictId).HasColumnName("DICT_ID");
            entity.Property(e => e.DictCreateTime)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("DICT_CREATE_TIME");
            entity.Property(e => e.DictDescription)
                .HasMaxLength(500)
                .HasColumnName("DICT_DESCRIPTION");
            entity.Property(e => e.DictIsDel).HasColumnName("DICT_IS_DEL");
            entity.Property(e => e.DictStatus)
                .HasDefaultValue(1)
                .HasColumnName("DICT_STATUS");
            entity.Property(e => e.DictStrokes).HasColumnName("DICT_STROKES");
            entity.Property(e => e.DictUpdateTime)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("DICT_UPDATE_TIME");
            entity.Property(e => e.DictWord)
                .HasMaxLength(100)
                .HasColumnName("DICT_WORD");
            entity.Property(e => e.RadicalId).HasColumnName("RADICAL_ID");
            entity.Property(e => e.ScriptId).HasColumnName("SCRIPT_ID");

            entity.HasOne(d => d.Radical).WithMany(p => p.Dictionaries)
                .HasForeignKey(d => d.RadicalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dictionary_radical_fk");

            entity.HasOne(d => d.Script).WithMany(p => p.Dictionaries)
                .HasForeignKey(d => d.ScriptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dictionary_script_style_fk");
        });

        modelBuilder.Entity<DictionaryPic>(entity =>
        {
            entity.HasKey(e => e.DictPicId).HasName("PK__dictiona__1E22D66A1EA9507E");

            entity.ToTable("dictionary_pic");

            entity.HasIndex(e => e.DictId, "idx_dictionary_pic_dict_id");

            entity.Property(e => e.DictPicId).HasColumnName("DICT_PIC_ID");
            entity.Property(e => e.DictId).HasColumnName("DICT_ID");
            entity.Property(e => e.DictImage)
                .HasMaxLength(100)
                .HasColumnName("DICT_IMAGE");
            entity.Property(e => e.DictPicSort).HasColumnName("DICT_PIC_SORT");
            entity.Property(e => e.DictPicture)
                .HasMaxLength(300)
                .HasColumnName("DICT_PICTURE");

            entity.HasOne(d => d.Dict).WithMany(p => p.DictionaryPics)
                .HasForeignKey(d => d.DictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dictionary_pic_dictionary_fk");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemId).HasName("PK__member__1B42917C3429BA66");

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

        modelBuilder.Entity<Radical>(entity =>
        {
            entity.HasKey(e => e.RadicalId).HasName("PK__radical__901454229381F345");

            entity.ToTable("radical");

            entity.Property(e => e.RadicalId).HasColumnName("RADICAL_ID");
            entity.Property(e => e.RadicalStrokes).HasColumnName("RADICAL_STROKES");
            entity.Property(e => e.RadicalWord)
                .HasMaxLength(100)
                .HasColumnName("RADICAL_WORD");
        });

        modelBuilder.Entity<RadicalPic>(entity =>
        {
            entity.HasKey(e => e.RadicalPicId).HasName("PK__radical___45A6F3BBE5024C97");

            entity.ToTable("radical_pic");

            entity.HasIndex(e => e.RadicalId, "idx_radical_pic_radical_id");

            entity.Property(e => e.RadicalPicId).HasColumnName("RADICAL_PIC_ID");
            entity.Property(e => e.RadicalId).HasColumnName("RADICAL_ID");
            entity.Property(e => e.RadicalImage)
                .HasMaxLength(100)
                .HasColumnName("RADICAL_IMAGE");
            entity.Property(e => e.RadicalPicSort).HasColumnName("RADICAL_PIC_SORT");
            entity.Property(e => e.RadicalPicture)
                .HasMaxLength(300)
                .HasColumnName("RADICAL_PICTURE");

            entity.HasOne(d => d.Radical).WithMany(p => p.RadicalPics)
                .HasForeignKey(d => d.RadicalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("radical_pic_radical_fk");
        });

        modelBuilder.Entity<ScriptStyle>(entity =>
        {
            entity.HasKey(e => e.ScriptId).HasName("PK__script_s__13A8E1F30B268430");

            entity.ToTable("script_style");

            entity.Property(e => e.ScriptId).HasColumnName("SCRIPT_ID");
            entity.Property(e => e.ScriptDescription)
                .HasMaxLength(500)
                .HasColumnName("SCRIPT_DESCRIPTION");
            entity.Property(e => e.ScriptWord)
                .HasMaxLength(100)
                .HasColumnName("SCRIPT_WORD");
        });

        modelBuilder.Entity<Style>(entity =>
        {
            entity.HasKey(e => e.StyleId).HasName("PK__style__056BC21C5BEEC261");

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
