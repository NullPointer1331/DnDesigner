
///<summary>
/// Adds an item to the Inventory accordion of the
/// Character Sheet
///</summary>
///<param name="id">The id to be used by the anchor tag</param>
///<param name="name">The name of the item</param>
///<param name="sourcebook">The sourcebook the item is from</param>
///<param name="traits">The traits of the item</param>
///<param name="price">The price of the item</param>
///<param name="weight">The weight of the item</param>
///<param name="attuneable">If the item requires attunement</param>
///<param name="description">The description of the item</param>
///<param name="itemId">The unique ID number of the item</param>
function AddItem(id, name, sourcebook, traits, price, weight, attunement
    , equipable, description, itemId) {
    // check for quantity
    let quantity = parseInt(GetById(id).value);
    let header;
    if (quantity < 2) {
        header = "Item Added!";
    }
    else {
        header = "Items Added!";
    }
    let body = quantity + " " + name + " added to your Inventory";
    
    if (quantity > 0) { // actually adds something

        // get CharacterId
        let characterId = GetById('characterId').value;

        if (ItemNotInInventory(itemId)) { // check if item can be added

            // get elements to place new item in
            let links = GetById('inventoryLinks');
            let list = GetById('inventoryList');

            // create new item anchor 
            let newLink = Create("a");
            newLink.class = "p-1 rounded";
            newLink.href = "#item" + itemId;
            newLink.id = "itemLink" + itemId;
            newLink.innerHTML = name;

            // create new item div
            let newListItemDiv = Create("div");
            newListItemDiv.id = "item" + itemId;

            // create new item name
            let newListItemName = Create("h4");
            newListItemName.innerHTML = name;

            // create new item quantity div
            let newListItemQuantity = Create("div");
            newListItemQuantity.setAttribute("class", "input-group w-50");

            // create new quantity label
            let quantityLabel = Create("label");
            quantityLabel.setAttribute("class", "input-group-text");
            quantityLabel.innerHTML = "Quantity";
            newListItemQuantity.appendChild(quantityLabel);

            // create new quantity input
            let quantityInput = Create("input");
            quantityInput.type = "number";
            quantityInput.value = quantity;
            quantityInput.min = "0";
            quantityInput.setAttribute("class", "form-control");
            quantityInput.ariaLabel = "Item quantity";
            quantityInput.setAttribute("onchange", `UpdateQuantity(${itemId}, this.value)`);
            newListItemQuantity.appendChild(quantityInput);

            // create buttons
            if (equipable != 0) {
                let equipButton = Create("button");
                equipButton.setAttribute("class", "btn btn-primary");
                equipButton.innerHTML = "Equip";
                equipButton.setAttribute("onclick", `EquipItem(${itemId})`);
                newListItemQuantity.appendChild(equipButton);

                let unequipButton = Create("button");
                unequipButton.setAttribute("class", "btn btn-primary");
                unequipButton.innerHTML = "Unequip";
                unequipButton.setAttribute("onclick", `UnequipItem(${itemId})`);
                unequipButton.hidden = true;
                newListItemQuantity.appendChild(unequipButton);
            }
            else if (attunement) {
                let attuneButton = Create("button");
                attuneButton.setAttribute("class", "btn btn-primary");
                attuneButton.innerHTML = "Attune";
                attuneButton.setAttribute("onclick", `AttuneItem(${itemId})`);
                newListItemQuantity.appendChild(attuneButton);

                let unattuneButton = Create("button");
                unattuneButton.setAttribute("class", "btn btn-primary");
                unattuneButton.innerHTML = "Unattune";
                unattuneButton.setAttribute("onclick", `UnattuneItem(${itemId})`);
                unattuneButton.hidden = true;
                newListItemQuantity.appendChild(unattuneButton);
            }

            let removeButton = Create("button");
            removeButton.setAttribute("class", "btn btn-danger");
            removeButton.innerHTML = "Remove";
            removeButton.setAttribute("onclick", `RemoveItem(${itemId})`);
            newListItemQuantity.appendChild(removeButton);


            // create new item source
            let newListItemSource = Create("h5");
            newListItemSource.innerHTML = sourcebook;

            // create new item trait
            let newListItemTrait = Create("h6");
            newListItemTrait.innerHTML = traits;

            // create new item price
            let newListItemPrice = Create("h6");
            newListItemPrice.innerHTML = "Price: " + price;

            // create new item weight
            let newListItemWeight = Create("h6");
            newListItemWeight.innerHTML = "Weight: " + weight;

            // create new item description
            let newListItemDescription = Create("p");
            newListItemDescription.innerHTML = description;


            // append new anchor
            links.appendChild(newLink);

            // append item details to div
            newListItemDiv.appendChild(newListItemName);
            newListItemDiv.appendChild(newListItemQuantity);
            newListItemDiv.appendChild(newListItemSource);
            newListItemDiv.appendChild(newListItemTrait);

            // create attunement if needed
            if (attunement) {
                let attunementRequired = Create("h6");
                attunementRequired.innerHTML = "Requires Attunement";
                newListItemDiv.appendChild(attunementRequired);
            }

            newListItemDiv.appendChild(newListItemPrice);
            newListItemDiv.appendChild(newListItemWeight);


            
            newListItemDiv.appendChild(newListItemDescription);

            // append new div to list
            if (list.childNodes.length != 1) { // if 1st item don't add HR
                let hr = Create("hr");
                list.appendChild(hr);
            }            
            list.appendChild(newListItemDiv);

            // construct call string
            let callString = "characterId=" + characterId + "&itemId=" + itemId +
                "&quantity=" + quantity;

            // trigger controller to add item to inventory
            let xhttp = new XMLHttpRequest();
            xhttp.open("POST", "/Characters/AddItem", true);
            xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xhttp.send(callString);

        }
        else { 
            // get existing item
            let existingItem = GetById("item" + itemId);
            // get existing quantity
            let existingQuantity = parseInt(existingItem.childNodes[1].childNodes[1].value);
            // add new quantity to existing quantity
            existingItem.childNodes[1].childNodes[1].value = existingQuantity + quantity;

            // construct call string
            let callString = "characterId=" + characterId + "&itemId=" + itemId +
                "&quantity=" + quantity;

            // trigger controller to add item to inventory
            let xhttp = new XMLHttpRequest();
            xhttp.open("POST", "/Characters/AddItem", true);
            xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xhttp.send(callString);
        }
    }
    else { // display error message
        header = "No Items Added!"
        body = "You can't add 0 of an item.";
    }
    AssignToast(header, body);
}

function RemoveItem(itemId) {
    let characterId = GetById('characterId').value;
    let callString = "characterId=" + characterId + "&itemId=" + itemId;

    let xhttp = new XMLHttpRequest();
    xhttp.open("POST", "/Characters/RemoveItem", true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.send(callString);

    let item = GetById("item" + itemId);
    item.remove();

    let itemLink = GetById("itemLink" + itemId);
    itemLink.remove();
}

///<summary>
/// Updates the quantity of an item in the Inventory
///</summary>
function UpdateQuantity(itemId, quantity) {
    if (quantity > 0) {
        let characterId = GetById('characterId').value;
        let callString = "characterId=" + characterId + "&itemId=" + itemId +
            "&quantity=" + quantity;

        let xhttp = new XMLHttpRequest();
        xhttp.open("POST", "/Characters/UpdateQuantity", true);
        xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        xhttp.send(callString);
    }
    else {
        RemoveItem(itemId);
    }
}

function EquipItem(itemId) {
    let characterId = GetById('characterId').value;
    let callString = "characterId=" + characterId + "&itemId=" + itemId;
    let xhttp = new XMLHttpRequest();
    xhttp.open("POST", "/Characters/EquipItem", true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4) {
            location.reload(true);
        }
    };

    xhttp.send(callString);
}

function UnequipItem(itemId) {
    let characterId = GetById('characterId').value;
    let callString = "characterId=" + characterId + "&itemId=" + itemId;
    let xhttp = new XMLHttpRequest();
    xhttp.open("POST", "/Characters/UnequipItem", true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4) {
            location.reload(true);
        }
    };

    xhttp.send(callString);
}

function AttuneItem(itemId) {
    let characterId = GetById('characterId').value;
    let callString = "characterId=" + characterId + "&itemId=" + itemId;
    let xhttp = new XMLHttpRequest();
    xhttp.open("POST", "/Characters/AttuneItem", true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4) {
            location.reload(true);
        }
    };

    xhttp.send(callString);
}

function UnattuneItem(itemId) {
    let characterId = GetById('characterId').value;
    let callString = "characterId=" + characterId + "&itemId=" + itemId;
    let xhttp = new XMLHttpRequest();
    xhttp.open("POST", "/Characters/UnattuneItem", true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4) {
            location.reload(true);
        }
    };

    xhttp.send(callString);
}

///<summary>
/// Checks if an item is already displayed in the Inventory
///</summary>
///<param name="itemId">The unique ID number of the item</param>
///<returns>True if the item is not in the Inventory, False if not</returns>
function ItemNotInInventory(itemId) {
    let inInventory = GetById("item" + itemId);
    if (inInventory == null) {
        return true;
    }
    return false;
}

///<summary>
/// Convenience function
///</summary>
///<param name="element">The element to be created</param>
///<returns>The new element</returns>
function Create(element) {
    return document.createElement(element);
}