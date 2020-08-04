using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/userdashboard")]
    [ApiController]

    public class UserDashboardApiController : BaseApiController
    {
        private IUserDashboardService _service = null;
        private IAuthenticationService<int> _authService = null;

        public UserDashboardApiController(IUserDashboardService service
            , ILogger<UserDashboardApiController> logger
            , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        [HttpGet("favoritevendors")]
        public ActionResult<ItemsResponse<UserDashShoppingCart>> GetTopFavoriteVendors()
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<UserDashFavoriteVendors> list = _service.GetTopFavoriteVendors();

                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found");
                }
                else
                {
                    response = new ItemsResponse<UserDashFavoriteVendors> { Items = list };
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

        [HttpGet("shoppingcart")]
        public ActionResult<ItemsResponse<UserDashShoppingCart>> GetTopShoppingCartItems()
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<UserDashShoppingCart> list = _service.GetTopShoppingCartItems();

                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found");
                }
                else
                {
                    response = new ItemsResponse<UserDashShoppingCart> { Items = list };
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


        [HttpGet("products")]
        public ActionResult<ItemsResponse<UserDashEvent>> GetTopProducts()
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<UserDashProduct> list = _service.GetTopProducts();

                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found");
                }
                else
                {
                    response = new ItemsResponse<UserDashProduct> { Items = list };
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

        [HttpGet("events")]
        public ActionResult<ItemsResponse<UserDashEvent>> GetTopEvents()
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<UserDashEvent> list = _service.GetTopEvents();

                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found");
                }
                else
                {
                    response = new ItemsResponse<UserDashEvent> { Items = list };
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
    }
}
