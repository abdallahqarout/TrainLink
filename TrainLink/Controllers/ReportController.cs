using Microsoft.AspNetCore.Mvc;
using DAL.Services;
using DAL.Entities;

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

		public ReportController(
			IWeeklyReportService weeklyReportService,
			IFinalReportService finalReportService,
			IFinalReportTaskService finalReportTaskService,
			IFinalReportReferenceService finalReportReferenceService,
			IFinalReportAppendixService finalReportAppendixService,
			IReportReviewService reportReviewService)
		{
			_weeklyReportService = weeklyReportService;
			_finalReportService = finalReportService;
			_finalReportTaskService = finalReportTaskService;
			_finalReportReferenceService = finalReportReferenceService;
			_finalReportAppendixService = finalReportAppendixService;
			_reportReviewService = reportReviewService;
		}

		// WEEKLY REPORT
		public IActionResult Weekly()
		{
			var reports = _weeklyReportService.GetAll();

			return View(reports);
		}


		// CREATE WEEKLY
		public IActionResult CreateWeekly()
		{
			var report = new WeeklyReport
			{
				DateFrom = DateTime.Today,
				DateTo = DateTime.Today,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				Status = "Pending"
			};

			return View(report);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateWeekly(WeeklyReport report)
		{
			if (!ModelState.IsValid)
			{
				return View(report);
			}

			report.CreatedAt = DateTime.Now;
			report.UpdatedAt = DateTime.Now;

			if (string.IsNullOrEmpty(report.Status))
			{
				report.Status = "Pending";
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

			report.UpdatedAt = DateTime.Now;

			await _weeklyReportService.Update(report);

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
		/// REPORT REVIEWS
		public IActionResult Reviews()
		{
			var reviews = _reportReviewService.GetAll();

			return View(reviews);
		}
	}
}