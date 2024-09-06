using Company.Data.Models;
using Company.Repository.Interfaces;

namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Department department)
        {
            var mappedDepartment = new Department
            {
                Code = department.Code,
                Name = department.Name
            };
            _unitOfWork.DepartmentRepository.Add(mappedDepartment);
            _unitOfWork.Complete();
        }

        public void Delete(Department department)
        {
            _unitOfWork.DepartmentRepository.Delete(department);
            _unitOfWork.Complete();
        }

        public IEnumerable<Department> GetAll()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return departments;
        }

        public Department GetById(int? id)
        {
            if (id is null)
                return null;
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (department is null)
                return null;
            return department;
        }

        public void Update(Department department)
        {
            var updatedDepartment = GetById(department.Id);
            if (updatedDepartment.Name != department.Name)
            {
                if (GetAll().Any(x => x.Name == department.Name))
                    throw new Exception("DuplicateDeparmentName");
            }
            updatedDepartment.Code = department.Code;
            updatedDepartment.Name = department.Name;
            _unitOfWork.DepartmentRepository.Update(updatedDepartment);
            _unitOfWork.Complete();
        }
    }
}
