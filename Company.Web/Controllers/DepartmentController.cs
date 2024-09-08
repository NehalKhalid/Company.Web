using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service;
using Company.Service.Services.Department.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentDto departmentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmentService.Add(departmentDto);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("DepartmentError", "ValidationErrors");
                return View(departmentDto);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("DepartmentError",ex.Message);
                return View(departmentDto); 
            }
            
        }

        [HttpGet]
        public IActionResult Details(int? id , string viewName = "Details") 
        {
            var department = _departmentService.GetById(id);
            if (department is null) 
                return RedirectToAction("NotFoundPage",null,"Home");
            return View(viewName,department);
        }
        [HttpGet]
        public IActionResult Update(int? id)
        {
            return Details(id,"Update");
        }
        [HttpPost]
        public IActionResult Update(int id,DepartmentDto departmentDto) 
        {
            if(departmentDto.Id != id)
                return RedirectToAction("NotFoundPage", null, "Home");
            _departmentService.Update(departmentDto);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            var department = _departmentService.GetById(id);
            if (department is null)
                return RedirectToAction("NotFoundPage", null, "Home");
            _departmentService.Delete(department);
            return RedirectToAction("Index");
        }
    }
}
