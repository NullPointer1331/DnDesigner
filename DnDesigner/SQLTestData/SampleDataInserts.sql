USING DnDesigner;

/*
	add at least two rows of test data to every necessary table, so that I can make a character sheet.
*/

INSERT INTO Backgrounds (Name, Sourcebook, StarterGold, Description)
VALUES ('TestName1', 'PHB', 0, 'TestDescription1');
INSERT INTO Backgrounds (Name, Sourcebook, StarterGold, Description)
VALUES ('TestName2', 'PHB', 0, 'TestDescription2');


INSERT INTO Classes (Name, Sourcebook, HitDie, SpellcastingId) 
VALUES ('TestName1', 'PHB', 8, 1);
INSERT INTO Classes (Name, Sourcebook, HitDie, SpellcastingId)
VALUES ('TestName2', 'PHB', 10, 2);


INSERT INTO Items (Name, Sourcebook, Description, Price, Weight
				   , Equipable, Attuneable, Traits)
VALUES ('TestName1', 'PHB', 'TestDescription1', 0, 0, 1, 0, 'TestTraits1');
INSERT INTO Items (Name, Sourcebook, Description, Price, Weight
				   , Equipable, Attuneable, Traits)
VALUES ('TestName2', 'PHB', 'TestDescription2', 0, 0, 1, 1, 'TestTraits2');


INSERT INTO Races (Name, Sourcebook, Description, StatBonuses, Size, Speed)
VALUES ('TestName1', 'PHB', 'TestDescription1', 'TestStatBonuses1', 'TestSize1', 0);
INSERT INTO Races (Name, Sourcebook, Description, StatBonuses, Size, Speed)
VALUES ('TestName2', 'PHB', 'TestDescription2', 'TestStatBonuses2', 'TestSize2', 0);


INSERT INTO Spellcasting (Name, SpellcastingAttribute, SpellcastingType
						  , PreparedCasting, RitualCasting, Spellbook)
VALUES ('TestName1', 'TestSpellcastingAttribute1', 'TestSpellcastingType1'
		, 0, 0, 0);
INSERT INTO Spellcasting (Name, SpellcastingAttribute, SpellcastingType
						  , PreparedCasting, RitualCasting, Spellbook)
VALUES ('TestName2', 'TestSpellcastingAttribute2', 'TestSpellcastingType2'
		, 1, 1, 1);


INSERT INTO Spells (Name, Sourcebook, SpellLevel, SpellSchool, CastingTime
					, Range, Components, Duration, Description, IsRitual
					, RequiresConcentration)
VALUES ('TestName1', 'PHB', 0, 'TestSpellSchool1', 'TestCastingTime1', 'TestRange1'
		, 'TestComponents1', 'TestDuration1', 'TestDescription1', 0, 0);
INSERT INTO Spells (Name, Sourcebook, SpellLevel, SpellSchool, CastingTime
					, Range, Components, Duration, Description, IsRitual
					, RequiresConcentration)
VALUES ('TestName2', 'PHB', 0, 'TestSpellSchool2', 'TestCastingTime2', 'TestRange2'
		, 'TestComponents2', 'TestDuration2', 'TestDescription2', 1, 1);