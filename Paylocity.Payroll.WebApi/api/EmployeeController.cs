using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Paylocity.Models;
using Paylocity.Business;

namespace Paylocity.Payroll.WebApi.Controllers
{
    [RoutePrefix("api/v1")]
    [EnableCors("http://localhost:10086", "*","*")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeBL _employeeService;

        public EmployeeController(IEmployeeBL employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: Employees
        [Route("employees")]
        [HttpGet]
        public async Task<List<EmployeeModel>> GetEmployees()
        {
            return await _employeeService.GetEmployees();
        }

        [Route("employee/{id}")]
        [HttpGet]
        public async Task<EmployeeModel> GetEmployee(int id)
        {
            return await _employeeService.GetEmployee(id);
        }


        [Route("employee")]
        [HttpPost]
        public async Task<EmployeeModel> SaveEmployee(EmployeeModel employee)
        {
            return await _employeeService.SaveEmployee(employee);
        }
    }
}