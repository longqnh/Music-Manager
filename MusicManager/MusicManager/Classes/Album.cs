using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicManager.Classes
{
    public class Album
    {
        #region Constructor
        public Album()
        {
            TrackList = new List<Song>();
        }
        #endregion

        #region Properties
        public string Name { get; set; }
        public string AlbumArtist { get; set; }
        public int Year { get; set; }
        public List<Song> TrackList { get; set; }
        public string CoverPath { get; set; }
        #endregion

        #region Methods

        #endregion
    }
}
