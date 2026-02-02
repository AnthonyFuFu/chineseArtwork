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

    public virtual DbSet<Association> Associations { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Authority> Authorities { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<Dictionary> Dictionaries { get; set; }

    public virtual DbSet<DictionaryPic> DictionaryPics { get; set; }

    public virtual DbSet<Dynasty> Dynasties { get; set; }

    public virtual DbSet<FamousArtist> FamousArtists { get; set; }

    public virtual DbSet<FamousArtwork> FamousArtworks { get; set; }

    public virtual DbSet<FamousArtworkPic> FamousArtworkPics { get; set; }

    public virtual DbSet<Function> Functions { get; set; }

    public virtual DbSet<GroupInfo> GroupInfos { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Poetry> Poetries { get; set; }

    public virtual DbSet<Radical> Radicals { get; set; }

    public virtual DbSet<RadicalPic> RadicalPics { get; set; }

    public virtual DbSet<Style> Styles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:ChineseArtwork");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtId).HasName("PK__artist__FCD631071740F22C");

            entity.ToTable("artist");

            entity.HasIndex(e => e.ArtAccount, "UQ_ART_ACCOUNT").IsUnique();

            entity.HasIndex(e => e.ArtAccount, "idx_artist_account");

            entity.HasIndex(e => e.ArtPhone, "idx_artist_phone");

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
            entity.HasKey(e => e.AwId).HasName("PK__artwork__64D812B2020F1EDB");

            entity.ToTable("artwork");

            entity.HasIndex(e => e.ArtId, "idx_artwork_artist_id");

            entity.HasIndex(e => e.CatId, "idx_artwork_category_id");

            entity.HasIndex(e => e.StyleId, "idx_artwork_style_id");

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
                .HasMaxLength(100)
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
            entity.HasKey(e => e.AwPicId).HasName("PK__artwork___02B3E5F84D79B494");

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

        modelBuilder.Entity<Association>(entity =>
        {
            entity.HasKey(e => e.AssocId).HasName("PK__associat__E84AB27FEA37FAD0");

            entity.ToTable("association");

            entity.Property(e => e.AssocId).HasColumnName("ASSOC_ID");
            entity.Property(e => e.ArtId).HasColumnName("ART_ID");
            entity.Property(e => e.AssocJoinDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("ASSOC_JOIN_DATE");
            entity.Property(e => e.AssocPaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("ASSOC_PAYMENT_DATE");
            entity.Property(e => e.AssocRemark)
                .HasMaxLength(500)
                .HasColumnName("ASSOC_REMARK");
            entity.Property(e => e.AssocRole)
                .HasMaxLength(50)
                .HasColumnName("ASSOC_ROLE");
            entity.Property(e => e.AssocStatus)
                .HasDefaultValue(1)
                .HasColumnName("ASSOC_STATUS");
            entity.Property(e => e.AssocValidUntil).HasColumnName("ASSOC_VALID_UNTIL");
            entity.Property(e => e.GroupId).HasColumnName("GROUP_ID");

            entity.HasOne(d => d.Art).WithMany(p => p.Associations)
                .HasForeignKey(d => d.ArtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("association_artist_fk");

            entity.HasOne(d => d.Group).WithMany(p => p.Associations)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("association_group_info_fk");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__author__A83981454C48C889");

            entity.ToTable("author");

            entity.HasIndex(e => e.DynastyId, "idx_author_dynasty_id");

            entity.Property(e => e.AuthorId).HasColumnName("AUTHOR_ID");
            entity.Property(e => e.AuthorCourtesyName)
                .HasMaxLength(50)
                .HasColumnName("AUTHOR_COURTESY_NAME");
            entity.Property(e => e.AuthorDescription)
                .HasMaxLength(1000)
                .HasColumnName("AUTHOR_DESCRIPTION");
            entity.Property(e => e.AuthorGivenName)
                .HasMaxLength(50)
                .HasColumnName("AUTHOR_GIVEN_NAME");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(100)
                .HasColumnName("AUTHOR_NAME");
            entity.Property(e => e.AuthorPseudonymName)
                .HasMaxLength(50)
                .HasColumnName("AUTHOR_PSEUDONYM_NAME");
            entity.Property(e => e.DynastyId).HasColumnName("DYNASTY_ID");

            entity.HasOne(d => d.Dynasty).WithMany(p => p.Authors)
                .HasForeignKey(d => d.DynastyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("author_dynasty_fk");
        });

        modelBuilder.Entity<Authority>(entity =>
        {
            entity.HasKey(e => new { e.ArtId, e.FuncId }).HasName("authority_ART_ID_FUNC_ID_pk");

            entity.ToTable("authority");

            entity.Property(e => e.ArtId).HasColumnName("ART_ID");
            entity.Property(e => e.FuncId).HasColumnName("FUNC_ID");
            entity.Property(e => e.AuthStatus)
                .HasDefaultValue(1)
                .HasColumnName("AUTH_STATUS");

            entity.HasOne(d => d.Art).WithMany(p => p.Authorities)
                .HasForeignKey(d => d.ArtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("authority_artist_fk");

            entity.HasOne(d => d.Func).WithMany(p => p.Authorities)
                .HasForeignKey(d => d.FuncId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("authority_function_fk");
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.BanId).HasName("PK__banner__DA9979A16C2E91D6");

            entity.ToTable("banner");

            entity.Property(e => e.BanId).HasColumnName("BAN_ID");
            entity.Property(e => e.BanImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BAN_IMAGE");
            entity.Property(e => e.BanPicture)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("BAN_PICTURE");
            entity.Property(e => e.NewsId).HasColumnName("NEWS_ID");

            entity.HasOne(d => d.News).WithMany(p => p.Banners)
                .HasForeignKey(d => d.NewsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("banner_news_fk");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK__category__5F8323A8BA940186");

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

        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__chat_roo__2F3DD4826CC8E382");

            entity.ToTable("chat_room");

            entity.Property(e => e.RoomId).HasColumnName("ROOM_ID");
            entity.Property(e => e.RoomLastUpdate)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("ROOM_LAST_UPDATE");
            entity.Property(e => e.RoomStatus)
                .HasDefaultValue(1)
                .HasColumnName("ROOM_STATUS");
            entity.Property(e => e.RoomUpdateStatus)
                .HasDefaultValue(1)
                .HasColumnName("ROOM_UPDATE_STATUS");
            entity.Property(e => e.RoomUrl)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("ROOM_URL");
        });

        modelBuilder.Entity<Dictionary>(entity =>
        {
            entity.HasKey(e => e.DictId).HasName("PK__dictiona__CB0CC840C6E03D27");

            entity.ToTable("dictionary");

            entity.HasIndex(e => e.RadicalId, "idx_dictionary_radical_id");

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
                .HasMaxLength(20)
                .HasColumnName("DICT_WORD");
            entity.Property(e => e.RadicalId).HasColumnName("RADICAL_ID");

            entity.HasOne(d => d.Radical).WithMany(p => p.Dictionaries)
                .HasForeignKey(d => d.RadicalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dictionary_radical_fk");
        });

        modelBuilder.Entity<DictionaryPic>(entity =>
        {
            entity.HasKey(e => e.DictPicId).HasName("PK__dictiona__1E22D66A929F0952");

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
            entity.Property(e => e.StyleId).HasColumnName("STYLE_ID");

            entity.HasOne(d => d.Dict).WithMany(p => p.DictionaryPics)
                .HasForeignKey(d => d.DictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dictionary_pic_dictionary_fk");

            entity.HasOne(d => d.Style).WithMany(p => p.DictionaryPics)
                .HasForeignKey(d => d.StyleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dictionary_pic_style_fk");
        });

        modelBuilder.Entity<Dynasty>(entity =>
        {
            entity.HasKey(e => e.DynastyId).HasName("PK__dynasty__FD518255BC3DBD28");

            entity.ToTable("dynasty");

            entity.Property(e => e.DynastyId).HasColumnName("DYNASTY_ID");
            entity.Property(e => e.DynastyDescription)
                .HasMaxLength(500)
                .HasColumnName("DYNASTY_DESCRIPTION");
            entity.Property(e => e.DynastyName)
                .HasMaxLength(50)
                .HasColumnName("DYNASTY_NAME");
        });

        modelBuilder.Entity<FamousArtist>(entity =>
        {
            entity.HasKey(e => e.FmsArtId).HasName("PK__famous_a__ACBD812DC2D6CB49");

            entity.ToTable("famous_artist");

            entity.Property(e => e.FmsArtId).HasColumnName("FMS_ART_ID");
            entity.Property(e => e.FmsArtCourtesyName)
                .HasMaxLength(10)
                .HasColumnName("FMS_ART_COURTESY_NAME");
            entity.Property(e => e.FmsArtDescription)
                .HasMaxLength(500)
                .HasColumnName("FMS_ART_DESCRIPTION");
            entity.Property(e => e.FmsArtGender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("FMS_ART_GENDER");
            entity.Property(e => e.FmsArtGivenName)
                .HasMaxLength(10)
                .HasColumnName("FMS_ART_GIVEN_NAME");
            entity.Property(e => e.FmsArtImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FMS_ART_IMAGE");
            entity.Property(e => e.FmsArtName)
                .HasMaxLength(20)
                .HasColumnName("FMS_ART_NAME");
            entity.Property(e => e.FmsArtPicture)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("FMS_ART_PICTURE");
            entity.Property(e => e.FmsArtPseudonymName)
                .HasMaxLength(10)
                .HasColumnName("FMS_ART_PSEUDONYM_NAME");
        });

        modelBuilder.Entity<FamousArtwork>(entity =>
        {
            entity.HasKey(e => e.FmsAwId).HasName("PK__famous_a__1F642E328BC1D613");

            entity.ToTable("famous_artwork");

            entity.HasIndex(e => e.FmsArtId, "idx_famous_artwork_famous_artist_id");

            entity.HasIndex(e => e.FmsAwStatus, "idx_famous_artwork_status");

            entity.Property(e => e.FmsAwId).HasColumnName("FMS_AW_ID");
            entity.Property(e => e.CatId).HasColumnName("CAT_ID");
            entity.Property(e => e.FmsArtId).HasColumnName("FMS_ART_ID");
            entity.Property(e => e.FmsAwCreateTime)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("FMS_AW_CREATE_TIME");
            entity.Property(e => e.FmsAwDimension)
                .HasMaxLength(100)
                .HasColumnName("FMS_AW_DIMENSION");
            entity.Property(e => e.FmsAwStatus)
                .HasDefaultValue(1)
                .HasColumnName("FMS_AW_STATUS");
            entity.Property(e => e.FmsAwTitle)
                .HasMaxLength(100)
                .HasColumnName("FMS_AW_TITLE");
            entity.Property(e => e.FmsAwUpdateTime)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("FMS_AW_UPDATE_TIME");
            entity.Property(e => e.StyleId).HasColumnName("STYLE_ID");

            entity.HasOne(d => d.Cat).WithMany(p => p.FamousArtworks)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("famous_artwork_category_fk");

            entity.HasOne(d => d.FmsArt).WithMany(p => p.FamousArtworks)
                .HasForeignKey(d => d.FmsArtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("famous_artwork_famous_artist_fk");

            entity.HasOne(d => d.Style).WithMany(p => p.FamousArtworks)
                .HasForeignKey(d => d.StyleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("famous_artwork_style_fk");
        });

        modelBuilder.Entity<FamousArtworkPic>(entity =>
        {
            entity.HasKey(e => e.FmsAwPicId).HasName("PK__famous_a__3F0738674A37EEAF");

            entity.ToTable("famous_artwork_pic");

            entity.Property(e => e.FmsAwPicId).HasColumnName("FMS_AW_PIC_ID");
            entity.Property(e => e.FmsAwId).HasColumnName("FMS_AW_ID");
            entity.Property(e => e.FmsAwImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FMS_AW_IMAGE");
            entity.Property(e => e.FmsAwPicSort).HasColumnName("FMS_AW_PIC_SORT");
            entity.Property(e => e.FmsAwPicture)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("FMS_AW_PICTURE");

            entity.HasOne(d => d.FmsAw).WithMany(p => p.FamousArtworkPics)
                .HasForeignKey(d => d.FmsAwId)
                .HasConstraintName("famous_artwork_pic_famous_artwork_fk");
        });

        modelBuilder.Entity<Function>(entity =>
        {
            entity.HasKey(e => e.FuncId).HasName("PK__function__68DC74266E705B89");

            entity.ToTable("function");

            entity.Property(e => e.FuncId).HasColumnName("FUNC_ID");
            entity.Property(e => e.FuncIcon)
                .HasMaxLength(50)
                .HasDefaultValueSql("((1))")
                .HasColumnName("FUNC_ICON");
            entity.Property(e => e.FuncLayer)
                .HasMaxLength(5)
                .HasColumnName("FUNC_LAYER");
            entity.Property(e => e.FuncLink)
                .HasMaxLength(100)
                .HasColumnName("FUNC_LINK");
            entity.Property(e => e.FuncName)
                .HasMaxLength(20)
                .HasColumnName("FUNC_NAME");
            entity.Property(e => e.FuncParentId)
                .HasMaxLength(5)
                .HasColumnName("FUNC_PARENT_ID");
            entity.Property(e => e.FuncStatus)
                .HasDefaultValue(1)
                .HasColumnName("FUNC_STATUS");
        });

        modelBuilder.Entity<GroupInfo>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__group_in__3EFEA3DE2AE8F5E7");

            entity.ToTable("group_info");

            entity.Property(e => e.GroupId).HasColumnName("GROUP_ID");
            entity.Property(e => e.GroupAddress)
                .HasMaxLength(200)
                .HasColumnName("GROUP_ADDRESS");
            entity.Property(e => e.GroupDescription)
                .HasMaxLength(500)
                .HasColumnName("GROUP_DESCRIPTION");
            entity.Property(e => e.GroupEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GROUP_EMAIL");
            entity.Property(e => e.GroupEstablishedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("GROUP_ESTABLISHED_DATE");
            entity.Property(e => e.GroupName)
                .HasMaxLength(100)
                .HasColumnName("GROUP_NAME");
            entity.Property(e => e.GroupOwner)
                .HasMaxLength(100)
                .HasColumnName("GROUP_OWNER");
            entity.Property(e => e.GroupPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("GROUP_PHONE");
            entity.Property(e => e.GroupStatus)
                .HasDefaultValue(1)
                .HasColumnName("GROUP_STATUS");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemId).HasName("PK__member__1B42917C1F6F7F5F");

            entity.ToTable("member");

            entity.HasIndex(e => e.MemAccount, "UQ_MEMBER_ACCOUNT").IsUnique();

            entity.HasIndex(e => e.MemAccount, "idx_member_account");

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
            entity.Property(e => e.MemVerificationStatus).HasColumnName("MEM_VERIFICATION_STATUS");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MsgId).HasName("PK__message__825DA51C54464EA7");

            entity.ToTable("message");

            entity.Property(e => e.MsgId).HasColumnName("MSG_ID");
            entity.Property(e => e.ArtId).HasColumnName("ART_ID");
            entity.Property(e => e.MemId).HasColumnName("MEM_ID");
            entity.Property(e => e.MsgContent)
                .HasMaxLength(3000)
                .HasColumnName("MSG_CONTENT");
            entity.Property(e => e.MsgDirection).HasColumnName("MSG_DIRECTION");
            entity.Property(e => e.MsgImage)
                .HasMaxLength(100)
                .HasColumnName("MSG_IMAGE");
            entity.Property(e => e.MsgPicture)
                .HasMaxLength(300)
                .HasColumnName("MSG_PICTURE");
            entity.Property(e => e.MsgTime)
                .HasPrecision(3)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("MSG_TIME");
            entity.Property(e => e.RoomId).HasColumnName("ROOM_ID");

            entity.HasOne(d => d.Mem).WithMany(p => p.Messages)
                .HasForeignKey(d => d.MemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("message_member_fk");

            entity.HasOne(d => d.Room).WithMany(p => p.Messages)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("message_chat_room_fk");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__news__D55D9908AFAD6425");

            entity.ToTable("news");

            entity.Property(e => e.NewsId).HasColumnName("NEWS_ID");
            entity.Property(e => e.NewsContent)
                .HasMaxLength(3000)
                .IsUnicode(false)
                .HasColumnName("NEWS_CONTENT");
            entity.Property(e => e.NewsCreateBy)
                .HasMaxLength(100)
                .HasColumnName("NEWS_CREATE_BY");
            entity.Property(e => e.NewsCreateDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime")
                .HasColumnName("NEWS_CREATE_DATE");
            entity.Property(e => e.NewsEnd)
                .HasColumnType("datetime")
                .HasColumnName("NEWS_END");
            entity.Property(e => e.NewsName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NEWS_NAME");
            entity.Property(e => e.NewsStart)
                .HasColumnType("datetime")
                .HasColumnName("NEWS_START");
            entity.Property(e => e.NewsStatus)
                .HasDefaultValue(1)
                .HasColumnName("NEWS_STATUS");
            entity.Property(e => e.NewsUpdateBy)
                .HasMaxLength(100)
                .HasColumnName("NEWS_UPDATE_BY");
            entity.Property(e => e.NewsUpdateDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime")
                .HasColumnName("NEWS_UPDATE_DATE");
        });

        modelBuilder.Entity<Poetry>(entity =>
        {
            entity.HasKey(e => e.PoetryId).HasName("PK__poetry__0575DE228CA8C8A6");

            entity.ToTable("poetry");

            entity.HasIndex(e => e.PoetryAddedBy, "idx_poetry_added_by");

            entity.HasIndex(e => e.AuthorId, "idx_poetry_author_id");

            entity.Property(e => e.PoetryId).HasColumnName("POETRY_ID");
            entity.Property(e => e.AuthorId).HasColumnName("AUTHOR_ID");
            entity.Property(e => e.PoetryAddedBy)
                .HasMaxLength(100)
                .HasColumnName("POETRY_ADDED_BY");
            entity.Property(e => e.PoetryAnalysis)
                .HasColumnType("ntext")
                .HasColumnName("POETRY_ANALYSIS");
            entity.Property(e => e.PoetryCategory)
                .HasMaxLength(100)
                .HasColumnName("POETRY_CATEGORY");
            entity.Property(e => e.PoetryContent)
                .HasColumnType("ntext")
                .HasColumnName("POETRY_CONTENT");
            entity.Property(e => e.PoetryKeywords)
                .HasMaxLength(500)
                .HasColumnName("POETRY_KEYWORDS");
            entity.Property(e => e.PoetryTitle)
                .HasMaxLength(255)
                .HasColumnName("POETRY_TITLE");
            entity.Property(e => e.PoetryTranslation)
                .HasColumnType("ntext")
                .HasColumnName("POETRY_TRANSLATION");
            entity.Property(e => e.PoetryWordCount).HasColumnName("POETRY_WORD_COUNT");

            entity.HasOne(d => d.Author).WithMany(p => p.Poetries)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("poetry_author_fk");
        });

        modelBuilder.Entity<Radical>(entity =>
        {
            entity.HasKey(e => e.RadicalId).HasName("PK__radical__90145422E079EF75");

            entity.ToTable("radical");

            entity.Property(e => e.RadicalId).HasColumnName("RADICAL_ID");
            entity.Property(e => e.RadicalStrokes).HasColumnName("RADICAL_STROKES");
            entity.Property(e => e.RadicalWord)
                .HasMaxLength(100)
                .HasColumnName("RADICAL_WORD");
        });

        modelBuilder.Entity<RadicalPic>(entity =>
        {
            entity.HasKey(e => e.RadicalPicId).HasName("PK__radical___45A6F3BB800A86A3");

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

        modelBuilder.Entity<Style>(entity =>
        {
            entity.HasKey(e => e.StyleId).HasName("PK__style__056BC21C573742D3");

            entity.ToTable("style");

            entity.Property(e => e.StyleId).HasColumnName("STYLE_ID");
            entity.Property(e => e.CatId).HasColumnName("CAT_ID");
            entity.Property(e => e.StyleDescription)
                .HasMaxLength(500)
                .HasColumnName("STYLE_DESCRIPTION");
            entity.Property(e => e.StyleKeyword)
                .HasMaxLength(200)
                .HasColumnName("STYLE_KEYWORD");
            entity.Property(e => e.StyleName)
                .HasMaxLength(30)
                .HasColumnName("STYLE_NAME");
            entity.Property(e => e.StyleStatus)
                .HasDefaultValue(1)
                .HasColumnName("STYLE_STATUS");

            entity.HasOne(d => d.Cat).WithMany(p => p.Styles)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("style_category_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
