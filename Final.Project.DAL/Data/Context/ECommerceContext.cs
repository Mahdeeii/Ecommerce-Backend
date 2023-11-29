using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Final.Project.DAL;

// Edit IdentityDbContext to change  Id Column In IdentityUser Table From String To int
// See User Class to Understand What We Change !!!
//public class ECommerceContext : IdentityDbContext<User, IdentityRole<int>, int>
public class ECommerceContext : IdentityDbContext<User>
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<OrderProductDetails> OrderProductDetails => Set<OrderProductDetails>();
    public DbSet<UserProductsCart> UserProductsCarts => Set<UserProductsCart>();
    public DbSet<UserAddress> UserAddresses => Set<UserAddress>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<WishList> wishLists => Set<WishList>();
    public DbSet<ContactUs> ContactUs => Set<ContactUs>();



    public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region User


        builder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FName)
                  .HasMaxLength(10)
                  .IsRequired();

            entity.Property(e => e.LName)
                  .HasMaxLength(10)
                  .IsRequired();

            entity.Property(e => e.Email)
                  .IsRequired();

            entity.Property(e => e.Role)
                  .IsRequired();

            entity.Property(e => e.City)
                  .IsRequired(false);

            entity.Property(e => e.Street)
                  .HasMaxLength(30)
                  .IsRequired(false);
        });
        #endregion

     



    List<User> Users = new List<User>
        {
            new User{Id="f17a0f7f-57d6-44f6-8705-5f3480028488",FName="Mohamed",LName="Hesham",Email="mohamedmahdeei10@gmail.com",PasswordHash="AQAAAAIAAYagAAAAEIVp+xlXAoQ4LJOBnUKnmOq8SXvdtMvFCSd2GHJdkLfUyzXHBKtE5arAxy77uhZS6Q==",City="Alex",Street="Fouad", Code="0012",Role=0},

        };
        builder.Entity<User>().HasData(Users);
     

        #region Product

        builder.Entity<Product>(entity =>
        {

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(e => e.Price)
                 .IsRequired();

            entity.Property(e => e.Description)
                .IsRequired();


            entity.HasOne(e => e.Category)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.CategoryID);
        });


        #endregion

        #region Category

        builder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });


        #endregion

        #region Order
        builder.Entity<Order>(entity =>
        {

            entity.Property(e => e.OrderStatus)
           .IsRequired();

            entity.Property(e => e.OrderDate)
           .IsRequired();


            entity.HasOne(e => e.User)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.UserAddress)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(e => e.UserAddressId)
                    .OnDelete(DeleteBehavior.SetNull);




        });
        #endregion

        #region OrderDetails
        builder.Entity<OrderProductDetails>(entity =>
        {
            entity.Property(e => e.Quantity)
                .IsRequired();

            entity.HasOne(e => e.Product)
                    .WithMany(e => e.OrdersProductDetails)
                    .HasForeignKey(e => e.ProductId);

            entity.HasOne(e => e.Order)
                   .WithMany(e => e.OrdersProductDetails)
                   .HasForeignKey(e => e.OrderId);

            entity.HasKey(e => new { e.OrderId, e.ProductId });

        });
        #endregion

        #region Cart
        builder.Entity<UserProductsCart>(entity =>
        {
            entity.Property(e => e.Quantity)
                .IsRequired();

            entity.HasOne(e => e.User)
                    .WithMany(e => e.UsersProductsCarts)
                    .HasForeignKey(e => e.UserId);

            entity.HasOne(e => e.Product)
                   .WithMany(e => e.UsersProductsCarts)
                   .HasForeignKey(e => e.ProductId);

            entity.HasKey(e => new { e.UserId, e.ProductId });

        });
        #endregion

        #region WishList
        builder.Entity<WishList>(entity =>
        {

            entity.HasKey(e => new { e.UserId, e.ProductId });

        });
        #endregion
        #region UserAddress

        builder.Entity<UserAddress>(entity => {

            entity.HasKey(e => e.Id);

            entity.Property(e => e.City)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(e => e.Street)
               .HasMaxLength(200)
               .IsRequired();

            entity.HasOne(e => e.User)
                  .WithMany(u => u.UserAddresses)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.NoAction);


        });

        #endregion

        #region review
        builder.Entity<Review>(entity =>
        {
            entity.HasOne(x => x.User)
            .WithMany(x => x.Reviews)
            .HasForeignKey(e => e.UserId);

            entity.HasOne(x => x.Product)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.ProductId);

            entity.HasOne(x => x.Order)
           .WithMany(x => x.Reviews)
           .HasForeignKey(e => e.OrderId);

            entity.HasKey(e => new { e.UserId, e.ProductId,e.OrderId });
        });


        #endregion

        #region Category Seeding
        List<Category> categoryList = new List<Category>
        {
            new Category{Id=1,Name="Logitech"},
            new Category{Id=2,Name="Realme"},
            new Category{Id=3,Name="JBL"},
            new Category{Id=4,Name="Redragon"},
            new Category{Id=5,Name="soundcore"},
            new Category{Id=6,Name="Anwangda"},
          
        };
        builder.Entity<Category>().HasData(categoryList);
        #endregion

        
    }
}