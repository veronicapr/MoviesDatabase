﻿#pragma checksum "..\..\AddMovies.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "76AB96D15D4A2193BA3E30234A1C4FB3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
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


namespace Projecto_BD_WPF {
    
    
    /// <summary>
    /// AddMovies
    /// </summary>
    public partial class AddMovies : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox movie_id;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox duration;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox rating;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox description;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox age_restriction;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox add_movie_actors;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox add_movie_writers;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox directors_combo_box;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button add_movie_button;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancel_add_movie_button;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox genre_listbox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox studios_combo_box;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox addNewGenres;
        
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
            System.Uri resourceLocater = new System.Uri("/Projecto_BD_WPF;component/addmovies.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddMovies.xaml"
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
            this.movie_id = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.duration = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.rating = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.description = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.age_restriction = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.add_movie_actors = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            this.add_movie_writers = ((System.Windows.Controls.ListBox)(target));
            return;
            case 8:
            this.directors_combo_box = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.add_movie_button = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\AddMovies.xaml"
            this.add_movie_button.Click += new System.Windows.RoutedEventHandler(this.add_movie_button_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.cancel_add_movie_button = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\AddMovies.xaml"
            this.cancel_add_movie_button.Click += new System.Windows.RoutedEventHandler(this.cancel_add_movie_button_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.genre_listbox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 12:
            this.studios_combo_box = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 13:
            this.addNewGenres = ((System.Windows.Controls.TextBox)(target));
            return;
            case 14:
            
            #line 45 "..\..\AddMovies.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

