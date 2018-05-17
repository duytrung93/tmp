using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Quanlyphuongtien
{
    public class DataAccess
    {
        private static SQLiteAsyncConnection database { get; set; }
        public DataAccess()
        {
            database = new SQLiteAsyncConnection(DependencyService.Get<iService>().GetLocalFilePath("Quanlyphuongtien.db3"));
            database.CreateTableAsync<UserLogin>().Wait();
        }
        public Task<UserLogin> GetUserLogin()
        {
            database.CreateTableAsync<UserLogin>().Wait();
            return database.Table<UserLogin>().FirstOrDefaultAsync();
        }
        public Task<int> SaveUserLogin(UserLogin iInfo)
        {
            database.CreateTableAsync<UserLogin>().Wait();
            return database.InsertAsync(iInfo);
        }

        public Task<int> DeleteUserLogin()
        {
            database.CreateTableAsync<UserLogin>().Wait();

            return database.DropTableAsync<UserLogin>();
        }

    }
}
