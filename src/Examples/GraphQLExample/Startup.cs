using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.Types;
using GraphQL.Server.Ui.Playground;
using GraphQL;
using GraphQL.Http;
using GraphQL.DataLoader;
using GraphQL.Execution;
using GraphQLExample.Data;

namespace GraphQLExample
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            AddGraphQL(services);

            services.AddScoped<IUserRepository, UserRepository>();
        }

        private void AddGraphQL(IServiceCollection services)
        {
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<IDocumentExecutionListener, DataLoaderDocumentListener>();

            var targetType = typeof(IGraphType);
            var graphTypes = typeof(Startup).Assembly.GetTypes().Where(t => targetType.IsAssignableFrom(t));
            foreach (var type in graphTypes)
            {
                services.AddScoped(type);
            }

            services.AddScoped<ISchema, GraphQLExample.Schemas.Schema>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()
            {
                GraphQLEndPoint = "/api/graphql",
                Path = "/graphql"
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
