
///<summary>
/// Handles assigning the toast box and rolling d20s for
/// all skills and saves without a modifier
/// </summary>
/// <param name="button">The button that was clicked to call this function</param>
/// <param name="rollType">The type of roll to be made</param>
function assignToast(button, rollType)
{
    let toastBox = document.getElementById('rollToast');
    let toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastBox);

    toastBox.firstElementChild.firstElementChild.innerHTML = rollType;
    
    switch (button)
    {
        case "singleD20":
            toastBox.lastElementChild.innerHTML = RollD20();
            break;

        case "adv":
            toastBox.lastElementChild.innerHTML = RollAdvOrDis(true);
            break;

        case "dis":
            toastBox.lastElementChild.innerHTML = RollAdvOrDis(false);
            break;
    }
    toastBootstrap.show()
}

///<summary>
/// Handles assigning the toast box and rolling d20s for
/// all skills and saves with a modifier
/// </summary>
/// <param name="button">The button that was clicked to call this function</param>
/// <param name="rollType">The type of roll to be made</param>
/// <param name="modifier">The character's modifier for this roll</param>
function assignToastMod(button, rollType, modifier)
{
    let toastBox = document.getElementById('rollToast');
    let toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastBox);

    toastBox.firstElementChild.firstElementChild.innerHTML = rollType;

    switch (button)
    {
        case "singleD20_mod":
            toastBox.lastElementChild.innerHTML = RollD20Mod(modifier);
            break;

        case "adv_mod":
            toastBox.lastElementChild.innerHTML = RollAdvOrDisMod(true, modifier);
            break;

        case "dis_mod":
            toastBox.lastElementChild.innerHTML = RollAdvOrDisMod(false, modifier);
            break;
    }
    toastBootstrap.show()
}

/// <summary>
/// Generates a random number, simulates a die roll
/// </summary>
/// <param name="minValue">The minimum number on the die</param>
/// <param name="maxValue">How maximum number on the die</param>
/// <returns>A random roll of the given die</returns>
function generateRandomValue(minValue, maxValue)
{
    var random = Math.floor(Math.random() * (maxValue - minValue + 1)) + minValue;
    return random;
}

/// <summary>
/// Handles basic dice rolls from the character sheet.
/// Takes die size and die count from page and displays
/// an appropriate message.
/// </summary>
function DieRoller()
{
    let toastBox = document.getElementById('rollToast');
    let toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastBox);
    let dieCount = document.getElementById('dieCount').value;
    let dieSize = document.getElementById('dieSize').value;

    if (100 > dieCount && dieCount > 0)
    {
        toastBox.firstElementChild.firstElementChild.innerHTML = dieCount + "d" + dieSize;
        toastBox.lastElementChild.innerHTML = RollString(dieSize, dieCount);
    }
    else
    {
        toastBox.firstElementChild.firstElementChild.innerHTML = "How many dice?";
        toastBox.lastElementChild.innerHTML = "You must roll at least 1 die (99 max).";
    }
    toastBootstrap.show()
}

/// <summary>
/// Rolls a given size die a given number of times
/// </summary>
/// <param name="dieSize">The maximum number on the die</param>
/// <param name="dieCount">How many dice will be rolled</param>
/// <returns>The sum of the rolls as an int</returns>
function Roll(dieSize, dieCount)
{
    var rollTotal = generateRandomValue(1, dieSize);

    // if die count is greater than 1, roll and add to total
    for (var i = 1; i < dieCount; i++)
    {
        rollTotal += generateRandomValue(1, dieSize);
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
    var rollTotal = generateRandomValue(1, dieSize);
    var resultString = rollTotal.toString();

    for (var i = 1; i < dieCount; i++) {
        var currRoll = generateRandomValue(1, dieSize);
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
    var rollTotal = generateRandomValue(1, dieSize);
    var resultString = rollTotal.toString();

    for (var i = 1; i < dieCount; i++)
    {
        var currRoll = generateRandomValue(1, dieSize);
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
    var singleRoll = generateRandomValue(1, 20);
    return CheckNat20Or1(singleRoll);
}


/// <summary>
/// Rolls one d20
/// </summary>
/// <param name="modifier">The characters modifier for this roll</param>
/// <returns>The result of the roll</returns>
function RollD20Mod(modifier)
{
    var singleRoll = generateRandomValue(1, 20);
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
    var roll1 = generateRandomValue(1, 20);
    var roll2 = generateRandomValue(1, 20);

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
    var roll1 = generateRandomValue(1, 20);
    var roll2 = generateRandomValue(1, 20);

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

///<summary>
/// Rolls a given hit die and adds the characters 
/// constitution modifier to the result.
///</summary>
///<param name="button">The button that was clicked to call this function</param>
function RollHitDice(conMod) {
    let healthRolled = document.getElementById('totalHitDieRoll');
    let totalHealthRolled = parseInt(healthRolled.value);
    let hitDieSize = document.getElementById('spendHitDie').value;

    totalHealthRolled += RollMod(hitDieSize, 1, conMod);
    healthRolled.value = totalHealthRolled;

    switch (hitDieSize) {
        case "6":
            let hitDieUsed = document.querySelectorAll('d6HitDiceAvailable');
            let dieCount = hitDieUsed[0].charAt(0);
            dieCount--;
            hitDieUsed.forEach(charAt[0] = dieCount);
            break;

        case "8":
            ;
            break;

        case "10":
            ;
            break;
        case "12":
            ;
            break;
    }
}