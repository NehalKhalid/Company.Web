using Company.Repository.Interfaces;
using Company.Service.Services.Employee.Dto;
using Company.Data.Models;
using AutoMapper;
using Company.Service.Helpers;
namespace Company.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void Add(EmployeeDto employeeDto)
        {
            employeeDto.ImageUrl = DocumentSettings.UploadFile(employeeDto.Image, "Images");
            Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.Complete();
        }
        public void Delete(EmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }
        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            IEnumerable<EmployeeDto> mappedEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return mappedEmployees;
        }
        public EmployeeDto GetById(int? id)
        {
            if (id is null)
                return null;
            var employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
            if (employee is null)
                return null;
            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }
        public IEnumerable<EmployeeDto> GetEmployeeByName(string name)
        {
            var employees =_unitOfWork.EmployeeRepository.GetEmployeeByName(name);

            IEnumerable<EmployeeDto> mappedEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return mappedEmployees;
        }
        public void Update(EmployeeDto employeeDto)
        {
            var updatedEmployee = GetById(employeeDto.Id);
            if (updatedEmployee.Name != employeeDto.Name)
            {
                if (GetAll().Any(x => x.Name == employeeDto.Name))
                    throw new Exception("DuplicateEmployeeName");
            }
            updatedEmployee.Name = employeeDto.Name;
            updatedEmployee.Email = employeeDto.Email;
            updatedEmployee.Salary = employeeDto.Salary;
            updatedEmployee.Address = employeeDto.Address;
            updatedEmployee.PhoneNumber = employeeDto.PhoneNumber;
            updatedEmployee.HireDate = employeeDto.HireDate;
            updatedEmployee.ImageUrl =  employeeDto.ImageUrl;
            updatedEmployee.Age = employeeDto.Age;
            Employee employee = _mapper.Map<Employee>(updatedEmployee);
            _unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.Complete();
        }
    }
}
