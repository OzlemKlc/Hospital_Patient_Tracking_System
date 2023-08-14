using hospital_management_system.Data;
using hospital_management_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace hospital_management_system.Controllers
{
    public class VisitsController:Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Ziyaret ekleme için GET action methodu
        public IActionResult Create()
        {
            return View();
        }

        // Ziyaret ekleme için POST action methodu
        [HttpPost]
        public IActionResult Create(Visit visit)
        {
            if (ModelState.IsValid)
            {

                var patient = _context.Patients.Find(visit.PatientId);

                if (patient == null)
                {
                    ModelState.AddModelError("PatientId", "No patient record.");
                    ViewBag.ErrorMessage = "No patient record.";

                    return View(visit);
                }

                _context.Visit.Add(visit); // Yeni ziyareti ekleyin _context.Database.ExecuteSqlRaw($"call public.addpatient({patient.tc},'{patient.name}' , )")
                // _context.Database.ExecuteSqlRaw($"CALL addVisit({visit.PatientId}, '{visit.VisitingDate.ToString("yyyy-MM-dd HH:mm:ss")}', '{visit.DoctorName}', '{visit.Complaint}', '{visit.TypeOfTreatment}');");
                //_context.Database.ExecuteSqlInterpolated($"CALL addVisit({visit.PatientId}, {visit.VisitingDate}, {visit.DoctorName}, {visit.Complaint}, {visit.TypeOfTreatment})");

                _context.SaveChanges();
                return RedirectToAction("Index", "Visits");
            }

            return View(visit);
        }

        // Ziyaret düzenleme için GET action methodu
        public IActionResult Edit(int id)
        {
            var visit = _context.Visit.Find(id); // Verilen id'ye sahip ziyareti bulun
            if (visit == null)
            {
                return NotFound(); // Ziyaret bulunamadıysa 404 hatası döndürün
            }

            var patient = _context.Patients.FirstOrDefault(p => p.Id == visit.PatientId); // Ziyaretin ait olduğu hastayı bulun
            if (patient == null)
            {
                return NotFound(); // Hastayı bulamadıysa 404 hatası döndürün
            }

            //ViewData["Visit"] = visit;
            //ViewData["Patient"] = patient;

            return View(visit);

            /*
             
            

            var visit = _context.Visits.Find(id); // Verilen id'ye sahip ziyareti bulun
            if (visit == null)
            {
                return NotFound(); // Ziyaret bulunamadıysa 404 hatası döndürün
            }

            var patient = _context.Patients.FirstOrDefault(p => p.Id == visit.PatientId); // Ziyaretin ait olduğu hastayı bulun
            if (patient == null)
            {
                return NotFound(); // Hastayı bulamadıysa 404 hatası döndürün
            }

            var viewModel = new VisitEditViewModel
            {
                Visit = visit,
                Patient = patient
            };

            return View(viewModel);
             */


            /*   
            var visit = _context.Visits.FirstOrDefault(id); // Verilen id'ye sahip ziyareti bulun
            if (visit == null)
            {
                return NotFound(); // Ziyaret bulunamadıysa 404 hatası döndürün
            }

            return View(visit);
            */
        }

        // Ziyaret düzenleme için POST action methodu
        [HttpPost]
        public IActionResult Edit( Visit visit)
        {
            //visit.Id = null;
            if (ModelState.IsValid)
            {
                var patient = _context.Patients.Find(visit.PatientId);

                if (patient == null)
                {
                    ModelState.AddModelError("PatientId", "No patient record.");
                    ViewBag.ErrorMessage = "No patient record.";

                    return View(visit);
                }

                _context.Visit.Update(visit); // Ziyareti güncelleyin  // bu satır yerine stored proceduredları yazmam lazım call 
                // _context.Database.ExecuteSqlRaw($"CALL UpdateVisit({visit.Id}, {visit.PatientId}, '{visit.VisitingDate.ToString("yyyy-MM-dd HH:mm:ss")}', '{visit.DoctorName}', '{visit.Complaint}', '{visit.TypeOfTreatment}')");
               // _context.Database.ExecuteSqlInterpolated($"CALL UpdateVisit({visit.Id}, {visit.PatientId}, {visit.VisitingDate}, {visit.DoctorName}, {visit.Complaint}, {visit.TypeOfTreatment})");


                _context.SaveChanges();
                return RedirectToAction("Index", "Visits");
            }

            return RedirectToAction("Index","Visits");
        }

        // Ziyaret silme için GET action methodu
        public IActionResult Delete(int id)
        {
            var visit = _context.Visit.Find(id); // Verilen id'ye sahip ziyareti bulun
            if (visit == null)
            {
                return NotFound(); // Ziyaret bulunamadıysa 404 hatası döndürün
            }

            return View(visit);
        }

        // Ziyaret silme için POST action methodu
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var visit = _context.Visit.Find(id); // Verilen id'ye sahip ziyareti bulun
            if (visit == null)
            {
                return NotFound(); // Ziyaret bulunamadıysa 404 hatası döndürün
            }

            _context.Visit.Remove(visit); // Ziyareti silin
            //_context.Database.ExecuteSqlRaw($"CALL DeleteVisit({visit.Id});");

            _context.SaveChanges();
            return RedirectToAction("Index", "Visits");
        }

        public IActionResult Index()
        {
            var visits = _context.Visit.ToList(); // Tüm ziyaretleri veritabanından alın
                                                  // TimeSpan türünde bir değişken oluşturun ve ziyaret zamanlarını atayın
            var visit = visits.Select(v => v.VisitingDate).ToList();


            return View(visits);
        }


    }
}
