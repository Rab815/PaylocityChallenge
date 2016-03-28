using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Paylocity.Models;
using Paylocity.Business;

namespace Paylocity.Payroll.WebApi.api
{
    [System.Web.Http.RoutePrefix("api/v1")]
    [EnableCors("http://localhost:10086", "*", "*")]
    public class BenefitsController : ApiController
    {
        private readonly IBenefitsBL _benefitsService;

        public BenefitsController(IBenefitsBL benefitsService)
        {
            _benefitsService = benefitsService;
        }

        [Route("benefits/employee/{id}")]
        [HttpGet]
        public async Task<BenefitsModel> GetEmployeeBenefits(int id)
        {
            return await _benefitsService.GetEmployeeBenefits(id);
        }
    }
}