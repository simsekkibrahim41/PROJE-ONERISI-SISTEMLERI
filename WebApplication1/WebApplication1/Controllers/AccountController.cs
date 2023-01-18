using AspNetCoreHero.ToastNotification.Abstractions;
using F23.StringSimilarity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApplication1.Entities;
using WebApplication1.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration; 
        private readonly INotyfService _notyf;

		public AccountController(DatabaseContext databaseContext, IConfiguration configuration, INotyfService notyf) //Constructer
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
			_notyf = notyf;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model) // Login Yapma Modeli
        {
            Ogrenci ogrenciKontrol = _databaseContext.Ogrenciler.Where(u => u.StudentName.ToLower() == model.StudentName.ToLower()).FirstOrDefault();

            if (ModelState.IsValid)
            {
                if(ogrenciKontrol != null)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, ogrenciKontrol.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, ogrenciKontrol.FullName ?? String.Empty));
                    claims.Add(new Claim(ClaimTypes.Role, ogrenciKontrol.Role));
                    claims.Add(new Claim("StudentName", ogrenciKontrol.StudentName));

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                    _notyf.Success("Giriş başarı ile yapıldı.", 3);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
					_notyf.Error("Kullanıcı adı veya şifre yanlış. Tekrar deneyiniz.", 3);
				}
				// is valid
			}
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool ogrenciKontrol = _databaseContext.Ogrenciler.Any(u => u.StudentName.ToLower() == model.StudentName.ToLower());

                if (ogrenciKontrol)
                {
                    _notyf.Error("Bu kullanıcı ismi alındı.", 3);
                    return View(model);
                }

                Ogrenci ogrenci = new()
                {
                    StudentName = model.StudentName,
                    Password = model.Password,
                };

                _databaseContext.Ogrenciler.Add(ogrenci);
                int affectedRowCount = _databaseContext.SaveChanges();

                if (affectedRowCount == 0)
                {
                    _notyf.Error("Öğrenci Eklenemedi.", 3);
                }
                else
                {
                    _notyf.Success("Kayıt Yapıldı. Login ekranına yönlendiriliyorsunuz...", 3);
                    return RedirectToAction("Login");
                }
                // is valid 
            }

            return View(model);
        }

        [Authorize]
        public IActionResult ProjectRecommend()
        {
            var query = _databaseContext.ProjectRecommends.ToList();
            return View(query);
        }

        [HttpPost]
        public IActionResult ProjectRecommend(ProjectRecommendModel model)
        {
            var studentIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentId = int.TryParse(studentIdClaim, out var id) ? id : 0;
            var studentName = _databaseContext.Ogrenciler.Where(p => p.Id == studentId).Select(p => p.StudentName).FirstOrDefault();
            var similarityCheck = new JaroWinkler(); // SmilarityCheck Kütüphanesi nesnesini cagırdı

            var projeQuery = _databaseContext.ProjectRecommends.ToList();
            bool flag = false;
            double similarityOrtalamaScore = 0;
            double similarityOrtalamaOzetScore = 0;

            foreach (var projeBaslik in projeQuery) // proje benzerlik oranını yüzde olarak hesaplıyoruz
            {
                double similarityOneLineScore = ((similarityCheck.Similarity(model.Baslik, projeBaslik.Baslik) * 100) + (similarityCheck.Similarity(model.AnahtarKelimeler, projeBaslik.AnahtarKelimeler) * 100) + (similarityCheck.Similarity(model.Ozet, projeBaslik.Ozet) * 100))/3;

                if (projeBaslik.OgrenciId == studentId)
                {
                    if (projeBaslik.OgretmenOnayDurumu != false)
                    {
                        _notyf.Error("Daha önceden proje önerisi eklediğiniz için proje öneriniz reddedilene kadar proje önerisi ekleyemezsiniz.", 3);
                        flag = true;
                        break;
                    }
                }
                if (similarityOneLineScore >= 75)
                {
                    _notyf.Error("Proje önerisinin benzeri ile karşılaşıldı.", 3);
                    return View(projeQuery);
                    break;
                }
                double similarityBaslikScore = similarityCheck.Similarity(model.Baslik, projeBaslik.Baslik) * 100; // benzerlik oranı puanını hesaplıyoruz 

                double similarityAnahtarKelimeScore = similarityCheck.Similarity(model.AnahtarKelimeler, projeBaslik.AnahtarKelimeler) * 100; // benzerlik oranı puanını hesaplıyoruz

                double similarityOzetScore = similarityCheck.Similarity(model.Ozet, projeBaslik.Ozet) * 100; // benzerlik oranı puanını hesaplıyoruz


                double ortalama = (similarityBaslikScore/2 + similarityAnahtarKelimeScore*0.7  + similarityOzetScore*1.5) / 3;


                similarityOrtalamaScore = (similarityOrtalamaScore + ortalama);

                similarityOrtalamaOzetScore = (similarityOrtalamaOzetScore + similarityOzetScore);
            }
            if(flag==false)
            { 
                similarityOrtalamaScore = similarityOrtalamaScore / projeQuery.Count;
                similarityOrtalamaOzetScore = similarityOrtalamaOzetScore / projeQuery.Count;
                if (similarityOrtalamaScore >= 60 || similarityOrtalamaOzetScore >= 70)
                {
                    _notyf.Error("Proje önerisinin benzeri ile karşılaşıldı.", 3);
                    return View(projeQuery);
                }
                else
                {
                    ProjectRecommendEntity projectRecommend = new()
                    {
                        Id = model.Id,
                        Baslik = model.Baslik,
                        Ozet = model.Ozet,
                        Yil = model.Yil,
                        AnahtarKelimeler = model.AnahtarKelimeler,
                        OgretmenOnayDurumu = null,
                        OgrenciId = studentId,
                        OgrenciName = studentName,
                        SimilarityScore = Convert.ToInt32(similarityOrtalamaScore),
                        SimilarityOnayDurumu = true,
                        TeacherComment = ""
                    };

                    _databaseContext.ProjectRecommends.Add(projectRecommend);

                    int affectedRowCount = _databaseContext.SaveChanges();
                    if (affectedRowCount == 0)
                    {
                        _notyf.Error("Proje önerisi eklenemedi.", 3);
                        return RedirectToAction("ProjectRecommend");
                    }
                    else
                    {
                        if (ModelState.IsValid)
                        {
                            Ogrenci ogrenci = _databaseContext.Ogrenciler.FirstOrDefault(u => u.Id == studentId);
                            ogrenci.ProjectRecommendId = projectRecommend.Id;
                            _databaseContext.SaveChanges();
                            _notyf.Success("Proje önerisi eklendi.", 3);
                            return RedirectToAction("ProjectRecommend");
                        }
                    }
                }
            }
            return RedirectToAction("ProjectRecommend");
        }

        [Authorize]
        public IActionResult Profile()
        {
            ProfileInfoLoading();

            return View();
        }
        private void ProfileInfoLoading()
		{
            var studentIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentId = int.TryParse(studentIdClaim, out var id) ? id : 0;
            Ogrenci ogrenci = _databaseContext.Ogrenciler.FirstOrDefault(u => u.Id == studentId);

			ViewData["fullname"] = ogrenci.FullName;
		}
		[HttpPost]
        public IActionResult ProfileUpdateFullName([Required][StringLength(30)]string fullname)
        {
			if (ModelState.IsValid) 
            {
                var studentIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var studentId = int.TryParse(studentIdClaim, out var id) ? id : 0;
                Ogrenci ogrenci = _databaseContext.Ogrenciler.FirstOrDefault(u => u.Id == studentId);

                ogrenci.FullName = fullname;
                _databaseContext.SaveChanges();
                ViewData["result"] = "UpdateFullName";             
            }

            ProfileInfoLoading();
            return View("Profile");
        }

        [HttpPost]
        public IActionResult ProfileUpdatePassword([Required][MinLength(8)][MaxLength(16)] string password)
        {
            if (ModelState.IsValid)
            {
                var studentIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var studentId = int.TryParse(studentIdClaim, out var id) ? id : 0;
                Ogrenci ogrenci = _databaseContext.Ogrenciler.FirstOrDefault(u => u.Id == studentId);
                
                string newPassword = password;

                if(newPassword != ogrenci.Password)
				{
                    ogrenci.Password = newPassword;
                    _databaseContext.SaveChanges();
                    ViewData["result"] = "UpdatePassword";
                }
				else
				{
                    ViewData["result"] = "SamePassword";
				}
            }

            ProfileInfoLoading();
            return View("Profile"); // For see the validation error
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _notyf.Success("Çıkış yapıldı...", 3);
            return RedirectToAction(nameof(Login));
        }
    }
}
