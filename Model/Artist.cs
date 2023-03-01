using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    class Artist
    {
        public string name;

        public List<Album> albumlist;

        public Artist(string Name, List<Album> list)
        {
            name = Name;
            albumlist = list;
        }
        public Album this[int ind]
        {
            get
            {
                return albumlist[ind];
            }
        }
        public int Count
        {
            get { return albumlist.Count; }
        }

    }
}
