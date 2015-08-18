using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMCookBook
{
    [Table("Recipe")]
    class Recipe
    {    
            [SQLite.PrimaryKey, SQLite.AutoIncrement]
            public int Id { get; set; }
            public int Category_Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Ingredients { get; set; }
            public string Method { get; set; }
            public string Image { get; set; }
            public int Rating { get; set; }
            public int PreparationTime { get; set; }
            //public List<string> IngredientsList { get; set; }
        
    }
}
