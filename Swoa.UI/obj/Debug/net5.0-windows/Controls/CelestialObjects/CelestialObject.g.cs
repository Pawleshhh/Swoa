﻿#pragma checksum "..\..\..\..\..\Controls\CelestialObjects\CelestialObject.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2D43B40F13CF750C8AB6DF68693D71BF0EE3B898"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Core;
using Microsoft.Xaml.Behaviors.Input;
using Microsoft.Xaml.Behaviors.Layout;
using Microsoft.Xaml.Behaviors.Media;
using Swoa.UI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Swoa.UI {
    
    
    /// <summary>
    /// CelestialObject
    /// </summary>
    public partial class CelestialObject : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\..\..\Controls\CelestialObjects\CelestialObject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle hitboxEllipse;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\Controls\CelestialObjects\CelestialObject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse selectedEllipse;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\..\Controls\CelestialObjects\CelestialObject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ScaleTransform scaleTransformSelectedEllipse;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\..\Controls\CelestialObjects\CelestialObject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse celestialObjectEllipse;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Swoa.UI;component/controls/celestialobjects/celestialobject.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Controls\CelestialObjects\CelestialObject.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.hitboxEllipse = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 2:
            this.selectedEllipse = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 3:
            this.scaleTransformSelectedEllipse = ((System.Windows.Media.ScaleTransform)(target));
            return;
            case 4:
            this.celestialObjectEllipse = ((System.Windows.Shapes.Ellipse)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

