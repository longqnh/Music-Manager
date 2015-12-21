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
    /// Interaction logic for Track.xaml
    /// </summary>
    public partial class Track : UserControl
    {
        #region Constructor
        public Track(MainWindow main, short index, short trackno)
        {
            InitializeComponent();
            this._Main = main;
            this._Index = index;
            this._TrackNo = trackno;
        }
        #endregion

        #region Properties
        private MainWindow _Main;
        private short _Index;
        private short _TrackNo;
        #endregion

        #region Events
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
<<<<<<< HEAD
            Song tmp = this._Main.SongList.ctAlbumView.SongX_OfAlbum(_Index, _TrackNo);

            this._Main.TrackInfo.LoadInfo(tmp); // load trackinfo
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                this._Main.PlayList.AddtoList(tmp); // add track to playlist
            }

=======
            Song tmp;
            //get song reference
            if(!_inArtist)
                tmp = this._Main.SongList.ctAlbumView.SongX_OfAlbum(_Index, _TrackNo); 
            else
                tmp = this._Main.SongList.ctArtistView.SongX_OfAlbum(_Index, _TrackNo);

            this._Main.TrackInfo.LoadInfo(tmp); // load trackinfo
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2 && !_inArtist)
                this._Main.PlayList.AddtoList(tmp); // add track to playlist
>>>>>>> origin/master
        }
        #endregion
    }
}
