// JavaScript for AddDialog component
export function showDialog(element) {
    element.showModal();
}

export function closeDialog(element) {
    element.close();
}

export function makeDraggable(dialog, header) {
    header.onmousedown = e => {

        // 1. Get the current position on your visible screen
        const rect = dialog.getBoundingClientRect();

        // 2. Compute distance between mouse and the top-left corner of the dialog
        const shiftX = e.clientX - rect.left;
        const shiftY = e.clientY - rect.top;

        // 3. Remove the CSS transform that was causing your original "jump" bug
        dialog.style.transform = "none";
        dialog.style.margin = "0";

        // 4. Lock the dialog to your visible screen coordinates
        dialog.style.position = "fixed";
        dialog.style.left = rect.left + "px";
        dialog.style.top = rect.top + "px";

        // 5. Move the dialog exactly where the mouse goes on the screen
        document.onmousemove = e => {
            dialog.style.left = e.clientX - shiftX + "px";
            dialog.style.top = e.clientY - shiftY + "px";
        };

        // 6. Stop moving when the mouse button is released
        document.onmouseup = () => {
            document.onmousemove = null;
            document.onmouseup = null;
        };
    };
}
