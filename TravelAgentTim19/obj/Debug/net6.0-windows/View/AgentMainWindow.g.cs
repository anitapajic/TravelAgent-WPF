﻿#pragma checksum "..\..\..\..\View\AgentMainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "67F9A65E350F3E43B68C671A16EBE5030D1069FC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
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
using TravelAgentTim19.Service;


namespace TravelAgentTim19.View {
    
    
    /// <summary>
    /// AgentMainWindow
    /// </summary>
    public partial class AgentMainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 42 "..\..\..\..\View\AgentMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid1;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\View\AgentMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TripsGrid;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\View\AgentMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton ToggleButtonTrip;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\View\AgentMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid AttractionGrid;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\..\View\AgentMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton ToggleButtonAttraction;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\..\View\AgentMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid AccomodationGrid;
        
        #line default
        #line hidden
        
        
        #line 171 "..\..\..\..\View\AgentMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton ToggleButtonAccomodation;
        
        #line default
        #line hidden
        
        
        #line 187 "..\..\..\..\View\AgentMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid RestaurantsGrid;
        
        #line default
        #line hidden
        
        
        #line 218 "..\..\..\..\View\AgentMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton ToggleButtonRestaurant;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TravelAgentTim19;component/view/agentmainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\AgentMainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 13 "..\..\..\..\View\AgentMainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.PutovanjaCRUD_CanExecute);
            
            #line default
            #line hidden
            
            #line 13 "..\..\..\..\View\AgentMainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.PutovanjaCRUD_Executed);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 30 "..\..\..\..\View\AgentMainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.TripItem_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 36 "..\..\..\..\View\AgentMainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AttractionItem_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 37 "..\..\..\..\View\AgentMainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AccomodationItem_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 38 "..\..\..\..\View\AgentMainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RestaurantItem_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 40 "..\..\..\..\View\AgentMainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Logout_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.grid1 = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.TripsGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.ToggleButtonTrip = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 75 "..\..\..\..\View\AgentMainWindow.xaml"
            this.ToggleButtonTrip.Click += new System.Windows.RoutedEventHandler(this.ToggleButtonTrip_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.AttractionGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 11:
            this.ToggleButtonAttraction = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 123 "..\..\..\..\View\AgentMainWindow.xaml"
            this.ToggleButtonAttraction.Click += new System.Windows.RoutedEventHandler(this.ToggleButtonAttraction_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.AccomodationGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 13:
            this.ToggleButtonAccomodation = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 171 "..\..\..\..\View\AgentMainWindow.xaml"
            this.ToggleButtonAccomodation.Click += new System.Windows.RoutedEventHandler(this.ToggleButtonAccomodation_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.RestaurantsGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 15:
            this.ToggleButtonRestaurant = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 218 "..\..\..\..\View\AgentMainWindow.xaml"
            this.ToggleButtonRestaurant.Click += new System.Windows.RoutedEventHandler(this.ToggleButtonRestaurant_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

