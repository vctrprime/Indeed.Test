using Indeed.Test.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Indeed.Test.DataAccess.Repositories.Implementation
{
    public class SettingsRepository : Repository<Settings>
    {
        public SettingsRepository(string jsonDataFileName) : base(jsonDataFileName)
        {
        }
        public override Task<Settings> CreateAsync(Settings item)
        {
            throw new NotImplementedException();
        }

        public override Settings Get(int id)
        {
            return Context.Settings;
        }

        public override IEnumerable<Settings> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<int> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async override Task<Settings> UpdateAsync(Settings item)
        {
            Context.Settings = item;
            await SaveContext();
            return item;
        }
    }
}
