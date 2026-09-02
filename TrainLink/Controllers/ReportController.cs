using Microsoft.AspNetCore.Mvc;
using DAL.Services;
using DAL.Entities;
using System.Security.Claims;

namespace TrainLink.Controllers
{
	public class ReportController : Controller
	{
		private readonly IWeeklyReportService _weeklyReportService;
		private readonly IFinalReportService _finalReportService;
		private readonly IFinalReportTaskService _finalReportTaskService;
		private readonly IFinalReportReferenceService _finalReportReferenceService;
		private readonly IFinalReportAppendixService _finalReportAppendixService;
		private readonly IReportReviewService _reportReviewService;
        private readonly IStudentService _studentService;
        private readonly ITrainingService _trainingService;
		private readonly IDoctorService _doctorService;
		private readonly ICompanySupervisorService _companySupervisorService;

        public ReportController(
			IWeeklyReportService weeklyReportService,
			IFinalReportService finalReportService,
			IFinalReportTaskService finalReportTaskService,
			IFinalReportReferenceService finalReportReferenceService,
			IFinalReportAppendixService finalReportAppendixService,
			IReportReviewService reportReviewService,
			IStudentService studentService,
			ITrainingService trainingService,
			IDoctorService doctorService,
			ICompanySupervisorService  companySupervisorService
			)
		{
			_weeklyReportService = weeklyReportService;
			_finalReportService = finalReportService;
			_finalReportTaskService = finalReportTaskService;
			_finalReportReferenceService = finalReportReferenceService;
			_finalReportAppendixService = finalReportAppendixService;
			_reportReviewService = reportReviewService;
			_studentService = studentService;
			_trainingService = trainingService;
			_doctorService = doctorService;
			_companySupervisorService = companySupervisorService;

		}

        // WEEKLY REPORT
        public IActionResult Weekly()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(userIdString) || string.IsNullOrEmpty(role))
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = int.Parse(userIdString);

            var reports = _weeklyReportService.GetAll();

            if (role == "Student")
            {
                var student = _studentService
                    .GetByUserId(userId)
                    .FirstOrDefault();

                if (student == null)
                {
                    return Unauthorized();
                }

                reports = reports
                    .Where(x => x.Training != null
                             && x.Training.StudentId == student.StudentId);
            }
            else if (role == "CompanySupervisor")
            {
                var supervisor = _companySupervisorService
                    .GetByUserId(userId)
                    .FirstOrDefault();

                if (supervisor == null)
                {
                    return Unauthorized();
                }

                reports = reports
                    .Where(x => x.Training != null
                             && x.Training.CompanySupervisorId == supervisor.CompanySupervisorId);
            }
            else if (role == "Doctor")
            {
                var doctor = _doctorService
                    .GetByUserId(userId)
                    .FirstOrDefault();

                if (doctor == null)
                {
                    return Unauthorized();
                }

                reports = reports
                    .Where(x => x.Training != null
                             && x.Training.DoctorId == doctor.DoctorId
                             && (x.Status == "PendingDoctor"
                                 || x.Status == "Approved"));
            }
            else if (role == "Admin")
            {
            }
            else
            {
                return Unauthorized();
            }

            return View(reports);
        }

        public IActionResult CreateWeekly()
        {
            var report = new WeeklyReport
            {
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today
            };

            return View(report);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWeekly(WeeklyReport report)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var student = _studentService
                .GetByUserId(userId.Value)
                .FirstOrDefault();

            if (student == null)
            {
                return NotFound();
            }
            var training = _trainingService
                .GetByStudentId(student.StudentId)
                .FirstOrDefault();

            if (training == null)
            {
                ModelState.AddModelError(
                    "",
                    "No training has been assigned to this student."
                );

                return View(report);
            }
            report.TrainingId = training.TrainingId;
            report.Status = "PendingSupervisor";
            report.CreatedAt = DateTime.Now;
            report.UpdatedAt = DateTime.Now;
            ModelState.Remove("TrainingId");
            ModelState.Remove("Status");
            if (!ModelState.IsValid)
            {
                return View(report);
            }
            var existingReport = _weeklyReportService
                .GetWeekNumber(training.TrainingId, report.WeekNumber)
                .FirstOrDefault();

            if (existingReport != null)
            {
                ModelState.AddModelError(
                    "WeekNumber",
                    "You have already submitted a report for this week."
                );

                return View(report);
            }

            await _weeklyReportService.Add(report);

            return RedirectToAction(nameof(Weekly));
        }


        // EDIT WEEKLY
        public IActionResult EditWeekly(int id)
		{
			var report = _weeklyReportService
				.GetById(id)
				.FirstOrDefault();

			if (report == null)
			{
				return NotFound();
			}

			return View(report);
		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWeekly(WeeklyReport report)
        {
            if (!ModelState.IsValid)
            {
                return View(report);
            }

            var existingReport = _weeklyReportService
                .GetById(report.WeeklyReportId)
                .FirstOrDefault();

            if (existingReport == null)
            {
                return NotFound();
            }

            existingReport.WeekNumber = report.WeekNumber;
            existingReport.DateFrom = report.DateFrom;
            existingReport.DateTo = report.DateTo;
            existingReport.Tasks = report.Tasks;
            existingReport.Challenges = report.Challenges;
            existingReport.Skills = report.Skills;
            existingReport.HoursWorked = report.HoursWorked;

            // Resubmit to supervisor
            if (existingReport.Status == "ReturnedBySupervisor"
                || existingReport.Status == "ReturnedByDoctor")
            {
                existingReport.Status = "PendingSupervisor";
            }

            existingReport.UpdatedAt = DateTime.Now;

            await _weeklyReportService.Update(existingReport);

            return RedirectToAction(nameof(Weekly));
        }

        // WEEKLY DETAILS
        public IActionResult WeeklyDetails(int id)
		{
			var report = _weeklyReportService
				.GetById(id)
				.FirstOrDefault();

			if (report == null)
			{
				return NotFound();
			}
            var reviews = _reportReviewService.GetByWeeklyReportId(report.WeeklyReportId)
                .OrderByDescending(x => x.ReviewDate).ToList();
            ViewBag.Reviews = reviews;
            return View("~/Views/Report/WeeklyDetails.cshtml", report);
        }


		// DELETE WEEKLY
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteWeekly(int id)
		{
			await _weeklyReportService.Delete(id);

			return RedirectToAction(nameof(Weekly));
		}


		// FINAL REPORT
		public IActionResult Final()
		{
			var reports = _finalReportService.GetAll();

			return View(reports);
		}


		// CREATE FINAL
		public IActionResult CreateFinal()
		{
			var report = new FinalReport
			{
				SubmissionDate = DateTime.Now
			};

			return View(report);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateFinal(FinalReport report)
		{
			if (!ModelState.IsValid)
			{
				return View(report);
			}

			report.SubmissionDate = DateTime.Now;

			await _finalReportService.Add(report);

			return RedirectToAction(nameof(Final));
		}


		// EDIT FINAL
		public IActionResult EditFinal(int id)
		{
			var report = _finalReportService
				.GetById(id)
				.FirstOrDefault();

			if (report == null)
			{
				return NotFound();
			}

			return View(report);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditFinal(FinalReport report)
		{
			if (!ModelState.IsValid)
			{
				return View(report);
			}

			await _finalReportService.Update(report);

			return RedirectToAction(nameof(Final));
		}


		// FINAL DETAILS
		public IActionResult FinalDetails(int id)
		{
			var report = _finalReportService
				.GetById(id)
				.FirstOrDefault();

			if (report == null)
			{
				return NotFound();
			}

			return View(report);
		}


		// DELETE FINAL

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteFinal(int id)
		{
			await _finalReportService.Delete(id);

			return RedirectToAction(nameof(Final));
		}


		// FINAL REPORT TASKS
		public IActionResult Tasks(int finalReportId)
		{
			var finalReport = _finalReportService
				.GetById(finalReportId)
				.FirstOrDefault();

			if (finalReport == null)
			{
				return NotFound();
			}

			var tasks = _finalReportTaskService
				.GetByFinalReportId(finalReportId);

			ViewBag.FinalReportId = finalReportId;

			return View(tasks);
		}


		public IActionResult CreateTask(int finalReportId)
		{
			var finalReport = _finalReportService
				.GetById(finalReportId)
				.FirstOrDefault();

			if (finalReport == null)
			{
				return NotFound();
			}

			var task = new FinalReportTask
			{
				FinalReportId = finalReportId
			};

			return View(task);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateTask(FinalReportTask task)
		{
			if (!ModelState.IsValid)
			{
				return View(task);
			}

			await _finalReportTaskService.Add(task);

			return RedirectToAction(
				nameof(Tasks),
				new
				{
					finalReportId = task.FinalReportId
				});
		}


		// FINAL REPORT REFERENCES
		public IActionResult References(int finalReportId)
		{
			var finalReport = _finalReportService
				.GetById(finalReportId)
				.FirstOrDefault();

			if (finalReport == null)
			{
				return NotFound();
			}

			var references = _finalReportReferenceService
				.GetByFinalReportId(finalReportId);

			ViewBag.FinalReportId = finalReportId;

			return View(references);
		}


		public IActionResult CreateReference(int finalReportId)
		{
			var finalReport = _finalReportService
				.GetById(finalReportId)
				.FirstOrDefault();

			if (finalReport == null)
			{
				return NotFound();
			}

			var reference = new FinalReportReference
			{
				FinalReportId = finalReportId
			};

			return View(reference);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateReference(
			FinalReportReference reference)
		{
			if (!ModelState.IsValid)
			{
				return View(reference);
			}

			await _finalReportReferenceService.Add(reference);

			return RedirectToAction(
				nameof(References),
				new
				{
					finalReportId = reference.FinalReportId
				});
		}


		// FINAL REPORT APPENDICES
		public IActionResult Appendices(int finalReportId)
		{
			var finalReport = _finalReportService
				.GetById(finalReportId)
				.FirstOrDefault();

			if (finalReport == null)
			{
				return NotFound();
			}

			var appendices = _finalReportAppendixService
				.GetByFinalReportId(finalReportId);

			ViewBag.FinalReportId = finalReportId;

			return View(appendices);
		}


		public IActionResult CreateAppendix(int finalReportId)
		{
			var finalReport = _finalReportService
				.GetById(finalReportId)
				.FirstOrDefault();

			if (finalReport == null)
			{
				return NotFound();
			}

			var appendix = new FinalReportAppendix
			{
				FinalReportId = finalReportId
			};

			return View(appendix);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateAppendix(
			FinalReportAppendix appendix)
		{
			if (!ModelState.IsValid)
			{
				return View(appendix);
			}

			await _finalReportAppendixService.Add(appendix);

			return RedirectToAction(
				nameof(Appendices),
				new
				{
					finalReportId = appendix.FinalReportId
				});
		}
		
        public IActionResult Reviews()
		{
			var reviews = _reportReviewService.GetAll();

			return View(reviews);
		}

        // supervisor approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SupervisorApprove(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var supervisor = _companySupervisorService
                .GetByUserId(userId.Value)
                .FirstOrDefault();

            if (supervisor == null)
            {
                return Unauthorized();
            }
            var report = _weeklyReportService
                .GetById(id)
                .FirstOrDefault();

            if (report == null)
            {
                return NotFound();
            }
            var training = _trainingService
                .GetById(report.TrainingId)
                .FirstOrDefault();

            if (training == null)
            {
                return NotFound();
            }
            if (training.CompanySupervisorId != supervisor.CompanySupervisorId)
            {
                return Unauthorized();
            }

            if (report.Status != "PendingSupervisor")
            {
                return BadRequest();
            }
            report.Status = "PendingDoctor";
            report.UpdatedAt = DateTime.Now;
            await _weeklyReportService.Update(report);
            await _reportReviewService.Add(new ReportReview
            {
                WeeklyReportId = report.WeeklyReportId,
                ReviewerUserId = userId.Value,
                Decision = "Approved",
                Comments = "Approved by company supervisor.",
                ReviewDate = DateTime.Now
            });
            return RedirectToAction(nameof(Weekly));
        }


        //Supervisor Return
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SupervisorReturn(int id,string comments)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (string.IsNullOrWhiteSpace(comments))
            {
                return BadRequest("Comments are required.");
            }
            var supervisor = _companySupervisorService.GetByUserId(userId.Value).FirstOrDefault();
            if (supervisor == null)
            {
                return Unauthorized();
            }
            var report = _weeklyReportService.GetById(id).FirstOrDefault();
            if (report == null)
            {
                return NotFound();
            }
            var training = _trainingService.GetById(report.TrainingId).FirstOrDefault();
            if (training == null)
            {
                return NotFound();
            }
            if (training.CompanySupervisorId != supervisor.CompanySupervisorId)
            {
                return Unauthorized();
            }
            if (report.Status != "PendingSupervisor")
            {
                return BadRequest();
            }
            report.Status = "ReturnedBySupervisor";
            report.UpdatedAt = DateTime.Now;
            await _weeklyReportService.Update(report);
            await _reportReviewService.Add(new ReportReview
            {
                WeeklyReportId = report.WeeklyReportId,
                ReviewerUserId = userId.Value,
                Decision = "Returned",
                Comments = comments,
                ReviewDate = DateTime.Now
            });
            return RedirectToAction(nameof(Weekly));
        }


        // Doctor Approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoctorApprove(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var doctor = _doctorService.GetByUserId(userId.Value).FirstOrDefault();
            if (doctor == null)
            {
                return Unauthorized();
            }
            var report = _weeklyReportService.GetById(id).FirstOrDefault();
            if (report == null)
            {
                return NotFound();
            }
            var training = _trainingService.GetById(report.TrainingId).FirstOrDefault();
            if (training == null)
            {
                return NotFound();
            }
            if (training.DoctorId != doctor.DoctorId)
            {
                return Unauthorized();
            }
            if (report.Status != "PendingDoctor")
            {
                return BadRequest();
            }
            report.Status = "Approved";
            report.UpdatedAt = DateTime.Now;
            await _weeklyReportService.Update(report);
            await _reportReviewService.Add(new ReportReview
            {
                WeeklyReportId = report.WeeklyReportId,
                ReviewerUserId = userId.Value,
                Decision = "Approved",
                Comments = "Approved by university doctor.",
                ReviewDate = DateTime.Now
            });
            return RedirectToAction(nameof(Weekly));
        }


        // Doctor Return
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoctorReturn(int id,string comments)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (string.IsNullOrWhiteSpace(comments))
            {
                return BadRequest("Comments are required.");
            }
            var doctor = _doctorService.GetByUserId(userId.Value).FirstOrDefault();
            if (doctor == null)
            {
                return Unauthorized();
            }
            var report = _weeklyReportService.GetById(id).FirstOrDefault();
            if (report == null)
            {
                return NotFound();
            }
            var training = _trainingService.GetById(report.TrainingId).FirstOrDefault();
            if (training == null)
            {
                return NotFound();
            }
            if (training.DoctorId != doctor.DoctorId)
            {
                return Unauthorized();
            }
            if (report.Status != "PendingDoctor")
            {
                return BadRequest();
            }
            report.Status = "ReturnedByDoctor";
            report.UpdatedAt = DateTime.Now;
            await _weeklyReportService.Update(report);
            await _reportReviewService.Add(new ReportReview
            {
                WeeklyReportId = report.WeeklyReportId,
                ReviewerUserId = userId.Value,
                Decision = "Returned",
                Comments = comments,
                ReviewDate = DateTime.Now
            });
            return RedirectToAction(nameof(Weekly));
        }
    }
}