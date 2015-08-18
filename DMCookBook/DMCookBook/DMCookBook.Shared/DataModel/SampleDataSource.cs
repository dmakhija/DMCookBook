using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The data model defined by this file serves as a representative example of a strongly-typed
// model.  The property names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs. If using this model, you might improve app 
// responsiveness by initiating the data loading task in the code behind for App.xaml when the app 
// is first launched.

namespace DMCookBook.Data
{
    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class SampleDataItem
    {
        public int Id { get; private set; }
        public int Category_Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Ingredients { get; private set; }
        public string Method { get; private set; }
        public string Image { get; private set; }
        public int Rating { get; private set; }
        public int PreparationTime { get; private set; }
        public List<string> IngredientsList { get; private set; }        

        public SampleDataItem(int Id, int categoryId, String name, String description, string ingredients, String method, String imagePath, int rating, int prepTime)
        {
            this.Id = Id;
            this.Category_Id = categoryId;
            this.Name = name;
            this.Description = description;
            this.Ingredients = ingredients;
            this.Method = method;
            this.Image = "Assets/images/" + imagePath;
            this.Rating = rating;
            this.PreparationTime = prepTime;
            this.IngredientsList = ingredients.Split('\n').ToList();          

        }        

        public override string ToString()
        {
            return this.Name;
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class SampleDataGroup
    {
       public SampleDataGroup(int Id, string name)
        {
            this.Id = Id;
            this.Name = name;
            this.Items = new ObservableCollection<SampleDataItem>();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public ObservableCollection<SampleDataItem> Items { get; private set; }

        public override string ToString()
        {
            return this.Name;
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a Recipe.sqlite database
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class SampleDataSource
    {
        private static SampleDataSource _sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataGroup> _groups = new ObservableCollection<SampleDataGroup>();

        public ObservableCollection<SampleDataGroup> Groups
        {
            get { return this._groups; }
        }

        public static async Task<IEnumerable<SampleDataGroup>> GetGroupsAsync()
        {
            await _sampleDataSource.GetSampleDataAsync();

            return _sampleDataSource.Groups;
        }

        public static async Task<SampleDataGroup> GetGroupAsync(int categoryId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.Where((group) => group.Id.Equals(categoryId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<SampleDataItem> GetItemAsync(int recipeId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items).Where((item) => item.Id.Equals(recipeId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        
        private async Task GetSampleDataAsync()
        {
            if (this._groups.Count != 0)
                return;

            Database db = new Database(Database.GetDBPath());
            await db.Init();

            List<Category> lCategory = new List<Category>();

            List<Recipe> lRecipe = new List<Recipe>();

            //get collection of all categories
            AsyncTableQuery<Category> category = db.Table<Category>();
            List<Category> lCategories = await category.ToListAsync();

            for (int i = 0; i < lCategories.Count; i++)
            {
                Category c = lCategories[i];
                SampleDataGroup group = new SampleDataGroup(c.Id, c.Name);

                //get collection of all recipes for this particular category
                AsyncTableQuery<Recipe> recipe = db.Table<Recipe>().Where(a => a.Category_Id == c.Id);
                List<Recipe> lRecipes = await recipe.ToListAsync();
                lRecipes=lRecipes.OrderByDescending(a=>a.Rating).ToList();
                //iterate through each recipe in this category
                for (int k = 0; k < lRecipes.Count; k++)
                {
                    Recipe r = lRecipes[k];                    
                    group.Items.Add(new SampleDataItem(r.Id, r.Category_Id, r.Name, r.Description, r.Ingredients, r.Method, r.Image, r.Rating, r.PreparationTime));                    
                }

                this.Groups.Add(group);


            }

            /*
            Uri dataUri = new Uri("ms-appx:///DataModel/SampleData.json");

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Groups"].GetArray();

            foreach (JsonValue groupValue in jsonArray)
            {
                JsonObject groupObject = groupValue.GetObject();
                SampleDataGroup group = new SampleDataGroup(groupObject["UniqueId"].GetString(),
                                                            groupObject["Title"].GetString(),
                                                            groupObject["Subtitle"].GetString(),
                                                            groupObject["ImagePath"].GetString(),
                                                            groupObject["Description"].GetString());

                foreach (JsonValue itemValue in groupObject["Items"].GetArray())
                {
                    JsonObject itemObject = itemValue.GetObject();
                    group.Items.Add(new SampleDataItem(itemObject["UniqueId"].GetString(),
                                                       itemObject["Title"].GetString(),
                                                       itemObject["Subtitle"].GetString(),
                                                       itemObject["ImagePath"].GetString(),
                                                       itemObject["Description"].GetString(),
                                                       itemObject["Content"].GetString()));
                }
                this.Groups.Add(group);
            }*/
        }
    }
}