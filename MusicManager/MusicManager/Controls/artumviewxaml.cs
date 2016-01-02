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

        private int _Cur = 0;
        private List<Artist> _ArtistList;
        private MainWindow _Main;

        private void CreateSongListofArtist(int index)
        {
            this.pnTrackList.Children.Clear(); // first, remove all the song contenting
            //then add songlist of _ArtistList[index]
            int i;
            for (i = 0; i < this._ArtistList[index].Songlist.Count; i++)
            {
                Song tmp = this._ArtistList[index].Songlist[i];

                Track track = new Track(_Main, (short)index, (short)i);
                track.tbNo.Text = i + ".";

                track.tbTitle.Text = tmp.Title;
                track.tbDur.Text = "0" + tmp.Dur.Minutes.ToString() + ":" + tmp.Dur.Seconds;
                this.pnTrackList.Children.Add(track);
            }

        }
        public void ReceiveArtistList(List<Artist> list, MainWindow main)
        {          
            this._ArtistList = list;
            for (int i = 0; i < this._ArtistList.Count; i++)
                this._ArtistList[i].coverpath = "/Albums/6.jpg";

            this._Main = main;

            //imgCurArtist.Source = new BitmapImage(new Uri(_ArtistList[_Cur].coverpath, UriKind.Relative));
            imgCurArtist.Source = _ArtistList[_Cur].cover;
            CreateSongListofArtist(_Cur);
            //imgNextArtist.Source = new BitmapImage(new Uri(_ArtistList[_Cur + 1].coverpath, UriKind.Relative));
            imgNextArtist.Source = _ArtistList[_Cur + 1].cover;
        }

        private void imgPreArtist_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void imgNextArtist_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
            };
        }        
    }
}
