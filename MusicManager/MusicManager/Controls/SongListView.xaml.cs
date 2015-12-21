using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MusicManager.Classes;
using HundredMilesSoftware.UltraID3Lib;

namespace MusicManager.Controls
{
    /// <summary>
    /// Interaction logic for SongListView.xaml
    /// </summary>
    public partial class SongListView : System.Windows.Controls.UserControl
    {
        #region Constructor
        public SongListView()
        {
            InitializeComponent();
        }
        #endregion

        #region Propertis
        private bool _isAlbumSelected = true;
        private Color _coSelect = Color.FromRgb(255,249,249);
        private Color _coDeSelect = Color.FromRgb(81, 79, 84);
        public MainWindow Main { get; set; }
        #endregion

        #region Events
        private void tbAlbum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(!_isAlbumSelected) // if album is not selected, then select it
            {
                tbAlbum.Foreground = new SolidColorBrush(_coSelect);
                tbArtist.Foreground = new SolidColorBrush(_coDeSelect);
                ctAlbumView.Visibility = Visibility.Visible;
                ctArtistView.Visibility = Visibility.Hidden;
                _isAlbumSelected = true;
            }
        }
        private void tbArtist_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_isAlbumSelected) // if album is selected, then select de-select it
            {
                tbAlbum.Foreground = new SolidColorBrush(_coDeSelect);
                tbArtist.Foreground = new SolidColorBrush(_coSelect);                
                ctArtistView.Visibility = Visibility.Visible;
                ctAlbumView.Visibility = Visibility.Hidden;
                _isAlbumSelected = false;
            }
        }

        #endregion

        #region Methods
        
        #endregion
    }
}
