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
using SwoaDatabaseAPI;

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
            var swoaManagerModel = new SwoaManager(SwoaSqliteDb.SwoaSqliteDbSingleton);
            swoaManagerVM = new SwoaManagerViewModel(swoaManagerModel, new WpfThread());

            //AddRandomObjs(swoaManagerModel);
            //AddDbObjs(swoaManagerModel);

            //swoaManagerModel.CelestialObjectManager.TimeMachine.TimeMachinePlayer.Start();
            swoaManagerModel.CelestialObjectManager.UpdateCurrentMapAsync();
            swoaManagerModel.CelestialObjectManager.WaitForTask();

            MainWindow = new MainWindow();
            MainWindow.DataContext = swoaManagerVM;
            MainWindow.Show();
        }


        private void AddRandomObjs(SwoaManager swoaManager)
        {
            var random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                double alt = random.NextDouble() * 90.0;
                double az = random.NextDouble() * 360.0;

                var horizonCoords = new HorizonCoordinates(alt, az);

                swoaManager.CelestialObjectManager.Add(new PlanetObject()
                {
                    HorizonCoordinates = horizonCoords
                });
            }
        }

        //private void AddDbObjs(SwoaManager swoaManager)
        //{
        //    var celestialObjs = SwoaSqliteDb.SwoaSqliteDbSingleton.GetSwoaDbRecords("SELECT * FROM stars WHERE mag < 7");

        //    foreach(var obj in celestialObjs)
        //    {
        //        swoaManager.CelestialObjectManager.Add(obj);
        //    }
        //}

    }
}
