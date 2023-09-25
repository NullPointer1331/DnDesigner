namespace DnDesigner.Models
{
    public class Modifier
    {
        public Character Character { get; set; }
        public int Amount { get; set; }
        public ICharacterModifier CharacterModifier { get; set; }
    }
}
