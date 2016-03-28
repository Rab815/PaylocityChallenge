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
    [System.Web.Http.RoutePrefix("api/v1")]
    [EnableCors("http://localhost:10086", "*", "*")]
    public class DependentController : ApiController
    {
        private readonly IDependentBL _dependentService;

        public DependentController(IDependentBL employeeService)
        {
            _dependentService = employeeService;
        }

        // GET: Employees
        [Route("dependents/{employeeId}")]
        [HttpGet]
        public async Task<List<DependentModel>> GetDependents(int employeeId)
        {
            return await _dependentService.GetDependents(employeeId);
        }

        [Route("dependent/{id}")]
        [HttpGet]
        public async Task<DependentModel> GetDependent(int id)
        {
            return await _dependentService.GetDependent(id);
        }


        [Route("dependent")]
        [HttpPost]
        public async Task<DependentModel> SaveDependent(DependentModel dependent)
        {
            return await _dependentService.SaveDependent(dependent);
        }

        [Route("dependent/{id}")]
        [HttpDelete]
        public async Task<bool> DeleteDependent(int id)
        {
            return await _dependentService.DeleteDependent(id);
        }
    }
}