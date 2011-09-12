using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Geckon.Portal.Data;

namespace Geckon.Portal.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageSessions()
        {
            return View();
        }

        public ActionResult SessionsGrid(string sidx, string sord, int page, int rows)
        {
            using( PortalDataContext db = new PortalDataContext( ConfigurationManager.ConnectionStrings["Portal"].ConnectionString ) )
            {
                int? totalCount = 0;
                var sessions = db.Session_Get( null, null, (page - 1), rows, ref totalCount );
 
                var jsonData = new
                {
                    total   = Math.Ceiling((double)totalCount / rows), // we'll implement later 
                    page    = page,
                    records = totalCount, // implement later 
                    rows    = ( from s in sessions
                                select new {
                                                id   = s.SessionID.ToString(),
                                                cell = new string[] { 
                                                                        s.SessionID.ToString(), 
                                                                        s.UserID.ToString(),
                                                                        s.DateCreated.ToString(),
                                                                        s.DateModified.ToString()
                                                                    }
                                           }
                                 ).ToArray()
                };

                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
