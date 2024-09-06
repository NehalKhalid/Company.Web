using Company.Data.Models;
using Company.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Employee employee)
        {
            var mappedEmployee = new Employee {
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Salary = employee.Salary,
                Email = employee.Email,
                HireDate = employee.HireDate,
                PhoneNumber = employee.PhoneNumber,
                ImageUrl = employee.ImageUrl,
            };
            _unitOfWork.EmployeeRepository.Add(mappedEmployee);
            _unitOfWork.Complete();
        }

        public void Delete(Employee employee)
        {
            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<Employee> GetAll()
            =>_unitOfWork.EmployeeRepository.GetAll();

        public Employee GetById(int? id)
        {
            if (id is null)
                return null;
            var employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
            if (employee is null)
                return null;
            return employee;
        }

        public IEnumerable<Employee> GetEmployeeByName(string name)
            =>_unitOfWork.EmployeeRepository.GetEmployeeByName(name);

        public void Update(Employee employee)
        {
            var updatedEmployee = GetById(employee.Id);
            if (updatedEmployee.Name != employee.Name)
            {
                if (GetAll().Any(x => x.Name == employee.Name))
                    throw new Exception("DuplicateEmployeeName");
            }
            updatedEmployee.Name = employee.Name;
            updatedEmployee.Email = employee.Email;
            updatedEmployee.Salary = employee.Salary;
            updatedEmployee.Address = employee.Address;
            updatedEmployee.PhoneNumber = employee.PhoneNumber;
            updatedEmployee.HireDate = employee.HireDate;
            updatedEmployee.ImageUrl = employee.ImageUrl;
            updatedEmployee.Age = employee.Age;
            _unitOfWork.EmployeeRepository.Update(updatedEmployee);
            _unitOfWork.Complete();
        }
    }
}
