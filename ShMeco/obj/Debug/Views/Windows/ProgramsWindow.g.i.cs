﻿#pragma checksum "..\..\..\..\Views\Windows\ProgramsWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BB93212074BF2B6BDD529B10D76AB59F3096AD5B80C2B4F1E6183FB59B0F324D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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


namespace Exhibition.Views.Windows {
    
    
    /// <summary>
    /// ProgramsWindow
    /// </summary>
    public partial class ProgramsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnClose;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DgPrograms;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnCreate;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnChange;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnDelete;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GrProgramCreate;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LblProgramName;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DgSteps;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnAddStep;
        
        #line default
        #line hidden
        
        
        #line 188 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnDeleteStep;
        
        #line default
        #line hidden
        
        
        #line 194 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnSave;
        
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
            System.Uri resourceLocater = new System.Uri("/Exhibition;component/views/windows/programswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
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
            
            #line 20 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.UIElement_OnMouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BtnClose = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
            this.BtnClose.Click += new System.Windows.RoutedEventHandler(this.BtnClose_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DgPrograms = ((System.Windows.Controls.DataGrid)(target));
            
            #line 62 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
            this.DgPrograms.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DgPrograms_OnSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BtnCreate = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
            this.BtnCreate.Click += new System.Windows.RoutedEventHandler(this.BtnCreate_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnChange = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
            this.BtnChange.Click += new System.Windows.RoutedEventHandler(this.BtnChange_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BtnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 92 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
            this.BtnDelete.Click += new System.Windows.RoutedEventHandler(this.BtnChange_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.GrProgramCreate = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.LblProgramName = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.DgSteps = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            this.BtnAddStep = ((System.Windows.Controls.Button)(target));
            
            #line 184 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
            this.BtnAddStep.Click += new System.Windows.RoutedEventHandler(this.BtnAddStep_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.BtnDeleteStep = ((System.Windows.Controls.Button)(target));
            
            #line 190 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
            this.BtnDeleteStep.Click += new System.Windows.RoutedEventHandler(this.BtnDeleteStep_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.BtnSave = ((System.Windows.Controls.Button)(target));
            
            #line 196 "..\..\..\..\Views\Windows\ProgramsWindow.xaml"
            this.BtnSave.Click += new System.Windows.RoutedEventHandler(this.BtnSave_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

