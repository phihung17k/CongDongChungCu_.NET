using CDCC.Bussiness.Catalog.AlbumSvc;
using CDCC.Bussiness.Catalog.Apartments;
using CDCC.Bussiness.Catalog.Buildings;
using CDCC.Bussiness.Catalog.Comments;
using CDCC.Bussiness.Catalog.Posts;
using CDCC.Bussiness.Catalog.Products;
using CDCC.Bussiness.Catalog.ImageSvc;
using CDCC.Bussiness.Catalog.NewsSvc;
using CDCC.Bussiness.Catalog.PoiSvc;
using CDCC.Bussiness.Catalog.Residents;
using CDCC.Bussiness.Catalog.Stores;
using CDCC.Bussiness.Catalog.Users;
using CDCC.Data.Models.DB;
using CDCC.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.IO;
using CDCC.Data.Repository.UserRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using CDCC.Bussiness.Catalog.CategorySvc;
using CDCC.Bussiness.Catalog.PoiTypeSvc;
using CDCC.Data.Repository.ProductRepository;
using CDCC.Data.Repository.StoreRepository;
using CDCC.Data.Repository.PostRepository;
using CDCC.Data.Repository.CommentRepository;
using CDCC.Bussiness.Requirements;
using Microsoft.AspNetCore.Authorization;
using CDCC.Bussiness.JWT;
using System.Text;
using CDCC.Data.Repository.ResidentRepo;
using CDCC.Data.Repository.CategoryRepository;

namespace CDCC.API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "myAllowSpecificOrigins";

        //Once a user account has been created, you can reload the user's information
        //to incorporate any changes the user might have made on another device.
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true);
            #region configure DI service in business
            //Versioning
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddTransient<IApartmentService, ApartmentService>();
            services.AddTransient<IBuildingService, BuildingService>();
            services.AddTransient<IResidentService, ResidentService>();
            services.AddTransient<IUserService, UserService>();

            //                                           SERVICE
            //Product
            services.AddTransient<IProductService, ProductService>();
            //Comment
            services.AddTransient<ICommentService, CommentService>();
            //Post
            services.AddTransient<IPostService, PostService>();
            //Store
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IPoiService, PoiService>();
            services.AddTransient<IAlbumService, AlbumService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IPoiTypeService, PoiTypeService>();
            services.AddTransient<ICategoryService, CategoryService>();

            //                                           REPOSITORY
            //ProductRepository
            services.AddTransient<IProductRepository, ProductRepository>();
            //StoreRepository
            services.AddTransient<IStoreRepository, StoreRepository>();
            //PostRepository
            services.AddTransient<IPostRepository, PostRepository>();
            //CommentRepository
            services.AddTransient<ICommentRepository, CommentRepository>();
            //Category
            services.AddScoped<ICateRepository, CategoryRepository>();

            services.AddDbContext<CongDongChungCuContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IResidentRepository, ResidentRepository>();

            services.AddScoped<IPoiRepository, PoiRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            #endregion
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CDCC.API", Version = "v1" });
            });
            //private key
            var pathToKey = Path.Combine(Directory.GetCurrentDirectory(), "congdongchungcu-520e7-firebase-adminsdk-xnu6q-e6f99bdf5b.json");
            GoogleCredential credential = GoogleCredential.FromFile(pathToKey);
            //[DEFAULT]
            FirebaseApp.Create(new AppOptions
            {
                Credential = credential,
                ProjectId = "congdongchungcu-520e7",
                ServiceAccountId = "firebase-adminsdk-xnu6q@congdongchungcu-520e7.iam.gserviceaccount.com"
            });
            //Register your Authentication Service.
            //verify the token in the Authorization header on every request
            //register the services necessary to handle authentication, and
            //to specify the parameters of our Firebase project. (JwtBearer)
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => { 
                    options.Authority = "https://securetoken.google.com/congdongchungcu-520e7";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Cong dong chung cu")),
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/congdongchungcu-520e7",
                        ValidateAudience = true,
                        ValidAudience = "congdongchungcu-520e7",
                        ValidateLifetime = true
                    };
                });
            //Authorize
            services.AddAuthorization(configure =>
            {
                configure.AddPolicy("SystemAdmin", policyBuilder => {
                    //system = true, group = false
                    policyBuilder.AddRequirements(new SystemAdminRequirement(true, false));
                    });
                configure.AddPolicy("Admin", policyBuilder => {
                    //system = false, group = true
                    policyBuilder.AddRequirements(new SystemAdminRequirement(false, true));
                });
                //configure.AddPolicy("GroupAdmin", policyBuilder => {
                //    policyBuilder.AddRequirements(new GroupAdminRequirement(true));
                //});
            });
            services.AddSingleton<IAuthorizationHandler, SystemAdminRequirementHandler>();
            //enable CORS
            services.AddCors(options => {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders(new string[] { "Security-Data", "security-data", "Authorization", "authorization"})
                    );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CDCC.API v1"));
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            //the CORS middleware must be configured to execute between the calls to UseRouting and UseEndpoints.
            app.UseCors(MyAllowSpecificOrigins);
            //call to register the actual middleware that will handle the authentication (JwtBearer)
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
