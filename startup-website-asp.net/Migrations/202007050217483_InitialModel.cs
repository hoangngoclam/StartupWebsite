namespace startup_website_asp.net.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ATTRIBUTE.AttributeRelationship",
                c => new
                    {
                        AttributeRelationshipId = c.Int(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        SubAttributeId1 = c.Int(),
                        SubAttributeId2 = c.Int(),
                        price = c.Long(),
                        imageUrl = c.String(maxLength: 500, unicode: false),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.AttributeRelationshipId)
                .ForeignKey("ATTRIBUTE.SubAttribute", t => t.SubAttributeId1)
                .ForeignKey("ATTRIBUTE.SubAttribute", t => t.SubAttributeId2)
                .ForeignKey("PRODUCT.Product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.SubAttributeId1)
                .Index(t => t.SubAttributeId2);
            
            CreateTable(
                "CUSTOMER_PRODUCT.Cart",
                c => new
                    {
                        CartId = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        AttributeRelationshipId = c.Int(),
                        Quality = c.Int(nullable: false),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("ATTRIBUTE.AttributeRelationship", t => t.AttributeRelationshipId)
                .ForeignKey("CUSTOMER.Customer", t => t.CustomerId)
                .ForeignKey("PRODUCT.Product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId)
                .Index(t => t.AttributeRelationshipId);
            
            CreateTable(
                "CUSTOMER.Customer",
                c => new
                    {
                        CustomerId = c.Guid(nullable: false),
                        ProvinceId = c.String(maxLength: 15),
                        DistrictId = c.String(maxLength: 15),
                        WardId = c.String(maxLength: 15),
                        ProvinceName = c.String(maxLength: 200),
                        Name = c.String(maxLength: 200),
                        UserName = c.String(maxLength: 200),
                        Email = c.String(maxLength: 70, unicode: false),
                        Password = c.String(maxLength: 500, unicode: false),
                        FbId = c.String(maxLength: 500, unicode: false),
                        GgId = c.String(maxLength: 500, unicode: false),
                        DisplayName = c.String(maxLength: 500),
                        MetaTitle = c.String(maxLength: 250, unicode: false),
                        Address = c.String(maxLength: 700),
                        BirthDay = c.DateTime(storeType: "date"),
                        Gender = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(maxLength: 15, unicode: false),
                        AvatarUrl = c.String(maxLength: 500, unicode: false),
                        Description = c.String(storeType: "ntext"),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "ORDER.Order",
                c => new
                    {
                        OrderId = c.Long(nullable: false, identity: true),
                        CustomerId = c.Guid(nullable: false),
                        PhoneNumber = c.String(maxLength: 15, unicode: false),
                        Address = c.String(maxLength: 700),
                        Name = c.String(maxLength: 250),
                        Email = c.String(maxLength: 100),
                        IsTrial = c.Boolean(),
                        TotalPrice = c.Long(),
                        Note = c.String(storeType: "ntext"),
                        ShipPrice = c.Long(),
                        Status = c.String(maxLength: 50),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("CUSTOMER.Customer", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "ORDER.OrderDetail",
                c => new
                    {
                        OrderDetailId = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        OrderId = c.Long(nullable: false),
                        AttributeRelationshipId = c.Int(),
                        Quality = c.Int(),
                        TotalPrice = c.Long(),
                        Status = c.String(maxLength: 50),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("ATTRIBUTE.AttributeRelationship", t => t.AttributeRelationshipId)
                .ForeignKey("PRODUCT.Product", t => t.ProductId)
                .ForeignKey("ORDER.Order", t => t.OrderId)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId)
                .Index(t => t.AttributeRelationshipId);
            
            CreateTable(
                "PRODUCT.Product",
                c => new
                    {
                        ProductId = c.Long(nullable: false, identity: true),
                        ProductCategoryId = c.Int(nullable: false),
                        StartupId = c.Long(nullable: false),
                        AttributeId1 = c.Int(),
                        AttributeId2 = c.Int(),
                        StartupCategoryId = c.Int(),
                        Name = c.String(maxLength: 500),
                        metaTitle = c.String(maxLength: 200, unicode: false),
                        SeoTitle = c.String(maxLength: 500, unicode: false),
                        Price = c.Long(),
                        PromotionPrice = c.Long(),
                        Quality = c.Int(),
                        Description = c.String(storeType: "ntext"),
                        Status = c.String(maxLength: 50),
                        ViewCount = c.Long(),
                        KeyWord = c.String(maxLength: 500),
                        MainImage = c.String(maxLength: 300),
                        isTrial = c.Boolean(),
                        SubImages = c.String(maxLength: 500, unicode: false),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("ATTRIBUTE.Attribute", t => t.AttributeId1)
                .ForeignKey("ATTRIBUTE.Attribute", t => t.AttributeId2)
                .ForeignKey("STARTUP.Startup", t => t.StartupId)
                .ForeignKey("PRODUCT_CATEGORY.StartupCategory", t => t.StartupCategoryId)
                .ForeignKey("PRODUCT_CATEGORY.ProductCategory", t => t.ProductCategoryId)
                .Index(t => t.ProductCategoryId)
                .Index(t => t.StartupId)
                .Index(t => t.AttributeId1)
                .Index(t => t.AttributeId2)
                .Index(t => t.StartupCategoryId);
            
            CreateTable(
                "ATTRIBUTE.Attribute",
                c => new
                    {
                        AttributeId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.AttributeId);
            
            CreateTable(
                "ATTRIBUTE.SubAttribute",
                c => new
                    {
                        SubAttributeId = c.Int(nullable: false, identity: true),
                        AttributeId = c.Int(nullable: false),
                        Name = c.String(maxLength: 200),
                        TextColor = c.String(nullable: false, maxLength: 20, unicode: false),
                        BackgroundColor = c.String(nullable: false, maxLength: 20, unicode: false),
                        Index = c.Int(),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.SubAttributeId)
                .ForeignKey("ATTRIBUTE.Attribute", t => t.AttributeId)
                .Index(t => t.AttributeId);
            
            CreateTable(
                "PROMOTION.Code",
                c => new
                    {
                        CodeId = c.Int(nullable: false, identity: true),
                        StartupId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                        PercentDiscount = c.Byte(),
                        PriceDiscount = c.Long(),
                        Status = c.String(maxLength: 50),
                        EndTime = c.DateTime(),
                        StartTime = c.DateTime(),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.CodeId)
                .ForeignKey("STARTUP.Startup", t => t.StartupId)
                .ForeignKey("PRODUCT.Product", t => t.ProductId)
                .Index(t => t.StartupId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "STARTUP.Startup",
                c => new
                    {
                        StartupId = c.Long(nullable: false, identity: true),
                        ProvinceId = c.String(maxLength: 15),
                        DistrictId = c.String(maxLength: 15),
                        WardId = c.String(maxLength: 15),
                        ProvinceName = c.String(maxLength: 200),
                        StartupTypeId = c.Int(nullable: false),
                        Name = c.String(maxLength: 250),
                        LogoUrl = c.String(maxLength: 500, unicode: false),
                        CoverUrl = c.String(maxLength: 500, unicode: false),
                        Description = c.String(storeType: "ntext"),
                        Slogan = c.String(maxLength: 300),
                        Address = c.String(maxLength: 700),
                        PhoneNumber = c.String(maxLength: 15, unicode: false),
                        FanpageLink = c.String(maxLength: 500, unicode: false),
                        YoutubeLink = c.String(maxLength: 500, unicode: false),
                        InstagramLink = c.String(maxLength: 500, unicode: false),
                        Status = c.String(maxLength: 50),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.StartupId)
                .ForeignKey("STARTUP.StartupType", t => t.StartupTypeId, cascadeDelete: true)
                .Index(t => t.StartupTypeId);
            
            CreateTable(
                "ORDER.Rate",
                c => new
                    {
                        RateId = c.Long(nullable: false, identity: true),
                        OrderDetailId = c.Long(nullable: false),
                        StartupId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                        ImagesUrl = c.String(maxLength: 500, unicode: false),
                        Content = c.String(maxLength: 500),
                        RateNumber = c.Byte(),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.RateId)
                .ForeignKey("STARTUP.Startup", t => t.StartupId)
                .ForeignKey("PRODUCT.Product", t => t.ProductId)
                .ForeignKey("ORDER.OrderDetail", t => t.OrderDetailId)
                .Index(t => t.OrderDetailId)
                .Index(t => t.StartupId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "STARTUP.StartupAccount",
                c => new
                    {
                        StartupAccountId = c.Int(nullable: false, identity: true),
                        StatupId = c.Long(),
                        Name = c.String(maxLength: 500),
                        UserName = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200, unicode: false),
                        Password = c.String(nullable: false, maxLength: 250, unicode: false),
                        DisplayName = c.String(maxLength: 500),
                        Description = c.String(storeType: "ntext"),
                        AvatarUrl = c.String(maxLength: 500, unicode: false),
                        PhoneNumber = c.String(maxLength: 15, unicode: false),
                        BirthDay = c.DateTime(storeType: "date"),
                        Gender = c.String(maxLength: 50),
                        type = c.String(maxLength: 50),
                        Status = c.String(maxLength: 50),
                        Address = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.StartupAccountId)
                .ForeignKey("STARTUP.Startup", t => t.StatupId)
                .Index(t => t.StatupId);
            
            CreateTable(
                "BLOG.Blog",
                c => new
                    {
                        BlogId = c.Long(nullable: false, identity: true),
                        StartupBlogCategoryId = c.Int(),
                        BlogCategoryId = c.Int(),
                        SystemAdminAuthId = c.Int(),
                        StartupAccountId = c.Int(),
                        MainImageUrl = c.String(maxLength: 500),
                        Title = c.String(maxLength: 500),
                        MetaTitle = c.String(maxLength: 400, unicode: false),
                        SeoTitle = c.String(maxLength: 500),
                        Label = c.String(maxLength: 500),
                        Content = c.String(storeType: "ntext"),
                        AllowComment = c.Boolean(),
                        Status = c.String(maxLength: 50),
                        ViewCount = c.Long(),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("BLOG.BlogCategory", t => t.BlogCategoryId)
                .ForeignKey("STARTUP.StartupAccount", t => t.StartupAccountId)
                .ForeignKey("BLOG.StartupBlogCategory", t => t.StartupBlogCategoryId)
                .ForeignKey("SYSTEM.SystemAccount", t => t.SystemAdminAuthId)
                .Index(t => t.StartupBlogCategoryId)
                .Index(t => t.BlogCategoryId)
                .Index(t => t.SystemAdminAuthId)
                .Index(t => t.StartupAccountId);
            
            CreateTable(
                "BLOG.BlogCategory",
                c => new
                    {
                        BlogCategoryId = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Name = c.String(maxLength: 250),
                        MetaTitle = c.String(maxLength: 250, unicode: false),
                        DisplayOrder = c.Double(),
                        SeoTitle = c.String(maxLength: 200),
                        Status = c.String(maxLength: 50),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.BlogCategoryId)
                .ForeignKey("BLOG.BlogCategory", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "BLOG.StartupBlogCategory",
                c => new
                    {
                        StartupBlogCategoryId = c.Int(nullable: false, identity: true),
                        StartupId = c.Long(),
                        Name = c.String(maxLength: 250),
                        MetaTitle = c.String(maxLength: 250, unicode: false),
                        DisplayOrder = c.Double(),
                        SeoTitle = c.String(maxLength: 200),
                        Status = c.String(maxLength: 50),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.StartupBlogCategoryId)
                .ForeignKey("STARTUP.Startup", t => t.StartupId)
                .Index(t => t.StartupId);
            
            CreateTable(
                "SYSTEM.SystemAccount",
                c => new
                    {
                        SystemAccountId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        UserName = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200, unicode: false),
                        Password = c.String(maxLength: 250, unicode: false),
                        DisplayName = c.String(maxLength: 500),
                        Description = c.String(storeType: "ntext"),
                        AvatarUrl = c.String(maxLength: 500, unicode: false),
                        PhoneNumber = c.String(maxLength: 15, unicode: false),
                        BirthDay = c.DateTime(storeType: "date"),
                        Gender = c.String(maxLength: 50),
                        type = c.String(maxLength: 50),
                        Status = c.String(maxLength: 50),
                        Address = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.SystemAccountId);
            
            CreateTable(
                "PRODUCT_CATEGORY.StartupCategory",
                c => new
                    {
                        StartupCategoryId = c.Int(nullable: false, identity: true),
                        StartupId = c.Long(),
                        Name = c.String(maxLength: 250),
                        MetaTitle = c.String(maxLength: 250, unicode: false),
                        DisplayOrder = c.Double(),
                        Status = c.String(maxLength: 50),
                        SeoTitle = c.String(maxLength: 200),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.StartupCategoryId)
                .ForeignKey("STARTUP.Startup", t => t.StartupId)
                .Index(t => t.StartupId);
            
            CreateTable(
                "STARTUP.StartupLiked",
                c => new
                    {
                        StartupLikedId = c.Long(nullable: false, identity: true),
                        StartupId = c.Long(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.StartupLikedId)
                .ForeignKey("STARTUP.Startup", t => t.StartupId)
                .ForeignKey("CUSTOMER.Customer", t => t.CustomerId)
                .Index(t => t.StartupId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "STARTUP.StartupType",
                c => new
                    {
                        StartupTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        MetaTitle = c.String(nullable: false, maxLength: 200),
                        ImageUrl = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.StartupTypeId);
            
            CreateTable(
                "PRODUCT_CATEGORY.ProductCategory",
                c => new
                    {
                        ProductCategoryId = c.Int(nullable: false, identity: true),
                        ParentCategoryId = c.Int(),
                        Name = c.String(maxLength: 250),
                        MetaTitle = c.String(maxLength: 250, unicode: false),
                        DisplayOrder = c.Double(),
                        Status = c.String(maxLength: 50),
                        SeoTitle = c.String(maxLength: 200),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ProductCategoryId)
                .ForeignKey("PRODUCT_CATEGORY.ProductCategory", t => t.ParentCategoryId)
                .Index(t => t.ParentCategoryId);
            
            CreateTable(
                "PRODUCT.ProductGift",
                c => new
                    {
                        ProductGiftId = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        GiftId = c.Long(nullable: false),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ProductGiftId)
                .ForeignKey("PRODUCT.Gift", t => t.GiftId)
                .ForeignKey("PRODUCT.Product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.GiftId);
            
            CreateTable(
                "PRODUCT.Gift",
                c => new
                    {
                        GiftId = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 300),
                        ImageUrl = c.String(maxLength: 500, unicode: false),
                        Quality = c.Int(),
                        price = c.Long(),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.GiftId);
            
            CreateTable(
                "CUSTOMER_PRODUCT.ProductLiked",
                c => new
                    {
                        ProductLikedId = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ProductLikedId)
                .ForeignKey("PRODUCT.Product", t => t.ProductId)
                .ForeignKey("CUSTOMER.Customer", t => t.CustomerId)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "LOCATION.District",
                c => new
                    {
                        DistrictId = c.String(nullable: false, maxLength: 6, unicode: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Type = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                        ProvinceId = c.String(nullable: false, maxLength: 6, unicode: false),
                    })
                .PrimaryKey(t => t.DistrictId)
                .ForeignKey("LOCATION.Province", t => t.ProvinceId)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "LOCATION.Province",
                c => new
                    {
                        ProvinceId = c.String(nullable: false, maxLength: 6, unicode: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Type = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ProvinceId);
            
            CreateTable(
                "LOCATION.ward",
                c => new
                    {
                        WardId = c.String(nullable: false, maxLength: 6, unicode: false),
                        DistrictId = c.String(nullable: false, maxLength: 6, unicode: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Type = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.WardId)
                .ForeignKey("LOCATION.District", t => t.DistrictId)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "SYSTEM.FeedBack",
                c => new
                    {
                        FeedBackId = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 100, unicode: false),
                        PhoneNumber = c.String(maxLength: 15, unicode: false),
                        type = c.String(maxLength: 50),
                        Content = c.String(storeType: "ntext"),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.FeedBackId);
            
            CreateTable(
                "SYSTEM.Log",
                c => new
                    {
                        LogId = c.Long(nullable: false, identity: true),
                        IdUser = c.Int(),
                        Name = c.String(maxLength: 500),
                        type = c.String(maxLength: 50),
                        SubConent = c.String(maxLength: 500),
                        DetailContent = c.String(storeType: "ntext"),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.LogId);
            
            CreateTable(
                "SYSTEM.WebSystem",
                c => new
                    {
                        WebSystemId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        Content = c.String(storeType: "ntext"),
                        CreatedAt = c.DateTime(storeType: "date"),
                        UpdatedAt = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.WebSystemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("LOCATION.ward", "DistrictId", "LOCATION.District");
            DropForeignKey("LOCATION.District", "ProvinceId", "LOCATION.Province");
            DropForeignKey("STARTUP.StartupLiked", "CustomerId", "CUSTOMER.Customer");
            DropForeignKey("CUSTOMER_PRODUCT.ProductLiked", "CustomerId", "CUSTOMER.Customer");
            DropForeignKey("ORDER.Order", "CustomerId", "CUSTOMER.Customer");
            DropForeignKey("ORDER.OrderDetail", "OrderId", "ORDER.Order");
            DropForeignKey("ORDER.Rate", "OrderDetailId", "ORDER.OrderDetail");
            DropForeignKey("ORDER.Rate", "ProductId", "PRODUCT.Product");
            DropForeignKey("CUSTOMER_PRODUCT.ProductLiked", "ProductId", "PRODUCT.Product");
            DropForeignKey("PRODUCT.ProductGift", "ProductId", "PRODUCT.Product");
            DropForeignKey("PRODUCT.ProductGift", "GiftId", "PRODUCT.Gift");
            DropForeignKey("PRODUCT.Product", "ProductCategoryId", "PRODUCT_CATEGORY.ProductCategory");
            DropForeignKey("PRODUCT_CATEGORY.ProductCategory", "ParentCategoryId", "PRODUCT_CATEGORY.ProductCategory");
            DropForeignKey("ORDER.OrderDetail", "ProductId", "PRODUCT.Product");
            DropForeignKey("PROMOTION.Code", "ProductId", "PRODUCT.Product");
            DropForeignKey("STARTUP.Startup", "StartupTypeId", "STARTUP.StartupType");
            DropForeignKey("STARTUP.StartupLiked", "StartupId", "STARTUP.Startup");
            DropForeignKey("PRODUCT_CATEGORY.StartupCategory", "StartupId", "STARTUP.Startup");
            DropForeignKey("PRODUCT.Product", "StartupCategoryId", "PRODUCT_CATEGORY.StartupCategory");
            DropForeignKey("STARTUP.StartupAccount", "StatupId", "STARTUP.Startup");
            DropForeignKey("BLOG.Blog", "SystemAdminAuthId", "SYSTEM.SystemAccount");
            DropForeignKey("BLOG.StartupBlogCategory", "StartupId", "STARTUP.Startup");
            DropForeignKey("BLOG.Blog", "StartupBlogCategoryId", "BLOG.StartupBlogCategory");
            DropForeignKey("BLOG.Blog", "StartupAccountId", "STARTUP.StartupAccount");
            DropForeignKey("BLOG.Blog", "BlogCategoryId", "BLOG.BlogCategory");
            DropForeignKey("BLOG.BlogCategory", "ParentId", "BLOG.BlogCategory");
            DropForeignKey("ORDER.Rate", "StartupId", "STARTUP.Startup");
            DropForeignKey("PRODUCT.Product", "StartupId", "STARTUP.Startup");
            DropForeignKey("PROMOTION.Code", "StartupId", "STARTUP.Startup");
            DropForeignKey("CUSTOMER_PRODUCT.Cart", "ProductId", "PRODUCT.Product");
            DropForeignKey("ATTRIBUTE.AttributeRelationship", "ProductId", "PRODUCT.Product");
            DropForeignKey("ATTRIBUTE.SubAttribute", "AttributeId", "ATTRIBUTE.Attribute");
            DropForeignKey("ATTRIBUTE.AttributeRelationship", "SubAttributeId2", "ATTRIBUTE.SubAttribute");
            DropForeignKey("ATTRIBUTE.AttributeRelationship", "SubAttributeId1", "ATTRIBUTE.SubAttribute");
            DropForeignKey("PRODUCT.Product", "AttributeId2", "ATTRIBUTE.Attribute");
            DropForeignKey("PRODUCT.Product", "AttributeId1", "ATTRIBUTE.Attribute");
            DropForeignKey("ORDER.OrderDetail", "AttributeRelationshipId", "ATTRIBUTE.AttributeRelationship");
            DropForeignKey("CUSTOMER_PRODUCT.Cart", "CustomerId", "CUSTOMER.Customer");
            DropForeignKey("CUSTOMER_PRODUCT.Cart", "AttributeRelationshipId", "ATTRIBUTE.AttributeRelationship");
            DropIndex("LOCATION.ward", new[] { "DistrictId" });
            DropIndex("LOCATION.District", new[] { "ProvinceId" });
            DropIndex("CUSTOMER_PRODUCT.ProductLiked", new[] { "CustomerId" });
            DropIndex("CUSTOMER_PRODUCT.ProductLiked", new[] { "ProductId" });
            DropIndex("PRODUCT.ProductGift", new[] { "GiftId" });
            DropIndex("PRODUCT.ProductGift", new[] { "ProductId" });
            DropIndex("PRODUCT_CATEGORY.ProductCategory", new[] { "ParentCategoryId" });
            DropIndex("STARTUP.StartupLiked", new[] { "CustomerId" });
            DropIndex("STARTUP.StartupLiked", new[] { "StartupId" });
            DropIndex("PRODUCT_CATEGORY.StartupCategory", new[] { "StartupId" });
            DropIndex("BLOG.StartupBlogCategory", new[] { "StartupId" });
            DropIndex("BLOG.BlogCategory", new[] { "ParentId" });
            DropIndex("BLOG.Blog", new[] { "StartupAccountId" });
            DropIndex("BLOG.Blog", new[] { "SystemAdminAuthId" });
            DropIndex("BLOG.Blog", new[] { "BlogCategoryId" });
            DropIndex("BLOG.Blog", new[] { "StartupBlogCategoryId" });
            DropIndex("STARTUP.StartupAccount", new[] { "StatupId" });
            DropIndex("ORDER.Rate", new[] { "ProductId" });
            DropIndex("ORDER.Rate", new[] { "StartupId" });
            DropIndex("ORDER.Rate", new[] { "OrderDetailId" });
            DropIndex("STARTUP.Startup", new[] { "StartupTypeId" });
            DropIndex("PROMOTION.Code", new[] { "ProductId" });
            DropIndex("PROMOTION.Code", new[] { "StartupId" });
            DropIndex("ATTRIBUTE.SubAttribute", new[] { "AttributeId" });
            DropIndex("PRODUCT.Product", new[] { "StartupCategoryId" });
            DropIndex("PRODUCT.Product", new[] { "AttributeId2" });
            DropIndex("PRODUCT.Product", new[] { "AttributeId1" });
            DropIndex("PRODUCT.Product", new[] { "StartupId" });
            DropIndex("PRODUCT.Product", new[] { "ProductCategoryId" });
            DropIndex("ORDER.OrderDetail", new[] { "AttributeRelationshipId" });
            DropIndex("ORDER.OrderDetail", new[] { "OrderId" });
            DropIndex("ORDER.OrderDetail", new[] { "ProductId" });
            DropIndex("ORDER.Order", new[] { "CustomerId" });
            DropIndex("CUSTOMER_PRODUCT.Cart", new[] { "AttributeRelationshipId" });
            DropIndex("CUSTOMER_PRODUCT.Cart", new[] { "CustomerId" });
            DropIndex("CUSTOMER_PRODUCT.Cart", new[] { "ProductId" });
            DropIndex("ATTRIBUTE.AttributeRelationship", new[] { "SubAttributeId2" });
            DropIndex("ATTRIBUTE.AttributeRelationship", new[] { "SubAttributeId1" });
            DropIndex("ATTRIBUTE.AttributeRelationship", new[] { "ProductId" });
            DropTable("SYSTEM.WebSystem");
            DropTable("SYSTEM.Log");
            DropTable("SYSTEM.FeedBack");
            DropTable("LOCATION.ward");
            DropTable("LOCATION.Province");
            DropTable("LOCATION.District");
            DropTable("CUSTOMER_PRODUCT.ProductLiked");
            DropTable("PRODUCT.Gift");
            DropTable("PRODUCT.ProductGift");
            DropTable("PRODUCT_CATEGORY.ProductCategory");
            DropTable("STARTUP.StartupType");
            DropTable("STARTUP.StartupLiked");
            DropTable("PRODUCT_CATEGORY.StartupCategory");
            DropTable("SYSTEM.SystemAccount");
            DropTable("BLOG.StartupBlogCategory");
            DropTable("BLOG.BlogCategory");
            DropTable("BLOG.Blog");
            DropTable("STARTUP.StartupAccount");
            DropTable("ORDER.Rate");
            DropTable("STARTUP.Startup");
            DropTable("PROMOTION.Code");
            DropTable("ATTRIBUTE.SubAttribute");
            DropTable("ATTRIBUTE.Attribute");
            DropTable("PRODUCT.Product");
            DropTable("ORDER.OrderDetail");
            DropTable("ORDER.Order");
            DropTable("CUSTOMER.Customer");
            DropTable("CUSTOMER_PRODUCT.Cart");
            DropTable("ATTRIBUTE.AttributeRelationship");
        }
    }
}
