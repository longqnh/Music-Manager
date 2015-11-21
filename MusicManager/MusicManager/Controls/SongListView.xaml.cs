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

using HundredMilesSoftware.UltraID3Lib;

namespace MusicManager.Controls
{
    /// <summary>
    /// Interaction logic for SongListView.xaml
    /// </summary>
    public partial class SongListView : System.Windows.Controls.UserControl
    {
        public SongListView()
        {
            InitializeComponent();
            PassCoverPath();
        }

        #region Propertis
        private bool _isAlbumSelected = true;
        private Color _coSelect = Color.FromRgb(255,249,249);
        private Color _coDeSelect = Color.FromRgb(81, 79, 84);


        private MainWindow _Main;
        public MainWindow Main
        {
            set { _Main = value; }
        }
        #endregion

        #region for testing
        int i = 1;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Track track = new Track(_Main);
            track.tbNo.Text = i + ".";
            track.tbTitle.Text += i;
            ctArtistView.pnTrackList.Children.Add(track);
            i++;
            //UltraID3 Tag = new UltraID3();
            //Tag.Read("C:/Users/Administrator/Music/松浦亜弥/20-(20.10.2004)-Single-Watarasebashi/1 - Watarasebashi.mp3");
            //MessageBox.Show(Tag.Album);
        }
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
        private void PassCoverPath()
        {
            // for testing
            string[] AlbumPath = {"/Albums/1.jpg","/Albums/2.jpg","/Albums/3.jpg","/Albums/4.jpg",
                                    "/Albums/5.jpg","/Albums/6.jpg"};
            ctAlbumView.RecCoverPath(AlbumPath);
        }

        #endregion
    }
}
