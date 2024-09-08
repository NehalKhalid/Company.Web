using AutoMapper;
using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Services.Department.Dto;

namespace Company.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(DepartmentDto departmentDto)
        {
            var mappedDepartment = _mapper.Map<Department>(departmentDto); 
            _unitOfWork.DepartmentRepository.Add(mappedDepartment);
            _unitOfWork.Complete();
        }

        public void Delete(DepartmentDto departmentDto)
        {
            var mappedDepartment = _mapper.Map<Department>(departmentDto);
            _unitOfWork.DepartmentRepository.Delete(mappedDepartment);
            _unitOfWork.Complete();
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            var mappedDepartments = _mapper.Map<IEnumerable<DepartmentDto>>(departments);

            return mappedDepartments;
        }

        public DepartmentDto GetById(int? id)
        {
            if (id is null)
                return null;
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (department is null)
                return null;
            var mappedDepartment = _mapper.Map<DepartmentDto>(department);

            return mappedDepartment;
        }

        public void Update(DepartmentDto department)
        {
            var updatedDepartment = GetById(department.Id);
            if (updatedDepartment.Name != department.Name)
            {
                if (GetAll().Any(x => x.Name == department.Name))
                    throw new Exception("DuplicateDeparmentName");
            }
            updatedDepartment.Code = department.Code;
            updatedDepartment.Name = department.Name;
            Department employee = _mapper.Map<Department>(updatedDepartment);

            _unitOfWork.DepartmentRepository.Update(employee);
            _unitOfWork.Complete();
        }
    }
}
