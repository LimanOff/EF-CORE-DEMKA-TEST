using EFCore.Model.DatabaseModel;
using System.IO;
using System.Reflection;
using System.Windows;

namespace EFCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DatabaseContext DatabaseContext;
        public static string RootFolder;
        protected override void OnStartup(StartupEventArgs e)
        {
            int howMuchGetUpOnDirectory = 4;

            RootFolder = Assembly.GetExecutingAssembly().Location;

            for (int currentDirectoryStage = 0; currentDirectoryStage < howMuchGetUpOnDirectory; currentDirectoryStage++)
            {
                RootFolder = Directory.GetParent(RootFolder).ToString();
            }

            DatabaseContext = new DatabaseContext();
        }
    }

}
