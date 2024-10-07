using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Appliction.Models.Repository
{
    interface IEmployeeRepository:IDisposable
    {
        IEnumerable<Employee> GetEmployees();

        Employee GetEmployeeById(int Id);

        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(int Id);
        void Save();
    }
}
