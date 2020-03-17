using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraWebApiCore.Helpers
{
    public class MiFiltroDeAccion : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
