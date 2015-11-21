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
        #endregion

        #region Methods
        public void LoadInfo(Song song) // load from songlist (track), playinglist
        {
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

            tbTrackNo.tbText.Text = song.Track.ToString();
            tbTrackNo.CheckLength();

            //tbType.Text
            tbBitRate.Text = song.Bitrate.ToString() + "kbps";
            //tbPath.Text
        }
        #endregion
    }
}
