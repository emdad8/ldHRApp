using HR_Appliction.Models;
using HR_Appliction.Models.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace HR_Appliction.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository employeeRepo;

        public EmployeeController() {

            this.employeeRepo = new EmployeeRepository(new HR_DBEntities());
        }

        // GET: Employee
        public ActionResult Index()
        {
            var data = from m in employeeRepo.GetEmployees() select m;
            return View(data);
        }


        [HttpPost]
        public ActionResult Index(string txtSearch) {

            if (txtSearch.ToString()==null||txtSearch.ToString()=="")
            {
                return View(employeeRepo.GetEmployees().ToList());
            }
            else
            {
                int Id = Convert.ToInt32(txtSearch.ToString());
                Employee empObj = employeeRepo.GetEmployeeById(Id);
                List<Employee> elist = new List<Employee>();
                elist.Add(empObj);
                return View(elist);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFile file)
        {
            List<Employee> employees = new List<Employee>();

            var path = "";
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                path = Path.Combine(Server.MapPath("~/HRApplication/Content"), fileName);
                file.SaveAs(path);
            }

            XmlDocument xmlDoc = new XmlDocument();
            string xmlPath = path;
            xmlDoc.Load(path);

            XmlNodeList Clist = xmlDoc.GetElementsByTagName("name");
            //for (int i=0;i<Clist.Count;i++) {

            //    Console.WriteLine(Clist[i].InnerText.ToString());
            //    System.Threading.Thread.Sleep(1000);
            //}
            //Console.WriteLine(Environment.NewLine);

            foreach (XmlNode node in xmlDoc.SelectNodes("Content/Employee"))
            {
                employees.Add(new Employee
                {
                    FirstName = node["FirstName"].InnerText,
                    LastName = node["LastName"].InnerText,
                    Division = node["Division"].InnerText,
                    Building = node["Room"].InnerText,
                    Title = node["Title"].InnerText,
                    Room = node["Room"].InnerText
                });
            }
            // call save changes

            return View(employees);
        }

        [HttpPost]
        public ActionResult Create(Employee employee) { 
        
            employeeRepo.Add(employee);
            employeeRepo.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id) {

            Employee e=employeeRepo.GetEmployeeById(Id);
            return View(e);
        }

        [HttpPost]
        public ActionResult Edit(int Id,Employee employee)
        {
            employeeRepo.Update(employee);
            employeeRepo.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int Id)
        {
            Employee e=employeeRepo.GetEmployeeById(Id);
            return View(e);
        }

        public ActionResult Delete(int Id) { 
        Employee e=employeeRepo.GetEmployeeById(Id);
            return View(e);
        }

        [HttpPost]
        public ActionResult Delete(int Id,Employee e) {
        
            employeeRepo.Delete(Id);
            employeeRepo.Save();
            return RedirectToAction("Index");
        }



    }
}