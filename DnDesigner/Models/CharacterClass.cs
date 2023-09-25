namespace DnDesigner.Models
{
    public class CharacterClass
    {
        Character Character { get; set; }
        AdventurerClass Class { get; set; }
        AdventurerSubclass Subclass { get; set; }
        int Level { get; set; }
    }
}
