namespace DnDesigner.Models
{
    /* I inteded to use this to help make FeatChoices work, but it seems to cause problems with the database
    public static class GlobalData
    {
        private static List<Feature> AllFeatures = new List<Feature>();

        private static List<Proficiency> AllProficiencies = new List<Proficiency>();

        public async static void LoadData(IDBHelper dBHelper)
        {
            AllFeatures = await dBHelper.GetAllFeatures();
            AllProficiencies = await dBHelper.GetAllProficiencies();
        }

        public static List<Feature> GetFeatures()
        {
            return AllFeatures;
        }

        public static List<Feat> GetFeats() 
        {
            return AllFeatures.Where(f => f is Feat).Cast<Feat>().ToList();
        }

        public static List<Proficiency> GetProficiencies()
        {
            return AllProficiencies;
        }

        public static List<Proficiency> GetSkills()
        {
            return AllProficiencies.Where(p => p.Type == "skill").ToList();
        }

        public static List<Proficiency> GetSavingThrows()
        {
            return AllProficiencies.Where(p => p.Type == "saving throw").ToList();
        }
    } */
}
