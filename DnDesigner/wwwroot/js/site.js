// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function assignToast(button, toast) {
        let toastTrigger = document.getElementById(button)
        let toastBox = document.getElementById(toast)

        if (toastTrigger) {
            let toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastBox)
            toastBootstrap.show()
        }
}


function generateRandomValue(minValue, maxValue) {
    var random = Math.floor(Math.random() * (maxValue - minValue + 1)) + minValue;
    return random;
}

/// <summary>
/// Rolls a given size die a given number of times
/// </summary>
/// <param name="dieSize">The maximum number on the die</param>
/// <param name="dieCount">How many dice will be rolled</param>
/// <returns>The sum of the rolls as an int</returns>
function Roll(dieSize, dieCount) {
    var rollTotal = generateRandomValue(1, dieSize);

    // if die count is greater than 1, roll and add to total
    for (var i = 1; i < dieCount; i++) {
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
function Roll(dieSize, dieCount, modifier)
{
    return Roll(dieSize, dieCount) + modifier;
}

/// <summary>
/// Rolls a given size die a given number of times
/// </summary>
/// <param name="dieSize">The maximum number on the die</param>
/// <param name="dieCount">How many dice will be rolled</param>
/// <returns>The sum of the rolls as a string</returns>
function RollString(dieSize, dieCount) {
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
function RollString(dieSize, dieCount, modifier) {
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

    // add on modifier
    rollTotal += modifier;
    resultString += " + " + modifier.ToString() + " = " + rollTotal.ToString();

    return resultString;
}

/// <summary>
/// Checks if a roll is a 20
/// </summary>
/// <param name="roll">The roll to check</param>
/// <returns>Natural 20 or the original value</returns>
function CheckNat20(roll)
{
    if (roll == 20) {
        return "Natural 20!";
    }
    return roll.ToString();
}


/// <summary>
/// Rolls one d20
/// </summary>
/// <returns>The result of the roll</returns>
function RollD20()
{
    var singleRoll = generateRandomValue(1, 20);
    return CheckNat20(singleRoll);
}


/// <summary>
/// Rolls one d20
/// </summary>
/// <param name="modifier">The characters modifier for this roll</param>
/// <returns>The result of the roll</returns>
function RollD20(modifier)
{
    var singleRoll = generateRandomValue(1, 20);
    return CheckNat20(singleRoll) + " Modified by " + modifier.ToString() +
        " = " + (singleRoll + modifier).ToString();
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
    if (rollType) {
        return "You Keep A: " + CheckNat20(Math.Max(roll1, roll2)) +
            ", Lower Roll: " + CheckNat20(Math.Min(roll1, roll2));
    }
    // Disadvantage, take the lower roll
    return "You Keep A: " + CheckNat20(Math.Min(roll1, roll2)) +
        ", Higher Roll: " + CheckNat20(Math.Max(roll1, roll2));
}


/// <summary>
/// Rolls two d20 and returns the result
/// </summary>
/// <param name="rollType">True if roll is at advantage
///    , False if roll is at disadvantage</param>
/// <param name="modifier">The characters modifier for this roll</param>
/// <returns>The result of the roll</returns>
function RollAdvOrDis(rollType, modifier) {
    var roll1 = generateRandomValue(1, 20);
    var roll2 = generateRandomValue(1, 20);

    // Advantage, take the higher roll
    if (rollType) {
        return "You Keep A: " + CheckNat20(Math.Max(roll1, roll2)) +
            " Modified by " + modifier.ToString() +
            " = " + (Math.Max(roll1, roll2) + modifier).ToString() +
            ", Lower Roll: " + CheckNat20(Math.Min(roll1, roll2));
    }
    // Disadvantage, take the lower roll
    return "You Keep A: " + CheckNat20(Math.Min(roll1, roll2)) +
        " Modified by " + modifier.ToString() +
        " = " + (Math.Min(roll1, roll2) + modifier).ToString() +
        ", Higher Roll: " + CheckNat20(Math.Max(roll1, roll2));
}