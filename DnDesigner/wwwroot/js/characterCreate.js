function updateSubclasses(classNumber) {
    var levelSelector = document.getElementById("Classes_" + classNumber + "__0_");
    var classSelector = document.getElementById("Classes_" + classNumber + "__1_");
    var subclassSelector = document.getElementById("Classes_" + classNumber + "__2_");
    var classId = parseInt(classSelector.value);
    var level = parseInt(levelSelector.value);
    for (var i = 0; i < Model.availableClasses.length; i++) {
        if (Model.availableClasses[i].classId == classId) {
            if (level >= Model.availableClasses[i].subclassLevel) {
                subclassSelector.hidden = false;
            }
            else {
                subclassSelector.hidden = true;
            }
            subclassSelector.innerHTML = "";
            for (var j = 0; j < Model.availableClasses[i].subclasses.length; j++) {
                var subclass = Model.availableClasses[i].subclasses[j];
                var option = document.createElement("option");
                option.value = subclass.subclassId;
                option.text = subclass.name;
                subclassSelector.appendChild(option);
            }
            break;
        }
    }
}
function displaySubclasses(classNumber) {
    var levelSelector = document.getElementById("Classes_" + classNumber + "__0_");
    var classSelector = document.getElementById("Classes_" + classNumber + "__1_");
    var subclassSelector = document.getElementById("Classes_" + classNumber + "__2_");
    var classId = parseInt(classSelector.value);
    var level = parseInt(levelSelector.value);
    for (var i = 0; i < Model.availableClasses.length; i++) {
        if (Model.availableClasses[i].classId == classId) {
            if (level >= Model.availableClasses[i].subclassLevel) {
                subclassSelector.hidden = false;
            }
            else {
                subclassSelector.hidden = true;
            }
            break;
        }
    }
}
function addClassSelector() {
    if (numClasses < Model.availableClasses.length) {
        numClasses++;
        for (var i = 1; i < Model.availableClasses.length; i++) {
            var classDiv = document.getElementById("Class" + i);
            if (classDiv.hidden) {
                classDiv.hidden = false;
                document.getElementById("Classes_" + i + "__0_").value = 1;
                break;
            }
        }
    }
}
function hideClassSelector(classNumber) {
    var classDiv = document.getElementById("Class" + classNumber);
    classDiv.hidden = true;
    numClasses--;
    document.getElementById("Classes_" + classNumber + "__0_").value = 0;
}
function updateBackgroundDescription() {
    var backgroundSelector = document.getElementById("BackgroundId");
    var backgroundId = parseInt(backgroundSelector.value);
    for (var i = 0; i < Model.availableBackgrounds.length; i++) {
        if (Model.availableBackgrounds[i].backgroundId == backgroundId) {
            document.getElementById("descriptionBackground").innerHTML = Model.availableBackgrounds[i].description;
            break;
        }
    }
}
function updateRaceDescription() {
    var raceSelector = document.getElementById("RaceId");
    var raceId = parseInt(raceSelector.value);
    for (var i = 0; i < Model.availableRaces.length; i++) {
        if (Model.availableRaces[i].raceId == raceId) {
            document.getElementById("descriptionRace").innerHTML = Model.availableRaces[i].description;
            break;
        }
    }
}
function averageMaxHealth() {
    var maxHealth = 0;
    var constitutionSelector = document.getElementById("Constitution");
    var constitution = parseInt(constitutionSelector.value);
    var conMod = Math.floor((constitution - 10) / 2);
    for (var i = 0; i < Model.availableClasses.length; i++) {
        var levelSelector = document.getElementById("Classes_" + i + "__0_");
        var level = parseInt(levelSelector.value);
        if (level > 0) {
            var classSelector = document.getElementById("Classes_" + i + "__1_");
            var classId = parseInt(classSelector.value);
            var hitDie = 0;
            for (var j = 0; j < Model.availableClasses.length; j++) {
                if (Model.availableClasses[j].classId == classId) {
                    hitDie = Model.availableClasses[j].hitDie;
                    break;
                }
            }
            if (i == 0) {
                maxHealth += hitDie + conMod;
                maxHealth += ((hitDie / 2) + 1 + conMod) * (level - 1);
            }
            else {
                maxHealth += ((hitDie / 2) + 1 + conMod) * level;
            }
        }
    }
    document.getElementById("MaxHealth").value = maxHealth;
}

function updateContent() {
    // Get the selected radio button value
    var selectedValue = document.querySelector('input[name="generationMethod"]:checked').value;

    // get a list of the children of the stat container
    var pointBuyElements = document.querySelectorAll(".point-buy");
    var statInputElements = document.querySelectorAll("input.form-control");

    // check what the selected value is
    if (selectedValue == "manual") {
        // iterate trough the list of children
        pointBuyElements.forEach(function (element) {
            element.setAttribute("hidden", "true");
        });
        statInputElements.forEach(function (element) {
            element.removeAttribute("readonly");
        });
    } else if (selectedValue == "pointBuy") {
        // iterate trough the list of children
        pointBuyElements.forEach(function (element) {
            element.removeAttribute("hidden");
        });
        statInputElements.forEach(function (element) {
            element.value = "8";
            element.setAttribute("readonly", "true");
            document.getElementById("pointsRemaining").innerText = "27";
        });
    } else {
        // if the selected value is neither, display an error message
        console.log("Error: no stat generation method selected");
    }
}

function updatePointsRemaining(pointsRemaining) {
    // gets the element that displays the points remaining
    var pointRemainingElement = document.getElementById("pointsRemaining");
    pointRemainingElement.innerText = pointsRemaining;
}

function increaseStat() {
    console.log("stat increasing");
    var incrementButton = this;
    var buttonClasses = incrementButton.classList;
    var statElement;

    if (buttonClasses.contains("strength")) {
        statElement = document.querySelector("input.strength");
        console.log("strength");
    } else if (buttonClasses.contains("dexterity")) {
        statElement = document.querySelector("input.dexterity");
        console.log("dexterity");
    } else if (buttonClasses.contains("constitution")) {
        statElement = document.querySelector("input.constitution");
        console.log("constitution");
    } else if (buttonClasses.contains("intelligence")) {
        statElement = document.querySelector("input.intelligence");
        console.log("intelligence");
    } else if (buttonClasses.contains("wisdom")) {
        statElement = document.querySelector("input.wisdom");
        console.log("wisdom");
    } else if (buttonClasses.contains("charisma")) {
        statElement = document.querySelector("input.charisma");
        console.log("charisma");
    } else {
        console.log("Error: no stat element found");
        return;
    }

    var pointsRemaining = parseInt(document.getElementById("pointsRemaining").innerText);

    // if the value is less than 15, it increases the value
    if (statElement.value < 13 && pointsRemaining > 0) {
        statElement.value = parseInt(statElement.value) + 1;
        pointsRemaining--;
    } else if (statElement.value < 15 && pointsRemaining > 1) {
        statElement.value = parseInt(statElement.value) + 1;
        pointsRemaining -= 2;
    }

    // updates the points remaining
    updatePointsRemaining(pointsRemaining);
    console.log("stat increased");
}

function decreaseStat() {
    console.log("stat decreasing");
    // gets the value of the input
    var incrementButton = this;
    var buttonClasses = incrementButton.classList;
    var statElement;

    if (buttonClasses.contains("strength")) {
        statElement = document.querySelector("input.strength");
        console.log("strength");
    } else if (buttonClasses.contains("dexterity")) {
        statElement = document.querySelector("input.dexterity");
        console.log("dexterity");
    } else if (buttonClasses.contains("constitution")) {
        statElement = document.querySelector("input.constitution");
        console.log("constitution");
    } else if (buttonClasses.contains("intelligence")) {
        statElement = document.querySelector("input.intelligence");
        console.log("intelligence");
    } else if (buttonClasses.contains("wisdom")) {
        statElement = document.querySelector("input.wisdom");
        console.log("wisdom");
    } else if (buttonClasses.contains("charisma")) {
        statElement = document.querySelector("input.charisma");
        console.log("charisma");
    } else {
        console.log("Error: no stat element found");
        return;
    }

    var pointsRemaining = parseInt(document.getElementById("pointsRemaining").innerText);

    // if the value is greater than 8, it decreases the value
    if (statElement.value > 13) {
        statElement.value = parseInt(statElement.value) - 1;
        pointsRemaining += 2;
    } else if (statElement.value > 8) {
        statElement.value = parseInt(statElement.value) - 1;
        pointsRemaining++;
    }

    // updates the points remaining
    updatePointsRemaining(pointsRemaining);
    console.log("stat decreased");
}

// Add event listeners to the stat generation radio buttons
document.querySelectorAll('input[name="generationMethod"]').forEach(function (radioButton) {
    radioButton.addEventListener('change', updateContent);
});

// Add event listeners to the stat input fields
document.querySelectorAll('.increment').forEach(function (input) {
    input.addEventListener('click', increaseStat);
});

document.querySelectorAll('.decrement').forEach(function (input) {
    input.addEventListener('click', decreaseStat);
});