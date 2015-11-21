using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicManager.Classes
{
    class Artist
    {
        #region Constructor
        public Artist()
        { 
        }
        #endregion

        #region Properties
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        #endregion
    }
}
