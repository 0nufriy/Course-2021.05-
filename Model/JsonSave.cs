using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    class JsonSave
    {
        public List<JSONElement> list;
        public int Count
        {
            get { return list.Count; }
        }
        public JsonSave()
        {
            list = new List<JSONElement>();
        }
        public JSONElement this[int ind]
        {
            get { return list[ind]; }
        }

        
    }
}
