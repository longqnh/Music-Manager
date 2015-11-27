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
using System.Windows.Forms;
using System.IO;
using MusicManager.Classes;
using HundredMilesSoftware.UltraID3Lib;
using System.Data.SQLite;
namespace MusicManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SongList.Main = PlayList.Main = TrackInfo.Main = this;

        }

        #region Properties
        private string _Media;
        private List<FileInfo> _MusicList; //list of files that are music file
        #endregion

        #region Thread
        public class DialogState
        {
            public DialogResult result;
            public FolderBrowserDialog dialog;


            public void ThreadProcShowDialog()
            {
                result = dialog.ShowDialog();
            }
        }
        private DialogResult STAShowDialog(FolderBrowserDialog dialog)
        {
            DialogState state = new DialogState();
            state.dialog = dialog;
            System.Threading.Thread t = new
                   System.Threading.Thread(state.ThreadProcShowDialog);
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            t.Join();
            return state.result;
        }
        #endregion

        #region Events
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _MusicList = new List<FileInfo>();
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult _result = STAShowDialog(fbd);
            if (_result == System.Windows.Forms.DialogResult.OK)
            {
                _Media = fbd.SelectedPath;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(_Media);
            foreach (FileInfo file in dir.GetFiles("*.*", SearchOption.AllDirectories))
            {
                if (file.Extension == ".wmv" || file.Extension == ".mp3" || file.Extension == ".mp4"
                    || file.Extension == ".flac" || file.Extension == ".wma"
                    || file.Extension == ".m4a" || file.Extension == ".wav")
                    _MusicList.Add(file);
            }
            this.CreateAlbumList(_MusicList);
        }
        #endregion

        #region Methods
        private void CreateAlbumList(List<FileInfo> MusicList) // find all album of music list
        {
            List<Album> AlbumList = new List<Album>();
            foreach(FileInfo musicfile in MusicList)
            {
                UltraID3 Tag = new UltraID3();
                Tag.Read(musicfile.FullName);
                string albumName = Tag.Album; // current musicfile's albumname
                int i;
                for (i = 0; i < AlbumList.Count; i++) 
                {
                    if (albumName == AlbumList[i].Name) // found it's album in the albumlist
                    {
                        //create a song
                        Song aSong = new Song(Tag.Title, Tag.Artist, Tag.Album, Tag.Year, Tag.Genre, 
                            Tag.TrackNum, Tag.GetMPEGTrackInfo().AverageBitRate, Tag.Duration);
                        aSong.Path = musicfile.FullName;
                        AlbumList[i].TrackList.Add(aSong);
                        break;
                    }                   
                }
                if (i == AlbumList.Count) // current musicfile's albumname not in the album list
                {
                    //create new album
                    Album NewAlbum = new Album();
                    NewAlbum.Name = albumName;
                    //NewAlbum.CoverPath =???;
                    AlbumList.Add(NewAlbum);

                    //create a song
                    Song aSong = new Song(Tag.Title, Tag.Artist, Tag.Album, Tag.Year, Tag.Genre, 
                            Tag.TrackNum, Tag.GetMPEGTrackInfo().AverageBitRate, Tag.Duration);
                    aSong.Path = musicfile.FullName;
                    AlbumList[i].TrackList.Add(aSong);

                    //add song to album
                    NewAlbum.TrackList.Add(aSong);
                }
            }
            this.SongList.ctAlbumView.ReceiveAlbumList(AlbumList, this);
        }
        //--- Hàm thêm list<fileinfo> vào database 
        void AddListToDB(List<FileInfo> List, int Idstarform) // lưu ý hàm này nhận một số int vào để bắt đầu đánh dấu ID trong DB ++
        {

            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            TagLib.File song;
            // create a connection 
            sqlite_conn = new SQLiteConnection("Data Source = music.sqlite");
            //Open connection 
            sqlite_conn.Open();
            //create command
            sqlite_cmd = sqlite_conn.CreateCommand();
            //   sqlite_cmd.CommandText = "ALTER TABLE Album ADD FOREIGN KEY (AlbumName) REFERENCES Song(Album);"; cái này t test tạo khóa mà vẫn chưa được
            sqlite_cmd.ExecuteNonQuery();
            foreach (FileInfo musicfile in List)
            {
                song = TagLib.File.Create((string)(musicfile.FullName)); // with a path of file we create a file tag;
                sqlite_cmd.CommandText = "INSERT INTO Song(Songid,Title,Dur,Year,Album,Artist,Path,Bitrate,Genre) VALUES ('" + Idstarform + "','" + song.Tag.Title + "','" + song.Properties.Duration + "','" + song.Tag.Year + "','" + song.Tag.Album + "','" + song.Tag.Artists[0] + "','" + musicfile.DirectoryName + "'," + song.Properties.AudioBitrate + ",'" + song.Tag.Genres[0] + "');";
                sqlite_cmd.ExecuteNonQuery();
                Idstarform++;
            };
            sqlite_conn.Close();
        }


        //-- kết thúc
        #endregion
    }
}
