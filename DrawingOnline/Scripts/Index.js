/// <reference path="jquery-3.4.1.slim.js" />
console.log('hello');



var colorPicker = document.getElementById('colorPicker')
var sizePicker = document.getElementById('sizePicker')
var sizeValue = document.getElementById('sizeValue')
var bColorPicker = document.getElementById('bColorPicker')




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

function toggleMode(mode) {
    switch (mode) {
        case modes.drawing:
            canvas.freeDrawingBrush.color = colorPicker.value
            canvas.freeDrawingBrush.width = sizePicker.value
            currentMode = modes.drawing;
            canvas.isDrawingMode = true;
            canvas.renderAll();
            break;
        case modes.transform:
            canvas.isDrawingMode = false;
            currentMode = modes.transform
            canvas.renderAll();
            break;
    }
     
}

function reloadCanvas(canvas) {
    var bufferCanvas = JSON.stringify(canvas)
    canvas.clear();
    canvas.loadFromJSON(bufferCanvas);

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
        reloadCanvas(canvas);
    })

}


const canvas = initCanvas('c');
toggleMode(modes.drawing); 
var mousePressed = false;
setEvents(canvas);

