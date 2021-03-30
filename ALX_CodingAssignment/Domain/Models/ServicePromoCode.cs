using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.Domain.Models
{
    public class ServicePromoCode
    {
        public ServicePromoCode()
        {

        }
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string PromoCode { get; set; }
        //public bool IsActivated { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public virtual IEnumerable<UserPromoCode> UserPromoCodes { get; set; }
    }
}
