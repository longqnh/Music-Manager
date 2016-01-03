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

        #region Methods
        private void CreateSongListofArtist(int index)
        {
            this.pnTrackList.Children.Clear();

            short i;
            for (i = 0; i < this._ArtistList[index].Songlist.Count; i++)
            {
                Song tmp = this._ArtistList[index].Songlist[i];

                Track track = new Track(_Main, (short)index, i, true);
                track.tbNo.Text = i + ".";

                track.tbTitle.Text = tmp.Title;
                track.tbDur.Text = "0" + tmp.Dur.Minutes.ToString() + ":" + tmp.Dur.Seconds;
                this.pnTrackList.Children.Add(track);
            }
        }
        public void ReceiveArtistList(List<Artist> list, MainWindow main)
        {
            this._Main = main;
            this._Cur = 0;
            this._ArtistList = list;

            for (int i = 0; i < this._ArtistList.Count; i++)
                this._ArtistList[i].coverpath = "/Albums/6.jpg";

            //imgCurArtist.Source = new BitmapImage(new Uri(_ArtistList[_Cur].coverpath, UriKind.Relative));
            imgCurArtist.Source = _ArtistList[_Cur].cover;
            CreateSongListofArtist(_Cur);

            if (this._ArtistList.Count > 1)
            {
                //imgNextArtist.Source = new BitmapImage(new Uri(_ArtistList[_Cur + 1].coverpath, UriKind.Relative));
                imgNextArtist.Source = _ArtistList[_Cur + 1].cover;
            }
        }
        public void ResetArtistList()
        {
            this._ArtistList.Clear();
            imgCurArtist.Source = null;
            imgNextArtist.Source = null;
            imgPreArtist.Source = null;
        }
        public Song SongX_OfAlbum(short index, short x) // return song x of artist[index]
        {
            return this._ArtistList[index].Songlist[x];
        }
        private void PreArtist()
        {
            if (_Cur - 1 >= 0)
            {
                _Cur--;
                if (_ArtistList[_Cur].cover == null)
                    imgCurArtist.Source = _ArtistList[_Cur].cover;//new BitmapImage(new Uri(_ArtistList[_Cur].CoverPath, UriKind.Relative));
                else
                    imgCurArtist.Source = _ArtistList[_Cur].cover;

                this.CreateSongListofArtist(_Cur);

                if (_ArtistList[_Cur + 1].cover == null)
                    imgNextArtist.Source = _ArtistList[_Cur + 1].cover;// new BitmapImage(new Uri(_ArtistList[_Cur + 1].CoverPath, UriKind.Relative));
                else
                    imgNextArtist.Source = _ArtistList[_Cur + 1].cover;

                tbArtistName.Text = _ArtistList[_Cur].Name;

                if (_Cur - 1 < 0) // this current album is the last album
                    imgPreArtist.Source = null;
                else
                    if (_ArtistList[_Cur - 1].cover == null)
                        imgPreArtist.Source = new BitmapImage(new Uri("/Albums/6.jpg", UriKind.Relative));
                    else
                        imgPreArtist.Source = _ArtistList[_Cur - 1].cover;
            }
        }
        private void NextArtist()
        {
            if (_Cur + 1 < this._ArtistList.Count)
            {
                if (_ArtistList[_Cur].cover == null)
                    imgPreArtist.Source = _ArtistList[_Cur].cover;// new BitmapImage(new Uri(_ArtistList[_Cur].CoverPath, UriKind.Relative));
                else
                    imgPreArtist.Source = _ArtistList[_Cur].cover;

                _Cur++;
                if (_ArtistList[_Cur].cover == null)
                    imgCurArtist.Source = _ArtistList[_Cur].cover;//new BitmapImage(new Uri(_ArtistList[_Cur].CoverPath, UriKind.Relative));
                else
                    imgCurArtist.Source = _ArtistList[_Cur].cover;

                tbArtistName.Text = _ArtistList[_Cur].Name;

                this.CreateSongListofArtist(_Cur);

                if (_Cur == _ArtistList.Count - 1) // this current album is the last album
                    imgNextArtist.Source = null;
                else
                {
                    if (_ArtistList[_Cur + 1].cover == null)
                        imgNextArtist.Source = _ArtistList[_Cur + 1].cover;// new BitmapImage(new Uri(_ArtistList[_Cur + 1].CoverPath, UriKind.Relative));
                    else
                        imgNextArtist.Source = _ArtistList[_Cur + 1].cover;// _ArtistList[_Cur + 1].cover;
                }
            }
        }
        #endregion

        #region Events
        private void imgPreArtist_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.PreArtist();
        }
        private void imgNextArtist_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NextArtist();
        }
        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                this.NextArtist();
            else
                this.PreArtist();
        }
        #endregion
    }
}
