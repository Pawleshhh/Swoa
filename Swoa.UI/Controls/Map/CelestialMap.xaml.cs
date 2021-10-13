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

        private bool mouseCaptured;

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Angle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(CelestialMap));

        public double ScaleFactorStep
        {
            get { return (double)GetValue(ScaleFactorStepProperty); }
            set { SetValue(ScaleFactorStepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScaleFactorStep.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleFactorStepProperty =
            DependencyProperty.Register("ScaleFactorStep", typeof(double), typeof(CelestialMap), new PropertyMetadata(0.1));

        public double ScaleFactor
        {
            get { return (double)GetValue(ScaleFactorProperty); }
            set { SetValue(ScaleFactorProperty, value); }
        }

        public static readonly DependencyProperty ScaleFactorProperty = DependencyProperty.Register("ScaleFactor", typeof(double), 
            typeof(CelestialMap), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double MaxScaleFactor
        {
            get { return (double)GetValue(MaxScaleFactorProperty); }
            set { SetValue(MaxScaleFactorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxScaleFactor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxScaleFactorProperty =
            DependencyProperty.Register("MaxScaleFactor", typeof(double), typeof(CelestialMap), new PropertyMetadata(3.0));

        public double MinScaleFactor
        {
            get { return (double)GetValue(MinScaleFactorProperty); }
            set { SetValue(MinScaleFactorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinScaleFactor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinScaleFactorProperty =
            DependencyProperty.Register("MinScaleFactor", typeof(double), typeof(CelestialMap), new PropertyMetadata(1.0));

        public CelestialMap()
        {
            InitializeComponent();

            MouseLeftButtonDown += OnMouseLeftButtonDown;
            MouseRightButtonDown += CelestialMap_MouseRightButtonDown;
            MouseUp += OnMouseUp;
            MouseMove += OnMouseMove;

            MouseWheel += CelestialMap_MouseWheel;
        }

        private void ZoomCelestialMap(double d)
        {
            if (ScaleFactor >= MaxScaleFactor && d > 0)
            {
                ScaleFactor = MaxScaleFactor;
                return;
            }

            if (ScaleFactor < MinScaleFactor)
            {
                ScaleFactor = MinScaleFactor;
                RenderTransformOrigin = new Point(0.5, 0.5);
                return;
            }


            if (ScaleFactor == MinScaleFactor && d < 0)
                return;

            var currentPosition = Mouse.GetPosition(this);

            RenderTransformOrigin = new Point(currentPosition.X / ActualHeight, currentPosition.Y / ActualWidth);

            if (d > 0)
                ScaleFactor += ScaleFactorStep;
            else
                ScaleFactor -= ScaleFactorStep;
        }

        private void CelestialMap_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ZoomCelestialMap(e.Delta);
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void CelestialMap_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(mainGrid);
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            mouseCaptured = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.Captured == mainGrid && !mouseCaptured)
                mouseCaptured = true;
            else if (Mouse.Captured == mainGrid)
            {
                // Get the current mouse position relative to the control
                Point currentLocation = Mouse.GetPosition(this);

                // We want to rotate around the center of the map, not the top corner
                Point mapCenter = new Point(this.ActualHeight / 2, this.ActualWidth / 2);

                // Calculate an angle
                double radians = Math.Atan((currentLocation.Y - mapCenter.Y) /
                                           (currentLocation.X - mapCenter.X));
                Angle = (radians * 180 / Math.PI) - 180.0;

                // Apply a 180 degree shift when X is negative so that we can rotate
                // all of the way around
                if (currentLocation.X - mapCenter.X < 0)
                {
                    Angle += 180;
                }
            }
        }

    }
}
