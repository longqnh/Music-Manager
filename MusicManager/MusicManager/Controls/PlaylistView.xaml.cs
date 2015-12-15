using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private short _SelectedSong;
        public MainWindow Main { get; set; }
        #endregion

        #region Events

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
            return this._PlayingList[x];
        }
        public void SongX_Selected(short x)
        {
            //Load info
            try
            {
                this.Main.TrackInfo.LoadInfo(_PlayingList[x]);
                //song ready to play
                Player.SelectedSong(_PlayingList[x].Path);
                //change color of the selected song to default
                ((PlayingSong)pnPlayList.Children[_SelectedSong]).DeSelected();
                //update new selected song
                _SelectedSong = x;
                ((PlayingSong)pnPlayList.Children[_SelectedSong]).Selected();
            } 
            catch  (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        #endregion

          


    }
}
