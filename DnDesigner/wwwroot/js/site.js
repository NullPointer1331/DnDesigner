// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function assignToast(button, toast) {
        let toastTrigger = document.getElementById(button)
        let toastBox = document.getElementById(toast)

        if (toastTrigger) {
            let toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastBox)
            toastBootstrap.show()
        }
}
