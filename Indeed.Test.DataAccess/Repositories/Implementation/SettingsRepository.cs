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
        public override Task<Settings> Create(Settings item)
        {
            throw new NotImplementedException();
        }

        public async override Task<Settings> Get(int id)
        {
            return Context.Settings;
        }

        public override Task<IEnumerable<Settings>> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<int> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public async override Task<Settings> Update(Settings item)
        {
            Context.Settings = item;
            SaveContext();
            return item;
        }
    }
}
