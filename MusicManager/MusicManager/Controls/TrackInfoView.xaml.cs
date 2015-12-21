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
            this.LoadTrackEditor();
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
        private void LoadTrackEditor()
        {
            tbTittle.Load(this, 0);
            tbArtist.Load(this, 1);
            tbAlbum.Load(this, 2);
            tbDate.Load(this, 3);
            tbGenre.Load(this, 4);
            tbTrackNo.Load(this, 5);
        }
        public void EditInfo(short infoID, string newInfo)
        {
            switch (infoID)
            {
                case 0: // Title edited, apply changes in newInfo to song's tag here
                    break;
                case 1: // Artist edited, apply changes in newInfo to song's tag here
                    break;
                case 2: // Album edited, apply changes in newInfo to song's tag here
                    break;
                case 3: // Date edited, apply changes in newInfo to song's tag here
                    break;
                case 4: // Genre edited, apply changes in newInfo to song's tag here
                    break;
                case 5: // Track Number edited, apply changes in newInfo to song's tag here
                    break;
                default: // wrong infoID, do nothing
                    break;
            }
        }
        #endregion
    }
}
