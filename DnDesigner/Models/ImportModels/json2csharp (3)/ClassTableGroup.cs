using System.Collections.Generic; 
namespace DnDesigner.5eTools{ 

    public class ClassTableGroup
    {
        public List<string> colLabels { get; set; }
        public List<List<int>> rows { get; set; }
        public string title { get; set; }
        public List<List<int>> rowsSpellProgression { get; set; }
    }

}