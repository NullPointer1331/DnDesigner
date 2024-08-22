
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
    , description, itemId) {
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
            newLink.innerHTML = name;

            // create new item div
            let newListItemDiv = Create("div");
            newListItemDiv.id = "item" + itemId;

            // create new item name
            let newListItemName = Create("h4");
            newListItemName.innerHTML = name;

            // create new item quantity div
            let newListItemQuantity = Create("div");
            newListItemQuantity.setAttribute("class", "input-group w-25");

            // create new quantity label
            let quantityLabel = Create("label");
            quantityLabel.setAttribute("class", "input-group-text");
            quantityLabel.innerHTML = "Quantity";

            // create new quantity input
            let quantityInput = Create("input");
            quantityInput.type = "number";
            quantityInput.value = quantity;
            quantityInput.min = "0";
            quantityInput.setAttribute("class", "form-control");
            quantityInput.ariaLabel = "Item quantity";
            quantityInput.setAttribute("onchange", `UpdateQuantity(${itemId}, this.value)`);

            // append label and input to div
            newListItemQuantity.appendChild(quantityLabel);
            newListItemQuantity.appendChild(quantityInput);


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

///<summary>
/// Checks if an item can be added to an Inventory
///</summary>
///<param name="itemId">The unique ID number of the item</param>
///<returns>True if the item can be added, False if not</returns>
function ItemNotInInventory(itemId) {
    let inInventory = GetById("item" + itemId);
    if (inInventory == null) {
        return true;
    }
    return false;
}

function UpdateQuantity(itemId, quantity) {
    let characterId = GetById('characterId').value;
    let callString = "characterId=" + characterId + "&itemId=" + itemId +
        "&quantity=" + quantity;

    let xhttp = new XMLHttpRequest();
    xhttp.open("POST", "/Characters/UpdateQuantity", true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.send(callString);
}

///<summary>
/// Convenience function
///</summary>
///<param name="element">The element to be created</param>
///<returns>The new element</returns>
function Create(element) {
    return document.createElement(element);
}