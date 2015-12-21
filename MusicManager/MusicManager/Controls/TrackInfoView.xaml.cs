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
    /// Interaction logic for TrackInfoView.xaml
    /// </summary>
    public partial class TrackInfoView : UserControl
    {
        #region Constructor
        public TrackInfoView()
        {
            InitializeComponent();
        }
        #endregion 

        #region Properties
        public MainWindow Main { get; set; }
        private Song SongEditor;
        #endregion

        #region Methods
        public void LoadInfo(Song song) // load from songlist (track), playinglist
        {
            SongEditor = song;
            tbTittle.tbText.Text = song.Title;
            tbTittle.CheckLength();

            tbArtist.tbText.Text = song.Artist;
            tbArtist.CheckLength();

            tbAlbum.tbText.Text = song.Album;
            tbAlbum.CheckLength();

            tbDate.tbText.Text = song.Year.ToString();
            tbDate.CheckLength();

            tbGenre.tbText.Text = song.Genre;
            tbGenre.CheckLength();
            
            tbTrackNo.CheckLength();
            tbType.Text = song.filetype;
            tbTrackNo.tbText.Text = Convert.ToString (song.Track);
            //tbType.Text
            tbBitRate.Text = song.Bitrate.ToString() + "kbps";
            tbPath.Text = song.Path;
            //tbPath.Text
        }

        public void abc()
        {

        }
        #endregion
    }
}
