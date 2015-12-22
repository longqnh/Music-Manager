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

        private int _Cur = 0;
        private List<Artist> _ArtistList;
        private MainWindow _Main;

        private void CreateSongListofArtist(int index)
        {
            this.pnTrackList.Children.Clear(); // first, remove all the song contenting
            //then add songlist of _AlbumList[index]
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
                imgCurArtist.Source = new BitmapImage(new Uri(_ArtistList[_Cur].coverpath, UriKind.Relative));
                CreateSongListofArtist(_Cur);
                 imgNextArtist.Source = new BitmapImage(new Uri(_ArtistList[_Cur + 1].coverpath, UriKind.Relative));
        }

        #endregion
    }
}
