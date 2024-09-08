using Company.Data.Models;
using Company.Service;
using Company.Service.Services;
using Company.Service.Services.Employee.Dto;
using Company.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService,IDepartmentService departmentService) 
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        public IActionResult Index(string searchInp)
        {
            IEnumerable<EmployeeDto> employees = new List<EmployeeDto>();
            if (string.IsNullOrEmpty(searchInp))
                employees = _employeeService.GetAll();
            else
                employees = _employeeService.GetEmployeeByName(searchInp);
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeDto employeeDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.Add(employeeDto);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("EmployeeError", "ValidationErrors");
                return View(employeeDto);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("EmployeeError", ex.Message);
                return View(employeeDto);
            }

        }

    }
}
