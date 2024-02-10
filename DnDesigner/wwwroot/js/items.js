
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
/// Takes in information from the character sheet and passes it to AssignToast
///</summary>
///<param name="itemName">The name of the item</param>
///<param name="itemDescription">The description of the item</param>
function ShowDescription(itemName, itemDescription) {
    AssignToast(itemName, itemDescription);
}