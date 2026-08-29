using Microsoft.AspNetCore.Mvc;
using DAL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrainLink.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ITrainingService _trainingService;
        private readonly IStudentService _studentService;
        private readonly IDoctorService _doctorService;
        private readonly ICompanyService _companyService;
        private readonly ICompanySupervisorService _companySupervisorService;


        public TrainingController(
            ITrainingService trainingService,
            IStudentService studentService,
            IDoctorService doctorService,
            ICompanyService companyService,
            ICompanySupervisorService companySupervisorService)
        {
            _trainingService = trainingService;
            _studentService = studentService;
            _doctorService = doctorService;
            _companyService = companyService;
            _companySupervisorService = companySupervisorService;
        }


        public ActionResult Index()
        {
            var trinings = _trainingService.GetAll();
            return View(trinings);

        }

        //CREATE
        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Training training)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(training);
            }
            await _trainingService.Add(training);
            return RedirectToAction(nameof(Index));

        }

        //DETAILs
        public IActionResult Details(int id)
        {
            var training = _trainingService.GetById(id).FirstOrDefault();
            if (training == null)
            {
                return NotFound();
            }
            return View(training);

        }


        //EDIT
        public IActionResult Edit(int id)
        {
            var training = _trainingService.GetById(id).FirstOrDefault();

            if (training == null)
            {
                return NotFound();
            }
            LoadDropdowns();
            return View(training);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Training training)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(training);
            }
            await _trainingService.Update(training);
            return RedirectToAction(nameof(Index));
        }


        //Delete 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _trainingService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


        private void LoadDropdowns()
        {
            ViewBag.Students = new SelectList(
                _studentService.GetAll(),
                "StudentId",
                "StudentNumber"
            );
            ViewBag.Doctors = new SelectList(
                _doctorService.GetAll(),
                "DoctorId",
                "Name"
            );
            ViewBag.Companies = new SelectList(
                _companyService.GetAll(),
                "CompanyId",
                "Name"
            );
            ViewBag.CompanySupervisors = new SelectList(
                _companySupervisorService.GetAll(),
                "CompanySupervisorId",
                "Name"
            );
        }
    }
}
