using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicManager.Classes
{
    public class Artist
    {
        #region Constructor
        public Artist()
        { 
        }
        #endregion

        #region Properties
        public string Name { get; set; }
        public List<Song> Songlist { get; set; }
        public string coverpath { get; set; }
        public int ID { get; set; }
        #endregion
    }
}
