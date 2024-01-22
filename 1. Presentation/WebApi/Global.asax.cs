using System.Web.Http;
using System.Web.Mvc;
using WebApi.App_Start;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfiguration.Register);
            Bootstrapper.Run();
            //SwaggerConfig.Register();
        }
    }
}
