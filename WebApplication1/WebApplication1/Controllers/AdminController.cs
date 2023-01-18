using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly DatabaseContext _databaseContext;
        public AdminController(INotyfService notyf, DatabaseContext databaseContext)
        {
            _notyf = notyf;
            _databaseContext = databaseContext;
        }
        public IActionResult Index()
        {
            var query = _databaseContext.ProjectRecommends.ToList();
            return View(query);
        }
        [HttpPost]
        public IActionResult Index(string submitButton,int id, string yorum)
        {
            switch (submitButton)
            {
                case "Onayla":
                    return (Onayla(id));
                case "Reddet":
                    return (Reddet(id));
                case "Kaydet":
                    return (YorumEkle(id,yorum));
                default:
                    return (View());
            }
        }
        [HttpPost]
        public ActionResult Onayla(int id)
        {
            var query = _databaseContext.ProjectRecommends.FirstOrDefault(u => u.Id == id);
            query.OgretmenOnayDurumu = true;
            _databaseContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Reddet(int id)
        {
            var query = _databaseContext.ProjectRecommends.FirstOrDefault(u => u.Id == id);
            query.OgretmenOnayDurumu = false;
            _databaseContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult YorumEkle(int id, string yorum)
        {
			var query = _databaseContext.ProjectRecommends.FirstOrDefault(u => u.Id == id);
            query.TeacherComment = yorum;
            _databaseContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
