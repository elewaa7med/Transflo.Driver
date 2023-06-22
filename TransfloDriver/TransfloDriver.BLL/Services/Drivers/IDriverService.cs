using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransflowDriver.DTO.ViewModels.Driver;
using TransflowDriver.DTO.ViewModels.Helper;

namespace TransfloDriver.BLL.Services.Drivers
{
    public interface IDriverService
    {
        ListResponseViewModel<DriverViewModel> GetDriversList();
        DriverViewModel GetDriverById(Guid driverId);
        Task<DriverViewModel> AddDriver(AddDriverModel driver);
        void Add100RundomDriver();
        Task<DriverViewModel> EditDriver(Guid Id ,UpdateDriverModel driver);
        Task<bool?> RemoveDriver(Guid driverId);
        string GetAlphDriverById(Guid driverId);

    }
}
