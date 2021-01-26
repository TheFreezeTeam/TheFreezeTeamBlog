Title: Entity Framework Core with Asp.net Core Web Application
Published: 01/05/2021
Tags:
  - CSharp
  - Entity Framerwork
  - ASP.NET Core
AuthorImage: 55555.jpg
Image: ef-core-featured.png
Description: Step by step to use entity framework in ASP.NET Core
Excerpt: Step by step to use entity framework in ASP.NET Core
Author: Mike Yoshino

---
# Install Entity Framework Package

Let's install Entity Framwork package by typing this `dotnet add package EntityFramework --version 6.4.4 ` or you can search package on Nuget Manager.

We have to create a class call ApplicationDbContext and inherits from `DbContext` this class will act as database context class when we use ours entity framwork.
Inside the class we will have constructor which take `DbContextOptions` with the base contructor.

# Use it inside APS.NET CORE

Class should look like this 
```
public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
    }
```

After that we have to add Dbcontext to our services. Open a Startup.cs and replace with below code.
```
public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(aDbContextOptionsBuilder => aDbContextOptionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddTransient<MovieService>();
            services.AddTransient<MovieGenreService>();
        }
 ```

 Thats it, when we using Code-First approach. we simply have models class which represent table in database. We add models to DbContext class which you have it earlier.
 Something like `public DbSet<MovieEntity> MovieEntity { get; set; }` 



 Now thing is ready to go, open terminal or package manager and using `Add-Migration anyname` to intialize your first Entity Framwork.
 Entity Framwork will generate below script.

 ```
 migrationBuilder.CreateTable(
                name: "MovieEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Player = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sound = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrailerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieEntity", x => x.Id);
                });
```

Have to check if you have correct schema then type command `Updata-Database` to push and create tables in your database.

