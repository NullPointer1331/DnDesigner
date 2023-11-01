using System.Collections.Generic; 
namespace DnDesigner.Models.ImportModels
{ 

    public class Choose
    {
        public List<string> from { get; set; }
        public int count { get; set; }
        public int? amount { get; set; }
    }

}