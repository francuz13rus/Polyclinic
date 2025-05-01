using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Polyclinic.JWT;
using Polyclinic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Polyclinic.Models;

namespace Polyclinic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Настройка контекста базы данных для SQLite
            services.AddDbContext<ClinicApiContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // Регистрация сервиса для работы с JWT
            services.AddScoped<JwtTokenService>();

            // Настройка CORS (разрешаем запросы с любых источников)
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // Настройка JWT-авторизации
            var jwtSettings = Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                        ValidAudience = jwtSettings.GetValue<string>("Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            // Настройка Swagger для документации API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clinic API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Введите JWT токен. Пример: Bearer {ваш токен}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Добавление контроллеров
            services.AddControllers();
            services.AddEndpointsApiExplorer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            // Применение миграций при запуске
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ClinicApiContext>();
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // Логирование ошибки (можно заменить на твой логгер)
                    Console.WriteLine($"Ошибка при применении миграций: {ex.Message}");
                    throw;
                }

                // (Опционально) Инициализация начальных данных
                SeedData(context);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Применение CORS
            app.UseCors("CorsPolicy");

            // Настройка перенаправления заголовков (если нужно для прокси)
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            // Подключение аутентификации и авторизации
            app.UseAuthentication();
            app.UseAuthorization();

            // Настройка маршрутизации для контроллеров
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void SeedData(ClinicApiContext context)
        {
            // Проверка, есть ли пользователи
            if (!context.Users.Any())
            {
                // Добавление тестового пользователя (пароль захеширован для примера)
                context.Users.Add(new User
                {
                    Username = "admin",
                    PasswordHash = "AQAAAAEAACcQAAAAEK...==", // Замени на реальный захешированный пароль
                    Role = "Admin"
                });
                context.SaveChanges();
            }
        }
    }
}