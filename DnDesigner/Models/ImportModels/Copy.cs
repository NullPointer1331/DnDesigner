namespace DnDesigner.Models.ImportModels
{ 

    public class Copy
    {
        public string name { get; set; }
        public string source { get; set; }
        public Mod _mod { get; set; }
        public Preserve _preserve { get; set; }
    }

}