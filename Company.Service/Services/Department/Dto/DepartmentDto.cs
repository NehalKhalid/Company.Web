using Company.Service.Services.Employee.Dto;

namespace Company.Service.Services.Department.Dto
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
        public DateTime CreateAt { get; set; }
    }
}
