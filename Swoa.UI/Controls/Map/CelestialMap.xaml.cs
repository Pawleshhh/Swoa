using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Swoa.UI
{
    /// <summary>
    /// Interaction logic for CelestialMap.xaml
    /// </summary>
    public partial class CelestialMap : UserControl
    {
        public CelestialMap()
        {
            InitializeComponent();

            ObservableCollection<Node> nodes = new ObservableCollection<Node>();

            //var random = new Random();
            //for (int i = 0; i < 10; i++)
            //{
            //    var (alt, az) = (random.Next(0, 90), random.Next(0, 360));
            //    var (x, y) = FromHorizonCoords(alt, az);
            //    nodes.Add(new Node(x, y, alt, az));
            //}

            var (alt, az) = (90, 0);
            var (x, y) = FromHorizonCoords(alt, az);
            nodes.Add(new Node(x, y, alt, az));

            mainItemsControl.ItemsSource = nodes;
        }

        public (double x, double y) FromHorizonCoords(double alt, double az)
        {
            double dAlt = 180 - (alt * 2);

            double x = dAlt * Math.Cos(az * Math.PI / 180.0);
            double y = dAlt * Math.Sin(az * Math.PI / 180.0);

            return (x + 180, y + 180);
        }


        class Node
        {

            public Node(double x, double y, double alt, double az)
            {
                XPos = x;
                YPos = y;
                Alt = alt;
                Az = az;
            }

            public double Alt { get; set; }
            public double Az { get; set; }

            public double XPos { get; set; }
            public double YPos { get; set; }

            public override string ToString()
            {
                return $"({(int)Alt}, {(int)Az})";
            }
        }
    }
}
