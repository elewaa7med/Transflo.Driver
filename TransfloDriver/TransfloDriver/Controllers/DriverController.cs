using Microsoft.AspNetCore.Mvc;
using TransfloDriver.BLL.Services.Drivers;
using TransflowDriver.DTO.ViewModels.Driver;
using TransflowDriver.DTO.ViewModels.Helper;

namespace TransfloDriver.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DriverController : Controller
    {

        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public IActionResult GetDriverbyId(Guid Id)
        {
            try
            {
                DriverViewModel viewModel = _driverService.GetDriverById(Id);
                if (viewModel == null)
                {
                    return HandelNotFound();
                }
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return HandelInternalServerErrorMessage(ex.Message);
            }
        }


        [HttpGet]
        public IActionResult GetSortedAlphDriverList()
        {
            try
            {
                ListResponseViewModel<DriverViewModel> viewModel = _driverService.GetDriversList();
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return HandelInternalServerErrorMessage(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDriver([FromBody] AddDriverModel driverModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return HandelBadRequest();
                }
                DriverViewModel model = await _driverService.AddDriver(driverModel);
                if (model == null)
                    return HandelInternalServerErrorMessage("Problem In Server Try again later");
                return Ok(model);
            }
            catch (Exception ex)
            {
                return HandelInternalServerErrorMessage(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add100RundomDriver()
        {
            try
            {
                _driverService.Add100RundomDriver();
                return Ok(Json("100 random driver added successfully"));
            }
            catch (Exception ex)
            {
                return HandelInternalServerErrorMessage(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDriver(Guid Id)
        {
            try
            {
                bool? isDeleted = await _driverService.RemoveDriver(Id);
                if (isDeleted == null)
                   return HandelNotFound();
                if(!isDeleted == true)
                    return HandelInternalServerErrorMessage("Problem in server try again later");
                
                return Ok(Json("Driver deleted successfully"));

            }
            catch (Exception ex)
            {
                return HandelInternalServerErrorMessage(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> EditDriver(Guid Id, [FromBody] UpdateDriverModel driverModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return HandelBadRequest();
                }
                
                DriverViewModel model =_driverService.GetDriverById(Id);
                if(model == null)
                    return HandelNotFound();
                
                model = await _driverService.EditDriver(Id, driverModel);
                if(model == null)
                    return HandelInternalServerErrorMessage("Problem in server try again later");
                return Ok(model);
            }
            catch (Exception ex)
            {
                return HandelInternalServerErrorMessage(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAlphDriverNamebyId(Guid Id)
        {
            try
            {
                string result = _driverService.GetAlphDriverById(Id);
                if (result == null)
                    return HandelNotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandelInternalServerErrorMessage(ex.Message);
            }
        }

        private ObjectResult HandelInternalServerErrorMessage(string ex)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            errorResponse.StatusCode = StatusCodes.Status500InternalServerError;
            errorResponse.Message = ex;
            return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }

        private ObjectResult HandelNotFound()
        {
            ErrorResponse errorResponse = new ErrorResponse();
            errorResponse.StatusCode = StatusCodes.Status404NotFound;
            errorResponse.Message = "Can't find driver with this Id";
            return NotFound(errorResponse);
        }

        private ObjectResult HandelBadRequest()
        {
            ErrorResponse errorResponse = new ErrorResponse();
            errorResponse.StatusCode = StatusCodes.Status400BadRequest;
            errorResponse.Message = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorResponse);
        }

    }
}
