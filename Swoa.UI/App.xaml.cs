using Swoa.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CelestialObjects;
using Astronomy.Units;

namespace Swoa.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private SwoaManagerViewModel swoaManagerVM;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var swoaManagerModel = new SwoaManager();
            swoaManagerVM = new SwoaManagerViewModel(swoaManagerModel);

            var random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                double alt = random.Next(0, 91);
                double az = random.Next(0, 360);

                var horizonCoords = new HorizonCoordinates(alt, az);

                swoaManagerModel.CelestialObjectManager.Add(new PlanetObject()
                {
                    HorizontalCoordinates = horizonCoords
                });
            }

            MainWindow = new MainWindow();
            MainWindow.DataContext = swoaManagerVM;
            MainWindow.Show();
        }

    }
}
