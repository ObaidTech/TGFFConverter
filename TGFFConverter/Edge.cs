using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGFFConverter
{
    public class Edge
    {
        public int edgeID;
        public int start;
        public int end;
        public float volume;
        public int type;

        public string print()
        {
            return "ID: " + edgeID.ToString() + " From: " + start.ToString() + " To: " + end.ToString() +" Type: " + type.ToString() +  " Volume: " + volume.ToString();
        }
    }
}
