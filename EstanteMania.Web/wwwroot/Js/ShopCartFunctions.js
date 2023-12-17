function SetButtonVisible(id, visible) {
    const updateQuantityButton = document.querySelector("button[data-itemId='" + id + "']");

    if (visible == true) {
        updateQuantityButton.style.display = "inline-block";
    }
    else {
        updateQuantityButton.style.display = "none";
    }
}