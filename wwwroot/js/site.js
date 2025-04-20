// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", () => {
    const toast = document.querySelector('.toast_class');
    if (toast) {
        const shouldShow = toast.getAttribute('data-show');

        if (JSON.parse(shouldShow.toLowerCase())) {
             toast.classList.add("open")
            //console.log('Adiciona a classe OPEN')
            setTimeout(() => {
                toast.classList.remove("open")
                //console.log('Remove a classe OPEN')
            }, 6000)
        }
    }
})
