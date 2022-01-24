var menubot = document.getElementById('menuIcon');
var exMenu = document.getElementById('coll')

var x = window.matchMedia("(min-width: 750px)")
myFunction(x) // Call listener function at run time
x.addListener(myFunction)

function myFunction(x) {
    if (x.matches) { // If media query matches
        exMenu.style.maxWidth = "100%";
        menubot.classList.remove('menuRotated')
        exMenu.classList.remove('nav-list-transtion');
    } else {
        exMenu.style.maxWidth = "0";
        
    }
}

menubot.addEventListener("click", function () {
    exMenu.classList.add('nav-list-transtion');
    if (exMenu.style.maxWidth == "158px") {
        exMenu.style.maxWidth = '0px'
        menubot.classList.toggle('menuRotated')
    }
    else {
        exMenu.style.maxWidth = "158px";
        menubot.classList.toggle('menuRotated')
    }
});
