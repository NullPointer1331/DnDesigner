namespace DnDesigner.Models
{
    public class Background
    {
        public int BackgroundId { get; set; }
        public string Name { get; set; }
        public List<Proficiency> SkillProficiencies { get; set; }
        public string OtherProficiencies { get; set; }
        public List<string> Equipment { get; set; }
        public string BackgroundFeature { get; set; }
    }
}
