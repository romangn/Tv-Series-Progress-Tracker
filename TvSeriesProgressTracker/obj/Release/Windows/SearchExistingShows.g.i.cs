﻿#pragma checksum "..\..\..\Windows\SearchExistingShows.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D27A7633153E48C821152A6ABE837167"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ShowProgressRecorder;
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


namespace ShowProgressRecorder {
    
    
    /// <summary>
    /// SearchExistingShows
    /// </summary>
    public partial class SearchExistingShows : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\Windows\SearchExistingShows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView searchShows;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Windows\SearchExistingShows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchBox;
        
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
            System.Uri resourceLocater = new System.Uri("/ShowProgressRecorder;component/windows/searchexistingshows.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\SearchExistingShows.xaml"
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
            this.searchShows = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            
            #line 29 "..\..\..\Windows\SearchExistingShows.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.onClick_Edit);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 32 "..\..\..\Windows\SearchExistingShows.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.onClick_Remove);
            
            #line default
            #line hidden
            return;
            case 4:
            this.searchBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 59 "..\..\..\Windows\SearchExistingShows.xaml"
            this.searchBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.searchBox_KeyDown);
            
            #line default
            #line hidden
            
            #line 60 "..\..\..\Windows\SearchExistingShows.xaml"
            this.searchBox.GotFocus += new System.Windows.RoutedEventHandler(this.searchBox_GotFocus);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

