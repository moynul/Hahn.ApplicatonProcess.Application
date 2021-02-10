using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Entities
{
    public class Applicant 
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public string EmailAdress { get; set; }
        public int Age { get; set; }
        public bool Hired { get; set; }

        #region Aduit Field
        //public DateTime CreatedDate { get; set; } 
        //public DateTime? UpdateDate { get; set; }
        //public string CreatedBy { get; set; }
        //public string UpdatedBy { get; set; }
        #endregion
    }
}
