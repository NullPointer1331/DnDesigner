using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDesigner.Models
{
    public class Action
    {
        #region properties
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public int ActionId { get; set; }

        /// <summary>
        /// The name of the action
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the action
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The time it takes to use the action, 
        /// can be an action, bonus action, reaction, minutes, or hours
        /// </summary>
        public string ActionTime { get; set; }

        /// <summary>
        /// The range of the action
        /// </summary>
        public string Range { get; set; }

        /// <summary>
        /// The formula used to calculate the attack bonus, if any
        /// </summary>
        public string? AttackBonusCalculation { get; set; }

        /// <summary>
        /// Does the action require an attack roll
        /// </summary>
        public bool IsAttack { get { return AttackBonusCalculation != null; } }

        /// <summary>
        /// The formula used to calculate the saving throw DC, if any
        /// </summary>
        public string? SaveDCCalculation { get; set; }

        /// <summary>
        /// What attribute the saving throw is based on
        /// </summary>
        public string? SaveAttribute { get; set; }

        /// <summary>
        /// Does the action require a saving throw
        /// </summary>
        public bool IsSave { get { return SaveDCCalculation != null; } }

        /// <summary>
        /// The damage the action deals, if any
        /// </summary>
        public string? Damage { get; set; }

        /// <summary>
        /// The type of damage the action deals, if any
        /// </summary>
        public string? DamageType { get; set; }

        /// <summary>
        /// Does the action deal damage
        /// </summary>
        public bool IsDamaging { get { return Damage != null; } }
        #endregion

        public Action()
        {
            Name = "";
            Description = "";
            ActionTime = "";
            Range = "";
        }
    }

    [PrimaryKey("CharacterId", "ActionId")]
    public class CharacterAction
    {
        public Character Character { get; set; }
        public Action Action { get; set; }

        public int? AttackBonus { get
            {
                if (Action.AttackBonusCalculation != null)
                {
                    return Character.Calculate(Action.AttackBonusCalculation);
                }
                return null;
            } }

        public int? SaveDC { get
            {
                if (Action.SaveDCCalculation != null)
                {
                    return Character.Calculate(Action.SaveDCCalculation);
                }
                return null;
            } }

        public CharacterAction(Character character, Action action)
        {
            Character = character;
            Action = action;
        }
        private CharacterAction() { }
    }
}
