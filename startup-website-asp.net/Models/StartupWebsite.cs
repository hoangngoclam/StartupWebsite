using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace startup_website_asp.net.Models
{
    public partial class StartupWebsite : DbContext
    {
        public StartupWebsite()
            : base("name=StartupWebsite")
        {
        }

        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<AttributeRelationship> AttributeRelationships { get; set; }
        public virtual DbSet<SubAttribute> SubAttributes { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<BlogCategory> BlogCategories { get; set; }
        public virtual DbSet<StartupBlogCategory> StartupBlogCategories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<ProductLiked> ProductLikeds { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<ward> wards { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Gift> Gifts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGift> ProductGifts { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<StartupCategory> StartupCategories { get; set; }
        public virtual DbSet<Code> Codes { get; set; }
        public virtual DbSet<Startup> Startups { get; set; }
        public virtual DbSet<StartupAccount> StartupAccounts { get; set; }
        public virtual DbSet<StartupLiked> StartupLikeds { get; set; }
        public virtual DbSet<FeedBack> FeedBacks { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<SystemAccount> SystemAccounts { get; set; }
        public virtual DbSet<WebSystem> WebSystems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attribute>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Attribute)
                .HasForeignKey(e => e.AttributeId1);

            modelBuilder.Entity<Attribute>()
                .HasMany(e => e.Products1)
                .WithOptional(e => e.Attribute1)
                .HasForeignKey(e => e.AttributeId2);

            modelBuilder.Entity<Attribute>()
                .HasMany(e => e.SubAttributes)
                .WithRequired(e => e.Attribute)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AttributeRelationship>()
                .Property(e => e.imageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<SubAttribute>()
                .Property(e => e.TextColor)
                .IsUnicode(false);

            modelBuilder.Entity<SubAttribute>()
                .Property(e => e.BackgroundColor)
                .IsUnicode(false);

            modelBuilder.Entity<SubAttribute>()
                .HasMany(e => e.AttributeRelationships)
                .WithOptional(e => e.SubAttribute)
                .HasForeignKey(e => e.SubAttributeId1);

            modelBuilder.Entity<SubAttribute>()
                .HasMany(e => e.AttributeRelationships1)
                .WithOptional(e => e.SubAttribute1)
                .HasForeignKey(e => e.SubAttributeId2);

            modelBuilder.Entity<Blog>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<BlogCategory>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<BlogCategory>()
                .HasMany(e => e.BlogCategory1)
                .WithOptional(e => e.BlogCategory2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<StartupBlogCategory>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.WardId)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.FbId)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.GgId)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.AvatarUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ProductLikeds)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.StartupLikeds)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<District>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .Property(e => e.ProvinceId)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .HasMany(e => e.wards)
                .WithRequired(e => e.District)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Province>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Province>()
                .HasMany(e => e.Districts)
                .WithRequired(e => e.Province)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ward>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<ward>()
                .Property(e => e.DistrictId)
                .IsUnicode(false);

            modelBuilder.Entity<ward>()
                .HasMany(e => e.Startups)
                .WithRequired(e => e.ward)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .HasMany(e => e.Rates)
                .WithRequired(e => e.OrderDetail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rate>()
                .Property(e => e.ImagesUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Gift>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Gift>()
                .HasMany(e => e.ProductGifts)
                .WithRequired(e => e.Gift)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.metaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.SeoTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.SubImages)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.AttributeRelationships)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductLikeds)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Rates)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Codes)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductGifts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.ProductCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.ProductCategory1)
                .WithOptional(e => e.ProductCategory2)
                .HasForeignKey(e => e.ParentCategoryId);

            modelBuilder.Entity<StartupCategory>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Startup>()
                .Property(e => e.WardId)
                .IsUnicode(false);

            modelBuilder.Entity<Startup>()
                .Property(e => e.LogoUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Startup>()
                .Property(e => e.CoverUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Startup>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Startup>()
                .Property(e => e.FanpageLink)
                .IsUnicode(false);

            modelBuilder.Entity<Startup>()
                .Property(e => e.YoutubeLink)
                .IsUnicode(false);

            modelBuilder.Entity<Startup>()
                .Property(e => e.InstagramLink)
                .IsUnicode(false);

            modelBuilder.Entity<Startup>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Startup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Startup>()
                .HasMany(e => e.Codes)
                .WithRequired(e => e.Startup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Startup>()
                .HasMany(e => e.StartupAccounts)
                .WithOptional(e => e.Startup)
                .HasForeignKey(e => e.StatupId);

            modelBuilder.Entity<Startup>()
                .HasMany(e => e.StartupLikeds)
                .WithRequired(e => e.Startup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StartupAccount>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<StartupAccount>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<StartupAccount>()
                .Property(e => e.AvatarUrl)
                .IsUnicode(false);

            modelBuilder.Entity<StartupAccount>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<FeedBack>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<FeedBack>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<SystemAccount>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<SystemAccount>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<SystemAccount>()
                .Property(e => e.AvatarUrl)
                .IsUnicode(false);

            modelBuilder.Entity<SystemAccount>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<SystemAccount>()
                .HasMany(e => e.Blogs)
                .WithOptional(e => e.SystemAccount)
                .HasForeignKey(e => e.SystemAdminAuthId);
        }
    }
}
