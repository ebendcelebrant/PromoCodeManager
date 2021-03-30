using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.Domain.Models
{
    public class UserPromoCode
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServicePromoCodeId{ get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public virtual User User { get; set; }
        public virtual ServicePromoCode ServicePromoCode { get; set; }
    }
}
