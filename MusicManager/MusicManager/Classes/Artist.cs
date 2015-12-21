using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
namespace MusicManager.Classes
{
    public class Artist
    {
        #region Constructor
        public Artist()
        {
            SongList = new List<Song>();
            Cover = new BitmapImage(new Uri("/Albums/6.jpg", UriKind.Relative));
            CoverPath = "/Albums/6.jpg";
        }
        #endregion

        #region Properties
        public string Name { get; set; }
        public List<Song> SongList { get; set; }
        public string CoverPath { get; set; }
        public BitmapImage Cover { get; set; }
        #endregion
    }
}
