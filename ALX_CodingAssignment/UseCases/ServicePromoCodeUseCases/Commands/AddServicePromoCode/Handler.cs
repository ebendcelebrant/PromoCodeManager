using ALX_CodingAssignment.Domain.Interfaces;
using ALX_CodingAssignment.Domain.Models;
using ALX_CodingAssignment.Domain.Responders;
using ALX_CodingAssignment.Domain.Validations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Commands.AddServicePromoCode
{
    public class AddServicePromoCodeHandler : ICommandHandler<AddServicePromoCode>
    {
        private readonly IRepository<ServicePromoCode, int> servicePromoCodeRepository;
        private readonly ILogger<AddServicePromoCodeHandler> logger;
        public AddServicePromoCodeHandler(IRepository<ServicePromoCode, int> aServicePromoCodeRepository,
            ILogger<AddServicePromoCodeHandler> aLogger)
        {
            servicePromoCodeRepository = Guard.IsNotNull(aServicePromoCodeRepository, nameof(aServicePromoCodeRepository));
            logger = aLogger;
        }

        private IEnumerable<IResponseMessage> Validate(AddServicePromoCode request)
        {
            Guard.IsNotNull(request.ServicePromoCode, nameof(request.ServicePromoCode));
            Guard.IsNotNull(request.ServicePromoCode.ServiceName, nameof(request.ServicePromoCode.ServiceName));

            return new PremiseValidator()
              .For(request.ServicePromoCode.ServiceName)
                    .TextIsNotEmpty<ServiceNameIsNotSpecified>()
              .For(request.ServicePromoCode.PromoCode)
                    .TextIsNotEmpty<PromoCodeIsNotSpecified>()
                    .TextIsAlphaNumeric<InvalidPromoCode>()
              .GetFailures();
        }
        public Response Handle(AddServicePromoCode command)
        {
            var messages = Validate(command);

            if (messages.HasErrors())
            {
                return new Response(messages);
            }

            servicePromoCodeRepository.Add(new ServicePromoCode()
            {
                Description = command.ServicePromoCode.Description,
                ServiceName = command.ServicePromoCode.ServiceName,
                PromoCode = command.ServicePromoCode.PromoCode,
                DateAdded = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });
            servicePromoCodeRepository.Complete();

            return Response.Success();
        }
    }
}
