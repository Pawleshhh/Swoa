using Swoa.ViewModel;
using System;
using System.Collections;
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

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Angle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(CelestialMap));

        public CelestialMap()
        {
            InitializeComponent();

            MouseLeftButtonDown += OnMouseLeftButtonDown;
            MouseUp += OnMouseUp;
            MouseMove += OnMouseMove;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(mainGrid);
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.Captured == mainGrid)
            {
                // Get the current mouse position relative to the control
                Point currentLocation = Mouse.GetPosition(this);

                // We want to rotate around the center of the map, not the top corner
                Point mapCenter = new Point(this.ActualHeight / 2, this.ActualWidth / 2);

                // Calculate an angle
                double radians = Math.Atan((currentLocation.Y - mapCenter.Y) /
                                           (currentLocation.X - mapCenter.X));
                this.Angle = (radians * 180 / Math.PI) - 180.0;

                // Apply a 180 degree shift when X is negative so that we can rotate
                // all of the way around
                if (currentLocation.X - mapCenter.X < 0)
                {
                    this.Angle += 180;
                }
            }
        }

    }
}
