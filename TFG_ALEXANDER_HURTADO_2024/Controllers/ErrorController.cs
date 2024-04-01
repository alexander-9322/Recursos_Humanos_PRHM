using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TFG_ALEXANDER_HURTADO_2024.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ViewResult Error404()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}