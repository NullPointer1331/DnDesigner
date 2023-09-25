namespace DnDesigner.Models
{
    public class Action
    {
        public string Name { get; set; }
        public string Description { get; set; }
        // Action, bonus action, reaction, minutes, etc.
        public string ActionTime { get; set; }
        public bool HasAttackRoll { get; set; }
        public bool HasSavingThrow { get; set; }
    }
}
