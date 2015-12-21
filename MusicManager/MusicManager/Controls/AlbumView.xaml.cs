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
            //set current album cover
            _Cur--;
            if (_AlbumList[_Cur].Cover == null)
                imgCurAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
            else
                imgCurAlbum.Source=_AlbumList[_Cur].Cover;

            /////set album info to show
            tbAlbumName.Text = _AlbumList[_Cur].Name;
            tbArtist.Text = _AlbumList[_Cur].AlbumArtist;
            tbYear.Text = Convert.ToString(_AlbumList[_Cur].Year);
            this.CreateSongList_ofAlbum(_Cur);

            //set next album cover
            if (_AlbumList[_Cur + 1].Cover == null)
                imgNextAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur + 1].CoverPath, UriKind.Relative));
            else
                imgNextAlbum.Source = _AlbumList[_Cur + 1].Cover;

            
            //set previous album cover
            if (_Cur - 1 < 0) // this current album is the last album
                imgPreAlbum.Source = null;
            else
                if (_AlbumList[_Cur - 1].Cover == null)
                    imgPreAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur - 1].CoverPath, UriKind.Relative));
                else
                    imgPreAlbum.Source = _AlbumList[_Cur - 1].Cover;
        }
        private void imgNextAlbum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //set previous album cover
            if(_AlbumList[_Cur].Cover==null)
                imgPreAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
            else
                imgPreAlbum.Source = _AlbumList[_Cur].Cover;

            //set current album cover
            _Cur++;
            if (_AlbumList[_Cur].Cover == null)
                imgCurAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
            else
                imgCurAlbum.Source=_AlbumList[_Cur].Cover;

            /////set album info to show
            tbAlbumName.Text = _AlbumList[_Cur].Name;
            tbArtist.Text = _AlbumList[_Cur].AlbumArtist;
            tbYear.Text = Convert.ToString(_AlbumList[_Cur].Year);

            this.CreateSongList_ofAlbum(_Cur);

            //set next album cover
            if (_Cur == _AlbumList.Count - 1) // this current album is the last album
                imgNextAlbum.Source = null;
            else
            {
                if (_AlbumList[_Cur + 1].Cover == null) 
                    imgNextAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur + 1].CoverPath, UriKind.Relative));
                else
                    imgNextAlbum.Source= _AlbumList[_Cur + 1].Cover;
            }
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
            for (int i = 0; i < this._AlbumList.Count; i++)
                this._AlbumList[i].CoverPath = Path[5];
            // set image on form
           if(_AlbumList[_Cur].Cover==null) 
                    imgCurAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
           else  imgCurAlbum.Source = _AlbumList[_Cur].Cover;

            this.CreateSongList_ofAlbum(_Cur);
            if (_AlbumList[_Cur + 1].Cover==null) imgNextAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur + 1].CoverPath, UriKind.Relative));
            else imgNextAlbum.Source = _AlbumList[_Cur+1].Cover;
           
            #endregion
        }
        private void CreateSongList_ofAlbum(int index)
        {
            this.pnTrackList.Children.Clear(); // first, remove all the song contenting
            //then add songlist of _AlbumList[index]
            int i;
            for (i = 0; i < this._AlbumList[index].TrackList.Count; i++)
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
