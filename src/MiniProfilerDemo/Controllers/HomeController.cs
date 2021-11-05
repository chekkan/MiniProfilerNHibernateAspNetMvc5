using System.Threading.Tasks;
using System.Web.Mvc;
using MiniProfilerDemo.Infrastructure;
using MiniProfilerDemo.Models;
using NHibernate.Linq;

namespace MiniProfilerDemo.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var employees = await session.Query<Employee>().ToListAsync();
                return View(employees);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}