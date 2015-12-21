using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
namespace MusicManager.Classes
{
    public class Album
    {
        #region Constructor
        public Album()
        {
            TrackList = new List<Song>();
            havecover = false;
        }
        #endregion

        #region Properties
        public string Name { get; set; }
        public string AlbumArtist { get; set; }
        public int Year { get; set; }
        public List<Song> TrackList { get; set; }
        public string CoverPath { get; set; }
        public int ID { get; set; }
        public BitmapImage Cover { get; set; }
        public bool havecover { get; set; }
        #endregion
    }
}
