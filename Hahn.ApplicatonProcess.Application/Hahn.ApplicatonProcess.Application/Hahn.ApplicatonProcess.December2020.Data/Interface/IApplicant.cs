using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Interface
{
    public interface IApplicant
    {
        Task<Applicant> AddApplicant(Applicant applicant);
        Task<ApiOutput> DeleteApplicant(int id);
        IEnumerable<Applicant> GetApplicantById(int id);
        IEnumerable<Applicant> GetApplicant();
        Task Update(Applicant applicant);
        Task<(ApiOutput, int)> Update(int id, Applicant applicant);
    }
}
