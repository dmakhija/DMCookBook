using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMCookBook
{
    [Table("Category")]
    class Category
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
