using System.Web.Mvc;
using System.Web.Routing;
using System.Globalization;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Web;

namespace TicketManager.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

//            var strconn = System.Configuration.ConfigurationManager.ConnectionStrings["TicketDBEntities"].ConnectionString;

            try
            {
                //var x = HttpContext.Current.Server..Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "");

                var path = Server.MapPath("/");

                //var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(strconn);
                //var evolve = new Evolve.Evolve("evolve.json", evolveConnection, msg => _logger.LogInformation(msg))
                var evolve = new Evolve.Evolve(path + "/App_Data/Evolve.json")
                {
                    Locations = new List<string> { path + "/db/migrations" },
                    IsEraseDisabled = true,
                };

                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database migration failed.", ex);
                //_logger.LogCritical("Database migration failed.", ex);
                throw;
            }


        }

        private void DoMigrations()
        {

            using (StreamReader r = new StreamReader("Evolve.json"))
            {
                string json = r.ReadToEnd();
                //RootObject company = JsonConvert.DeserializeObject<RootObject>(json);
            }
        }
    }
}
