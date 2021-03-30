using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.Domain.Models
{
    public class User : IdentityUser<int>
    {
        public virtual IEnumerable<UserPromoCode> UserPromoCodes { get; set; }
    }
}
