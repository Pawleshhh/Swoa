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

        //public Brush CelestialObjectColor
        //{
        //    get { return (Brush)GetValue(CelestialObjectColorProperty); }
        //    set { SetValue(CelestialObjectColorProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for CelestialObjectColor.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CelestialObjectColorProperty =
        //    DependencyProperty.Register("CelestialObjectColor", typeof(Brush), typeof(CelestialObject), new PropertyMetadata(Brushes.Yellow));

        //public ICommand Select
        //{
        //    get { return (ICommand)GetValue(SelectProperty); }
        //    set { SetValue(SelectProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Select.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty SelectProperty =
        //    DependencyProperty.Register("Select", typeof(ICommand), typeof(CelestialObject), new PropertyMetadata(null));

        public CelestialObject()
        {
            InitializeComponent();
        }
    }
}
