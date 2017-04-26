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
                    for(int lineNo = 0; lineNo < inputLines.Length; lineNo++)
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
                            for(int endingBracketLine = lineNo; endingBracketLine < inputLines.Length; endingBracketLine++)
                            {
                                if(inputLines[endingBracketLine].Contains("}"))
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
                            for(int edgeLines = lineNo; edgeLines < taskgraphEndLine; edgeLines++)
                            {
                                string edgeLineText = inputLines[edgeLines];
                                // If there is an edge identifier "ARC"
                                if(edgeLineText.Contains("ARC"))
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
                                        if(foundCore == null)
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
                    XElement appElement = new XElement("Application", new XAttribute("Id","0"), new XAttribute("Name", Path.GetFileNameWithoutExtension(saveFileDialog1.FileName)),
                            new XElement("Cores", 
                                from core in cores
                                select
                                    new XElement("Core",new XAttribute("Id",core.id.ToString()), new XAttribute("Name","Core "+core.id.ToString()))),
                            from mode in applicationModes
                            select
                                    new XElement("CoreGraph", new XAttribute("Id",mode.modeID.ToString()), new XAttribute("Name","Mode "+mode.modeID.ToString()),
                                                new XElement("Edges", 
                                                    from edge in mode.commEdges
                                                    select
                                                        new XElement("Edge", 
                                                        new XAttribute("Id",edge.edgeID.ToString()), 
                                                        new XAttribute("From",edge.start.ToString()),
                                                        new XAttribute("To", edge.end.ToString()),
                                                        new XAttribute("Volume", edge.volume.ToString())
                                                        )
                                                )
                                    )
                     );
                    XDocument doc = new XDocument(appElement);
                    // If the file already exsists, erase it completely
                    if(File.Exists(saveFileDialog1.FileName))
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
    }
}
