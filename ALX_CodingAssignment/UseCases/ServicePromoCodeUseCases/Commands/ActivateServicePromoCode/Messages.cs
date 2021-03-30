using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALX_CodingAssignment.Domain.Responders;

namespace ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Commands.ActivateServicePromoCode
{
    public class InvalidServicePromoCodeId : AResponseErrorMessage
    {
        public override string Message => nameof(InvalidServicePromoCodeId);
    }
    public class ServicePromoCodeIsAlreadyActivated : AResponseErrorMessage
    {
        public override string Message => nameof(ServicePromoCodeIsAlreadyActivated);
    }
}
