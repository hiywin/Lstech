using System;
using Lstech.Api.Authorize;
using Lstech.Api.Models;
using Lstech.BaseManager;
using Lstech.Entities.Base;
using Lstech.FrameManager;
using Lstech.IBaseManager;
using Lstech.IFrameManager;
using Lstech.Mobile.HealthManager;
using Lstech.Mobile.IHealthManager;
using Lstech.Models.Base;
using Lstech.PC.HealthManager;
using Lstech.PC.IHealthManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Lstech.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var _secoretKey = Configuration["SecoretKey"];
            var _signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secoretKey));
            //读取JWT配置
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuserOptions));
            services.Configure<JwtIssuserOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuserOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuserOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
            //JwtBearer验证:
            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuserOptions.Issuer)];
                configureOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuserOptions.Issuer)],
                    ValidateAudience = true,
                    ValidAudience = jwtAppSettingOptions[nameof(JwtIssuserOptions.Audience)],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _signingKey,
                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                configureOptions.SaveToken = true;
            });

            //注入接口
            services.AddSingleton<ISqlConnModel, SqlConnModel>();
            /****** Hiywin.IBaseManager ******/
            services.AddSingleton<IInitialManager, InitialManager>();
            /****** Hiywin.IFrameManager *****/
            services.AddSingleton<IModuleManager, ModuleManager>();
            /****** Lstech.PC.IHealthManager *****/
            services.AddSingleton<IHealthTitleManager, HealthTitleManager>();
            services.AddSingleton<IHealthContentManager, HealthContentManager>();
            /****** Hiywin.IHealth_titleManager *****/
            services.AddSingleton<IHealth_titleManager, Health_titleManager>();
            /****** Hiywin.IHealth_contentManager *****/
            services.AddSingleton<IHealth_contentManager, Health_contentManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IInitialManager manager, ISqlConnModel conn)
        {
            conn.MysqlConn = Configuration["ConnectionStrings:MysqlConn"];
            conn.MssqlConn = Configuration["ConnectionStrings:MssqlConn"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //启用跨域
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
            app.UseRouting();
            //请求错误提示配置
            app.UseErrorHandling();
            //使用认证授权
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //初始化
            manager.InitData(conn);
        }
    }
}
