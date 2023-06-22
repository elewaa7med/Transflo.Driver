using System.Data;
using System.Data.SqlClient;
using Transflo.Entities;
using TransfloDriver.DAL.DataProviders;

namespace TransfloDriver.DAL.Repositories.Entities
{
    public class DriverRepository : IDriverRepository
    {
        private readonly string TableName = "DriverDB";
        public ICollection<Driver> GetAll()
        {
            var query = $"select * from {TableName} ORDER BY FirstName,LastName";
            DataTable dataTable = DataProvider.SelectDataTable(query);
            return MapDriverDataListToCollectionModel(dataTable);
        }

        public Driver GetById(Guid Id)
        {
            var query = $"select * from {TableName} where Id = '{Id}'";
            DataRow dataRow = DataProvider.SelectDataRaw(query);
            return MapDriverDataToModel(dataRow);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            if (Guid.Empty == Id)
            {
                return false;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = $"delete from {TableName} where id=@Id";
            cmd.Parameters.AddWithValue("@Id", Id);

            int Result = await DataProvider.ExcuteQueryAsync(cmd);
            return Result > 0;
        }

        public async Task<bool> AddAsync(Driver entity)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = $@"insert into {TableName} (id, FirstName, LastName, Email, PhoneNumber) values (@Id,@firstName,@lastName,@email,@PhoneNumber)";
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@firstName", entity.FirstName??"");
            cmd.Parameters.AddWithValue("@lastName", entity.LastName??"");
            cmd.Parameters.AddWithValue("@email", entity.Email??"");
            cmd.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber??"");
            int Result = await DataProvider.ExcuteQueryAsync(cmd);
            return Result > 0;

        }

        public async Task<bool> EditAsync(Guid Id,Driver entity)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = $@"update {TableName} SET FirstName =@firstName, LastName=@lastName, Email=@email, PhoneNumber=@PhoneNumber Where id = @id";
            cmd.Parameters.AddWithValue("@firstName", entity.FirstName??"");
            cmd.Parameters.AddWithValue("@lastName", entity.LastName??"");
            cmd.Parameters.AddWithValue("@email", entity.Email??"");
            cmd.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber??"");
            cmd.Parameters.AddWithValue("@id", Id);

            int Result = await DataProvider.ExcuteQueryAsync(cmd);
            return Result > 0;
        }

        public void InserBulk(DataTable DriverDataTable)
        {
             DataProvider.InserBulkData(DriverDataTable);
        }

        private Driver MapDriverDataToModel(DataRow dataRow)
        {
            if (dataRow == null)
            {
                return null;
            }

            return new Driver
            {
                Id = dataRow.Field<Guid>("Id"),
                FirstName = dataRow.Field<string>("FirstName"),
                LastName = dataRow.Field<string>("LastName"),
                Email = dataRow.Field<string>("Email"),
                PhoneNumber = dataRow.Field<string>("PhoneNumber")
            };
        }
        private ICollection<Driver> MapDriverDataListToCollectionModel(DataTable dt)
        {
            if (dt == null)
            {
                return null;
            }
            return dt.AsEnumerable().Select(x => MapDriverDataToModel(x)).ToList();
        }

    }
}