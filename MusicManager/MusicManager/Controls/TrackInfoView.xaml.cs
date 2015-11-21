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

namespace MusicManager.Controls
{
    /// <summary>
    /// Interaction logic for TrackInfoView.xaml
    /// </summary>
    public partial class TrackInfoView : UserControl
    {
        public TrackInfoView()
        {
            InitializeComponent();
        }

        #region Properties
        private MainWindow _Main;

        public MainWindow Main
        {
            set { _Main = value; }
        }
        #endregion

        #region Methods
        public void LoadInfo(Track track)
        {
            tbTittle.tbText.Text = track.tbTitle.Text;
            tbTittle.CheckLength();

            //tbArtist.tbText.Text
            //tbArtist.CheckLength();

            //tbAlbum.tbText.Text
            //tbAlbum.CheckLength();

            //tbDate.tbText.Text
            //tbDate.CheckLength();

            //tbGenre.tbText.Text
            //tbGenre.CheckLength();

            //tbTrackNo.tbText.Text
            //tbTrackNo.CheckLength();

            //tbType.Text
            //tbBitRate.Text
            //tbPath.Text
        }
        public void LoadInfo(PlayingSong song)
        {
            tbTittle.tbText.Text = song.tbTitle.Text;
            tbTittle.CheckLength();

            tbArtist.tbText.Text = song.tbArtist.Text;
            tbArtist.CheckLength();

            tbAlbum.tbText.Text = song.tbAlbum.Text;
            tbAlbum.CheckLength();

            //tbDate.tbText.Text
            //tbDate.CheckLength();

            //tbGenre.tbText.Text
            //tbGenre.CheckLength();

            //tbTrackNo.tbText.Text
            //tbTrackNo.CheckLength();

            //tbType.Text
            //tbBitRate.Text
            //tbPath.Text
        }

        #endregion
    }
}
