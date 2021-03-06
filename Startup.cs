using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using maqta.identityserver.Data;
using maqta.identityserver.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace maqta.identityserver {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            string connectionString = Configuration.GetConnectionString ("MaqtaConnection");

            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseSqlServer (connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole> ()
                .AddEntityFrameworkStores<ApplicationDbContext> ()
                .AddDefaultTokenProviders ();

            services.Configure<CookiePolicyOptions> (options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_1);

            // configure identity server with in-memory stores, keys, clients and resources
            var migrationsAssembly = typeof (Startup).GetTypeInfo ().Assembly.GetName ().Name;

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer ()
                .AddDeveloperSigningCredential ()
                .AddAspNetIdentity<ApplicationUser> ()
                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore (options => {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer (connectionString,
                            sql => sql.MigrationsAssembly (migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore (options => {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer (connectionString,
                            sql => sql.MigrationsAssembly (migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    // options.TokenCleanupInterval = 30;
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Error");
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseStaticFiles ();
            app.UseCookiePolicy ();

            app.UseIdentityServer ();

            app.UseMvc ();
        }
    }
}