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
using CSCore.Codecs;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;
using CSCore.Streams;
using System.Collections.ObjectModel;
using AudioPlayerSample;
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
            using (var mmdeviceEnumerator = new MMDeviceEnumerator())
            {
                using (
                    var mmdeviceCollection = mmdeviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active))
                {
                    foreach (var device in mmdeviceCollection)
                    {
                        _devices.Add(device);
                        break;
                    }
                }
                btnPlay.IsEnabled = true;
                imgplay.Source = new BitmapImage(new Uri("/Image/Play Button.png", UriKind.Relative));
            }
        
        }
        #endregion

        #region Properties
        private MediaPlayer _Song = new MediaPlayer();
        private string _SongtoPlay;
        private System.Windows.Threading.DispatcherTimer _Timer = new System.Windows.Threading.DispatcherTimer();
        public AudioPlayerSample.MusicPlayer player =new MusicPlayer();
        private readonly ObservableCollection<MMDevice> _devices = new ObservableCollection<MMDevice>(); 

        #endregion

        #region Events
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(player.PlaybackState) == "Playing")
            {
                player.Stop();
                tbCurDur.Text = "00:00";
                imgplay.Source = new BitmapImage(new Uri("/Image/Play Button.png", UriKind.Relative));
                imgStop.Source = new BitmapImage(new Uri("/Image/stop 1.png", UriKind.Relative));
                this.imgplay.UpdateLayout();
            };
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            
            if (_SongtoPlay != null)
            {
                imgplay.Source =new BitmapImage(new Uri("/Image/Playing.png", UriKind.Relative));
                imgStop.Source = new BitmapImage(new Uri("/Image/stop 2.png", UriKind.Relative));
                imgpause.Source = new BitmapImage(new Uri("/Image/pause.png", UriKind.Relative));
                this.imgplay.UpdateLayout();
                TagLib.File file;
                file=TagLib.File.Create((string)(_SongtoPlay));
                    TimeSpan dur = file.Properties.Duration;
                    //set the song duration lable
                    tbSongDur.Text = dur.ToString(@"mm\:ss"); //"/ 0" + dur.Minutes.ToString() + ":" + dur.Seconds.ToString();
                    tbCurDur.Text = "00:00";
                    //    //set the seekbar
                    SeekBar.Maximum = dur.TotalSeconds;
                    SeekBar.SmallChange = 1;
                    SeekBar.LargeChange = Math.Min(10, dur.Seconds / 10);
                    //set the volume bar
                    VolumeBar.Maximum = player.Volume;
                    VolumeBar.Value = player.Volume;
                    //start the ticker timer
                    _Timer.Interval = TimeSpan.FromMilliseconds(500);
                    _Timer.Tick += _Timer_Tick;
                    _Timer.Start();
                    //start playing song
                    _Song.Play();
                    // System.Diagnostics.Process.Start(@_SongtoPlay);// hàm này sẽ chạy player mặc định của file mà bạn cài đặc cho máy , nhà mình cài foobar2000 ấn vào nó chạy foobar2000 tuyệt vời
                    player.Open(_SongtoPlay, _devices[0]);
                    player.Play();
                
            }
            else
            {
                MessageBox.Show(" Đường dẫn null ");
            }
            
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {

            if (Convert.ToString(player.PlaybackState) == "Playing")
            {
                player.Pause();
               
                imgpause.Source = new BitmapImage(new Uri("/Image/Play Button.png", UriKind.Relative));
            }
            else
                if (Convert.ToString(player.PlaybackState) == "Paused")
                {
                    player.Play();
                    imgpause.Source = new BitmapImage(new Uri("/Image/pause.png", UriKind.Relative));
                }
           
        }

        private void _Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan position = player.Position;
            TimeSpan length = player.Length;
            if (position > length)
                length = position;
            if (length != TimeSpan.Zero && position != TimeSpan.Zero)
            {
            tbCurDur.Text = String.Format(@"{0:mm\:ss}", position);
            double perc = position.TotalMilliseconds / length.TotalMilliseconds * SeekBar.Maximum;
            SeekBar.Value = (int)perc;
           
               
            }
           
          
        }

        private void SeekBar_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
           player.Position = TimeSpan.FromSeconds(SeekBar.Value);
        }

        private void VolumeBar_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            player.Volume =Convert.ToInt32( VolumeBar.Value);
        }
        #endregion

        #region Method
        public void SelectedSong(String path)
        {
            _SongtoPlay = path;
        }
        #endregion
    }
}
