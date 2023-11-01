namespace DnDesigner.Models
{
    /// <summary>
    /// An interface for classes that modify a character
    /// </summary>
    public interface ICharacterModifier
    {
        /// <summary>
        /// Has this modifier been applied?
        /// </summary>
        public bool IsApplied { get; set; }

        /// <summary>
        /// Apply this modifier to the character
        /// </summary>
        /// <param name="character">The character to be modified</param>
        public void Apply(Character character);

        /// <summary>
        /// Remove this modifier from the character
        /// </summary>
        /// <param name="character">The character to be modified</param>
        public void Remove(Character character);
    }

    public class ModifyAttribute : ICharacterModifier
    {
        public bool IsApplied { get; set; }
        private string Attribute { get; set; }
        private int Value { get; set; }

        public ModifyAttribute(string attribute, int value)
        {
            Attribute = attribute;
            Value = value;
        }

        public void Apply(Character character)
        {
            if (!IsApplied)
            {
                character.ModifyAttribute(Attribute, Value);
                IsApplied = true;
            }
        }

        public void Remove(Character character)
        {
            if (IsApplied)
            {
                character.ModifyAttribute(Attribute, -Value);
                IsApplied = true;
            }
        }
    }
}
