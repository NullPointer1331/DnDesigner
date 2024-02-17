
///<summary>
/// Assigns and displays the toast box on screen
///</summary>
///<param name="header">The text to be displayed in the toast's header</param>
///<param name="body">The text to be displayed in the toast's body</param>
function AssignToast(header, body) {
    let toastBox = GetById('rollToast');
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

///<summary>
/// Adds an item to the Inventory accordion of the
/// Character Sheet
///</summary>
///<param name="id">The id to be used by the anchor tag</param>
///<param name="name">The name of the item</param>
///<param name="sourcebook">The sourcebook the item is from</param>
///<param name="traits">The traits of the item</param>
///<param name="description">The description of the item</param>
function AddItem(id, name, sourcebook, traits, description) {
        // get elements to place new item in
        let links = GetById('inventoryLinks');
        let list = GetById('inventoryList');

        // create new item anchor 
        let newLink = document.createElement("a");
        newLink.class = "p-1 rounded";
        newLink.href = "#inventory" + id;
        newLink.innerHTML = name;

        // create new item name
        let newListItemName = document.createElement("h4");
        newListItemName.id = "inventory" + id;
        newListItemName.innerHTML = name;

        // create new item quantity div
        let newListItemQuantity = document.createElement("div");
        newListItemQuantity.class = "input-group w-25";
    
        // create new quantity label
        let quantityLabel = document.createElement("label");
        quantityLabel.class = "input-group-text";
        quantityLabel.innerHTML = "Quantity";
    
        // create new quantity input
        let quantityInput = document.createElement("input");
        quantityInput.type = "number";
        quantityInput.value = GetById(id).value;
        quantityInput.min = "0";
        quantityInput.class = "form-control";
        quantityInput.ariaLabel = "Item quantity";
    
        // append label and input to div
        newListItemQuantity.appendChild(quantityLabel);
        newListItemQuantity.appendChild(quantityInput);
        

        // create new item source
        let newListItemSource = document.createElement("h5");
        newListItemSource.innerHTML = sourcebook;

        // create new item trait
        let newListItemTrait = document.createElement("p");
        newListItemTrait.innerHTML = traits;

        // create new item description
        let newListItemDescription = document.createElement("p");
        newListItemDescription.innerHTML = description;


        // append new anchor
        links.appendChild(newLink);

        // append new item
        list.appendChild(newListItemName);
        list.appendChild(newListItemQuantity);
        list.appendChild(newListItemSource);
        list.appendChild(newListItemTrait);
        list.appendChild(newListItemDescription);
}

///<summary>
/// Convenience function
///</summary>
///<param name="id">The id of the element to get</param>
///<returns>The element with the given id</returns>
function GetById(id) {
    return document.getElementById(id);
}