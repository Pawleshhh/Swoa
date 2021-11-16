﻿using System;
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

        string Format { get; set; }

    }

    public interface ICelestialObjectInfoViewModel<T> : ICelestialObjectInfoViewModel
    {
        new T Value { get; }
    }
}
