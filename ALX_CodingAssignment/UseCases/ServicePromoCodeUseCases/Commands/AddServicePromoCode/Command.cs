using ALX_CodingAssignment.Domain.Interfaces;
using ALX_CodingAssignment.Domain.Models;
using ALX_CodingAssignment.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Commands.AddServicePromoCode
{
    public class AddServicePromoCode : ICommand
    {
        public AddServicePromoCode(ServicePromoCode servicePromoCode)
        {
            ServicePromoCode = Guard.IsNotNull(servicePromoCode, nameof(servicePromoCode));
        }
        public ServicePromoCode ServicePromoCode { get; set; }
    }
}
