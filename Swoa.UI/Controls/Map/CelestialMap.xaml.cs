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

        private readonly double maxOrigin = 0.75;
        private readonly double minOrigin = 0.25;

        private bool mouseCaptured;

        private Point startPoint;

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

        private Point rotateStart;

        private void PreRotateCelestialMap()
        {
            //rotateStart = Mouse.GetPosition(mainGrid);
            Mouse.Capture(mainGrid);
        }

        private Line line;

        private void RotateCelestialMap()
        {
            if (Mouse.Captured == mainGrid && !mouseCaptured)
                mouseCaptured = true;
            else if (Mouse.Captured == mainGrid)
            {
                // Get the current mouse position relative to the control
                Point currentLocation = Mouse.GetPosition(outerGrid);

                // We want to rotate around the center of the map, not the top corner
                Point mapCenter = new Point((this.ActualWidth) / 2, (this.ActualHeight) / 2);
                //Point mapCenter = mainGrid.TranslatePoint(new Point(-(this.ActualWidth / 2), -(this.ActualHeight / 2)), outerGrid);

                // Calculate an angle
                double radians = Math.Atan((currentLocation.Y - mapCenter.Y) /
                                           (currentLocation.X - mapCenter.X));
                Angle = (radians * 180 / Math.PI);

                // Apply a 180 degree shift when X is negative so that we can rotate
                // all of the way around
                if (currentLocation.X - mapCenter.X < 0)
                {
                    Angle += 180;
                }

                DirectionLabelsAngle = -180.0 - Angle;

                //if (line != null)
                //    mainGrid.Children.Remove(line);

                //line = new Line();
                //line.StrokeThickness = 2;
                //line.Stroke = Brushes.Red;

                //line.X1 = currentLocation.X;
                //line.Y1 = currentLocation.Y;
                //line.X2 = mapCenter.X;
                //line.Y2 = mapCenter.Y;

                //mainGrid.Children.Add(line);
            }
        }

        private void PreMoveCelestialMap()
        {
            startPoint = Mouse.GetPosition(this);
            Mouse.Capture(mainGrid);
        }

        private void MoveCelestialMap()
        {
            if (ScaleFactor == MinScaleFactor)
                return;

            (bool, double) setForLimit(double origin, double currentPos, double prevPos)
            {
                bool result = true;

                if (origin <= minOrigin || origin >= maxOrigin)
                {
                    if (origin <= minOrigin)
                    {
                        origin = minOrigin;
                        if (currentPos <= prevPos)
                            result = false;
                    }
                    else
                    {
                        origin = maxOrigin;
                        if (currentPos >= prevPos)
                            result = false;
                    }
                }

                return (result, origin);
            }

            (double, double) setNewPositionAndOrigin(double currentPos, double origin, double length)
            {
                double newPos = currentPos;

                if (newPos < 0)
                {
                    origin = 0.5 - (-newPos / length);
                }
                else
                {
                    origin = (newPos / length) + 0.5;
                }

                return (newPos, origin);
            }

            if (Mouse.Captured == mainGrid)
            {
                var mousePosition = Mouse.GetPosition(this);

                var (xorigin, yorigin) = (this.RenderTransformOrigin.X, this.RenderTransformOrigin.Y);

                var (xPos, yPos) = (XPosition + (startPoint.X - mousePosition.X), YPosition + (startPoint.Y - mousePosition.Y));

                bool xMove, yMove;
                (xMove, xorigin) = setForLimit(xorigin, xPos, XPosition);
                (yMove, yorigin) = setForLimit(yorigin, yPos, YPosition);

                if (xMove)
                    (XPosition, xorigin) = setNewPositionAndOrigin(xPos, xorigin, ActualWidth);

                if (yMove)
                    (YPosition, yorigin) = setNewPositionAndOrigin(yPos, yorigin, ActualHeight);

                this.RenderTransformOrigin = new Point(xorigin, yorigin);

                Origin = $"{xorigin}, {yorigin}";
            }
        }

        private void CelestialMap_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ZoomCelestialMap(e.Delta);
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PreMoveCelestialMap();
        }

        private void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            PreRotateCelestialMap();
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            mouseCaptured = false;
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
