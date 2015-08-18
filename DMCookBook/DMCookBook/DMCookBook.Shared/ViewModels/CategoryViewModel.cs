using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DMCookBook
{
    class CategoryViewModel
    {
        Database db;

        public async Task Init()
        {
            db = new Database(Database.GetDBPath());
            await db.Init();
        }
    }
}
