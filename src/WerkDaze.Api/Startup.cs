using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WerkDaze.Api.Interface;

namespace WerkDaze.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBusinessDayCounter, BusinessDayCounter>();
            services.AddTransient<IDateHash, DateHash>();
        }
    }
}
