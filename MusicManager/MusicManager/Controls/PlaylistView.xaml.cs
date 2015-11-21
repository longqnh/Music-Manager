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

namespace MusicManager.Controls
{
    /// <summary>
    /// Interaction logic for PlaylistView.xaml
    /// </summary>
    public partial class PlaylistView : UserControl
    {
        public PlaylistView()
        {
            InitializeComponent();
        }

        #region Properies
        private int i = 1;

        private MainWindow _Main;

        public MainWindow Main
        {
            set { _Main = value; }
        }
        #endregion

        #region fortesting
        //int a = 1;
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    PlayingSong song = new PlayingSong();
        //    //song.tbNo.Text = i.ToString() + ".";
        //    //song.tbTitle.Text += i.ToString();
        //    pnPlayList.Children.Add(song);
        //    i++;
        //}
        #endregion

        #region Methods
        public void AddtoList(Track track)
        {
            PlayingSong song = new PlayingSong(this._Main);
            //add in info
            song.tbNo.Text = i.ToString() + ".";

            song.tbTitle.Text = track.tbTitle.Text;
            //song.tbArtist.Text = track.tbartist;
            //song.tbAlbum.Text - track.tbalbum;
            song.tbDur.Text = track.tbDur.Text;

            song.Checklength(); // if the title is too long, set tooltip
            //add song to playlist
            pnPlayList.Children.Add(song);
            i++;
        }
        #endregion

        #region Events
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Music starting......");
        }
        #endregion
    }
}
