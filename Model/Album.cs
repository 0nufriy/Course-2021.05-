using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    class Album
    {
        public int years;
        public string name;
        public string Cover;
        public int Count
        {
            get { return musiclist.Count; }
        }
        public Artist artist;
        public List<Music> musiclist;
        public Album(int Years, string Name, Artist Artist, List<Music> list)
        {
            name = Name;
            artist = Artist;
            musiclist = list;
            years = Years;
            Cover = "Covers/" + Artist.name + '.' + Name + ".png";
        }
        public Music this[int ind]
        {
            get
            {
                return musiclist[ind];
            }
        }
    }
}
