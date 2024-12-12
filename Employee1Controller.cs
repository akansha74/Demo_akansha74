using AllFunctionalityNetCore.Data;
using AllFunctionalityNetCore.Models;
using AllFunctionalityNetCore.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace AllFunctionalityNetCore.Controllers.Employee1
{
    public class Employee1Controller : Controller
    {
        private readonly ApplicationContext context;

        public Employee1Controller(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var data = context.Employees.ToList();
            return View(data);
        }
        public IActionResult Create()
        {
          return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel emp)
        {
             if(ModelState.IsValid)
             {
                    var data = new Employee()
                    {
                        Name = emp.Name,
                        City = emp.City,
                        State = emp.State,
                        Salary = emp.Salary

                    };
                    context.Employees.Add(data);
                    context.SaveChanges();
                TempData["error"] = "Data saved successfully";
                return RedirectToAction("Index");
             }
             else
             {
                    TempData["error"] = "Empty form can't submit";
                    return View(emp);
             }
            
        }
        public IActionResult Edit(int id)
        {
            var emp = context.Employees.Where(e => e.Id == id).SingleOrDefault();
            var data = new Employee()
            {
                Name = emp.Name,
                City = emp.City,
                State = emp.State,
                Salary = emp.Salary

            };
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel emp)
        {
            if (ModelState.IsValid)
            {
                    var data = new Employee()
                    {
                        Id=emp.Id,
                        Name = emp.Name,
                        City = emp.City,
                        State = emp.State,
                        Salary = emp.Salary

                    };
                    context.Employees.Update(data);
                    context.SaveChanges();
                    TempData["error"] = "Data Update successfully";
                    return RedirectToAction("Index");
            
            }
            else
            {
                TempData["error"] = "Empty form can't submit";
                return View(emp);
            }

        }

        public IActionResult Delete(int id)
        {
            var emp = context.Employees.Where(e => e.Id == id).SingleOrDefault();
            context.Employees.Remove(emp);
            context.SaveChanges();
            TempData["error"] = "Data Removed";
            return RedirectToAction("index");
        }

    }
}
