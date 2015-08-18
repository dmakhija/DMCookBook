using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace DMCookBook
{
    class Database: SQLiteAsyncConnection
    {
        static public string GetDBPath()
        {
            //return ApplicationData.Current.LocalFolder.Path + "\\RecipeStore.sqlite";
           
            string newpath = ApplicationData.Current.LocalFolder.Path + "\\RecipeStore.sqlite";
            return newpath;
            
            //database path for Windows store App:
            //C:\Users\ampm\AppData\Local\Packages\bfe78db6-2adb-4346-9813-7b11d957a3a8_6mdm3q2gagn92\LocalState\RecipeStore.sqlite
            
            //database path for Windows Phone App:
            //"C:\\Data\\Users\\DefApps\\AppData\\Local\\Packages\\bdbdd004-72a6-4730-8c9d-bb68a4f35266_6mdm3q2gagn92\\LocalState\\RecipeStore.sqlite";

        }

        public Database(string databasePath)
            : base(databasePath, true)
        {
        }

        //static async void CopyDatabase()
        //{
        //    bool isDatabaseExisting = false;
        //    try
        //    {
        //        StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync("RecipeStore.sqlite");                
        //        isDatabaseExisting = true;

        //         //Uri uri = new Uri("/RecipeStore.sqlite", UriKind.Relative);
        //         //StorageFile mydbfile = await StorageFile.GetFileFromApplicationUriAsync(uri);
        //         //await mydbfile.CopyAndReplaceAsync(storageFile);
        //    }
        //    catch (Exception ex)
        //    {
        //        string err=ex.GetBaseException().ToString();
        //        isDatabaseExisting = false;
        //        Uri uri = new Uri("/RecipeStore.sqlite", UriKind.Relative);
        //    }

        //    if (isDatabaseExisting)
        //    {
        //        // var path = Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "\\RecipeStore.sqlite");
        //        var path = ApplicationData.Current.LocalFolder.Path + "\\RecipeStore.sqlite";
        //        StorageFile dbFile = await StorageFile.GetFileFromPathAsync(path);
        //        Uri uri = new Uri("/RecipeStore.sqlite", UriKind.Relative);
        //        // Uri err = new Uri("ms-appx:///Database/RecipeStore.sqlite");
        //        //var databaseFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Database/RecipeStore.sqlite"));
        //        //StorageFile dbfile = await ApplicationData.Current.LocalFolder.Path + "\\RecipeStore.sqlite";
        //        var databaseFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
        //        await databaseFile.CopyAsync(ApplicationData.Current.LocalFolder);
        //        //await databaseFile.CopyAndReplaceAsync(dbFile);
        //    }
        //}

        //static async void GetFile()
        //{                    
        //    StorageFile oldfile = await ApplicationData.Current.LocalFolder.GetFileAsync("RecipeStore.sqlite");
        //    string oldpath = oldfile.Path;

        //    string newpath = "C:\\Users\\ampm\\AppData\\Local\\Packages\\bfe78db6-2adb-4346-9813-7b11d957a3a8_6mdm3q2gagn92\\LocalState\\RecipeStore.sqlite";
        //    var newfile = await StorageFile.GetFileFromPathAsync(newpath);
        //    string err = newfile.Path;
            
        //}

        public async Task Init()
        {
            await CreateTableAsync<Category>();
            await CreateTableAsync<Recipe>();
        }

    }
}
