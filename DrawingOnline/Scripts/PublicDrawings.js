var elements = document.getElementsByClassName('drawingElement')

document.addEventListener("DOMContentLoaded", function () {
    for (var i = 0; i < elements.length; i++) {
        var element = elements[i].innerText;
        elements[i].innerHTML = '';
        elements[i].insertAdjacentHTML('afterbegin', element);
    }
});
