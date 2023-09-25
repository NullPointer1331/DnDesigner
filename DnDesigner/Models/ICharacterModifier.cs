namespace DnDesigner.Models
{
    /// <summary>
    /// This interface is used to mark classes that modify a character's stats and abilities.
    /// </summary>
    public interface ICharacterModifier
    {
        // This is a way we could apply the effects of class features, feats, and items to a character.
        // We would need a class to implement this interface for each type of modifier.
        
        // Type is a way to differentiate between the classes that implement this interface.
        string Type { get; }

        abstract void Apply(Character character, int amount);

        abstract void Remove(Character character, int amount);
    }
}
