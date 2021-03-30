using ALX_CodingAssignment.Domain.Interfaces;
using ALX_CodingAssignment.Domain.Models;
using ALX_CodingAssignment.Domain.Responders;
using ALX_CodingAssignment.Domain.Validations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Commands.ActivateServicePromoCode
{
    public class ActivateServicePromoCodeHandler : ICommandHandler<ActivateServicePromoCode>
    {
        private readonly IRepository<UserPromoCode, int> userPromoCodeRepository;
        private readonly ILogger<ActivateServicePromoCodeHandler> logger;

        public ActivateServicePromoCodeHandler(IRepository<UserPromoCode, int> aUserPromoCodeRepository,
            ILogger<ActivateServicePromoCodeHandler> aLogger)
        {
            userPromoCodeRepository = Guard.IsNotNull(aUserPromoCodeRepository, nameof(aUserPromoCodeRepository));
            logger = aLogger;
        }

        private IEnumerable<IResponseMessage> Validate(ActivateServicePromoCode request)
        {
            Guard.IsNotNull(request.UserId, "request.UserId");
            Guard.IsNotNull(request.ServicePromoCodeId, "request.ServicePromoCodeId");

            var validationResponse =  new PremiseValidator()
              .For(request.ServicePromoCodeId.ToString())
              .TextIsNumeric<InvalidServicePromoCodeId>()
              .GetFailures();

            bool HasUserActivatedServicePromoCode = userPromoCodeRepository.GetAll()
                                            .Where(u => u.UserId == request.UserId
                                                && u.ServicePromoCodeId == request.ServicePromoCodeId)
                                            .Count() > 0;
            
            if (HasUserActivatedServicePromoCode)
                validationResponse.Concat(new ResponseMessagesBuilder().AddMessage<ServicePromoCodeIsAlreadyActivated>().Build());

            return validationResponse;
        }

        public Response Handle(ActivateServicePromoCode command)
        {
            var messages = Validate(command);

            if (messages.HasErrors())
            {
                return new Response(messages);
            }

            userPromoCodeRepository.Add(new UserPromoCode()
            {
                ServicePromoCodeId = command.ServicePromoCodeId,
                UserId = command.UserId,
                DateAdded = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });
            userPromoCodeRepository.Complete();

            return Response.Success();
        }
    }
}
