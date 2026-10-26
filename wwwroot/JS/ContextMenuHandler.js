
  export function  registerOutsideClick(dotnetRef, menu) {

        function handler(e) {
            if (!menu.contains(e.target)) {
        dotnetRef.invokeMethodAsync('CloseMenu');
        document.removeEventListener('click', handler);

        document.dispatchEvent(
            new CustomEvent('contextmenu-close'));
            }
        }
        // The setTimeout(..., 0) is important. Without it, 
        // the click that opens the menu may immediately trigger the close handler.
        setTimeout(() => {
            document.addEventListener('click', handler);
        }, 0);
        
    }