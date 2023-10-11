using System.Collections.Generic; 
namespace DnDesigner.Models.ImportModels
{ 

    public class ClassRoot
    {
        public List<Class5ETools> @class { get; set; }
        public List<Subclass5ETools> subclass { get; set; }
        public List<ClassFeature5ETools> classFeature { get; set; }
        public List<SubclassFeature5ETools> subclassFeature { get; set; }
    }
    public class Class5ETools
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public bool srd { get; set; }
        public bool basicRules { get; set; }
        public Hd hd { get; set; }
        public List<string> proficiency { get; set; }
        public string spellcastingAbility { get; set; }
        public string casterProgression { get; set; }
        public string preparedSpells { get; set; }
        public List<int> cantripProgression { get; set; }
        public StartingProficiencies startingProficiencies { get; set; }
        public StartingEquipment startingEquipment { get; set; }
        public Multiclassing multiclassing { get; set; }
        public List<ClassTableGroup> classTableGroups { get; set; }
        public List<object> classFeatures { get; set; }
        public string subclassTitle { get; set; }
        public List<Fluff> fluff { get; set; }
    }
    public class Subclass5ETools
    {
        public string name { get; set; }
        public string shortName { get; set; }
        public string source { get; set; }
        public string className { get; set; }
        public string classSource { get; set; }
        public int page { get; set; }
        public List<AdditionalSpell> additionalSpells { get; set; }
        public List<string> subclassFeatures { get; set; }
        public bool? srd { get; set; }
        public bool? basicRules { get; set; }
        public bool? isReprinted { get; set; }
        public List<OtherSource> otherSources { get; set; }
    }
    public class ClassFeature5ETools
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public string className { get; set; }
        public string classSource { get; set; }
        public int level { get; set; }
        public bool isClassFeatureVariant { get; set; }
        public List<object> entries { get; set; }
        public bool? srd { get; set; }
        public bool? basicRules { get; set; }
        public Consumes consumes { get; set; }
        public int? header { get; set; }
    }
    public class SubclassFeature5ETools
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public string className { get; set; }
        public string classSource { get; set; }
        public string subclassShortName { get; set; }
        public string subclassSource { get; set; }
        public int level { get; set; }
        public List<object> entries { get; set; }
        public int? header { get; set; }
        public Consumes consumes { get; set; }
        public bool? isClassFeatureVariant { get; set; }
        public bool? srd { get; set; }
        public bool? basicRules { get; set; }
    }
}