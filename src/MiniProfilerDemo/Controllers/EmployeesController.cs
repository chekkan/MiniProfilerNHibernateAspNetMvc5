using System.Threading.Tasks;
using System.Web.Mvc;
using MiniProfilerDemo.Infrastructure;
using MiniProfilerDemo.Models;

namespace MiniProfilerDemo.Controllers
{
    public class EmployeesController : Controller
    {
        public ActionResult New() => View();

        [HttpPost]
        public async Task<ActionResult> New(Employee newEmployee)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                await session.SaveAsync(newEmployee).ConfigureAwait(false);
                return RedirectToAction("New");
            }
        }
    }
}