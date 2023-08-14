using hospital_management_system.Data;
using hospital_management_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hospital_management_system.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
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
                // TC kimlik numarasının sadece rakamlardan oluştuğunu kontrol et
                if (!IsTCNumberValid(patient.TcNum))
                {
                    ModelState.AddModelError("TcNum", "Error: The Identification Number must consist of numbers only!");
                    ViewBag.ErrorMessage = "Error: The Identification Number must consist of numbers only!";
                    return View(patient);
                }

                bool tcKimlikVarMi = _context.Patients.Any(p => p.TcNum == patient.TcNum);

                if (tcKimlikVarMi)
                {
                    ModelState.AddModelError("TcNum", "There is already a patient record with this TC ID number.");
                    ViewBag.ErrorMessage = "There is already a patient record with this TC ID number.";
                    return View(patient);
                    
                }
                _context.Patients.Add(patient);
               // _context.Database.ExecuteSqlRaw($"call addpatient({patient.TcNum}, '{patient.NameSurname}', '{patient.BirthDate}');");
                _context.SaveChanges();
                return RedirectToAction("Index","Patients");
            }
            return View(patient);
        }

        private bool IsTCNumberValid(string tcNumber)
        {
            return !string.IsNullOrEmpty(tcNumber) && tcNumber.Length == 11 && tcNumber.All(char.IsDigit);
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
                // TC kimlik numarasının sadece rakamlardan oluştuğunu kontrol et
                if (!TCValid(patient.TcNum))
                {
                    ModelState.AddModelError("TcNum", "Error: The Identification Number must consist of numbers only!");
                    ViewBag.ErrorMessage = "Error: The Identification Number must consist of numbers only!";
                    return View(patient);
                }


                bool tcKimlikVarMi = _context.Patients.Any(p => p.TcNum == patient.TcNum && p.Id != patient.Id);

                if (tcKimlikVarMi)
                {
                    ModelState.AddModelError("TcNum", "There is already a patient record with this TC ID number.");
                    ViewBag.ErrorMessage = "There is already a patient record with this TC ID number.";

                    return View(patient);
                }

                _context.Patients.Update(patient);
               // _context.Database.ExecuteSqlRaw($"CALL UpdatePatient({patient.Id}, '{patient.NameSurname}', '{patient.BirthDate.ToString("yyyy-MM-dd HH:mm:ss")}');");

                _context.SaveChanges();
                return RedirectToAction("Index", "Patients");
            }
            return View(patient);
        }

        private bool TCValid(string tcNumber)
        {
            return !string.IsNullOrEmpty(tcNumber) && tcNumber.Length == 11 && tcNumber.All(char.IsDigit);
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
           // _context.Database.ExecuteSqlRaw($"CALL DeletePatient({patient.Id});");

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Tüm hasta kayıtlarını listelemek için HTTP Get işlemi
        public IActionResult Index()
        {
            var patients = _context.Patients.ToList();
            return View(patients);
        }
    }
}
