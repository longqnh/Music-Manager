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
            cover = new BitmapImage(new Uri("/Albums/6.jpg", UriKind.Relative));
        }
        #endregion

        #region Properties
        public string Name { get; set; }
        public List<Song> Songlist { get; set; }
        public string coverpath { get; set; }
        public BitmapImage cover { get; set; }
        public int ID { get; set; }
        #endregion
    }
}
