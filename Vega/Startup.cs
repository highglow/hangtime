using Autofac;
using Hangfire;
using Hangfire.Firebase;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Vega.Startup))]

namespace Vega
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
//            ConfigureAuth(app);
            AuthConfig.ConfigureAuth(app);

            var container = GetContainer();

//            ConfigureHangfireFirebase();
            ConfigureHangfireSqlServer();

            ConfigureHangfire(app, container);

        }

        private IContainer GetContainer()
        {
           var builder = new ContainerBuilder();
            builder.RegisterType<PersonActor>().As<IPersonActor>();
            return builder.Build();
        }

        private void ConfigureHangfireSqlServer()
        {
            var connectionString = "Data Source=.;Initial Catalog=Vega;User Id=sa;Password=Mango123";
            GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
        }

        private void ConfigureHangfireFirebase()
        {
            // todo sort out slow enqueue time
            const string url = "https://vega-star.firebaseio.com/";
            const string authSecret = "SaMlpGB7rPMBekSWarSfcRmlTU4KAJw4Fe9BDjDV";

            GlobalConfiguration.Configuration.UseFirebaseStorage(url, authSecret);
            var firebaseStorage = new FirebaseStorage(url, authSecret);
            GlobalConfiguration.Configuration.UseStorage(firebaseStorage);
        }

        private static void ConfigureHangfire(IAppBuilder app, IContainer container)
        {
            GlobalConfiguration.Configuration.UseAutofacActivator(container);
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
