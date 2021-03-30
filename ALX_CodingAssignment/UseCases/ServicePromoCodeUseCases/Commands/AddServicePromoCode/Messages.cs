using ALX_CodingAssignment.Domain.Responders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Commands.AddServicePromoCode
{
    public class ServiceNameIsNotSpecified : AResponseErrorMessage
    {
        public override string Message => nameof(ServiceNameIsNotSpecified);
    }
    public class InvalidServiceName : AResponseErrorMessage
    {
        public override string Message => nameof(InvalidServiceName);
    }
    public class PromoCodeIsNotSpecified : AResponseErrorMessage
    {
        public override string Message => nameof(PromoCodeIsNotSpecified);
    }
    public class InvalidPromoCode : AResponseErrorMessage
    {
        public override string Message => nameof(InvalidPromoCode);
    }
}
