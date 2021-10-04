using System;
using System.Collections.Generic;
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
    /// Interaction logic for CelestialObject.xaml
    /// </summary>
    public partial class CelestialObject : UserControl
    {
        public CelestialObject()
        {
            InitializeComponent();
        }



        public double XPos
        {
            get { return (double)GetValue(XPosProperty); }
            set { SetValue(XPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XPosProperty =
            DependencyProperty.Register("XPos", typeof(double), typeof(CelestialObject), new PropertyMetadata(0.0));



        public double YPos
        {
            get { return (double)GetValue(YPosProperty); }
            set { SetValue(YPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for YPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YPosProperty =
            DependencyProperty.Register("YPos", typeof(double), typeof(CelestialObject), new PropertyMetadata(0.0));



        public ControlTemplate CelestialObjectTemplate
        {
            get { return (ControlTemplate)GetValue(CelestialObjectTemplateProperty); }
            set { SetValue(CelestialObjectTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CelestialObjectTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CelestialObjectTemplateProperty =
            DependencyProperty.Register("CelestialObjectTemplate", typeof(ControlTemplate), typeof(CelestialObject), new PropertyMetadata(null));

    }
}
