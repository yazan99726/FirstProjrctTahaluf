using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

       
        public virtual DbSet<Firstaboutu> Firstaboutus { get; set; }
        public virtual DbSet<Firstbank> Firstbanks { get; set; }
        public virtual DbSet<Firstcart> Firstcarts { get; set; }
        public virtual DbSet<Firstcartproduct> Firstcartproducts { get; set; }
        public virtual DbSet<Firstcatigory> Firstcatigories { get; set; }
        public virtual DbSet<Firstcontactu> Firstcontactus { get; set; }
        public virtual DbSet<Firstdiscount> Firstdiscounts { get; set; }
        public virtual DbSet<Firstfavorite> Firstfavorites { get; set; }
        public virtual DbSet<Firsthome> Firsthomes { get; set; }
        public virtual DbSet<Firsthomeimage> Firsthomeimages { get; set; }
        public virtual DbSet<Firstlogin> Firstlogins { get; set; }
        public virtual DbSet<Firstorder> Firstorders { get; set; }
        public virtual DbSet<Firstorderproduct> Firstorderproducts { get; set; }
        public virtual DbSet<Firstpayment> Firstpayments { get; set; }
        public virtual DbSet<Firstproduct> Firstproducts { get; set; }
        public virtual DbSet<Firstrole> Firstroles { get; set; }
        public virtual DbSet<FirstReviewProduct> FirstReviewProducts { get; set; }
        public virtual DbSet<Firststore> Firststores { get; set; }
        public virtual DbSet<Firsttestimonial> Firsttestimonials { get; set; }
        public virtual DbSet<Firstuser> Firstusers { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("Connection String ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TAH14_USER150")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Firstaboutu>(entity =>
            {
                entity.ToTable("FIRSTABOUTUS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Discription)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("DISCRIPTION");
            });

            modelBuilder.Entity<Firstbank>(entity =>
            {
                entity.ToTable("FIRSTBANK");

                entity.HasIndex(e => e.AccountNumber, "SYS_C00208420")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ACCOUNT_NUMBER");

                entity.Property(e => e.Balance)
                    .HasColumnType("NUMBER")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_EMAIL");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_NAME");

                entity.Property(e => e.CustomerPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_PHONE");
            });

            modelBuilder.Entity<Firstcart>(entity =>
            {
                entity.HasKey(e => e.CartId)
                    .HasName("SYS_C00207677");

                entity.ToTable("FIRSTCART");

                entity.Property(e => e.CartId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CART_ID");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Firstcarts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CART_USER");
            });

            modelBuilder.Entity<Firstcartproduct>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK");

                entity.ToTable("FIRSTCARTPRODUCT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CartId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CART_ID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.CartQuantity)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CART_QUANTITY");

                entity.HasOne(d => d.Cart)
                    .WithMany()
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CARTPRODUCT_CART");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CARTPRODUCT_PRODUCT");
            });

            modelBuilder.Entity<Firstcatigory>(entity =>
            {
                entity.HasKey(e => e.CatigoryId)
                    .HasName("SYS_C00207605");

                entity.ToTable("FIRSTCATIGORY");

                entity.Property(e => e.CatigoryId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CATIGORY_ID");

                entity.Property(e => e.CatigoryName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CATIGORY_NAME");

                

                entity.Property(e => e.StoreId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("STORE_ID");
                entity.Property(e => e.Discription)
                    .HasMaxLength(50)
                    .HasColumnName("DISCRIPTION");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Firstcatigories)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CATIGORY_STORE");
            });

            modelBuilder.Entity<Firstcontactu>(entity =>
            {
                entity.ToTable("FIRSTCONTACTUS");

                entity.HasIndex(e => e.Email, "SYS_C00207586")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<Firstdiscount>(entity =>
            {
                entity.HasKey(e => e.DiscountId)
                    .HasName("SYS_C00207613");

                entity.ToTable("FIRSTDISCOUNT");

                entity.Property(e => e.DiscountId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DISCOUNT_ID");

                entity.Property(e => e.DiscountEnddate)
                    .HasColumnType("DATE")
                    .HasColumnName("DISCOUNT_ENDDATE");

                entity.Property(e => e.DiscountPercentage)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DISCOUNT_PERCENTAGE");

                entity.Property(e => e.DiscountStartdate)
                    .HasColumnType("DATE")
                    .HasColumnName("DISCOUNT_STARTDATE");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCTID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Firstdiscounts)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("FK_DISCOUNT_PRODUCT");
            });

            modelBuilder.Entity<Firstfavorite>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_FIRSTFAVORITE");
                
                entity.ToTable("FIRSTFAVORITE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FAVORITE_PRODUCT");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FAVORITE_USER");
            });

            modelBuilder.Entity<Firsthome>(entity =>
            {
                entity.ToTable("FIRSTHOME");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Discription)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DISCRIPTION");

                entity.Property(e => e.Logo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LOGO");

                entity.Property(e => e.Projectname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PROJECTNAME");
            });

            modelBuilder.Entity<Firsthomeimage>(entity =>
            {
                entity.ToTable("FIRSTHOMEIMAGE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("IMAGE");
            });

            modelBuilder.Entity<Firstlogin>(entity =>
            {
                entity.HasKey(e => e.LoginId)
                    .HasName("SYS_C00207695");

                entity.ToTable("FIRSTLOGIN");

                entity.HasIndex(e => e.Username, "SYS_C00207696")
                    .IsUnique();

                entity.Property(e => e.LoginId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LOGIN_ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Firstlogins)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_LOGIN_ROLES");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Firstlogins)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FIRSTLOGIN_USER");
            });

            modelBuilder.Entity<Firstorder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("SYS_C00207737");

                entity.ToTable("FIRSTORDER");

                entity.Property(e => e.OrderId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.OrderPaydate)
                    .HasColumnType("DATE")
                    .HasColumnName("ORDER_PAYDATE");

                entity.Property(e => e.PaymentId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PAYMENT_ID");

                entity.Property(e => e.Totalprice)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TOTALPRICE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Firstorders)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ORDER_PAYMENT");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Firstorders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ORDER_USER");
            });

            modelBuilder.Entity<Firstorderproduct>(entity =>
            {
                entity.ToTable("FIRSTORDERPRODUCT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.OrderId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("QUANTITY");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Firstorderproducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ORDERPRODUCT_ORDER");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Firstorderproducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ORDERPRODUCT_PRODUCT");
            });

            modelBuilder.Entity<Firstpayment>(entity =>
            {
                entity.ToTable("FIRSTPAYMENT");

                entity.HasIndex(e => e.Cardnumber, "SYS_C00207546")
                    .IsUnique();

                entity.HasIndex(e => e.Cvc, "SYS_C00207547")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.BankId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("BANK_ID");

                entity.Property(e => e.Cardnumber)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CARDNUMBER");

                entity.Property(e => e.Cvc)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CVC");

                entity.Property(e => e.Expirydate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPIRYDATE");

                entity.Property(e => e.Nameoncard)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NAMEONCARD");

                entity.Property(e => e.Paymenttype)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PAYMENTTYPE");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.Firstpayments)
                    .HasForeignKey(d => d.BankId)
                    .HasConstraintName("FK_PAYMENT_BANK");
            });

            modelBuilder.Entity<Firstproduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("SYS_C00207662");

                entity.ToTable("FIRSTPRODUCT");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.CatigoryId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATIGORY_ID");

                entity.Property(e => e.ProductDicription)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT_DICRIPTION");

                entity.Property(e => e.ProductImage1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT_IMAGE1");

                entity.Property(e => e.ProductImage2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT_IMAGE2");

                entity.Property(e => e.ProductImage3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT_IMAGE3");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT_NAME");

                entity.Property(e => e.ProductPrice)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCT_PRICE");

                entity.Property(e => e.ProductQuantity)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PRODUCT_QUANTITY");

                entity.Property(e => e.ProductWholesaleprice)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCT_WHOLESALEPRICE");

                entity.HasOne(d => d.Catigory)
                    .WithMany(p => p.Firstproducts)
                    .HasForeignKey(d => d.CatigoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FPRODUCT_CATIGORY");
            });

            modelBuilder.Entity<Firstrole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("SYS_C00207529");

                entity.ToTable("FIRSTROLES");

                entity.HasIndex(e => e.RoleName, "ROLE_NAME_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_NAME");
            });

            modelBuilder.Entity<Firststore>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("SYS_C00207595");

                entity.ToTable("FIRSTSTORES");

                entity.Property(e => e.StoreId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("STORE_ID");

                entity.Property(e => e.StoreDiscription)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STORE_DISCRIPTION");

                entity.Property(e => e.StoreFloorNumber)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("STORE_FLOOR_NUMBER");

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("STORE_NAME");
            });

            modelBuilder.Entity<FirstReviewProduct>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("SYS_C00235909");
                entity.ToTable("FIRSTREVIEWPRODUCT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Review)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("REVIEW");

                entity.Property(e => e.Rating)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("RATING");

                entity.Property(e => e.User_Id)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.ProductId)
                   .HasColumnType("NUMBER")
                   .HasColumnName("PRODUCTID");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.User_Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_REVIEW_USER");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_REVIEW_PRODUCT");
            });

            modelBuilder.Entity<Firsttestimonial>(entity =>
            {
                entity.ToTable("FIRSTTESTIMONIAL");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Discription)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("DISCRIPTION");

                entity.Property(e => e.Rating)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("RATING");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Firsttestimonials)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TESTIMONIAL_USER");
            });

            modelBuilder.Entity<Firstuser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("SYS_C00207537");

                entity.ToTable("FIRSTUSER");

                entity.HasIndex(e => e.Username, "SYS_C00207538")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USER_ID");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.Street)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("STREET");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");
            });

            //modelBuilder.Entity<Product>(entity =>
            //{
            //    entity.ToTable("PRODUCT");

            //    entity.Property(e => e.Id)
            //        .HasColumnType("NUMBER")
            //        .ValueGeneratedOnAdd()
            //        .HasColumnName("ID");

            //    entity.Property(e => e.CatigoryId)
            //        .HasColumnType("NUMBER")
            //        .HasColumnName("CATIGORY_ID");

            //    entity.Property(e => e.Name)
            //        .IsRequired()
            //        .HasMaxLength(50)
            //        .IsUnicode(false)
            //        .HasColumnName("NAME");

            //    entity.Property(e => e.Price)
            //        .HasColumnType("FLOAT")
            //        .HasColumnName("PRICE");

            //    entity.Property(e => e.Sale)
            //        .HasColumnType("NUMBER")
            //        .HasColumnName("SALE");

            //    entity.HasOne(d => d.Catigory)
            //        .WithMany(p => p.Products)
            //        .HasForeignKey(d => d.CatigoryId)
            //        .OnDelete(DeleteBehavior.Cascade)
            //        .HasConstraintName("FK_PRODUCT_CATIGORY");
            //});

            //modelBuilder.Entity<ProductCustomer>(entity =>
            //{
            //    entity.ToTable("PRODUCT_CUSTOMER");

            //    entity.Property(e => e.Id)
            //        .HasColumnType("NUMBER")
            //        .ValueGeneratedOnAdd()
            //        .HasColumnName("ID");

            //    entity.Property(e => e.CustomerId)
            //        .HasColumnType("NUMBER")
            //        .HasColumnName("CUSTOMER_ID");

            //    entity.Property(e => e.DateFrom)
            //        .HasColumnType("DATE")
            //        .HasColumnName("DATE_FROM");

            //    entity.Property(e => e.DateTo)
            //        .HasColumnType("DATE")
            //        .HasColumnName("DATE_TO");

            //    entity.Property(e => e.ProductId)
            //        .HasColumnType("NUMBER")
            //        .HasColumnName("PRODUCT_ID");

            //    entity.Property(e => e.Quantity)
            //        .HasColumnType("NUMBER")
            //        .HasColumnName("QUANTITY");

            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.ProductCustomers)
            //        .HasForeignKey(d => d.CustomerId)
            //        .OnDelete(DeleteBehavior.Cascade)
            //        .HasConstraintName("FK_CUSTOMER_ID_PRODUCT_CUSTOME");

            //    entity.HasOne(d => d.Product)
            //        .WithMany(p => p.ProductCustomers)
            //        .HasForeignKey(d => d.ProductId)
            //        .OnDelete(DeleteBehavior.Cascade)
            //        .HasConstraintName("FK_PRODUCT_ID");
            //});

            //modelBuilder.Entity<Role>(entity =>
            //{
            //    entity.ToTable("ROLES");

            //    entity.Property(e => e.Id)
            //        .HasColumnType("NUMBER(38)")
            //        .HasColumnName("ID");

            //    entity.Property(e => e.Rolename)
            //        .HasMaxLength(20)
            //        .IsUnicode(false)
            //        .HasColumnName("ROLENAME");
            //});

            //modelBuilder.Entity<UserLogin>(entity =>
            //{
            //    entity.ToTable("USER_LOGIN");

            //    entity.Property(e => e.Id)
            //        .HasColumnType("NUMBER")
            //        .ValueGeneratedOnAdd()
            //        .HasColumnName("ID");

            //    entity.Property(e => e.CustomerId)
            //        .HasColumnType("NUMBER")
            //        .HasColumnName("CUSTOMER_ID");

            //    entity.Property(e => e.Password)
            //        .HasMaxLength(50)
            //        .IsUnicode(false)
            //        .HasColumnName("PASSWORD");

            //    entity.Property(e => e.RoleId)
            //        .HasColumnType("NUMBER(38)")
            //        .HasColumnName("ROLE_ID");

            //    entity.Property(e => e.UserName)
            //        .HasMaxLength(200)
            //        .IsUnicode(false)
            //        .HasColumnName("USER_NAME");

            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.UserLogins)
            //        .HasForeignKey(d => d.CustomerId)
            //        .OnDelete(DeleteBehavior.Cascade)
            //        .HasConstraintName("FK_CUSTOMER_ID");

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.UserLogins)
            //        .HasForeignKey(d => d.RoleId)
            //        .OnDelete(DeleteBehavior.Cascade)
            //        .HasConstraintName("FK_ROLE_ID");
            //});

            modelBuilder.HasSequence("DEPARTMENT_ID_SEQ")
                .IncrementsBy(10)
                .IsCyclic();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
