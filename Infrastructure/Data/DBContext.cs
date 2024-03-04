using System;
using System.Collections.Generic;
using Domain.Models;
using Domain.Models.MoonClothHouse;
using Microsoft.EntityFrameworkCore;
using Sales = Domain.Models.Sales;

namespace Infrastructure.Data;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Clothing> Clothings { get; set; }

    public virtual DbSet<ClothingType> ClothingTypes { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Fabric> Fabrics { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }


    public virtual DbSet<ProductAnalytic> ProductAnalytics { get; set; }

    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Sales> Sales { get; set; }
    //public virtual DbSet<Sale> Sale { get; set; }

    public virtual DbSet<Shipping> Shippings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WishlistItem> WishlistItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //=> optionsBuilder.UseSqlServer("Server=localhost;Database=MoonClothHouse;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;\r\n", options => options.EnableRetryOnFailure());
    => optionsBuilder.UseSqlServer(@"Server=localhost;Database=MoonClothHouse;Trusted_Connection=False;TrustServerCertificate=True;MultipleActiveResultSets=true;User ID=SA;Password=FormaniteFastian33245;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clothing>(entity =>
        {
            entity.HasKey(e => e.ClothingId).HasName("PK__Clothing__7D52820F4D58FFFC");

            entity.ToTable("Clothing");

            entity.Property(e => e.ClothingId)
                .ValueGeneratedNever()
                .HasColumnName("clothing_id");
            entity.Property(e => e.ClothingName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("clothing_name");
            entity.Property(e => e.FabricId).HasColumnName("fabric_id");
            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Fabric).WithMany(p => p.Clothings)
                .HasForeignKey(d => d.FabricId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Clothing__fabric__6477ECF3");

            entity.HasOne(d => d.Gender).WithMany(p => p.Clothings)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Clothing__gender__628FA481");

            entity.HasOne(d => d.Type).WithMany(p => p.Clothings)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Clothing__type_i__6383C8BA");
        });

        modelBuilder.Entity<ClothingType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Clothing__2C0005984A4CFDCE");

            entity.ToTable("ClothingType");

            entity.Property(e => e.TypeId)
                .ValueGeneratedNever()
                .HasColumnName("type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type_name");
        });


        modelBuilder.Entity<Fabric>(entity =>
        {
            entity.HasKey(e => e.FabricId).HasName("PK__Fabric__5BFCADFC57F88943");

            entity.ToTable("Fabric");

            entity.Property(e => e.FabricId)
                .ValueGeneratedNever()
                .HasColumnName("fabric_id");
            entity.Property(e => e.FabricName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fabric_name");
        });


        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__Gender__9DF18F87C0843C66");

            entity.ToTable("Gender");

            entity.Property(e => e.GenderId)
                .ValueGeneratedNever()
                .HasColumnName("gender_id");
            entity.Property(e => e.GenderName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gender_name");
        });

        modelBuilder.Entity<Sales>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sales__E1EB00B25AEBFD7B");

            entity.Property(e => e.SaleId).HasColumnName("sale_id");
            entity.Property(e => e.ActualPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("actual_price");
            entity.Property(e => e.ClothingId).HasColumnName("clothing_id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.SalePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sale_price");

            entity.HasOne(d => d.Clothing).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ClothingId)
                .HasConstraintName("FK__Sales__clothing___06CD04F7");


            entity.HasOne(d => d.Gender).WithMany(p => p.Sales)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK__Sales__gender_id__07C12930");


        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C98935A1A");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Usercategory)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Addresse__CAA247C8DAC303DE");

            entity.Property(e => e.AddressId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('ADDR'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [AddressSeq]),(5)))")
                .HasColumnName("address_id");
            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.AddressType)
                .HasMaxLength(20)
                .HasColumnName("address_type");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(10)
                .HasColumnName("customer_id");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.RecipientName)
                .HasMaxLength(100)
                .HasColumnName("recipient_name");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("state");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .HasColumnName("zip_code");

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Addresses__custo__22401542")
                .OnDelete(DeleteBehavior.Cascade); // Add cascade delete behavior
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brands__5E5A8E278603EFE1");

            entity.Property(e => e.BrandId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('BRD'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [BrandSeq]),(5)))")
                .HasColumnName("brand_id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(100)
                .HasColumnName("brand_name");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__2EF52A275588A768");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('CART'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [CartSeq]),(5)))")
                .HasColumnName("cart_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(10)
                .HasColumnName("customer_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Cart__customer_i__5C6CB6D7");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__Cart_Ite__5D9A6C6EBF41B7EA");

            entity.ToTable("Cart_Items");

            entity.Property(e => e.CartItemId)
                .HasMaxLength(10)
                .HasColumnName("cart_item_id")
                .ValueGeneratedNever(); // Remove the default value generation for CartItemId

            entity.Property(e => e.CartId)
                .IsRequired() // Mark CartId as required
                .HasMaxLength(10)
                .HasColumnName("cart_id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");

            entity.Property(e => e.PricePerUnit)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price_per_unit");

            entity.Property(e => e.ProductId)
                .IsRequired() // Mark ProductId as required
                .HasMaxLength(10)
                .HasColumnName("product_id");

            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            // Configure navigation properties
            entity.HasOne(d => d.Cart)
                .WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__Cart_Item__cart___603D47BB")
                .OnDelete(DeleteBehavior.Cascade); // Add OnDelete behavior for cascade delete

            entity.HasOne(d => d.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Cart_Item__produ__61316BF4")
                .OnDelete(DeleteBehavior.Cascade); // Add OnDelete behavior for cascade delete
        });




        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories", "MoonClothHouse"); // Assuming the table name is "Categories" and the schema is "MoonClothHouse"

            entity.HasKey(e => e.CategoryId).HasName("PK_Categories");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('CAT'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [CategorySeq]),(5)))")
                .HasColumnName("category_id");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");

            entity.Property(e => e.Gender)
                .HasMaxLength(10) // Adjust the size based on your actual data requirements
                .HasColumnName("gender");
        });


        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.CouponId).HasName("PK__Coupons__58CF63891C0893B8");

            entity.Property(e => e.CouponId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('CPN'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [CouponSeq]),(5)))")
                .HasColumnName("coupon_id");
            entity.Property(e => e.CouponCode)
                .HasMaxLength(20)
                .HasColumnName("coupon_code");
            entity.Property(e => e.DiscountPercentage)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("discount_percentage");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("date")
                .HasColumnName("expiration_date");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB850C227D10");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('CUST'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [CustomerSeq]),(5)))")
                .HasColumnName("customer_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("state");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .HasColumnName("zip_code");
    //        entity.Property(e => e.Gender)
    //.HasMaxLength(10) // Adjust the length as needed
    //.HasColumnName("gender");
            entity.HasMany(d => d.Coupons).WithMany(p => p.Customers)
                .UsingEntity<Dictionary<string, object>>(
                    "CustomersCoupon",
                    r => r.HasOne<Coupon>().WithMany()
                        .HasForeignKey("CouponId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Customers__coupo__6501FCD8"),
                    l => l.HasOne<Customer>().WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Customers__custo__640DD89F"),
                    j =>
                    {
                        j.HasKey("CustomerId", "CouponId").HasName("PK__Customer__48E93DBD5E514ABF");
                        j.ToTable("Customers_Coupons");
                        j.IndexerProperty<string>("CustomerId")
                            .HasMaxLength(10)
                            .HasColumnName("customer_id");
                        j.IndexerProperty<string>("CouponId")
                            .HasMaxLength(10)
                            .HasColumnName("coupon_id");
                    });
        });



        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__46596229F5E3DF5D");

            entity.Property(e => e.OrderId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('ORD'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [OrderSeq]),(5)))")
                .HasColumnName("order_id");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(10)
                .HasColumnName("customer_id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.ShippingAddress)
                .HasMaxLength(255)
                .HasColumnName("shipping_address");
            entity.Property(e => e.ShippingCity)
                .HasMaxLength(100)
                .HasColumnName("shipping_city");
            entity.Property(e => e.ShippingPhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("shipping_phone_number");
            entity.Property(e => e.ShippingState)
                .HasMaxLength(100)
                .HasColumnName("shipping_state");
            entity.Property(e => e.ShippingZipCode)
                .HasMaxLength(10)
                .HasColumnName("shipping_zip_code");
            entity.Property(e => e.StatusId)
                .HasMaxLength(10)
                .HasColumnName("status_id");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__customer__373B3228");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Orders__status_i__382F5661");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__Order_It__3764B6BC713D987A");

            entity.ToTable("Order_Items");

            entity.Property(e => e.OrderItemId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('OITEM'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [OrderItemSeq]),(5)))")
                .HasColumnName("order_item_id");
            entity.Property(e => e.OrderId)
                .HasMaxLength(10)
                .HasColumnName("order_id");
            entity.Property(e => e.PricePerUnit)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price_per_unit");
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Order_Ite__order__3BFFE745");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Order_Ite__produ__3CF40B7E");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Order_St__3683B531BBEB4B31");

            entity.ToTable("Order_Status");

            entity.Property(e => e.StatusId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('OSTAT'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [StatusSeq]),(5)))")
                .HasColumnName("status_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__ED1FC9EA9168DBAF");

            entity.Property(e => e.PaymentId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('PAY'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [PaymentSeq]),(5)))")
                .HasColumnName("payment_id");
            entity.Property(e => e.OrderId)
                .HasMaxLength(10)
                .HasColumnName("order_id");
            entity.Property(e => e.PaymentAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("payment_amount");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Payments__order___40C49C62");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__47027DF533D6415C");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('PRD'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [ProductSeq]),(5)))")
                .HasColumnName("product_id");
            entity.Property(e => e.BrandId)
                .HasMaxLength(10)
                .HasColumnName("brand_id");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(10)
                .HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("product_name");
            entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Brand)
                  .WithMany(p => p.Products)
                  .HasForeignKey(d => d.BrandId)
                  .OnDelete(DeleteBehavior.Cascade) // Set cascade delete
                  .HasConstraintName("FK_Product_Brand");

            // Configure the foreign key relationship with Categories
            entity.HasOne(d => d.Category)
                  .WithMany(p => p.Products)
                  .HasForeignKey(d => d.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade) // Set cascade delete
                  .HasConstraintName("FK_Product_Category");
            entity.HasMany(d => d.Categories).WithMany(p => p.ProductsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductCategoryMap",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Product_C__categ__68D28DBC"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Product_C__produ__67DE6983"),
                    j =>
                    {
                        j.HasKey("ProductId", "CategoryId").HasName("PK__Product___1A56936E0532D6C6");
                        j.ToTable("Product_Category_Map");
                        j.IndexerProperty<string>("ProductId")
                            .HasMaxLength(10)
                            .HasColumnName("product_id");
                        j.IndexerProperty<string>("CategoryId")
                            .HasMaxLength(10)
                            .HasColumnName("category_id");
                    });
        });

        modelBuilder.Entity<ProductAnalytic>(entity =>
        {
            entity.HasKey(e => e.AnalyticsId).HasName("PK__Product___D5DC3DE1FAC10569");

            entity.ToTable("Product_Analytics");

            entity.Property(e => e.AnalyticsId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('ANL'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [AnalyticsSeq]),(5)))")
                .HasColumnName("analytics_id");
            entity.Property(e => e.AddToCartCount).HasColumnName("add_to_cart_count");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .HasColumnName("product_id");
            entity.Property(e => e.SalesCount).HasColumnName("sales_count");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.ViewsCount).HasColumnName("views_count");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAnalytics)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Product_A__produ__4865BE2A");
        });

        modelBuilder.Entity<ProductAttribute>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PK__Product___9090C9BB5DCD9278");

            entity.ToTable("Product_Attributes");

            entity.Property(e => e.AttributeId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('ATTR'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [AttributeSeq]),(5)))")
                .HasColumnName("attribute_id");
            entity.Property(e => e.AttributeName)
                .HasMaxLength(100)
                .HasColumnName("attribute_name");
            entity.Property(e => e.AttributeValue)
                .HasMaxLength(255)
                .HasColumnName("attribute_value");
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAttributes)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Product_A__produ__5006DFF2");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Product___DC9AC95502B9E743");

            entity.ToTable("Product_Images");
            entity.Property(e => e.ImageId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('IMG'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [ImageSeq]),(5)))")
                .HasColumnName("image_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.IsPrimary).HasColumnName("is_primary");
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .HasColumnName("product_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Product)
                  .WithMany(p => p.ProductImages)
                  .HasForeignKey(d => d.ProductId)
                  .OnDelete(DeleteBehavior.Cascade) // Set cascade delete
                  .HasConstraintName("FK_ProductImages_Product");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(e => e.VariantId).HasName("PK__Product___EACC68B72E4792C9");

            entity.ToTable("Product_Variants");

            entity.Property(e => e.VariantId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('VAR'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [VariantSeq]),(5)))")
                .HasColumnName("variant_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .HasColumnName("product_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.VariantImageUrl)
                .HasMaxLength(255)
                .HasColumnName("variant_image_url");
            entity.Property(e => e.VariantName)
                .HasMaxLength(100)
                .HasColumnName("variant_name");
            entity.Property(e => e.VariantPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("variant_price");
            entity.Property(e => e.VariantStockQuantity).HasColumnName("variant_stock_quantity");

            entity.HasOne(d => d.Product)
                  .WithMany(p => p.ProductVariants)
                  .HasForeignKey(d => d.ProductId)
                  .OnDelete(DeleteBehavior.Cascade) // Set cascade delete
                  .HasConstraintName("FK_ProductVariant_Product");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__60883D900A419D3D");

            entity.Property(e => e.ReviewId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('REV'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [ReviewSeq]),(5)))")
                .HasColumnName("review_id");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(10)
                .HasColumnName("customer_id");
            entity.Property(e => e.IsVerified).HasColumnName("is_verified");
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .HasColumnName("product_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReviewDate)
                .HasColumnType("datetime")
                .HasColumnName("review_date");
            entity.Property(e => e.ReviewText)
                .HasColumnType("text")
                .HasColumnName("review_text");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Reviews__custome__54CB950F");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Reviews__product__53D770D6");
        });


        modelBuilder.Entity<Shipping>(entity =>
        {
            entity.HasKey(e => e.ShippingId).HasName("PK__Shipping__059B15A93CD08B69");

            entity.ToTable("Shipping");

            entity.Property(e => e.ShippingId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('SHIP'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [ShippingSeq]),(5)))")
                .HasColumnName("shipping_id");
            entity.Property(e => e.ActualDeliveryDate)
                .HasColumnType("date")
                .HasColumnName("actual_delivery_date");
            entity.Property(e => e.CarrierName)
                .HasMaxLength(100)
                .HasColumnName("carrier_name");
            entity.Property(e => e.EstimatedDeliveryDate)
                .HasColumnType("date")
                .HasColumnName("estimated_delivery_date");
            entity.Property(e => e.OrderId)
                .HasMaxLength(10)
                .HasColumnName("order_id");
            entity.Property(e => e.TrackingNumber)
                .HasMaxLength(100)
                .HasColumnName("tracking_number");

            entity.HasOne(d => d.Order).WithMany(p => p.Shippings)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Shipping__order___44952D46");
        });



        modelBuilder.Entity<WishlistItem>(entity =>
        {
            entity.HasKey(e => e.WishlistItemId).HasName("PK__Wishlist__190EBE2808EE74F5");

            entity.ToTable("Wishlist_Items");

            entity.Property(e => e.WishlistItemId)
                .HasMaxLength(10)
                .HasDefaultValueSql("('WITM'+right('00000'+CONVERT([nvarchar](5),NEXT VALUE FOR [WishlistItemSeq]),(5)))")
                .HasColumnName("wishlist_item_id");
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .HasColumnName("product_id");
            entity.Property(e => e.WishlistId)
                .HasMaxLength(10)
                .HasColumnName("wishlist_id");

            entity.HasOne(d => d.Product).WithMany(p => p.WishlistItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Wishlist___produ__1E6F845E");
        });
        modelBuilder.HasSequence<int>("AddressSeq");
        modelBuilder.HasSequence<int>("AnalyticsSeq");
        modelBuilder.HasSequence<int>("AttributeSeq");
        modelBuilder.HasSequence<int>("BrandSeq");
        modelBuilder.HasSequence<int>("CartItemSeq");
        modelBuilder.HasSequence<int>("CartSeq");
        modelBuilder.HasSequence<int>("CategorySeq");
        modelBuilder.HasSequence<int>("CouponSeq");
        modelBuilder.HasSequence<int>("CustomerSeq");
        modelBuilder.HasSequence<int>("ImageSeq");
        modelBuilder.HasSequence<int>("OrderItemSeq");
        modelBuilder.HasSequence<int>("OrderSeq");
        modelBuilder.HasSequence<int>("PaymentSeq");
        modelBuilder.HasSequence<int>("ProductSeq");
        modelBuilder.HasSequence<int>("QuestionSeq");
        modelBuilder.HasSequence<int>("ReviewSeq");
        modelBuilder.HasSequence<int>("ShippingSeq");
        modelBuilder.HasSequence<int>("StatusSeq");
        modelBuilder.HasSequence<int>("VariantSeq");
        modelBuilder.HasSequence<int>("WishlistItemSeq");
        modelBuilder.HasSequence<int>("WishlistSeq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
