using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
            List<Employee> employees = _employeeDbContext.Employees.ToList();

            EmployeeSearchResultViewModel model = new EmployeeSearchResultViewModel();
            model.Employees = Mapper.Map<List<EmployeeViewModel>>(employees);

            return View(model);
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
                Employee employee = Mapper.Map<Employee>(model);

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
            Employee employee = _employeeDbContext.Employees.FirstOrDefault(x => x.Id == id);

            EmployeeViewModel model = Mapper.Map<EmployeeViewModel>(employee);

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
                    Mapper.Map(model, employee);

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
            List<Employee> employees = _employeeDbContext.Employees.AsQueryable()
                  .Where(x => string.IsNullOrEmpty(model.EmployeeSearchParameters.FirstName) || x.FirstName.Contains(model.EmployeeSearchParameters.FirstName))
                  .Where(x => string.IsNullOrEmpty(model.EmployeeSearchParameters.LastName) || x.LastName.Contains(model.EmployeeSearchParameters.LastName))
                  .Where(x => string.IsNullOrEmpty(model.EmployeeSearchParameters.Address) || x.Address.Contains(model.EmployeeSearchParameters.Address))
                  .Where(x => string.IsNullOrEmpty(model.EmployeeSearchParameters.Email) || x.Email.Contains(model.EmployeeSearchParameters.Email))
                  .Where(x => string.IsNullOrEmpty(model.EmployeeSearchParameters.Profession) || x.Profession.Contains(model.EmployeeSearchParameters.Profession))
                  .Where(x => string.IsNullOrEmpty(model.EmployeeSearchParameters.TelNumber) || x.TelNumber.Contains(model.EmployeeSearchParameters.TelNumber))
                  .Where(x => string.IsNullOrEmpty(model.EmployeeSearchParameters.Website) || x.Website.Contains(model.EmployeeSearchParameters.Website)
                  ).ToList();


            model.Employees = Mapper.Map<List<EmployeeViewModel>>(employees);

            return View("Index", model);
        }
        
        [HttpGet]
        public IActionResult ShowPrintVersion(int id)
        {
            Employee employee = _employeeDbContext.Employees.FirstOrDefault(x => x.Id == id);

            EmployeeViewModel model = Mapper.Map<EmployeeViewModel>(employee);

            return View(model);
        }
    }
}