using System.Web.Mvc;
using System.Web.Routing;
using System.Globalization;

namespace TicketManager.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
        }
    }
}
