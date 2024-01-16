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

    // for testing purposes
    console.log(selectedValue);
    var generationContainer = document.getElementById("generationContainer");
    if (selectedValue == "standardArray") {
        console.log("displaying standard array");

    } else if (selectedValue == "pointBuy") {
        generationContainer.innerHTML = "";
        console.log("displaying point buy");
    }
}
// Add event listeners to the stat generation radio buttons
document.querySelectorAll('input[name="generationMethod"]').forEach(function (radioButton) {
    radioButton.addEventListener('change', updateContent);
});
