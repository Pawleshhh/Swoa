using CelestialObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swoa.ViewModel
{
    public interface ICelestialObjectInfoViewModel : INotifyPropertyChanged
    {

        string Name { get; }
        object Value { get; }
        string ValueStr => this.ToString();

        string Format { get; set; }
        IFormatProvider FormatProvider { get; set; }

        void Update();
    }

    public interface ICelestialObjectInfoViewModel<T> : ICelestialObjectInfoViewModel
    {
        new T Value { get; }
    }
}
