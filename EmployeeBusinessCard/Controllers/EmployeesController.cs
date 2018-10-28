using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeBusinessCard.Entities;
using EmployeeBusinessCard.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeBusinessCard.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeDbContext _employeeDbContext;

        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Microsoft.EntityFrameworkCore.DbSet<Employee> employees = _employeeDbContext.Employees;

            var employeeViewModels = new List<EmployeeViewModel>();

            foreach (var employee in employees)
            {
                EmployeeViewModel model = new EmployeeViewModel();
                model.Id = employee.Id;
                model.FirstName = employee.FirstName;
                model.LastName = employee.LastName;
                model.Address = employee.Address;
                model.Profession = employee.Profession;
                model.TelNumber = employee.TelNumber;
                model.Website = employee.Website;
                model.Email = employee.Email;
                model.PhotoName = employee.PhotoName;

                employeeViewModels.Add(model);
            }

            return View(new EmployeeSearchResultViewModel
            {
                Employees = employeeViewModels
            });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var employee = _employeeDbContext.Employees.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                _employeeDbContext.Employees.Remove(employee);
                _employeeDbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee();
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Address = model.Address;
                employee.Email = model.Email;
                employee.Profession = model.Profession;
                employee.TelNumber = model.TelNumber;
                employee.Website = model.Website;

                if (model.Photo != null && model.Photo.Length > 0)
                {
                    var photoName = $"{Guid.NewGuid()}_{model.Photo.FileName}";

                    var path = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "uploaded-user-photos",
                       photoName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        model.Photo.CopyTo(stream);
                        employee.PhotoName = photoName;

                    }
                }
                _employeeDbContext.Employees.Add(employee);

                _employeeDbContext.SaveChanges();


                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var employee = _employeeDbContext.Employees.FirstOrDefault(x => x.Id == id);

            EmployeeViewModel model = new EmployeeViewModel();
            model.Id = employee.Id;
            model.FirstName = employee.FirstName;
            model.LastName = employee.LastName;
            model.Address = employee.Address;
            model.Profession = employee.Profession;
            model.TelNumber = employee.TelNumber;
            model.Website = employee.Website;
            model.Email = employee.Email;
            model.PhotoName = employee.PhotoName;


            return View(model);
        }

        [HttpPost]
        public IActionResult Update(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeDbContext.Employees.FirstOrDefault(x => x.Id == model.Id);
                if (employee != null)
                {
                    employee.Address = model.Address;
                    employee.Email = model.Email;
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;
                    employee.Profession = model.Profession;
                    employee.TelNumber = model.TelNumber;
                    employee.Website = model.Website;

                    if (model.Photo != null && model.Photo.Length > 0)
                    {
                        var photoName = $"{Guid.NewGuid()}_{model.Photo.FileName}";

                        var path = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "uploaded-user-photos",
                           photoName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            model.Photo.CopyTo(stream);
                            employee.PhotoName = photoName;
                        }
                    }
                    _employeeDbContext.Employees.Update(employee);

                    _employeeDbContext.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Search(EmployeeSearchResultViewModel model)
        {
            var employees = _employeeDbContext.Employees.AsQueryable()
                  .Where(x => string.IsNullOrEmpty(model.EmployeeSearchParameters.FirstName) || x.FirstName.Contains(model.EmployeeSearchParameters.FirstName))
                  .Where(x => string.IsNullOrEmpty(model.EmployeeSearchParameters.LastName) || x.LastName.Contains(model.EmployeeSearchParameters.LastName))
                  .Where(x => string.IsNullOrEmpty(model.EmployeeSearchParameters.Address) || x.Address.Contains(model.EmployeeSearchParameters.Address))
                  .Where(x => string.IsNullOrEmpty(model.EmployeeSearchParameters.Email) || x.Email.Contains(model.EmployeeSearchParameters.Email))
                  .Where(x => string.IsNullOrEmpty(model.EmployeeSearchParameters.Profession) || x.Profession.Contains(model.EmployeeSearchParameters.Profession))
                  .Where(x => string.IsNullOrEmpty(model.EmployeeSearchParameters.TelNumber) || x.TelNumber.Contains(model.EmployeeSearchParameters.TelNumber))
                  .Where(x => string.IsNullOrEmpty(model.EmployeeSearchParameters.Website) || x.Website.Contains(model.EmployeeSearchParameters.Website)
                  ).ToList();

            var employeeViewModels = new List<EmployeeViewModel>();

            foreach (var employee in employees)
            {
                EmployeeViewModel employeeViewModel = new EmployeeViewModel();
                employeeViewModel.Id = employee.Id;
                employeeViewModel.FirstName = employee.FirstName;
                employeeViewModel.LastName = employee.LastName;
                employeeViewModel.Address = employee.Address;
                employeeViewModel.Profession = employee.Profession;
                employeeViewModel.TelNumber = employee.TelNumber;
                employeeViewModel.Website = employee.Website;
                employeeViewModel.Email = employee.Email;
                employeeViewModel.PhotoName = employee.PhotoName;

                employeeViewModels.Add(employeeViewModel);
            }

            model.Employees = employeeViewModels;

            return View("Index", model);
        }

        public IActionResult ShowPrintVersion(int id)
        {
            var employee = _employeeDbContext.Employees.FirstOrDefault(x => x.Id == id);

            EmployeeViewModel model = new EmployeeViewModel();
            model.Id = employee.Id;
            model.FirstName = employee.FirstName;
            model.LastName = employee.LastName;
            model.Address = employee.Address;
            model.Profession = employee.Profession;
            model.TelNumber = employee.TelNumber;
            model.Website = employee.Website;
            model.Email = employee.Email;

            return View(model);
        }
    }
}