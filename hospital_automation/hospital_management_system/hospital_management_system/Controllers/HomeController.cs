using hospital_management_system.Data;
using hospital_management_system.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace hospital_management_system.Controllers
{
    public class HomeController : Controller
    {

        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        /*

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hasta kaydı oluşturmak için HTTP Get işlemi
        public IActionResult Create()
        {
            return View();
        }

        // Hasta kaydını veritabanına kaydetmek için HTTP Post işlemi
        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // Hasta kaydını düzenlemek için HTTP Get işlemi
        public IActionResult Edit(int id)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // Hasta kaydını güncellemek için HTTP Post işlemi
        [HttpPost]
        public IActionResult Edit(Patient patient)
        {
            
            
            if (ModelState.IsValid)
            {
                _context.Patients.Update(patient);
                _context.SaveChanges();
                return RedirectToAction("Index" , "Home");
            }
            return View(patient);
        }

        // Hasta kaydını silmek için HTTP Get işlemi
        public IActionResult Delete(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // Hasta kaydını silmek için HTTP Post işlemi
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }
            _context.Patients.Remove(patient);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Tüm hasta kayıtlarını listelemek için HTTP Get işlemi
        public IActionResult Index()
        {
            var patients = _context.Patients.ToList();
            return View(patients);
        }
        */
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Kullanıcı adı ve şifreyi kontrol et
            if (username == "admin" && password == "password")
            {
                // Başarılı giriş durumunda ana sayfaya yönlendir
                return RedirectToAction("Index" , "Visits");
            }
            else
            {
                
                // Hatalı giriş durumunda hata mesajı göster
                ViewBag.ErrorMessage = "Wrong Username or Password";
                return View("Index");
           }
        }


        [HttpPost]
        public IActionResult Logout()
        {
            // Oturumu sonlandırma işlemlerini gerçekleştirin
            // Örneğin:
            HttpContext.SignOutAsync(); // Oturumu sonlandırmak için kullanılan bir yöntem, kullanılan kimlik doğrulama sistemine göre değişebilir.

            return RedirectToAction("Index", "Home");
        }

    }
}