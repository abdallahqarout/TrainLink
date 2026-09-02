using DAL.Entities;
using DAL.Services;
using Microsoft.AspNetCore.Mvc;
using TrainLink.ViewModels;

namespace TrainLink.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUniversityService _universityService;
        private readonly ICompanyService _companyService;

        public AdminController(
            IUniversityService universityService,
            ICompanyService companyService)
        {
            _universityService = universityService;
            _companyService = companyService;
        }

        public IActionResult Management()
        {
            var model = new ManagementViewModel
            {
                Universities = _universityService.GetAll().ToList(),

                Companies = _companyService.GetAll().ToList()
            };

            return View(model);
        }


        // ADD UNIVERSITY
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUniversity(string name,string email,string phone,string address)
        {
            if (string.IsNullOrWhiteSpace(name) ||string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||string.IsNullOrWhiteSpace(address))
            {
                TempData["Error"] ="All university fields are required.";
                return RedirectToAction("Admin", "Management");
            }

            var existingUnv = _universityService.GetByName(name).FirstOrDefault();

            if (existingUnv != null)
            {
                TempData["Error"] ="This university already exists.";
                return RedirectToAction("Admin", "Management");
            }

            var university = new University
            {
                Name =name, Email =email, Phone =phone, Address =address
            };

            await _universityService.Add(university);
            TempData["Success"] ="University added successfully.";
            return RedirectToAction("Admin", "Management");
        }


        // ADD COMPANY

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCompany(string name,string email,string phone,string address)
        {
            if (string.IsNullOrWhiteSpace(name) ||string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(address))
            {
                TempData["Error"] ="All company fields are required.";
                return RedirectToAction("Admin", "Management");
            }

            var existingCom = _companyService.GetByName(name).FirstOrDefault();

            if (existingCom != null)
            {
                TempData["Error"] ="This company already exists.";
                return RedirectToAction("Admin", "Management");
            }

            var company = new Company
            {
                Name = name,Email = email,Phone = phone,Address = address
            };
            await _companyService.Add(company);
            TempData["Success"] ="Company added successfully.";
            return RedirectToAction("Admin", "Management");
        }


        // DELETE UNIVERSITY 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUniversity(int id)
        {
            var university = _universityService.GetById(id).FirstOrDefault();

            if (university == null)
            {
                return NotFound();
            }

            await _universityService.Delete(id);
            TempData["Success"] ="University deleted successfully.";
            return RedirectToAction("Admin", "Management");
        }


        // DELETE COMPANY

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = _companyService.GetById(id).FirstOrDefault();

            if (company == null)
            {
                return NotFound();
            }
            await _companyService.Delete(id);

            TempData["Success"] ="Company deleted successfully.";

            return RedirectToAction("Admin", "Management");
        }
    }
}