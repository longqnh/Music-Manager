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

namespace MusicManager.Controls
{
    /// <summary>
    /// Interaction logic for PlaylistView.xaml
    /// </summary>
    public partial class PlaylistView : UserControl
    {
        #region Constructor
        public PlaylistView()
        {
            InitializeComponent();
        }
        #endregion 

        #region Properies
        private int i = 1;
        private List<Song> _PlayingList = new List<Song>();
        public MainWindow Main { get; set; }
        #endregion

        #region Methods
        public void AddtoList(Song song) // add from songlist(track)
        {
            PlayingSong songtoshow = new PlayingSong(this.Main, (short)(i - 1));
            //add in info
            songtoshow.tbNo.Text = i.ToString() + "."; songtoshow.Checklength(); // if the title is too long, set tooltip
            songtoshow.tbTitle.Text = song.Title;
            songtoshow.tbArtist.Text = song.Artist;
            songtoshow.tbAlbum.Text = song.Album;
            songtoshow.tbDur.Text = "0" + song.Dur.Minutes.ToString() + ":" + song.Dur.Seconds;

            //add song to playlist
            this._PlayingList.Add(song);
            pnPlayList.Children.Add(songtoshow);
            i++;
        }
        public Song SongX_inList(short x)
        {
            int tmp = this._PlayingList.Count;
            return this._PlayingList[x];
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
