﻿#pragma checksum "..\..\..\Controls\AlbumView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A8EDF2537E4D86FD904473499A9B1469"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MusicManager.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace MusicManager.Controls {
    
    
    /// <summary>
    /// AlbumView
    /// </summary>
    public partial class AlbumView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Controls\AlbumView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgCurAlbum;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Controls\AlbumView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgPreAlbum;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Controls\AlbumView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgNextAlbum;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Controls\AlbumView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbAlbumName;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Controls\AlbumView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbArtist;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Controls\AlbumView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbYear;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Controls\AlbumView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer srvTrackList;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Controls\AlbumView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel pnTrackList;
        
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
            System.Uri resourceLocater = new System.Uri("/MusicManager;component/controls/albumview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\AlbumView.xaml"
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
            
            #line 9 "..\..\..\Controls\AlbumView.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.test_MouseWheel);
            
            #line default
            #line hidden
            return;
            case 2:
            this.imgCurAlbum = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.imgPreAlbum = ((System.Windows.Controls.Image)(target));
            
            #line 11 "..\..\..\Controls\AlbumView.xaml"
            this.imgPreAlbum.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.imgPreAlbum_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.imgNextAlbum = ((System.Windows.Controls.Image)(target));
            
            #line 12 "..\..\..\Controls\AlbumView.xaml"
            this.imgNextAlbum.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.imgNextAlbum_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbAlbumName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.tbArtist = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.tbYear = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.srvTrackList = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 9:
            this.pnTrackList = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

