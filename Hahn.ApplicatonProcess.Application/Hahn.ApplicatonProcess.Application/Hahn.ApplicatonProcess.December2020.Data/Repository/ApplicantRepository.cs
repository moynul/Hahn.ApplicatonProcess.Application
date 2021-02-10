using Hahn.ApplicatonProcess.December2020.Data.Context;
using Hahn.ApplicatonProcess.December2020.Data.Interface;
using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repository
{
    public class ApplicantRepository : IApplicant
    {
        private readonly ApplicantDBContext _applicantContext;

        public ApplicantRepository(ApplicantDBContext applicantContext)
        {
            _applicantContext = applicantContext;
        }

        public async Task<Applicant> AddApplicant(Applicant applicant)
        {
            var applicantobj = await _applicantContext.Applicants.AddAsync(applicant);
            await _applicantContext.SaveChangesAsync();
            return applicantobj.Entity;
        }

        public async Task<ApiOutput> DeleteApplicant(int id)
        {
            var Applicant =  GetApplicantById(id).FirstOrDefault();
            if (Applicant != null)
            {
                _applicantContext.Applicants.Remove(Applicant);
                await _applicantContext.SaveChangesAsync();
                return ApiOutput.Success;
            }
            return ApiOutput.Fail;
        }

        public IEnumerable<Applicant> GetApplicantById(int id)
        {
            var Applicant =  _applicantContext.Applicants.Where(c=>c.Id==id).ToList();
            return Applicant;            
        }

        public IEnumerable<Applicant> GetApplicant()
        {
            var Applicant = _applicantContext.Applicants.ToList();
            return Applicant;
        }

        public async Task Update(Applicant applicant)
        {
            _applicantContext.Applicants.Update(applicant);
            await _applicantContext.SaveChangesAsync();
        }

        public async Task<(ApiOutput, int)> Update(int id, Applicant applicant)
        {
            var OldEntity = GetApplicantById(id).FirstOrDefault();
            if (OldEntity != null)
            {
                OldEntity.Name = applicant.Name;
                OldEntity.FamilyName = applicant.FamilyName;
                OldEntity.EmailAdress = applicant.EmailAdress;
                OldEntity.Address = applicant.Address;
                OldEntity.Age = applicant.Age;
                OldEntity.CountryOfOrigin = applicant.CountryOfOrigin;
                OldEntity.Hired = applicant.Hired;

                await _applicantContext.SaveChangesAsync();

                return (ApiOutput.Success, OldEntity.Id);
            }
            return (ApiOutput.Fail, default);
        }
    }
}
