﻿#pragma checksum "..\..\..\Presentationlager\KundVy.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A3AFDFE89906A506EF118EF1FCA0308972FDB897C81EB8B4C35C962C8BA4A3B7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Bilverkstad.PresentationLager;
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


namespace Bilverkstad.PresentationLager {
    
    
    /// <summary>
    /// KundVy
    /// </summary>
    public partial class KundVy : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\Presentationlager\KundVy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnMinimize;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Presentationlager\KundVy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnClose;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\Presentationlager\KundVy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSearch;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\Presentationlager\KundVy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid KunderDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/Bilverkstad;component/presentationlager/kundvy.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Presentationlager\KundVy.xaml"
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
            
            #line 14 "..\..\..\Presentationlager\KundVy.xaml"
            ((Bilverkstad.PresentationLager.KundVy)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BtnMinimize = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\Presentationlager\KundVy.xaml"
            this.BtnMinimize.Click += new System.Windows.RoutedEventHandler(this.BtnMinimize_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BtnClose = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\..\Presentationlager\KundVy.xaml"
            this.BtnClose.Click += new System.Windows.RoutedEventHandler(this.BtnClose_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtSearch = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.KunderDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

