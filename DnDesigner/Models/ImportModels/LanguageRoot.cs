namespace DnDesigner.Models.ImportModels
{
    public class LanguageRoot
    {
        public List<Language> language { get; set; }
        public List<LanguageScript> languageScript { get; set; }
    }
    public class Language
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public List<string> typicalSpeakers { get; set; }
        public string type { get; set; }
        public string script { get; set; }
        public bool? srd { get; set; }
        public bool? basicRules { get; set; }
        public List<object> entries { get; set; }
        public bool? hasFluffImages { get; set; }
        public List<string> fonts { get; set; }
        public List<AdditionalSource> additionalSources { get; set; }
    }
    public class LanguageScript
    {
        public string name { get; set; }
        public List<string> fonts { get; set; }
    }
}