using DAL.Entities;
using DAL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainLink.ViewModels;

namespace TrainLink.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;
        private readonly IDoctorService _doctorService;
        private readonly ICompanySupervisorService _companySupervisorService;
        private readonly IUniversityService _universityService;
        private readonly ICompanyService _companyService;

        public UserController(
            IUserService userService,
            IStudentService studentService,
            IDoctorService doctorService,
            ICompanySupervisorService companySupervisorService,
            IUniversityService universityService,
            ICompanyService companyService)
        {
            _userService = userService;
            _studentService = studentService;
            _doctorService = doctorService;
            _companySupervisorService = companySupervisorService;
            _universityService = universityService;
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAll().ToList();

            var students = _studentService.GetAll().ToList();

            var doctors = _doctorService.GetAll().ToList();

            var supervisors = _companySupervisorService
                .GetAll()
                .ToList();

            var universities = _universityService
                .GetAll()
                .ToList();

            var companies = _companyService
                .GetAll()
                .ToList();


            var result = users.Select(user =>
            {
                var student = students
                    .FirstOrDefault(x => x.UserId == user.UserId);

                var doctor = doctors
                    .FirstOrDefault(x => x.UserId == user.UserId);

                var supervisor = supervisors
                    .FirstOrDefault(x => x.UserId == user.UserId);


                int? universityId =
                    student?.UniversityId ??
                    doctor?.UniversityId;


                int? companyId =
                    supervisor?.CompanyId;


                return new UserListViewModel
                {
                    UserId = user.UserId,

                    Name = user.Name,

                    Email = user.Email,

                    Role = user.Role,
                    Phone = user.Phone,

                    UniversityName = universityId.HasValue
                        ? universities
                            .FirstOrDefault(
                                x => x.UniversityId == universityId.Value
                            )?.Name
                        : null,

                    CompanyName = companyId.HasValue
                        ? companies
                            .FirstOrDefault(
                                x => x.CompanyId == companyId.Value
                            )?.Name
                        : null,

                    StudentNumber = student?.StudentNumber,

                    Major = student?.Major
                };
            }).ToList();


            ViewBag.Universities = new SelectList(
                universities,
                "UniversityId",
                "Name"
            );


            ViewBag.Companies = new SelectList(
                companies,
                "CompanyId",
                "Name"
            );


            return View(result);
        }


        public IActionResult Create()
        {
            LoadDropdowns();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError(
                    "Name",
                    "Name is required."
                );
            }

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                ModelState.AddModelError(
                    "Email",
                    "Email is required."
                );
            }

            if (string.IsNullOrWhiteSpace(model.Role))
            {
                ModelState.AddModelError(
                    "Role",
                    "Please select a role."
                );
            }
            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                var existingUser = _userService
                    .GetByEmail(model.Email)
                    .FirstOrDefault();

                if (existingUser != null)
                {
                    ModelState.AddModelError(
                        "Email",
                        "This email already exists."
                    );
                }
            }
            if (model.Role == "Student")
            {
                if (model.UniversityId == null)
                {
                    ModelState.AddModelError(
                        "UniversityId",
                        "University is required."
                    );
                }

                if (string.IsNullOrWhiteSpace(model.StudentNumber))
                {
                    ModelState.AddModelError(
                        "StudentNumber",
                        "Student Number is required."
                    );
                }
                else
                {
                    var existingStudent = _studentService
                        .GetByStudentNumber(model.StudentNumber)
                        .FirstOrDefault();

                    if (existingStudent != null)
                    {
                        ModelState.AddModelError(
                            "StudentNumber",
                            "This student number already exists."
                        );
                    }
                }

                if (string.IsNullOrWhiteSpace(model.Major))
                {
                    ModelState.AddModelError(
                        "Major",
                        "Major is required."
                    );
                }
            }
            if (model.Role == "Doctor")
            {
                if (model.UniversityId == null)
                {
                    ModelState.AddModelError(
                        "UniversityId",
                        "University is required."
                    );
                }
            }
            if (model.Role == "CompanySupervisor")
            {
                if (model.CompanyId == null)
                {
                    ModelState.AddModelError(
                        "CompanyId",
                        "Company is required."
                    );
                }
            }
            if (!ModelState.IsValid)
            {
                LoadDropdowns();

                return View(model);
            }
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
                Phone =model.Phone
            };

            await _userService.Add(user);
            if (model.Role == "Student")
            {
                var student = new Student
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    UniversityId = model.UniversityId.Value,
                    StudentNumber = model.StudentNumber,
                    Major = model.Major
                };

                await _studentService.Add(student);
            }
            else if (model.Role == "Doctor")
            {
                var doctor = new Doctor
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    UniversityId = model.UniversityId.Value
                };

                await _doctorService.Add(doctor);
            }
            else if (model.Role == "CompanySupervisor")
            {
                var supervisor = new CompanySupervisor
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    CompanyId = model.CompanyId.Value
                };

                await _companySupervisorService.Add(supervisor);
            }
            else if (model.Role == "Admin")
            {
            }


            return RedirectToAction("Index","User");
        }

        public IActionResult Details(int id)
        {
            var user = _userService
                .GetById(id)
                .FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            var student = _studentService
                .GetByUserId(id)
                .FirstOrDefault();

            var doctor = _doctorService
                .GetByUserId(id)
                .FirstOrDefault();

            var supervisor = _companySupervisorService
                .GetByUserId(id)
                .FirstOrDefault();

            string universityName = null;
            string companyName = null;

            if (student != null)
            {
                universityName = _universityService
                    .GetById(student.UniversityId)
                    .FirstOrDefault()
                    ?.Name;
            }
            else if (doctor != null)
            {
                universityName = _universityService
                    .GetById(doctor.UniversityId)
                    .FirstOrDefault()
                    ?.Name;
            }

            if (supervisor != null)
            {
                companyName = _companyService
                    .GetById(supervisor.CompanyId)
                    .FirstOrDefault()
                    ?.Name;
            }

            var model = new UserDetailsViewModel
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,

                UniversityName = universityName,

                StudentNumber = student?.StudentNumber,

                Major = student?.Major,

                CompanyName = companyName
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = _userService
                .GetById(id)
                .FirstOrDefault();


            if (user == null)
            {
                return NotFound();
            }


            // Delete role record first
            if (user.Role == "Student")
            {
                var student = _studentService
                    .GetByUserId(id)
                    .FirstOrDefault();

                if (student != null)
                {
                    await _studentService.Delete(
                        student.StudentId
                    );
                }
            }


            else if (user.Role == "Doctor")
            {
                var doctor = _doctorService
                    .GetByUserId(id)
                    .FirstOrDefault();

                if (doctor != null)
                {
                    await _doctorService.Delete(
                        doctor.DoctorId
                    );
                }
            }


            else if (user.Role == "CompanySupervisor")
            {
                var supervisor = _companySupervisorService
                    .GetByUserId(id)
                    .FirstOrDefault();

                if (supervisor != null)
                {
                    await _companySupervisorService.Delete(
                        supervisor.CompanySupervisorId
                    );
                }
            }


            // Delete User
            await _userService.Delete(id);


            return RedirectToAction(nameof(Index));
        }


        private void LoadDropdowns()
        {
            ViewBag.Universities = new SelectList(
                _universityService.GetAll(),
                "UniversityId",
                "Name"
            );


            ViewBag.Companies = new SelectList(
                _companyService.GetAll(),
                "CompanyId",
                "Name"
            );
        }
    }
}