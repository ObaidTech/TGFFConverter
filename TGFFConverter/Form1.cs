using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace TGFFConverter
{
    public partial class Form1 : Form
    {
        // Variables
        string inputGraphLocation = String.Empty;
        string inputGraph = String.Empty;
        string outputGraph = String.Empty;
        List<ApplicationMode> applicationModes;
        List<Core> cores;

        enum trafficType
        {
            Random = 0,
            Random_Modal,
            Bit_Reversal,
            Shuffle,
            Transpose_Matrix,
            Tornado,
            Neighbour,
            Hot_Spot
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void inputBrowseBtn_Click(object sender, EventArgs e)
        {
            clearVariables();
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "Task Graph Files (.tgff)|*.tgff|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = true;

            // Call the ShowDialog method to show the dialog box.
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            int size = -1;
            // Process input if the user clicked OK.
            if (dialogResult == DialogResult.OK)
            {
                // Save the file to read path.
                inputGraphLocation = openFileDialog1.FileName;
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        public void clearVariables()
        {
            inputGraph = String.Empty;
            outputGraph = String.Empty;
            cores = new List<Core>();
            applicationModes = new List<ApplicationMode>();
        }

        private void convertBtn_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Application Graph XML|*.xml";
            saveFileDialog1.Title = "Save XML File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                try
                {
                    // Read all text in input file and save it in input graph
                    inputGraph = File.ReadAllText(inputGraphLocation);
                    string[] inputLines = inputGraph.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                    for (int lineNo = 0; lineNo < inputLines.Length; lineNo++)
                    {
                        int taskgraphBeginLine = lineNo;
                        int taskgraphEndLine = lineNo;
                        int commTypesEndLine = lineNo;
                        string lineText = inputLines[lineNo];
                        if (lineText.Contains("@TASK_GRAPH"))
                        {
                            // This is the start of task graph
                            ApplicationMode newMode = new ApplicationMode();
                            // Check where this task graph ends
                            for (int endingBracketLine = lineNo; endingBracketLine < inputLines.Length; endingBracketLine++)
                            {
                                if (inputLines[endingBracketLine].Contains("}"))
                                {
                                    // This is the ending
                                    taskgraphEndLine = endingBracketLine;
                                    break;
                                }
                            }
                            // Get task graph number
                            int startSplit = lineText.IndexOf('H') + 1;
                            int endSplit = lineText.IndexOf('{');
                            string res = lineText.Substring(startSplit, endSplit - startSplit);
                            int graphNo = Int32.Parse(res);
                            // Console.WriteLine("Found Graph " + graphNo);
                            newMode.modeID = graphNo;
                            // Go through inside the coregraph lines and search for edges
                            for (int edgeLines = lineNo; edgeLines < taskgraphEndLine; edgeLines++)
                            {
                                string edgeLineText = inputLines[edgeLines];
                                // If there is an edge identifier "ARC"
                                if (edgeLineText.Contains("ARC"))
                                {
                                    // This is an edge
                                    Edge newEdge = new Edge();
                                    string begin1String = "a" + newMode.modeID.ToString() + "_";
                                    int startCut3 = edgeLineText.IndexOf(begin1String) + begin1String.Length;
                                    int endCut3 = edgeLineText.IndexOf("FROM");
                                    string edgeIDString = edgeLineText.Substring(startCut3, endCut3 - startCut3);
                                    newEdge.edgeID = Int32.Parse(edgeIDString);
                                    string begin2String = "FROM t" + newMode.modeID.ToString() + "_";
                                    int startCut = edgeLineText.IndexOf(begin2String) + begin2String.Length;
                                    int endCut = edgeLineText.IndexOf("TO");
                                    string fromString = edgeLineText.Substring(startCut, endCut - startCut);
                                    newEdge.start = Int32.Parse(fromString);
                                    string begin3String = "TO  t" + newMode.modeID.ToString() + "_";
                                    int ok = edgeLineText.IndexOf(begin3String);
                                    int startCut2 = edgeLineText.IndexOf(begin3String) + begin3String.Length;
                                    int endCut2 = edgeLineText.IndexOf("TYPE");
                                    string toString = edgeLineText.Substring(startCut2, endCut2 - startCut2);
                                    newEdge.end = Int32.Parse(toString);
                                    string begin4String = "TYPE";
                                    int startCut4 = edgeLineText.IndexOf(begin4String) + begin4String.Length;
                                    int endCut4 = edgeLineText.Length;
                                    string typeString = edgeLineText.Substring(startCut4, endCut4 - startCut4);
                                    newEdge.type = Int32.Parse(typeString);
                                    // Add this newly created edge to the mode
                                    newMode.commEdges.Add(newEdge);
                                    // Print Edge
                                    // Console.WriteLine(newEdge.print());
                                }
                                // If there is an core identifier "TASK"
                                if (edgeLineText.Contains("TASK "))
                                {
                                    // This is a core
                                    Core newCore = new Core();
                                    string begin1String = "t" + newMode.modeID.ToString() + "_";
                                    int startCut = edgeLineText.IndexOf(begin1String) + begin1String.Length;
                                    int endCut = edgeLineText.IndexOf("TYPE");
                                    string coreIDString = edgeLineText.Substring(startCut, endCut - startCut);
                                    newCore.id = Int32.Parse(coreIDString);
                                    // Add this newly created core to the collection, if it doesn't exsist already
                                    try
                                    {
                                        Core foundCore = cores.Find(x => x.id == newCore.id);
                                        if (foundCore == null)
                                        {
                                            cores.Add(newCore);
                                        }
                                    }
                                    catch
                                    {
                                        cores.Add(newCore);
                                    }
                                }
                            }
                            applicationModes.Add(newMode);
                        }
                    }
                    for (int lineNo = 0; lineNo < inputLines.Length; lineNo++)
                    {
                        int taskgraphBeginLine = lineNo;
                        int taskgraphEndLine = lineNo;
                        int commTypesEndLine = lineNo;
                        string lineText = inputLines[lineNo];

                        if (lineText.Contains("@COMMUN"))
                        {
                            // This is the start of communication types
                            // Check where this ends
                            for (int endingBracketLine = lineNo; endingBracketLine < inputLines.Length; endingBracketLine++)
                            {
                                if (inputLines[endingBracketLine].Contains("}"))
                                {
                                    // This is the ending
                                    commTypesEndLine = endingBracketLine;
                                    break;
                                }
                            }
                            // Get communication type number
                            int startSplit = lineText.IndexOf('N') + 1;
                            int endSplit = lineText.IndexOf('{');
                            string res = lineText.Substring(startSplit, endSplit - startSplit);
                            int tableNo = Int32.Parse(res);
                            if (tableNo == 0)
                            {
                                // Console.WriteLine("Found Communication Type Map for Mode: " + graphNo);
                                // Find that graph
                                // ApplicationMode m = applicationModes.Find(x => x.modeID == graphNo);
                                // Go through inside the communication lines and search for begining line
                                bool start = false;
                                for (int typeLines = lineNo; typeLines < commTypesEndLine; typeLines++)
                                {
                                    string edgeLineText = inputLines[typeLines];
                                    // If there is an edge identifier "# type    exec_time"
                                    if (edgeLineText.Contains("# type    CommunicationVolume"))
                                    {
                                        // Here starts our type map
                                        start = true;
                                    }
                                    if (edgeLineText.Contains("}"))
                                    {
                                        break;
                                    }
                                    if (start && !edgeLineText.Contains("# type    CommunicationVolume") && !edgeLineText.Contains("}"))
                                    {
                                        edgeLineText = edgeLineText.Substring(4, edgeLineText.Length - 4);
                                        // This a type map
                                        int endCut1 = edgeLineText.IndexOf("     ");
                                        string typeNoString = edgeLineText.Substring(0, endCut1);
                                        int typeNo = Int32.Parse(typeNoString);
                                        string begin2String = "     ";
                                        int startCut = edgeLineText.IndexOf(begin2String) + begin2String.Length;
                                        int endCut = edgeLineText.Length;
                                        string typeValueString = edgeLineText.Substring(startCut, endCut - startCut);
                                        float typeValue = float.Parse(typeValueString);
                                        // Now map it in the mode
                                        for (int modeID = 0; modeID < applicationModes.Count(); modeID++)
                                        {
                                            ApplicationMode m = applicationModes.ElementAt(modeID);
                                            for (int edgeID = 0; edgeID < m.commEdges.Count; edgeID++)
                                            {
                                                if (m.commEdges.ElementAt(edgeID).type == typeNo)
                                                {
                                                    m.commEdges.ElementAt(edgeID).volume = Math.Abs(typeValue);
                                                    // Print Edge
                                                    // Console.WriteLine(m.commEdges.ElementAt(edgeID).print());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    // Now that we have all application modes, let's build a XML file
                    XElement appElement = new XElement("Application", new XAttribute("Id", "0"), new XAttribute("Name", Path.GetFileNameWithoutExtension(saveFileDialog1.FileName)),
                            new XElement("Cores",
                                from core in cores
                                select
                                    new XElement("Core", new XAttribute("Id", core.id.ToString()), new XAttribute("Name", "Core " + core.id.ToString()))),
                            from mode in applicationModes
                            select
                                    new XElement("CoreGraph", new XAttribute("Id", mode.modeID.ToString()), new XAttribute("Name", "Mode " + mode.modeID.ToString()),
                                                new XElement("Edges",
                                                    from edge in mode.commEdges
                                                    select
                                                        new XElement("Edge",
                                                        new XAttribute("Id", edge.edgeID.ToString()),
                                                        new XAttribute("From", edge.start.ToString()),
                                                        new XAttribute("To", edge.end.ToString()),
                                                        new XAttribute("Volume", edge.volume.ToString())
                                                        )
                                                )
                                    )
                     );
                    XDocument doc = new XDocument(appElement);
                    // If the file already exsists, erase it completely
                    if (File.Exists(saveFileDialog1.FileName))
                    {
                        File.WriteAllText(saveFileDialog1.FileName, string.Empty);
                    }
                    doc.Save(saveFileDialog1.FileName);
                    Console.WriteLine("All Done");
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "Error Generating Application Core Graph", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void infoBtn_Click(object sender, EventArgs e)
        {
            InfoForm infoDialog = new InfoForm();
            infoDialog.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trafficTypeBox.SelectedIndex = 5;
        }

        private void generateBtn_Click(object sender, EventArgs e)
        {
            List<Core> cores = new List<Core>();
            List<ApplicationMode> aModes = new List<ApplicationMode>();
            generateTraffic(cores, aModes);
            // STEP 3: Generating XML from objects
            makeCoregraphXMLFile(cores, aModes);
        }

        private string addZeros(string addressInBits, int requiredSize)
        {
            if (addressInBits.Length > requiredSize)
            {
                // This should not be possible
                return "";
            }
            else
            {
                string temp = addressInBits;
                while (temp.Length != requiredSize)
                {
                    temp = "0" + temp;
                }
                return temp;
            }
        }

        private string getThatDigit(string id, int index)
        {
            string temp = id.Substring(id.Length - index - 1, 1);
            return temp;
        }

        private string everydayImShuffling(string id, int times)
        {
            string temp = id;
            for (int i = 0; i < times; i++)
            {
                temp = shuffle(temp);
            }
            return temp;
        }

        private void generateTraffic(List<Core> cores, List<ApplicationMode> aModes)
        {
            Random rnd = new Random();
            int noOfCores = 0;
            int noOfModes = 0;
            int packetSize = 0;
            try
            {
                noOfCores = Int32.Parse(coresTxtBox.Text);
                noOfModes = Int32.Parse(modeTxtBox.Text);
                packetSize = Int32.Parse(sizeTxtBox.Text);
            }
            catch (Exception esd)
            {
                MessageBox.Show("Correct the format of '# of Cores' or '# of Modes'\nDetailed Message:\n" + esd.Message, "Parse Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((trafficTypeBox.SelectedIndex == (int)trafficType.Tornado || trafficTypeBox.SelectedIndex == (int)trafficType.Neighbour) && (Math.Log(noOfCores, 2.00) % 1 != 0))
            {
                MessageBox.Show("Cannot generate Digit Permutated traffic for meshes which # of cores is not power of 2.\n 2^n = # of cores where n should be an integer. \n 2^(" + Math.Log(noOfCores, 2.00) + ") = " + noOfCores, "Generation Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int width = (int)Math.Ceiling(Math.Sqrt(noOfCores));
            int height = width;
            // k, aka radix, is the size of one dimension
            int k = height;
            // b, aka size of address in bits
            int b = (int)Math.Ceiling(Math.Log(noOfCores, 2.00));

            // STEP 1: Generating all cores
            for (int i = 0; i < noOfCores; i++)
            {
                Core c = new Core();
                c.id = i;
                cores.Add(c);
            }
            // STEP 2: Generating all application modes with edges
            for (int modeId = 0; modeId < noOfModes; modeId++)
            {
                ApplicationMode am = new ApplicationMode();
                am.modeID = modeId;
                List<Edge> edges = new List<Edge>();
                int unluckyCore = 0;
                if (trafficTypeBox.SelectedIndex == (int)trafficType.Hot_Spot)
                {
                    // Pick a hotspot
                    unluckyCore = rnd.Next(0, noOfCores);
                }
                for (int source = 0; source < noOfCores; source++)
                {
                    // Convert the source integer id to binary string id
                    string sourceId = Convert.ToString(source, 2);
                    sourceId = addZeros(sourceId, b);
                    Edge ee = new Edge();
                    ee.edgeID = source;
                    ee.start = source;
                    ee.volume = packetSize;
                    string destinationId = "";
                    switch (trafficTypeBox.SelectedIndex)
                    {
                        case (int)trafficType.Random:
                            {
                                // Random Selected
                                for (int dest = 0; dest < noOfCores; dest++)
                                {
                                    if (dest != source)
                                    {
                                        Edge e2 = new Edge();
                                        e2.edgeID = source;
                                        e2.start = source;
                                        e2.volume = packetSize;
                                        e2.end = dest;
                                        if (e2.end < noOfCores)
                                        {
                                            edges.Add(e2);
                                        }
                                    }
                                }
                                break;
                            }
                        case (int)trafficType.Random_Modal:
                            {
                                int edgesToGenForThisSource = rnd.Next(0, noOfCores);
                                List<int> pool = new List<int>();
                                for (int i = 0; i < noOfCores; i++)
                                {
                                    if (i != source)
                                    {
                                        pool.Add(i);
                                    }
                                }
                                for (int edgeNo = 0; edgeNo < edgesToGenForThisSource; edgeNo++)
                                {
                                    int pickedDest = rnd.Next(0, pool.Count);
                                    Edge e2 = new Edge();
                                    e2.edgeID = edges.Count;
                                    e2.start = source;
                                    e2.volume = packetSize;
                                    e2.end = pool.ElementAt(pickedDest);
                                    pool.RemoveAt(pickedDest);
                                    edges.Add(e2);
                                }
                                break;
                            }
                        case (int)trafficType.Bit_Reversal:
                            {
                                // Bit Reversal Selected
                                for (int i = 0; i < b; i++)
                                {
                                    destinationId = destinationId + getThatDigit(sourceId, i);
                                }
                                break;
                            }
                        case (int)trafficType.Shuffle:
                            {
                                // Shuffle Selected
                                if (source == 0)
                                {
                                    // This is the first one, assign it last one
                                    destinationId = Convert.ToString(noOfCores - 1, 2);
                                }
                                else if (source == noOfCores - 1)
                                {
                                    destinationId = Convert.ToString(0, 2);
                                }
                                else
                                {
                                    destinationId = everydayImShuffling(sourceId, modeId + 1);
                                }
                                break;
                            }
                        case (int)trafficType.Transpose_Matrix:
                            {
                                // Transpose Matrix Selected
                                for (int dest_digit = 0; dest_digit < b; dest_digit++)
                                {
                                    int numerator = (int)(dest_digit + (b / 2.00));
                                    int calculation = (int)(numerator % b);
                                    string newDestDigit = getThatDigit(sourceId, calculation);
                                    destinationId = newDestDigit + destinationId;
                                }
                                break;
                            }
                        case (int)trafficType.Tornado:
                            {
                                // Tornado Selected
                                for (int dest_digit = 0; dest_digit < b; dest_digit++)
                                {
                                    int numerator = (int)(Math.Ceiling(((double)k) / 2.00) - 1);
                                    int sourceDigit = Convert.ToInt32(getThatDigit(sourceId, dest_digit), 2);
                                    int calculation = (int)(sourceDigit + numerator % k);
                                    string newDestDigit = getThatDigit(Convert.ToString(calculation, 2), 0);
                                    destinationId = newDestDigit + destinationId;
                                }
                                break;
                            }
                        case (int)trafficType.Neighbour:
                            {
                                // Neighbor Selected
                                if (((source + 1) % k == 0) && (source != (noOfCores - 1)))
                                {
                                    // Last column but not right-bottom corner
                                    destinationId = Convert.ToString(source + 1, 2);
                                }
                                else if (source >= (noOfCores - k) && (source != (noOfCores - 1)))
                                {
                                    // Last row but not right-bottom corner
                                    int colNo = (source + 1) % k;
                                    destinationId = Convert.ToString(colNo, 2);
                                }
                                else if (source == (noOfCores - 1))
                                {
                                    // right - bottom corner
                                    destinationId = Convert.ToString(0, 2);
                                }
                                else
                                {
                                    destinationId = Convert.ToString(source + k + 1, 2);
                                }
                                break;
                            }
                        case (int)trafficType.Hot_Spot:
                            {
                                // Hot Spot Selected
                                destinationId = Convert.ToString(unluckyCore, 2);
                                break;
                            }
                    }
                    if (trafficTypeBox.SelectedIndex != (int)trafficType.Random && trafficTypeBox.SelectedIndex != (int)trafficType.Random_Modal)
                    {
                        ee.end = Convert.ToInt32(destinationId, 2);
                        if (ee.end < noOfCores && destinationId != sourceId)
                        {
                            // This should not be done for any random traffic pattern
                            edges.Add(ee);
                        }
                    }
                }
                am.commEdges = edges;
                aModes.Add(am);
            }

        }

        private string shuffle(string id)
        {
            string firstBit = id.Substring(0,1);
            string remaining = id.Substring(1, id.Length - 1);
            return (remaining + firstBit);
        }

        private void makeCoregraphXMLFile(List<Core> cores, List<ApplicationMode> appModes)
        {
            try
            {
                // Displays a SaveFileDialog so the user can save the Image
                // assigned to Button2.
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Application Graph XML|*.xml";
                saveFileDialog1.Title = "Save XML File";
                saveFileDialog1.ShowDialog();
                // If the file name is not an empty string open it for saving.
                if (saveFileDialog1.FileName != "")
                {
                    // Now that we have all application modes, let's build a XML file
                    XElement appElement = new XElement("Application", new XAttribute("Id", "0"), new XAttribute("Name", Path.GetFileNameWithoutExtension(saveFileDialog1.FileName)),
                        new XElement("Cores",
                            from core in cores
                            select
                                new XElement("Core", new XAttribute("Id", core.id.ToString()), new XAttribute("Name", "Core " + core.id.ToString()))),
                        from mode in appModes
                        select
                                new XElement("CoreGraph", new XAttribute("Id", mode.modeID.ToString()), new XAttribute("Name", "Mode " + mode.modeID.ToString()),
                                            new XElement("Edges",
                                                from edge in mode.commEdges
                                                select
                                                    new XElement("Edge",
                                                    new XAttribute("Id", edge.edgeID.ToString()),
                                                    new XAttribute("From", edge.start.ToString()),
                                                    new XAttribute("To", edge.end.ToString()),
                                                    new XAttribute("Volume", edge.volume.ToString())
                                                    )
                                            )
                                )
                    );
                    XDocument doc = new XDocument(appElement);
                    // If the file already exsists, erase it completely
                    if (File.Exists(saveFileDialog1.FileName))
                    {
                        File.WriteAllText(saveFileDialog1.FileName, string.Empty);
                    }
                    doc.Save(saveFileDialog1.FileName);
                    Console.WriteLine("All Done");
                }
            } catch (Exception ep) {
                MessageBox.Show(ep.Message, "Error Generating Application Core Graph XML file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveCoregraphXMLFile(List<Core> cores, List<ApplicationMode> appModes, String fileName)
        {
            try
            {
                // Now that we have all application modes, let's build a XML file
                XElement appElement = new XElement("Application", new XAttribute("Id", "0"), new XAttribute("Name", Path.GetFileNameWithoutExtension(fileName)),
                    new XElement("Cores",
                        from core in cores
                        select
                            new XElement("Core", new XAttribute("Id", core.id.ToString()), new XAttribute("Name", "Core " + core.id.ToString()))),
                    from mode in appModes
                    select
                            new XElement("CoreGraph", new XAttribute("Id", mode.modeID.ToString()), new XAttribute("Name", "Mode " + mode.modeID.ToString()),
                                        new XElement("Edges",
                                            from edge in mode.commEdges
                                            select
                                                new XElement("Edge",
                                                new XAttribute("Id", edge.edgeID.ToString()),
                                                new XAttribute("From", edge.start.ToString()),
                                                new XAttribute("To", edge.end.ToString()),
                                                new XAttribute("Volume", edge.volume.ToString())
                                                )
                                        )
                            )
                );
                XDocument doc = new XDocument(appElement);
                // If the file already exsists, erase it completely
                if (File.Exists(fileName))
                {
                    File.WriteAllText(fileName, string.Empty);
                }
                doc.Save(fileName);
                Console.WriteLine("All Done");
            }
            catch (Exception ep)
            {
                MessageBox.Show(ep.Message, "Error Generating Application Core Graph XML file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void unUsedFunction()
        {
            int noOfCores = 0;
            int noOfModes = 0;
            List<int> widths = new List<int>();
            for (int i = 1; i <= noOfCores; i++)
            {
                int remainder = noOfCores % i;
                if (remainder == 0)
                {
                    widths.Add(i);
                }
            }
            List<int> heights = new List<int>();
            foreach (int wid in widths)
            {
                heights.Add(noOfCores / wid);
            }
            List<int> diffs = new List<int>();
            for (int i = 0; i < widths.Count(); i++)
            {
                diffs.Add(Math.Abs(widths.ElementAt(i) - heights.ElementAt(i)));
            }
            int minIndex = diffs.IndexOf(diffs.Min());
            int width = widths.ElementAt(minIndex);
            int height = heights.ElementAt(minIndex);
        }

        private string getTrafficType(int typeNo)
        {
            switch(typeNo)
            {
                case (int)trafficType.Bit_Reversal: { return "Bit_Reversal"; }
                case (int)trafficType.Hot_Spot: { return "Hot_Spot"; }
                case (int)trafficType.Neighbour: { return "Neighbour"; }
                case (int)trafficType.Random: { return "Random_Uniform"; }
                case (int)trafficType.Random_Modal: { return "Random_Modal"; }
                case (int)trafficType.Shuffle: { return "Shuffle"; }
                case (int)trafficType.Tornado: { return "Tornado"; }
                case (int)trafficType.Transpose_Matrix: { return "Transpose_Matrix"; }
                default: return "Unknown";
            }
        }

        private void autoGenBtn_Click(object sender, EventArgs e)
        {
            string savePath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                savePath = folderBrowserDialog1.SelectedPath;
            }
            else
            {
                return;
            }
            sizeTxtBox.Text = "100";
            for(int trafficT = 0; trafficT < trafficTypeBox.Items.Count; trafficT++)
            {
                trafficTypeBox.SelectedIndex = trafficT;
                if(trafficT == (int)trafficType.Random || trafficT == (int)trafficType.Transpose_Matrix || trafficT == (int)trafficType.Tornado || trafficT == (int)trafficType.Neighbour)
                {
                    modeTxtBox.Text = "1";
                    for (int coresT = 16; coresT < 257; coresT = coresT * 2)
                    {
                        coresTxtBox.Text = coresT.ToString();
                        List<Core> cores = new List<Core>();
                        List<ApplicationMode> aModes = new List<ApplicationMode>();
                        generateTraffic(cores, aModes);
                        saveCoregraphXMLFile(cores, aModes, savePath + "\\" + getTrafficType(trafficT) + "_" + coresT.ToString() + "_Cores.xml");
                    }
                }
                else
                {
                    for (int modesT = 1; modesT < 6; modesT++)
                    {
                        modeTxtBox.Text = modesT.ToString();
                        for (int coresT = 16; coresT < 257; coresT = coresT * 2)
                        {
                            coresTxtBox.Text = coresT.ToString();
                            List<Core> cores = new List<Core>();
                            List<ApplicationMode> aModes = new List<ApplicationMode>();
                            generateTraffic(cores, aModes);
                            saveCoregraphXMLFile(cores, aModes, savePath + "\\" + getTrafficType(trafficT) + "_" + modesT.ToString() + "_Modes_" + coresT.ToString() + "_Cores.xml");
                        }
                    }
                }
            }
        }
    }
}
