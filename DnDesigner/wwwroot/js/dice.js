
///<summary>
/// Assigns and displays the toast box on screen
///</summary>
///<param name="header">The text to be displayed in the toast's header</param>
///<param name="body">The text to be displayed in the toast's body</param>
function AssignToast(header, body) {
    let toastBox = document.getElementById('rollToast');
    let toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastBox);

    toastBox.firstElementChild.firstElementChild.innerHTML = header;
    toastBox.lastElementChild.innerHTML = body;

    toastBootstrap.show();
}

///<summary>
/// Handles all skills and saves without a modifier
/// </summary>
/// <param name="button">The button that was clicked to call this function</param>
/// <param name="rollType">The type of roll to be made</param>
function SkillOrSaveRoll(button, rollType)
{
    let roll;
    switch (button)
    {
        case "singleD20":
            roll = RollD20();
            break;

        case "adv":
            roll = RollAdvOrDis(true);
            break;

        case "dis":
            roll = RollAdvOrDis(false);
            break;
    }
    AssignToast(rollType, roll);
}

///<summary>
/// Handles all skills and saves with a modifier
/// </summary>
/// <param name="button">The button that was clicked to call this function</param>
/// <param name="rollType">The type of roll to be made</param>
/// <param name="modifier">The character's modifier for this roll</param>
function SkillOrSaveModRoll(button, rollType, modifier)
{
    let roll;
    switch (button)
    {
        case "singleD20_mod":
            roll = RollD20Mod(modifier);
            break;

        case "adv_mod":
            roll = RollAdvOrDisMod(true, modifier);
            break;

        case "dis_mod":
            roll = RollAdvOrDisMod(false, modifier);
            break;
    }
    AssignToast(rollType, roll);
}

/// <summary>
/// Generates a random number, simulates a die roll
/// </summary>
/// <param name="minValue">The minimum number on the die</param>
/// <param name="maxValue">How maximum number on the die</param>
/// <returns>A random roll of the given die</returns>
function GenerateRandomValue(minValue, maxValue)
{
    var random = Math.floor(Math.random() * (maxValue - minValue + 1)) + minValue;
    return random;
}

/// <summary>
/// Rolls a given size die a given number of times
/// </summary>
/// <param name="dieSize">The maximum number on the die</param>
/// <param name="dieCount">How many dice will be rolled</param>
/// <returns>The sum of the rolls as an int</returns>
function Roll(dieSize, dieCount)
{
    var rollTotal = GenerateRandomValue(1, dieSize);

    // if die count is greater than 1, roll and add to total
    for (var i = 1; i < dieCount; i++)
    {
        rollTotal += GenerateRandomValue(1, dieSize);
    }
    return rollTotal;
}

/// <summary>
/// Rolls a given size die a given number of times
/// </summary>
/// <param name="dieSize">The maximum number on the die</param>
/// <param name="dieCount">How many dice will be rolled</param>
/// <param name="modifier">The characters modifier for this roll</param>
/// <returns>The sum of the rolls as an int</returns>
function RollMod(dieSize, dieCount, modifier)
{
    return Roll(dieSize, dieCount) + modifier;
}

/// <summary>
/// Rolls a given size die a given number of times
/// </summary>
/// <param name="dieSize">The maximum number on the die</param>
/// <param name="dieCount">How many dice will be rolled</param>
/// <returns>The sum of the rolls as a string</returns>
function RollString(dieSize, dieCount)
{
    var rollTotal = GenerateRandomValue(1, dieSize);
    var resultString = rollTotal.toString();

    for (var i = 1; i < dieCount; i++) {
        var currRoll = GenerateRandomValue(1, dieSize);
        rollTotal += currRoll;
        resultString += " + " + currRoll.toString();
    }
    if (dieCount > 1) {
        resultString += " = " + rollTotal.toString();
    }
    return resultString;    
}

/// <summary>
/// Rolls a given size die a given number of times
/// </summary>
/// <param name="dieSize">The maximum number on the die</param>
/// <param name="dieCount">How many dice will be rolled</param>
/// <param name="modifier">The characters modifier for this roll</param>
/// <returns>The sum of the rolls as a string</returns>
function RollStringMod(dieSize, dieCount, modifier)
{
    var rollTotal = GenerateRandomValue(1, dieSize);
    var resultString = rollTotal.toString();

    for (var i = 1; i < dieCount; i++)
    {
        var currRoll = GenerateRandomValue(1, dieSize);
        rollTotal += currRoll;
        resultString += " + " + currRoll.toString();
    }
    if (dieCount > 1) {
        resultString += " = " + rollTotal.toString();
    }

    // add on modifier
    rollTotal += modifier;
    resultString += " + " + modifier.toString() + " = " + rollTotal.toString();

    return resultString;
}

/// <summary>
/// Checks if a roll is a 20 or a 1
/// </summary>
/// <param name="roll">The roll to check</param>
/// <returns>Natural 20, Natural 1, or the original value</returns>
function CheckNat20Or1(roll)
{
    if (roll == 20)
    {
        return "Natural 20!";
    }
    else if (roll == 1)
    {
        return "Oh no, Natural 1!";
    }
    return roll.toString();
}


/// <summary>
/// Rolls one d20
/// </summary>
/// <returns>The result of the roll</returns>
function RollD20()
{
    var singleRoll = GenerateRandomValue(1, 20);
    return CheckNat20Or1(singleRoll);
}


/// <summary>
/// Rolls one d20
/// </summary>
/// <param name="modifier">The characters modifier for this roll</param>
/// <returns>The result of the roll</returns>
function RollD20Mod(modifier)
{
    var singleRoll = GenerateRandomValue(1, 20);
    var total = parseInt(singleRoll) + parseInt(modifier);

    if (modifier > 0)
    {
        return CheckNat20Or1(singleRoll) + " + " + modifier +
                " = " + total;
    }
    return CheckNat20Or1(singleRoll) + " - " + Math.abs(modifier) +
        " = " + total;
}


/// <summary>
/// Rolls two d20 and returns the result
/// </summary>
/// <param name="rollType">True if roll is at advantage
///    , False if roll is at disadvantage</param>
/// <returns>The result of the roll</returns>
function RollAdvOrDis(rollType)
{
    var roll1 = GenerateRandomValue(1, 20);
    var roll2 = GenerateRandomValue(1, 20);

    // Advantage, take the higher roll
    if (rollType)
    {
        return "You Keep: " + CheckNat20Or1(Math.max(parseInt(roll1), parseInt(roll2))) +
            "<br/>Lower Roll: " + CheckNat20Or1(Math.min(parseInt(roll1), parseInt(roll2)));
    }
    // Disadvantage, take the lower roll
    return "You Keep: " + CheckNat20Or1(Math.min(parseInt(roll1), parseInt(roll2))) +
        "<br/>Higher Roll: " + CheckNat20Or1(Math.max(parseInt(roll1), parseInt(roll2)));
}


/// <summary>
/// Rolls two d20 and returns the result
/// </summary>
/// <param name="rollType">True if roll is at advantage
///    , False if roll is at disadvantage</param>
/// <param name="modifier">The characters modifier for this roll</param>
/// <returns>The result of the roll</returns>
function RollAdvOrDisMod(rollType, modifier) {
    var roll1 = GenerateRandomValue(1, 20);
    var roll2 = GenerateRandomValue(1, 20);

    // Advantage, take the higher roll
    if (rollType) {
        if (modifier > 0) {
            return "You Keep: " + CheckNat20Or1(Math.max(parseInt(roll1), parseInt(roll2))) +
                " + " + modifier +
                " = " + (Math.max(parseInt(roll1), parseInt(roll2)) + parseInt(modifier)).toString() +
                "<br/>Lower Roll: " + CheckNat20Or1(Math.min(parseInt(roll1), parseInt(roll2)));
        }
        return "You Keep: " + CheckNat20Or1(Math.max(parseInt(roll1), parseInt(roll2))) +
            " - " + Math.abs(modifier) +
            " = " + (Math.max(parseInt(roll1), parseInt(roll2)) + parseInt(modifier)).toString() +
            "<br/>Lower Roll: " + CheckNat20Or1(Math.min(parseInt(roll1), parseInt(roll2)));
    }
    // Disadvantage, take the lower roll
    else
    {
        if (modifier > 0)
        {
            return "You Keep: " + CheckNat20Or1(Math.min(parseInt(roll1), parseInt(roll2))) +
                " + " + modifier +
                " = " + (Math.min(parseInt(roll1), parseInt(roll2)) + parseInt(modifier)).toString() +
                "<br/>Higher Roll: " + CheckNat20Or1(Math.max(parseInt(roll1), parseInt(roll2)));
        }
        return "You Keep: " + CheckNat20Or1(Math.min(parseInt(roll1), parseInt(roll2))) +
            " - " + Math.abs(modifier) +
            " = " + (Math.min(parseInt(roll1), parseInt(roll2)) + parseInt(modifier)).toString() +
            "<br/>Higher Roll: " + CheckNat20Or1(Math.max(parseInt(roll1), parseInt(roll2)));
    }
}

/// <summary>
/// Handles basic dice rolls from the character sheet.
/// </summary>
function DieRoller() {
    let dieCount = document.getElementById('dieCount').value;
    let dieSize = document.getElementById('dieSize').value;

    let dice;
    let roll;
    if (100 > dieCount && dieCount > 0) {
        dice = dieCount + "d" + dieSize;
        roll = RollString(dieSize, dieCount);
    }
    else {
        dice = "How many dice?";
        roll = "You must roll at least 1 die (99 max).";
    }
    AssignToast(dice, roll);
}

///<summary>
/// Rolls a given hit die and adds the characters 
/// constitution modifier to the result.
///</summary>
///<param name="conMod">The character's constitution modifier to be added to the roll</param>
function RollHitDice(conMod) {
    let hitDieSize = document.getElementById('spendHitDie').value;
    let hitDieUsed;
    let dieCount;
    switch (hitDieSize) {
        case "6":
            hitDieUsed = document.querySelectorAll("[id='d6HitDiceAvailable']");
            dieCount = parseInt(hitDieUsed[0].value);
            break;
        case "8":
            hitDieUsed = document.querySelectorAll("[id='d8HitDiceAvailable']");
            dieCount = parseInt(hitDieUsed[0].value);
            break;
        case "10":
            hitDieUsed = document.querySelectorAll("[id='d10HitDiceAvailable']");
            dieCount = parseInt(hitDieUsed[0].value);
            break;
        case "12":
            hitDieUsed = document.querySelectorAll("[id='d12HitDiceAvailable']");
            dieCount = parseInt(hitDieUsed[0].value);
            break;
    }
    if (dieCount > 0) {
        dieCount--;
        for (let i = 0; i < hitDieUsed.length; i++) {
            hitDieUsed[i].value = dieCount;
        }

        let healthRolled = document.getElementById('totalHitDieRoll');
        let totalHealthRolled = parseInt(healthRolled.value);

        let roll = RollMod(hitDieSize, 1, conMod);
        if (roll <= 0) {
            let header = "Bad Luck";
            let body = "You didn't get any health from this die.";
            AssignToast(header, body);
        }
        else {
            totalHealthRolled += roll;
            healthRolled.value = totalHealthRolled;
        }
    }
    else {
        let header = "No d" + hitDieSize + " hit dice remaining.";
        let body = "You can't roll hit dice that aren't available.";
        AssignToast(header, body);
    }
}

///<summary>
/// Updates the amount of hit dice available
///</summary>
function UpdateHitDice() {
    let lockedHitDice = document.getElementById('lockedHitDice');
    let lockedInputs = lockedHitDice.getElementsByTagName('input');
    let unlockedHitDice = document.getElementById('unlockedHitDice');
    let unlockedInputs = unlockedHitDice.getElementsByTagName('input');

    for (let i = 0; i < lockedInputs.length; i++) {
        lockedInputs[i].value = unlockedInputs[i].value;
    }
}

///<summary>
/// Adds the total of health rolled from hit dice to
/// the character's current health.
/// Then sets the total health rolled to 0.
///</summary>
function AddRolledHealth() {
    let totalHitDieRoll = document.getElementById('totalHitDieRoll');
    let healthRolled = parseInt(totalHitDieRoll.value);
    let currentHealth = document.getElementById('currentHealth');
    let actualCurrentHealth = parseInt(currentHealth.value);
    let maxHealth = parseInt(document.getElementById('currentHealth').max);
    let newTotal = healthRolled + actualCurrentHealth;
    if (newTotal < maxHealth) {
        currentHealth.value = newTotal;
    }
    else {
        currentHealth.value = maxHealth;
    }
    totalHitDieRoll.value = 0;
}