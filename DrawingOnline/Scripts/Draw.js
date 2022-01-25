/// <reference path="jquery-3.4.1.slim.js" />
console.log('hello');

var finalCanvas;

var colorPicker = document.getElementById('colorPicker')
var sizePicker = document.getElementById('sizePicker')
var sizeValue = document.getElementById('sizeValue')
var bColorPicker = document.getElementById('bColorPicker')


var x = window.matchMedia("(max-width: 1124px)")
myFunction(x) // Call listener function at run time
x.addListener(myFunction)

function myFunction(x) {
    var elements = document.getElementsByClassName('editBut');
    if (x.matches) {
        for (var i = 0; i < elements.length; i++) {
            if (i == 0) {
                elements[i].style.width = '100%'
                console.log(elements[i].width);
            } else {
                elements[i].style.width = '100%'
                console.log(elements[i].width);
            }
        }// If media query matches
        console.log(elements[0].width);
    } else {
        for (var i = 0; i < elements.length; i++){
            if (i == 0) {
                elements[i].style.width = '80%'
                console.log(elements[i].width);
            } else {
                elements[i].style.width = '30%'
                console.log(elements[i].width);
            }
        }

    }
}

var exitPop = document.getElementById('exitPop');

exitPop.addEventListener('click', () => {
    exitPop.parentElement.parentElement.style.display = 'none';
})

const modes = {
    transform: 'transform',
    drawing: 'drawing'
}

function initCanvas(id) {
    return new fabric.Canvas(id, {
        width: 750,
        height: 750,
        backgroundColor: 'white',

    })
    sizeValue.innerHTML = '10'
    bColorPicker.value = 'white'
}

let currentMode


function setBackgroundColor() {
    canvas.backgroundColor = bColorPicker.value
    canvas.renderAll()
}

function setColor() {
    var color = colorPicker.value;
    console.log(color)
    canvas.freeDrawingBrush.color = color;
    canvas.renderAll();
}

function setBrushSize() {
    var size = sizePicker.value
    sizeValue.innerHTML = size;
    canvas.freeDrawingBrush.width = size
    canvas.renderAll();
}

function toggleMode() {
    if (currentMode == modes.drawing) {
        document.getElementById('toggleBut').innerHTML = 'Enable Drawing'
        canvas.isDrawingMode = false;
        currentMode = modes.transform
        canvas.renderAll();
    } else {
        document.getElementById('toggleBut').innerHTML = 'Enable Transform'
        canvas.freeDrawingBrush.color = colorPicker.value
        canvas.freeDrawingBrush.width = sizePicker.value
        currentMode = modes.drawing;
        canvas.isDrawingMode = true;
        canvas.renderAll();
    }
}

function reloadCanvas(canvas) {
    var bufferCanvas = JSON.stringify(canvas)
    canvas.clear();
    canvas.loadFromJSON(bufferCanvas);

}

function clearCanvas() {
    canvas.clear();
    setBackgroundColor();
}

function setEvents(canvas) {
    canvas.on('mouse:move', (event) => {
        if (mousePressed && currentMode == modes.drawing) {
            console.log('drawing');
        }
    })
    canvas.on('mouse:down', (event) => {
        mousePressed = true;
    })

    canvas.on('mouse:up', (event) => {
        mousePressed = false;
        if (currentMode == modes.drawing)
            reloadCanvas(canvas);
        saveCanvas(canvas);
    })

}



const canvas = initCanvas('c');
toggleMode(modes.drawing); 
var mousePressed = false;
setEvents(canvas);



function saveCanvas(canvas){
    finalCanvas = {
        canvas: canvas
    }
}



