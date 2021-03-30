using ALX_CodingAssignment.Domain.Interfaces;
using ALX_CodingAssignment.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Commands.ActivateServicePromoCode
{
    public class ActivateServicePromoCode : ICommand
    {
        public ActivateServicePromoCode(int userId, int servicePromoCodeId)
        {
            UserId = Guard.IsNotNull(userId, nameof(userId));
            ServicePromoCodeId = Guard.IsNotNull(servicePromoCodeId, nameof(servicePromoCodeId));
        }
        public int UserId { get; set; }
        public int ServicePromoCodeId { get; set; }
    }
}
