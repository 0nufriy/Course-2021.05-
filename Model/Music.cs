using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    class Music
    {
        public int years;
        public Album album;
        public Artist artist;
        public string name;
        public Music(Album Album, Artist Artist, string Name)
        {
            years = Album.years;
            album = Album;
            artist = Artist;
            name = Name;
        }

    }
}
