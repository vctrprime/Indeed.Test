using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indeed.Test.DataAccess;
using Indeed.Test.DataAccess.Repositories.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace Indeed.Test.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UnitOfWork Context;

        public BaseController()
        {
            Context = new UnitOfWork();
        }

    }
}