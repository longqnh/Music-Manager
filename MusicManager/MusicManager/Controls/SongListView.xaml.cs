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
using System.Windows.Forms;
using System.IO;

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
       
        //int i = 1; // for testing
        private void Button_Click(object sender, RoutedEventArgs e)
        {
        //    Track track = new Track();
        //    track.No.Text = i + ".";
        //    track.Title.Text += i;
        //    ctAlbumView.pnTrackList.Children.Add(track);
        //    i++;
        }

        int count = 1;
        List<string> music_list;
        string media = string.Empty;
        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            music_list = new List<string>();
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                media = fbd.SelectedPath;
                DirectoryInfo dir = new DirectoryInfo(media);
                foreach (FileInfo file in dir.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    if (file.Extension == ".wmv" || file.Extension == ".mp3" || file.Extension == ".mp4" || file.Extension == ".flac" || file.Extension == ".wma" || file.Extension == ".m4a")
                    {
                        music_list.Add(file.Name);
                    }
                }

                if (music_list != null)
                {
                    foreach (string ms_file in music_list)
                    {
                        Track track = new Track();
                        track.No.Text = count + ".";
                        track.Title.Text += ms_file;
                        ctAlbumView.pnTrackList.Children.Add(track);
                        count++;
                    }
                }
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
