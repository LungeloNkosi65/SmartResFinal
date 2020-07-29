using Microsoft.AspNet.Identity;
using  SmartRes_Official2019Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartRes_Official2019.Controllers
{
    public abstract class ApplicationBaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var context = new ApplicationDbContext();
                var username = User.Identity.GetUserName();

                //if (!string.IsNullOrEmpty(username))
                //{
                //    var user = context.SAN_Employee.SingleOrDefault(u => u.Eamil == username);
                //    string fullName = string.Concat(new string[] { user.Name, " ", user.Surname });
                //    ViewData.Add("FullName", fullName);
                //}
            }
            base.OnActionExecuted(filterContext);
        }

        public ApplicationBaseController()
        {

        }
    }
}