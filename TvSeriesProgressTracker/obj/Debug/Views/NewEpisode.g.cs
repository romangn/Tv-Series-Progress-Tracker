﻿#pragma checksum "..\..\..\Views\NewEpisode.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "03A9FF7B3AA5FB0D16B83A4DC0A39B3E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using TvSeriesProgressTracker;


namespace TvSeriesProgressTracker {
    
    
    /// <summary>
    /// NewEpisode
    /// </summary>
    public partial class NewEpisode : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\Views\NewEpisode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_episodeData;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Views\NewEpisode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox titlebox;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\Views\NewEpisode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox seasonBox;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\Views\NewEpisode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox episodeBox;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\Views\NewEpisode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock idElem;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\Views\NewEpisode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TvSeriesProgressTracker;component/views/newepisode.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\NewEpisode.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.grid_episodeData = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 50 "..\..\..\Views\NewEpisode.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.Confirm_CanExecute);
            
            #line default
            #line hidden
            
            #line 51 "..\..\..\Views\NewEpisode.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Confirm_Executed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.titlebox = ((System.Windows.Controls.TextBox)(target));
            
            #line 59 "..\..\..\Views\NewEpisode.xaml"
            this.titlebox.AddHandler(System.Windows.Controls.Validation.ErrorEvent, new System.EventHandler<System.Windows.Controls.ValidationErrorEventArgs>(this.Validation_Error));
            
            #line default
            #line hidden
            return;
            case 4:
            this.seasonBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 74 "..\..\..\Views\NewEpisode.xaml"
            this.seasonBox.AddHandler(System.Windows.Controls.Validation.ErrorEvent, new System.EventHandler<System.Windows.Controls.ValidationErrorEventArgs>(this.Validation_Error));
            
            #line default
            #line hidden
            return;
            case 5:
            this.episodeBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 89 "..\..\..\Views\NewEpisode.xaml"
            this.episodeBox.AddHandler(System.Windows.Controls.Validation.ErrorEvent, new System.EventHandler<System.Windows.Controls.ValidationErrorEventArgs>(this.Validation_Error));
            
            #line default
            #line hidden
            return;
            case 6:
            this.idElem = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.button = ((System.Windows.Controls.Button)(target));
            
            #line 104 "..\..\..\Views\NewEpisode.xaml"
            this.button.Click += new System.Windows.RoutedEventHandler(this.save_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

