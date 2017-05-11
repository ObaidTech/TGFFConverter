using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGFFConverter
{
    public class ApplicationMode
    {
        public int modeID;
        public List<Edge> commEdges;

        public ApplicationMode()
        {
            // Initialize the list
            commEdges = new List<Edge>();
        }
    }
}
