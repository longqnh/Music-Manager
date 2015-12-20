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
            try
            {
                CreateAlbumList();
            }
            catch
            {
                
            }

        }
        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source = music.db");
        SQLiteCommand sqlite_cmd;

        #region Properties
        private string _Media;
        private List<FileInfo> _MusicList; //list of files that are music file
        int ID_Album = 0;
        int ID_Artist = 0;
        int ID_Song = 0;
        bool loaded = false;
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
            loaded = true;
            this.AddListToDB(_MusicList);
            CreateAlbumList();
        }
        #endregion

        #region Methods
       
        
        //--- Hàm thêm list<fileinfo> vào database 
        private void AddListToDB(List<FileInfo> List)
        {

            TagLib.File song;
            //Open connection 
            sqlite_conn.Open();
            //create command
            sqlite_cmd = sqlite_conn.CreateCommand();
            int tmp_ID_Album,tmp_ID_Artist;
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
                    string titletemp;
                    
                        if(song.Tag.Title==null)  titletemp="unknow";
                            else titletemp=song.Tag.Title;

                        if (song.Tag.Title.IndexOf("'") > 0)
                        {
                            int a = song.Tag.Title.IndexOf("'");
                           titletemp= titletemp.Insert(a,"'");
                        };
                    
                    sqlite_cmd.CommandText = "INSERT INTO Song(Songid,Title,Dur,Year,Path,Bitrate,Genre,AlbumID,ArtistId) VALUES ('" + ID_Song + "','" + titletemp + "','" + song.Properties.Duration + "','" + song.Tag.Year + "','" + musicfile.FullName + "'," + song.Properties.AudioBitrate + ",'" + song.Tag.FirstGenre + "','" + tmp_ID_Album + "','"+tmp_ID_Artist+"');";
                    sqlite_cmd.ExecuteNonQuery();
                    ID_Song++;
                }
                catch (Exception e)
                {
                    //sqlite_cmd.CommandText = "INSERT INTO Song(Songid,Title,Dur,Year,Path,Bitrate,Genre,AlbumID,ArtistId,Composer) VALUES ('" + ID_Song + "','" + song.Tag.Title + "','" + song.Properties.Duration + "','" + song.Tag.Year + "','" + musicfile.DirectoryName + "'," + song.Properties.AudioBitrate + ",'" + song.Tag.FirstGenre + "','" + tmp_ID_Album + "','" + tmp_ID_Artist + "','" + song.Tag.FirstComposer + "');";
                    //sqlite_cmd.ExecuteNonQuery();
                    //ID_Song++;
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
            };
            sqlite_conn.Close();
        }
        private void CreateAlbumList()
        {
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            List<Album> Albums = new List<Album>();
            sqlite_cmd.CommandText = "select * from Album";
            SQLiteDataReader Dtreader;
            Dtreader = sqlite_cmd.ExecuteReader();
            int i=0;
                while (Dtreader.Read())
                {
                    Album temp = new Album();
                    temp.ID = Dtreader.GetInt32(0);
                    temp.Name = Dtreader.GetString(1);
                    Albums.Add(temp);

                }// Create List AlbumName With an Album ADD song;
            Dtreader.Close();
            foreach (Album album in Albums) 
            {
                sqlite_cmd.CommandText = "SELECT  * FROM Album,Song Where Song.AlbumId = Album.AlbumId and Album.AlbumID="+album.ID+";";
                Dtreader = sqlite_cmd.ExecuteReader();
                while (Dtreader.Read())
                {

                   // Song song = new Song(Dtreader.GetString(3), "test", album.Name, (short?)(Dtreader.GetInt32(5)), Dtreader.GetString(8), 3, (short)Dtreader.GetInt32(7), , Dtreader.GetString(6));
                    Song asong = new Song();
                    asong.Path = Dtreader.GetString(6);
                    asong.Genre = Dtreader.GetString(8);
                    asong.Year = (short?)Dtreader.GetInt32(5);
                    asong.Bitrate = Dtreader.GetInt32(7);
                    string time = Dtreader.GetString(4);
                    asong.Dur = TimeSpan.Parse(time);
                    asong.Album = album.Name;
                    TagLib.File tlfile2 = TagLib.File.Create(@asong.Path);
                    string extension;
                    extension = System.IO.Path.GetExtension(asong.Path);
                    asong.filetype = Convert.ToString(extension);
                    asong.Title = tlfile2.Tag.Title;
                    asong.Track = (short?)tlfile2.Tag.TrackCount;
                    if(!album.havecover)
                    {
                       try
                       {
                           TagLib.File tlfile = TagLib.File.Create(@asong.Path);
                           TagLib.IPicture pic;
                           if (tlfile.Tag.Pictures.Length > 0)
                           {
                               pic = tlfile.Tag.Pictures[0];
                               MemoryStream ms = new MemoryStream(pic.Data.Data);
                               ms.Seek(0, SeekOrigin.Begin);

                               // ImageSource for System.Windows.Controls.Image
                               BitmapImage bitmap = new BitmapImage();
                               bitmap.BeginInit();
                               bitmap.StreamSource = ms;
                               bitmap.EndInit();

                               // Create a System.Windows.Controls.Image control
                               System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                               album.cover = bitmap;
                           };
                           album.havecover = true;
                           album.AlbumArtist = tlfile.Tag.FirstArtist;
                           asong.Artist = tlfile.Tag.FirstArtist;
                           
                       }
                        catch
                       {

                       }
                    
                       album.TrackList.Add(asong); 

                    }
                }
                Dtreader.Close();
            }
            this.SongList.ctAlbumView.ReceiveAlbumList(Albums, this);
            sqlite_conn.Close();
        }

        private void SongList_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        //-- kết thúc
        #endregion
    }
}
