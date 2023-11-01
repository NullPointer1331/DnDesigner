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

    public class Choice : ICharacterModifier
    {
        public bool IsApplied { get; set; }
        public int ChosenIndex { get; set; }
        public IEnumerable<ICharacterModifier> Options { get; private set; }

        public Choice(IEnumerable<ICharacterModifier> options)
        {
            Options = options;
        }

        public void Apply(Character character)
        {
            if(!IsApplied)
            {
                Options.ElementAt(ChosenIndex).Apply(character);
                IsApplied = true;
            }
        }

        public void Remove(Character character)
        {
            if (IsApplied)
            {
                Options.ElementAt(ChosenIndex).Remove(character);
                IsApplied = false;
            }
        }
    }

    /// <summary>
    /// When applied, this modifier will modify a character's attribute
    /// </summary>
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
                IsApplied = false;
            }
        }
    }
}
