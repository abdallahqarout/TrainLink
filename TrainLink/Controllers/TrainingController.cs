using Microsoft.AspNetCore.Mvc;
using DAL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

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
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(userIdString) || string.IsNullOrEmpty(role))
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = int.Parse(userIdString);

            IQueryable<Training> trainings;

            switch (role)
            {
                case "Doctor":

                    var doctor = _doctorService.GetByUserId(userId).FirstOrDefault();
                    if (doctor == null)
                        return Unauthorized();
                    trainings = _trainingService.GetByDoctorId(doctor.DoctorId);
                    break;
                case "Student":
                    var student = _studentService.GetByUserId(userId).FirstOrDefault();
                    if (student == null)
                        return Unauthorized();
                    trainings = _trainingService.GetByStudentId(student.StudentId);
                    break;
                case "CompanySupervisor":
                    var supervisor = _companySupervisorService.GetByUserId(userId).FirstOrDefault();
                    if (supervisor == null)
                        return Unauthorized();
                    trainings = _trainingService.GetByCompanySupervisorId(supervisor.CompanySupervisorId);
                    break;
                case "Admin":
                    trainings = _trainingService.GetAll();
                    break;
                default:
                    return Unauthorized();
            }

            return View(trainings);
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
            var training = _trainingService
                .GetById(id).FirstOrDefault();

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
            ViewBag.Students = new SelectList(_studentService.GetAll(),
                "StudentId","Name"
            );
            ViewBag.Doctors = new SelectList(_doctorService.GetAll(),
                "DoctorId","Name"
            );
            ViewBag.Companies = new SelectList(_companyService.GetAll(),
                "CompanyId","Name"
            );
            ViewBag.CompanySupervisors = new SelectList(_companySupervisorService.GetAll()
                ,"CompanySupervisorId","Name"
            );
        }
    }
}
