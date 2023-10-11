using System.Collections.Generic; 
namespace DnDesigner.5eTools{ 

    public class StartingEquipment
    {
        public bool additionalFromBackground { get; set; }
        public List<string> @default { get; set; }
        public string goldAlternative { get; set; }
        public List<DefaultDatum> defaultData { get; set; }
    }

}