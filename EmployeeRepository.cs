using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Appliction.Models.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private HR_DBEntities _context;

        public EmployeeRepository(HR_DBEntities context) { 
        
            this._context = context;
        }

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
        }

        public void Delete(int Id)
        {
            Employee employee = _context.Employees.Find(Id);
            _context.Employees.Remove(employee);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeById(int Id)
        {
            return _context.Employees.Find(Id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        public void Save()
        {
          _context.SaveChanges();
        }

        public void Update(Employee employee)
        {
            _context.Entry(employee).State = System.Data.Entity.EntityState.Modified;
        }
    }
}