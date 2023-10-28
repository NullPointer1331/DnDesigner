USING DnDesigner;

/*
	add at least two rows of test data to every necessary table, so that I can make a character sheet.
*/

INSERT INTO Backgrounds (BackgroundName, BackgroundDescription
						 , BackgroundFeature, BackgroundProficiencies
						 , BackgroundLanguages, BackgroundEquipment
						 , BackgroundGold)
VALUES ('TestName1', 'TestDescription1', 'TestFeature1'
		, 'TestProficiencies1', 'TestLanguages1', 'TestEquipment1', 0);
INSERT INTO Backgrounds (BackgroundName, BackgroundDescription
						 , BackgroundFeature, BackgroundProficiencies
						 , BackgroundLanguages, BackgroundEquipment
						 , BackgroundGold)
VALUES ('TestName2', 'TestDescription2', 'TestFeature2'
		, 'TestProficiencies2', 'TestLanguages2', 'TestEquipment2', 0);


INSERT INTO Classes (ClassName, ClassDescription, ClassHitDie, ClassPrimaryAbility
					, ClassSavingThrows, ClassArmorProficiencies, ClassWeaponProficiencies
					, ClassToolProficiencies, ClassSkillProficiencies, ClassEquipment
					, ClassSpellCastingAbility, ClassSpellSaveDC, ClassSpellAttackBonus
					, ClassSpellSlots, ClassSpellList)
VALUES ('TestName1', 'TestDescription1', 0, 'TestPrimaryAbility1'
		, 'TestSavingThrows1', 'TestArmorProficiencies1', 'TestWeaponProficiencies1'
		, 'TestToolProficiencies1', 'TestSkillProficiencies1', 'TestEquipment1'
		, 'TestSpellCastingAbility1', 0, 0, 0, 'TestSpellList1');
INSERT INTO Classes (ClassName, ClassDescription, ClassHitDie, ClassPrimaryAbility
					, ClassSavingThrows, ClassArmorProficiencies, ClassWeaponProficiencies
					, ClassToolProficiencies, ClassSkillProficiencies, ClassEquipment
					, ClassSpellCastingAbility, ClassSpellSaveDC, ClassSpellAttackBonus
					, ClassSpellSlots, ClassSpellList)
VALUES ('TestName2', 'TestDescription2', 0, 'TestPrimaryAbility2'
		, 'TestSavingThrows2', 'TestArmorProficiencies2', 'TestWeaponProficiencies2'
		, 'TestToolProficiencies2', 'TestSkillProficiencies2', 'TestEquipment2'
		, 'TestSpellCastingAbility2', 0, 0, 0, 'TestSpellList2');


INSERT INTO Items (ItemName, ItemDescription, ItemWeight, ItemCost, ItemType)
VALUES ('TestName1', 'TestDescription1', 0, 0, 'TestType1');
INSERT INTO Items (ItemName, ItemDescription, ItemWeight, ItemCost, ItemType)
VALUES ('TestName2', 'TestDescription2', 0, 0, 'TestType2');

INSERT INTO Races (RaceName, RaceDescription, RaceAbilityScoreIncrease
					, RaceAge, RaceAlignment, RaceSize, RaceSpeed
					, RaceLanguages, RaceTraits)
VALUES ('TestName1', 'TestDescription1', 'TestAbilityScoreIncrease1')
INSERT INTO Races (RaceName, RaceDescription, RaceAbilityScoreIncrease
					, RaceAge, RaceAlignment, RaceSize, RaceSpeed
					, RaceLanguages, RaceTraits)
VALUES ('TestName2', 'TestDescription2', 'TestAbilityScoreIncrease2')

INSERT INTO Spells (SpellName, SpellDescription, SpellLevel, SpellSchool
					, SpellCastingTime, SpellRange, SpellComponents
					, SpellDuration, SpellClasses, SpellRitual)
VALUES ('TestName1', 'TestDescription1', 0, 'TestSchool1'
		, 'TestCastingTime1', 'TestRange1', 'TestComponents1'
		, 'TestDuration1', 'TestClasses1', 'TestRitual1')
INSERT INTO Spells (SpellName, SpellDescription, SpellLevel, SpellSchool
					, SpellCastingTime, SpellRange, SpellComponents
					, SpellDuration, SpellClasses, SpellRitual)
VALUES ('TestName2', 'TestDescription2', 0, 'TestSchool2'
		, 'TestCastingTime2', 'TestRange2', 'TestComponents2'
		, 'TestDuration2', 'TestClasses2', 'TestRitual2')