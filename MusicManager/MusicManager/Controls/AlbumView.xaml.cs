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
    /// Interaction logic for AlbumView.xaml
    /// </summary>
    public partial class AlbumView : UserControl
    {
        #region Constructor
        public AlbumView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int _Cur = 0;
        private List<Album> _AlbumList;
        private MainWindow _Main;
        #endregion

        #region Events
        private void imgPreAlbum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _Cur--;
            imgCurAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
            this.CreateSongListofAlbum(_Cur);
            imgNextAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur + 1].CoverPath, UriKind.Relative));
            if (_Cur - 1 < 0) // this current album is the last album
                imgPreAlbum.Source = null;
            else
                imgPreAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur - 1].CoverPath, UriKind.Relative));
        }
        private void imgNextAlbum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            imgPreAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
            _Cur++;
            imgCurAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
            this.CreateSongListofAlbum(_Cur);
            if (_Cur == _AlbumList.Count - 1) // this current album is the last album
                imgNextAlbum.Source = null;
            else
                imgNextAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur + 1].CoverPath, UriKind.Relative));
        }
        #endregion

        #region Methods
        public void ReceiveAlbumList(List<Album> list, MainWindow main)
        {
            this._AlbumList = list;
            this._Main = main;
            #region For Testing
            //cover path
            string[] Path = {"/Albums/1.jpg","/Albums/2.jpg","/Albums/3.jpg","/Albums/4.jpg",
                                    "/Albums/5.jpg","/Albums/6.jpg"};
            for (int i = 0; i < 6; i++)
                this._AlbumList[i].CoverPath = Path[i];
            // set image on form
            imgCurAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
            this.CreateSongListofAlbum(_Cur);
            imgNextAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur+1].CoverPath, UriKind.Relative));
            #endregion
        }
        private void CreateSongListofAlbum(int index)
        {
            this.pnTrackList.Children.Clear(); // first, remove all the song contenting
            //then add songlist of _AlbumList[index]
            int i;
            for (i = 1; i < this._AlbumList[index].TrackList.Count; i++)
            {
                Song tmp = this._AlbumList[index].TrackList[i];

                Track track = new Track(_Main, (short)index, (short)i);
                track.tbNo.Text = i + ".";
             
                track.tbTitle.Text = tmp.Title;
                track.tbDur.Text = "0" + tmp.Dur.Minutes.ToString() + ":" + tmp.Dur.Seconds;
                this.pnTrackList.Children.Add(track);
            }

        }
        public Song SongX_OfAlbum(short index, short x) // return song x of album[index]
        {
            return this._AlbumList[index].TrackList[x];
        }
        #endregion
    }
}
