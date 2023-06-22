using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transflo.Entities;

namespace TransfloDriver.DAL.Repositories.Entities
{
    public interface IDriverRepository
    {
        Driver GetById(Guid id);
        ICollection<Driver> GetAll();
        Task<bool> EditAsync(Guid Id ,Driver entity);
        Task<bool> AddAsync(Driver entity);
        Task<bool> DeleteAsync(Guid driverId);
        void InserBulk(DataTable DriverDataTable);
    }
}
