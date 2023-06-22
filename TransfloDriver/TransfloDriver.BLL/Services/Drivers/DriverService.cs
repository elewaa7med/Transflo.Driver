using AutoMapper;
using System.Data;
using Transflo.Entities;
using TransfloDriver.DAL.Repositories.Entities;
using TransflowDriver.DTO.ViewModels.Driver;
using TransflowDriver.DTO.ViewModels.Helper;

namespace TransfloDriver.BLL.Services.Drivers
{
    public class DriverService : IDriverService
    {
        public readonly IDriverRepository _driverRepository;
        public readonly IMapper _mapper;
        public DriverService(IDriverRepository driverRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public void Add100RundomDriver()
        {
            DataTable DriverDataTable = new DataTable("Driver");

            DataColumn Id = new DataColumn("Id");
            Id.DataType = typeof(Guid);
            DriverDataTable.Columns.Add(Id);
            DataColumn FirstName = new DataColumn("FirstName");
            DriverDataTable.Columns.Add(FirstName);
            DataColumn LastName = new DataColumn("LastName");
            DriverDataTable.Columns.Add(LastName);
            DataColumn Email = new DataColumn("Email");
            DriverDataTable.Columns.Add(Email);
            DataColumn PhoneNumber = new DataColumn("PhoneNumber");
            DriverDataTable.Columns.Add(PhoneNumber);
            for (int i = 0; i < 100; i++)
            {
                DriverDataTable.Rows.Add(Guid.NewGuid(), GenerateRandomString(), GenerateRandomString(), GenerateRandomString() + "@hotmail.com", GenerateRandomPhone());
            }
            _driverRepository.InserBulk(DriverDataTable);
        }

        public async Task<DriverViewModel> AddDriver(AddDriverModel driver)
        {
            Driver entity = _mapper.Map<Driver>(driver);
            bool result = await _driverRepository.AddAsync(entity);
            if (result) { return _mapper.Map<DriverViewModel>(entity); }
            return null;
        }

        public async Task<DriverViewModel> EditDriver(Guid Id, UpdateDriverModel driver)
        {
            Driver entity = _mapper.Map<Driver>(driver);
            bool result = await _driverRepository.EditAsync(Id, entity);
            if (result) {
                entity.Id = Id;
                return _mapper.Map<DriverViewModel>(entity);
            }
            return null;
        }

        public DriverViewModel GetDriverById(Guid driverId)
        {
            Driver entity = _driverRepository.GetById(driverId);
            if (entity == null) 
                return null;
            return _mapper.Map<DriverViewModel>(entity);
        }

        public string GetAlphDriverById(Guid driverId)
        {
            Driver entity = _driverRepository.GetById(driverId);
            if (entity == null)
                return null;
            string firstName = new string(entity.FirstName.OrderBy(x => x).ToArray());
            if (string.IsNullOrEmpty(entity.LastName))
            {
                string lastName = new string(entity.LastName.OrderBy(x => x).ToArray());
                return string.Concat(firstName, " ", lastName);
            }
            return firstName;
        }

        public ListResponseViewModel<DriverViewModel> GetDriversList()
        {
            ICollection<Driver> entity = _driverRepository.GetAll();
            ListResponseViewModel<DriverViewModel> listResponseViewModel = new ListResponseViewModel<DriverViewModel>();
            listResponseViewModel.Data = _mapper.Map<ICollection<DriverViewModel>>(entity);
            listResponseViewModel.ItemCount = entity.Count;
            return listResponseViewModel;
        }

        public async Task<bool?> RemoveDriver(Guid driverId)
        {
            Driver entity = _driverRepository.GetById(driverId);
            if (entity == null)
                return null;
            bool result = await _driverRepository.DeleteAsync(driverId);
            return result;
        }

        public string GenerateRandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvcwxyz";
            return new string(
                Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GenerateRandomPhone()
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(
                Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}