using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paylocity.Models;

namespace Paylocity.Business
{
    public interface IBenefitsBL
    {
        Task<BenefitsModel> GetEmployeeBenefits(int employeeId);
    }
}
