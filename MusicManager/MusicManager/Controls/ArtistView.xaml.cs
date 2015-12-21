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
    /// Interaction logic for ArtistView.xaml
    /// </summary>
    public partial class ArtistView : UserControl
    {
        #region Constructor
        public ArtistView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int _Cur = 0;
        private List<Artist> _ArtistList;
        private MainWindow _Main;
        #endregion

        #region Events
        private void imgPreArtist_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //set current album avatar
            _Cur--;
            if (_ArtistList[_Cur].Cover == null)
                imgCurArtist.Source = new BitmapImage(new Uri(_ArtistList[_Cur].CoverPath, UriKind.Relative));
            else imgCurArtist.Source = _ArtistList[_Cur].Cover;

            /////
            tbArtistName.Text = _ArtistList[_Cur].Name; // set artist name to show
            this.CreateSongList_ofArtist(_Cur);

            //set next artist avatar
            if (_ArtistList[_Cur + 1].Cover == null)
                imgNextArtist.Source = new BitmapImage(new Uri(_ArtistList[_Cur + 1].CoverPath, UriKind.Relative));
            else imgNextArtist.Source = _ArtistList[_Cur + 1].Cover;

            
            //set previous artist avatar
            if (_Cur - 1 < 0) // this current Artist is the last Artist
                imgPreArtist.Source = null;
            else
                if (_ArtistList[_Cur - 1].Cover == null)
                    imgPreArtist.Source = new BitmapImage(new Uri(_ArtistList[_Cur - 1].CoverPath, UriKind.Relative));
                else
                    imgPreArtist.Source = _ArtistList[_Cur - 1].Cover;
        }
        private void imgNextArtist_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //set previous artist avatar
            if (_ArtistList[_Cur].Cover == null) 
                imgPreArtist.Source = new BitmapImage(new Uri(_ArtistList[_Cur].CoverPath, UriKind.Relative));
            else 
                imgPreArtist.Source = _ArtistList[_Cur].Cover;

            //set current artist avatar
            _Cur++;
            if (_ArtistList[_Cur].Cover == null)
                imgCurArtist.Source = new BitmapImage(new Uri(_ArtistList[_Cur].CoverPath, UriKind.Relative));
            else
                imgCurArtist.Source = _ArtistList[_Cur].Cover;

            /////
            tbArtistName.Text = _ArtistList[_Cur].Name; // set artist name to show
            this.CreateSongList_ofArtist(_Cur);

            //set next artist avatar
            if (_Cur == _ArtistList.Count - 1) // this current Artist is the last Artist
                imgNextArtist.Source = null;
            else
            {
                if (_ArtistList[_Cur + 1].Cover == null)
                    imgNextArtist.Source = new BitmapImage(new Uri(_ArtistList[_Cur + 1].CoverPath, UriKind.Relative));
                else
                    imgNextArtist.Source = _ArtistList[_Cur + 1].Cover;
            }
        }
        private void imgCurArtist_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) // add artist avatar path
        {

        }
        #endregion

        #region Methods
        private void CreateSongList_ofArtist(int index)
        {
            this.pnTrackList.Children.Clear(); // first, remove all the song contenting

            //then add song list of _ArtistList[index]
            short i;
            for (i = 0; i < this._ArtistList[index].SongList.Count; i++)
            {
                Song tmp = this._ArtistList[index].SongList[i];

                Track track = new Track(_Main, (short)index, i, true);
                track.tbNo.Text = i + ".";

                track.tbTitle.Text = tmp.Title;
                track.tbDur.Text = "0" + tmp.Dur.Minutes.ToString() + ":" + tmp.Dur.Seconds;
                this.pnTrackList.Children.Add(track);
            }
        }
        public Song SongX_OfAlbum(short index, short x) // return song x of artist[index]
        {
            return this._ArtistList[index].SongList[x];
        }
        public void ReceiveArtistList(List<Artist> list, MainWindow main)
        {
            this._ArtistList = list;
            this._Main = main;
        }
        #endregion
    }
}
