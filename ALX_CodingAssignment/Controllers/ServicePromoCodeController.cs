using ALX_CodingAssignment.Domain.Interfaces;
using ALX_CodingAssignment.Domain.Validations;
using ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Commands.ActivateServicePromoCode;
using ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Commands.AddServicePromoCode;
using ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ALX_CodingAssignment.Helpers;

namespace ALX_CodingAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePromoCodesController : BaseController
    {
        /*
         * For simplicity purposes, the handlers will be injected directly. In a real world scenario
         * a mediator to automatically dispatch the request to the correct handler will be implemented
         */

        private readonly ICommandHandler<ActivateServicePromoCode> activateServicePromoCodeHandler;
        private readonly IQueryHandler<GetServicePromoCodeByName, ServicePromoCodePayload> getServicePromoCodeByNameHandler;
        private readonly ICommandHandler<AddServicePromoCode> addServicePromoCodeHandler;
        private readonly IQueryHandler<GetServicePromoCode, ServicePromoCodePayload> getServicePromoCodeHandler;
        private readonly IHttpContextAccessor httpContextAccessor;


        public ServicePromoCodesController(
        ICommandHandler<ActivateServicePromoCode> activateServicePromoCodeHandler,
        IQueryHandler<GetServicePromoCodeByName, ServicePromoCodePayload> getServicePromoCodeByNameHandler,
        ICommandHandler<AddServicePromoCode> addServicePromoCodeHandler,
        IQueryHandler<GetServicePromoCode, ServicePromoCodePayload> getServicePromoCodeHandler,
        IHttpContextAccessor aHttpContextAccessor)
        {
            this.activateServicePromoCodeHandler = Guard.IsNotNull(activateServicePromoCodeHandler, 
                nameof(activateServicePromoCodeHandler));
            this.getServicePromoCodeByNameHandler = Guard.IsNotNull(getServicePromoCodeByNameHandler, 
                nameof(getServicePromoCodeByNameHandler));
            this.addServicePromoCodeHandler = Guard.IsNotNull(addServicePromoCodeHandler, nameof(addServicePromoCodeHandler));
            this.getServicePromoCodeHandler = Guard.IsNotNull(getServicePromoCodeHandler, nameof(getServicePromoCodeHandler));
            httpContextAccessor = aHttpContextAccessor;
        }


        [HttpPost]
        [Route("ActivateServicePromoCode")]
        public IActionResult ActivateServicePromoCode([FromBody] ActivateServicePromoCode request)
        {
            var response = activateServicePromoCodeHandler.Handle(request);

            return BuildHttpResponse(response);
        }

        [HttpGet]
        [Route("GetServicePromoCodes")]
        public IActionResult GetServicePromoCodes([FromQuery] GetServicePromoCode request)
        {
            var response = getServicePromoCodeHandler.Handle(request);

            return Ok(response.Payload);
        }

        [HttpPost]
        [Route("AddServicePromoCode")]
        public IActionResult AddServicePromoCode([FromBody] AddServicePromoCode request)
        {
            var response = addServicePromoCodeHandler.Handle(request);

            return BuildHttpResponse(response);
        }
        [HttpGet]
        [Route("GetServicePromoCodes/Name")]
        public IActionResult GetServicePromoCodesByName([FromQuery] GetServicePromoCodeByName request)
        {
            var response = getServicePromoCodeByNameHandler.Handle(request);

            return Ok(response.Payload);
        }
    }
}
