﻿#pragma checksum "..\..\..\..\UDP\Client\UC_Client.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A75118820987E481A84C11E58C57DA6A40C1E751"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
using UDPExample;


namespace UDPExample {
    
    
    /// <summary>
    /// UC_Client
    /// </summary>
    public partial class UC_Client : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\..\UDP\Client\UC_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_ip;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\UDP\Client\UC_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_PortNumber;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\UDP\Client\UC_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Connection_Client;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\UDP\Client\UC_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_Connection_Client;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\UDP\Client\UC_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datagrid_ReciveClient;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\..\UDP\Client\UC_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_Senddata;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\..\UDP\Client\UC_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_SendClient;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\UDP\Client\UC_Client.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datagrid_SendClient;
        
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
            System.Uri resourceLocater = new System.Uri("/UDPExample;component/udp/client/uc_client.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UDP\Client\UC_Client.xaml"
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
            this.txt_ip = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txt_PortNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.btn_Connection_Client = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\..\UDP\Client\UC_Client.xaml"
            this.btn_Connection_Client.Click += new System.Windows.RoutedEventHandler(this.Btn_ConnectionClient_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lbl_Connection_Client = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.datagrid_ReciveClient = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.txt_Senddata = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.Btn_SendClient = ((System.Windows.Controls.Button)(target));
            
            #line 98 "..\..\..\..\UDP\Client\UC_Client.xaml"
            this.Btn_SendClient.Click += new System.Windows.RoutedEventHandler(this.Btn_SendClient_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.datagrid_SendClient = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

