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

    // Get the containers for the two stat generation methods
    var standardArrayContainer = document.getElementById("standardArrayContainer");
    var pointBuyContainer = document.getElementById("pointBuyContainer");

    // Display the selected container and hide the others
    if (selectedValue == "standardArray") {
        standardArrayContainer.removeAttribute("hidden");
        pointBuyContainer.setAttribute("hidden", "true");

    } else if (selectedValue == "pointBuy") {
        pointBuyContainer.removeAttribute("hidden");
        standardArrayContainer.setAttribute("hidden", "true");
    }
}

function updatePointsRemaining() {
    // making the total points and points used
    var totalPoints = 27;
    var pointsUsed = 0;

    // gets the element that displays the points remaining
    var pointRemainingElement = document.getElementById("pointsRemaining");

    // gets all the inputs for the point buy system
    var pointBuyInputs = document.querySelectorAll('#pointBuyContainer input.form-control');

    // itterates through the inputs and adds the points used to the total points
    pointBuyInputs.forEach(function (input) {
        pointsUsed += parseInt(input.value) - 8;
        if (input.value > 13) {
            pointsUsed += parseInt(input.value) - 13;
        }
    });

    // solves for points remaining and updates the display
    totalPoints = totalPoints - pointsUsed;
    pointRemainingElement.innerText = totalPoints;
}

// Add event listeners to the stat generation radio buttons
document.querySelectorAll('input[name="generationMethod"]').forEach(function (radioButton) {
    radioButton.addEventListener('change', updateContent);
});

// Add event listeners to the stat input fields
document.querySelectorAll('#pointBuyContainer input').forEach(function (input) {
    input.addEventListener('change', updatePointsRemaining);
});