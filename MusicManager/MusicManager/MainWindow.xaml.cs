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
        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source = music.db");
        SQLiteCommand sqlite_cmd;

        #region Properties
        private string _Media;
        private List<FileInfo> _MusicList; //list of files that are music file
        int ID_Album = 0;
        int ID_Artist = 0;
        int ID_Song = 0;
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
        private void Button_Click(object sender, RoutedEventArgs e) //brown
        {
            _MusicList = new List<FileInfo>();
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult _result = STAShowDialog(fbd);
            if (_result == System.Windows.Forms.DialogResult.OK)
            {
                _Media = fbd.SelectedPath;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //load
        {
            sqlite_conn.Open();                                 //xóa sạch database trước khi add data mới vào 
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "delete from Song";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "delete from Album";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "delete from Artist";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();

            DirectoryInfo dir = new DirectoryInfo(_Media);
            foreach (FileInfo file in dir.GetFiles("*.*", SearchOption.AllDirectories))
            {
                if (file.Extension == ".wmv" || file.Extension == ".mp3" || file.Extension == ".mp4"
                    || file.Extension == ".flac" || file.Extension == ".wma"
                    || file.Extension == ".m4a" || file.Extension == ".wav")
                    _MusicList.Add(file);
            }
       //  ..   this.CreateAlbumList(_MusicList);
            this.AddListToDB(_MusicList, 0);
        }
        #endregion

        #region Methods
        private void CreateAlbumList(List<FileInfo> MusicList) // find all album of music list
        {
            List<Album> AlbumList = new List<Album>();
            foreach (FileInfo musicfile in MusicList)
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

            TagLib.File song;
            //Open connection 
            sqlite_conn.Open();
            //create command
            sqlite_cmd = sqlite_conn.CreateCommand();
            int tmp_ID_Album,t,tmp_ID_Artist;
            sqlite_cmd.CommandText = "insert into Album(AlbumID, AlbumName) values ('0','Unknow')";
            sqlite_cmd.ExecuteNonQuery();
            ID_Album++;
            sqlite_cmd.CommandText = "insert into Artist(ArtistID, ArtistName) values ('0','Unknow')";
            sqlite_cmd.ExecuteNonQuery();
            ID_Artist++;
            foreach (FileInfo musicfile in List)
            {
                song = TagLib.File.Create((string)(musicfile.FullName)); // with a path of file we create a file tag
              
                try
                {
                        
                        sqlite_cmd.CommandText = "insert into Album(AlbumID, AlbumName) values (' " + ID_Album + " ','" + song.Tag.Album + "')";
                        sqlite_cmd.ExecuteNonQuery();
                        ID_Album++;
                }
                catch (Exception e)
                {

                }
                try
                {
                    sqlite_cmd.CommandText = "insert into Artist(ArtistID, ArtistName) values (' " + ID_Artist + " ','" + song.Tag.FirstArtist + "')";
                    sqlite_cmd.ExecuteNonQuery();
                    ID_Artist++;
                }
                catch (Exception)
                {

                }
               
                try
                {
                    sqlite_cmd.CommandText = "select AlbumID from Album where AlbumName = '" + song.Tag.Album + "';";
                    tmp_ID_Album = Convert.ToInt32(sqlite_cmd.ExecuteScalar());
                 
                }
                catch (Exception) // zero mean unknow
                {
                    tmp_ID_Album = 0;
                }
                try
                {
                    sqlite_cmd.CommandText = "select ArtistID from Artist where ArtistName = '" + song.Tag.FirstArtist + "';";
                    tmp_ID_Artist = Convert.ToInt32(sqlite_cmd.ExecuteScalar());
                    
                }
                catch (Exception) 
                {
                    tmp_ID_Artist = 0;
                }
                try 
                { 
                    sqlite_cmd.CommandText = "INSERT INTO Song(Songid,Title,Dur,Year,Path,Bitrate,Genre,AlbumID,ArtistId,Composer) VALUES ('" + ID_Song + "','" + song.Tag.Title + "','" + song.Properties.Duration + "','" + song.Tag.Year + "','" + musicfile.DirectoryName + "'," + song.Properties.AudioBitrate + ",'" + song.Tag.FirstGenre + "','" + tmp_ID_Album + "','"+tmp_ID_Artist+"','"+song.Tag.FirstComposer+"');";
                    sqlite_cmd.ExecuteNonQuery();
                    ID_Song++;
                }
                catch
                {
                    //sqlite_cmd.CommandText = "INSERT INTO Song(Songid,Title,Dur,Year,Path,Bitrate,Genre,AlbumID,ArtistId,Composer) VALUES ('" + ID_Song + "','" + song.Tag.Title + "','" + song.Properties.Duration + "','" + song.Tag.Year + "','" + musicfile.DirectoryName + "'," + song.Properties.AudioBitrate + ",'" + song.Tag.FirstGenre + "','" + tmp_ID_Album + "','" + tmp_ID_Artist + "','" + song.Tag.FirstComposer + "');";
                    //sqlite_cmd.ExecuteNonQuery();
                    //ID_Song++;
                }
            };
            sqlite_conn.Close();
        }


        //-- kết thúc
        #endregion
    }
}
