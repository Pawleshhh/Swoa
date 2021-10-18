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

        public double DirectionLabelsAngle
        {
            get { return (double)GetValue(DirectionLabelsAngleProperty); }
            set { SetValue(DirectionLabelsAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DirectionLabelsAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DirectionLabelsAngleProperty =
            DependencyProperty.Register("DirectionLabelsAngle", typeof(double), typeof(CelestialMap), new PropertyMetadata(-180.0));

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
            DependencyProperty.Register("MaxScaleFactor", typeof(double), typeof(CelestialMap), new PropertyMetadata(5.0));

        public double MinScaleFactor
        {
            get { return (double)GetValue(MinScaleFactorProperty); }
            set { SetValue(MinScaleFactorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinScaleFactor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinScaleFactorProperty =
            DependencyProperty.Register("MinScaleFactor", typeof(double), typeof(CelestialMap), new PropertyMetadata(1.0));

        public double XPosition
        {
            get { return (double)GetValue(XPositionProperty); }
            set { SetValue(XPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XPositionProperty =
            DependencyProperty.Register("XPosition", typeof(double), typeof(CelestialMap));

        public double YPosition
        {
            get { return (double)GetValue(YPositionProperty); }
            set { SetValue(YPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for YPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YPositionProperty =
            DependencyProperty.Register("YPosition", typeof(double), typeof(CelestialMap));

        public string Origin
        {
            get { return (string)GetValue(OriginProperty); }
            set { SetValue(OriginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Origin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OriginProperty =
            DependencyProperty.Register("Origin", typeof(string), typeof(CelestialMap));

        public CelestialMap()
        {
            InitializeComponent();

            MouseLeftButtonDown += OnMouseLeftButtonDown;
            MouseRightButtonDown += OnMouseRightButtonDown;
            MouseUp += OnMouseUp;
            MouseMove += OnMouseMove;

            MouseWheel += CelestialMap_MouseWheel;
        }

        private void ResetPosition()
        {
            ScaleFactor = MinScaleFactor;
            mainGrid.RenderTransformOrigin = new Point(0.5, 0.5);
            RenderTransformOrigin = new Point(0.5, 0.5);

            XPosition = YPosition = 0;

            Origin = "0.0, 0.0";
        }

        private void ZoomCelestialMap(double d)
        {
            if (ScaleFactor >= MaxScaleFactor && d > 0)
            {
                ScaleFactor = MaxScaleFactor;
                return;
            }

            if (ScaleFactor < MinScaleFactor || (ScaleFactor == MinScaleFactor && d < 0))
            {
                ResetPosition();
                return;
            }

            var currentPosition = Mouse.GetPosition(this);

            //RenderTransformOrigin = new Point(currentPosition.X / ActualHeight, currentPosition.Y / ActualWidth);

            if (d > 0)
                ScaleFactor += ScaleFactorStep;
            else
                ScaleFactor -= ScaleFactorStep;
        }

        private void RotateCelestialMap()
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

                DirectionLabelsAngle = -180.0 - Angle;
            }
        }

        private bool firstMove;
        private Point startPoint;

        private void MoveCelestialMap()
        {
            if (ScaleFactor == MinScaleFactor)
                return;

            if (Mouse.Captured == mainGrid)
            {
                var mousePosition = Mouse.GetPosition(this);

                var (xorigin, yorigin) = (RenderTransformOrigin.X, RenderTransformOrigin.Y);

                var (xPos, yPos) = (XPosition + (startPoint.X - mousePosition.X), YPosition + (startPoint.Y - mousePosition.Y));

                bool xMove = true, yMove = true;

                if (xorigin <= 0.25 || xorigin >= 0.75)
                {
                    if (xorigin <= 0.25)
                    {
                        xorigin = 0.25;
                        if (xPos <= XPosition)
                            xMove = false;
                    }
                    else
                    {
                        xorigin = 0.75;
                        if (xPos >= XPosition)
                            xMove = false;
                    }

                    //if (xMove)
                    //    startPoint = Mouse.GetPosition(this);
                }

                if (yorigin <= 0.25 || yorigin >= 0.75)
                {
                    if (yorigin <= 0.25)
                    {
                        yorigin = 0.25;
                        if (yPos <= YPosition)
                            yMove = false;
                    }
                    else
                    {
                        yorigin = 0.75;
                        if (yPos >= YPosition)
                            yMove = false;
                    }

                    //if (yMove)
                    //    startPoint = Mouse.GetPosition(this);
                }


                if (xMove)
                {
                    XPosition = xPos;

                    if (XPosition < 0)
                    {
                        xorigin = 0.5 - (-XPosition / ActualWidth);
                    }
                    else
                    {
                        xorigin = (XPosition / ActualWidth) + 0.5;
                    }
                }

                if (yMove)
                {
                    YPosition = yPos;

                    if (YPosition < 0)
                    {
                        yorigin = 0.5 - (-YPosition / ActualHeight);
                    }
                    else
                    {
                        yorigin = (YPosition / ActualHeight) + 0.5;
                    }
                }

                RenderTransformOrigin = new Point(xorigin, yorigin);

                Origin = $"{xorigin}, {yorigin}";
                //if (firstMove)
                //{
                //    firstMove = false;
                //    return;
                //}

                //var (rx, ry) = (RenderTransformOrigin.X, RenderTransformOrigin.Y);
                //var maxX = (ActualWidth * (1.0 - rx));
                //var minX = -(ActualWidth * rx);
                //var maxY = (ActualHeight * (1.0 - ry));
                //var minY = -(ActualHeight * ry);

                //if (XPosition > maxX || XPosition < minX)
                //{
                //    if (XPosition > maxX)
                //        XPosition = maxX;
                //    else
                //        XPosition = minX;
                //}
                //else

                //if (YPosition > maxY || YPosition < minY)
                //{
                //    if (YPosition > maxY)
                //        YPosition = maxY;
                //    else
                //        YPosition = minY;
                //}
                //else

            }
        }

        private void CelestialMap_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ZoomCelestialMap(e.Delta);
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = Mouse.GetPosition(this);
            Mouse.Capture(mainGrid);

            //RenderTransformOrigin = new Point(XPosition / ActualHeight, YPosition / ActualWidth);
        }

        private void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(mainGrid);
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            mouseCaptured = false;
            firstMove = true;

        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed && e.LeftButton != MouseButtonState.Pressed)
                RotateCelestialMap();
            else if (e.LeftButton == MouseButtonState.Pressed && e.RightButton != MouseButtonState.Pressed)
                MoveCelestialMap();
        }

    }
}
