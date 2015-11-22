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
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class SongPlayer : UserControl
    {
        #region Constructor
        public SongPlayer()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private MediaPlayer _Song = new MediaPlayer();
        private Song _SongtoPlay;
        private System.Windows.Threading.DispatcherTimer _Timer = new System.Windows.Threading.DispatcherTimer();
        #endregion

        #region Events
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _Song.Play();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            //_Song.Open(new Uri(_Songtoplay.FullPath);
            _Song.Open(new Uri("C:/Users/Administrator/Desktop/New folder/1/Dokki doki! Love mail.mp3"));
            if(_Song.NaturalDuration.HasTimeSpan)
            {
                TimeSpan dur = _Song.NaturalDuration.TimeSpan;
                //set the song duration lable
                tbSongDur.Text = dur.ToString(@"mm\:ss"); //"/ 0" + dur.Minutes.ToString() + ":" + dur.Seconds.ToString();
                tbCurDur.Text = "00:00";
                //set the seekbar
                SeekBar.Maximum = dur.TotalSeconds;
                SeekBar.SmallChange = 1;
                SeekBar.LargeChange = Math.Min(10, dur.Seconds / 10);
                //set the volume bar
                VolumeBar.Maximum = _Song.Volume;
                VolumeBar.Value = _Song.Volume;
                //start the ticker timer
                _Timer.Interval = TimeSpan.FromMilliseconds(500);
                _Timer.Tick += _Timer_Tick;
                _Timer.Start();
                //start playing song
                _Song.Play();
               
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            _Song.Pause();
        }

        private void _Timer_Tick(object sender, EventArgs e)
        {
            SeekBar.Value = _Song.Position.TotalSeconds;
            tbCurDur.Text = _Song.Position.ToString(@"mm\:ss");
        }

        private void SeekBar_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            _Song.Position = TimeSpan.FromSeconds(SeekBar.Value);
        }

        private void VolumeBar_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            _Song.Volume = VolumeBar.Value;
        }
        #endregion


    }
}
