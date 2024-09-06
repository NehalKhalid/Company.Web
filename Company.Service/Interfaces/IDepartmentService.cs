using Company.Data.Models;

namespace Company.Service
{
    public interface IDepartmentService
    {
        Department GetById(int? id);
        IEnumerable<Department> GetAll();
        void Add(Department department);
        void Update(Department department);
        void Delete(Department department);
    }
}
