using Final.Project.API;
using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Default Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion 

#region CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllDomains", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
#endregion

#region Repos Services
builder.Services.AddScoped<UserManager<User>>();

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IUserProductsCartRepo, UserProdutsCartRepo>();
builder.Services.AddScoped<IUserAddressRepo, UserAddressRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrdersDetailsRepo, OrdersDetailsRepo>();
builder.Services.AddScoped<IDashboardUserRepo, DashboardUserRepo>();
builder.Services.AddScoped<IWishListRepo, WishListRepo>();
builder.Services.AddScoped<IContactUsRepo, ContactUsRepo>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



#endregion

#region Managers


builder.Services.AddScoped<IUserProductsCartsManager, UserProductsCartsManager>();
builder.Services.AddScoped<IOrdersManager, OrdersManager>();
builder.Services.AddScoped<IUserAddressesManager, UserAddressesManager>();
builder.Services.AddScoped<IAdminDashboardManager, AdminDashboardManager>();
builder.Services.AddScoped<IWishListManager, WishListManager>();
builder.Services.AddScoped<IContactUsManager, ContactUsManager>();



builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));
builder.Services.AddTransient<IMailingService, MailingService>();
builder.Services.AddScoped<IUserService, UserService>();








builder.Services.AddScoped<IDataDashboardManager, DataDashboardManager>();







builder.Services.AddScoped<IProductsManager, ProductsManager>();
builder.Services.AddScoped<ICategoriesManager, CategoriesManager>();
builder.Services.AddScoped<IOrderDetailsManager, OrderDetailsManager>();







builder.Services.AddScoped<IUsersManager, UsersManager>();
builder.Services.AddScoped<IReviewsManager, ReviewsManager>();
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();






builder.Services.AddScoped<IHelper, Helper>();

#endregion

#region Database

builder.Services.AddDbContext<ECommerceContext>(options => options

    //.UseSqlServer(@"Server=DESKTOP-35F9698\SQLEXPRESS;Database=E-CommerceDB;Trusted_Connection=true;Encrypt=false"));

    //.UseSqlServer(@"Server=.;Database=E-CommerceDB;Trusted_Connection=true;Encrypt=false"));
    .UseSqlServer(@"Server=.;Database=E-CommerceDB;Trusted_Connection=true;Encrypt=false"));


#endregion

#region Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 3;
    options.Password.RequiredLength = 5;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;


}).AddEntityFrameworkStores<ECommerceContext>()
  .AddDefaultTokenProviders();
#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "UserScema"; // for handling Authentication
    options.DefaultChallengeScheme = "UserScema"; // for handling Challenge
}).AddJwtBearer("UserScema", options =>
{

    // Access secretkey to use it to validate Requests
    string? stringKey = builder.Configuration.GetValue<string>("SecretKey");
    byte[] keyASBytes = Encoding.ASCII.GetBytes(stringKey!);
    SymmetricSecurityKey key = new SymmetricSecurityKey(keyASBytes);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = key,
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});


#endregion

#region Authorization

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ForCustomer", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Customer");
    });

    options.AddPolicy("ForAdmin", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Admin");
    });
});


#endregion

var app = builder.Build();

#region Middlewares

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region make static file 
var staticFilesPath = Path.Combine(Environment.CurrentDirectory, "Images");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(staticFilesPath),
    RequestPath = "/Images"
});
#endregion

app.UseHttpsRedirection();

app.UseCors("AllowAllDomains");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();
