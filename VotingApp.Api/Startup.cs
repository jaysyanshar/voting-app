using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using VotingApp.Api.DataContexts;

namespace VotingApp.Api
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
            void AddDataContext<TContext>() where TContext : DbContext
            {
                // TODO: use option SQL Server instead of In-Memory Database
                services.AddDbContext<TContext>( opt => opt.UseInMemoryDatabase( "VotingApp" ) );
            }

            services.AddControllers();
            
            AddDataContext<VotingItemDataContext>();
            AddDataContext<CategoryDataContext>();
            AddDataContext<ClientDataContext>();
            AddDataContext<AdminDataContext>();
            AddDataContext<SessionDataContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
