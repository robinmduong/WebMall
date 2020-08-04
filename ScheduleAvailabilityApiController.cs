using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.ScheduleAvailability;
using Sabio.Services;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using SendGrid;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/availability")]
    [ApiController]

    public class ScheduleAvailabilityApiController : BaseApiController
    {
        private IScheduleAvailabilityService _service = null;
        private IAuthenticationService<int> _authService = null;

        public ScheduleAvailabilityApiController(IScheduleAvailabilityService service
            , ILogger<ScheduleAvailabilityApiController> logger
            , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }


        [HttpGet("current")]
        public ActionResult<ItemResponse<ScheduleAvailability>> GetByCreatedBy(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                
                Paged<ScheduleAvailability> page = _service.GetByCreatedBy(pageIndex, pageSize, userId);

                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource Not Found");
                }
                else
                {
                    response = new ItemResponse<Paged<ScheduleAvailability>> { Item = page };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }

            return StatusCode(code, response);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<SuccessResponse> Delete(int id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.Delete(id);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }

        [HttpGet]
        public ActionResult<ItemsResponse<ScheduleAvailability>> GetAll()
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<ScheduleAvailability> list = _service.GetAll();

                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<ScheduleAvailability> { Items = list };
                }
            }

            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }

            return StatusCode(code, response);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<ScheduleAvailability>> Get(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                ScheduleAvailability scheduleAvailability = _service.Get(id);

                if (scheduleAvailability == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<ScheduleAvailability> { Item = scheduleAvailability };
                }
            }

            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: ${ex.Message}");
            }

            return StatusCode(iCode, response);
        }

        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<ScheduleAvailability>>> Paginate(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Paged<ScheduleAvailability> page = _service.Paginate(pageIndex, pageSize);
                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource Not Found");
                }
                else
                {
                    response = new ItemResponse<Paged<ScheduleAvailability>> { Item = page };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                Logger.LogError(ex.ToString());
            }

            return StatusCode(code, response);
        }

        [HttpPut("{id:int}")]
        public ActionResult<SuccessResponse> Update(ScheduleAvailabilityUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.Update(model, userId);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> Add(ScheduleAvailabilityAddRequest model)
        {
            ObjectResult result = null;

            try
            {
                int userId = _authService.GetCurrentUserId();

                int id = _service.Add(model, userId);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };


                result = Created201(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }

            return result;
        }

    }
}
