using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;
using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        public HomeController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        [Authorize]
        public IActionResult Index()
        {
            var studentIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentId = int.TryParse(studentIdClaim, out var id) ? id : 0;
            var query = _databaseContext.ProjectRecommends.Where(p => p.OgretmenOnayDurumu == true && p.OgrenciId != studentId).ToList();
            return View(query);
        }
        public IActionResult ProjeOnerilerim()
        {
            var studentIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentId = int.TryParse(studentIdClaim, out var id) ? id : 0;
            var query = _databaseContext.ProjectRecommends.Where(p => p.OgrenciId == studentId).ToList();
            return View(query);
        }
    }
}