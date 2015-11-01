var canvas = new fabric.Canvas("canvas");
var canvascontainer = $("#canvas-container")[0];
var Topstring = new fabric.IText("Head", {
    fill: 'white',
    fontFamily: "Georgia",
    originX: 'center',
    left: 400,
    selectable: false,
    z_index: 1,
    fontSize: 50,
    lockScalingX: true,
    lockScalingY: true,
    editable: false,
    hasRotatingPoint: false,
    transparentCorners: false,
})
var Bottomstring = new fabric.IText("text", {
    fill: 'white',
    fontFamily: "Georgia",
    originX: "center",
    top: 290,
    left: 300,
    selectable: false,
    z_index: 1,
    fontSize: 30,
    lockScalingX: true,
    lockScalingY: true,
    editable: false,
    hasRotatingPoint: false,
    transparentCorners: false
})

$(document).ready(function () {
    $("#uploadFileBtn").change(UploadImageFromComputer);
    $("#PreviewBtn").click(UploadImageFromUrl);
    $("#topLine")[0].oninput = writeHeadLine;
    $("#bottomLine")[0].oninput = writeTextLine;
});

function UploadImageFromComputer() {
    UploadImage(URL.createObjectURL($('#uploadFileBtn')[0].files[0]));
}
function UploadImageFromUrl() {
    UploadImage($("#ImageUrl")[0].value)
}
function UploadImage(Url) {
    fabric.Image.fromURL(Url, function (oImg) {
        canvas.clear();
        var prop = 1, imgwidth, imgheight;
        var imgwidth = oImg.getWidth()
        imgheight = oImg.getHeight();
        if (imgwidth > 640 || imgwidth < 490) {
            if (imgwidth < 490) prop = 490 / imgwidth;
            else prop = 640 / imgwidth;
        }
        InitializeCanvas((imgwidth * prop) + 60, (imgheight * prop) + 200);
        oImg.setWidth(imgwidth * prop);
        oImg.setHeight(imgheight * prop);
        Topstring.set('top', oImg.getHeight() + 50);
        Topstring.set('left', canvas.getWidth() / 2);
        Bottomstring.set('top', oImg.getHeight() + 120);
        Bottomstring.set('left', canvas.getWidth() / 2);
        canvas.add(Topstring);
        canvas.add(Bottomstring);
        canvas.setBackgroundImage(oImg, canvas.renderAll.bind(canvas), {
            left: 30,
            top: 40
        });
    });
}
function InitializeCanvas(Width, Height) {
    canvas.setBackgroundColor("#000000");
    canvas.setWidth(Width);
    canvas.setHeight(Height);
    canvas.renderAll();
}
function writeHeadLine() {
    Topstring.set('text', $('#topLine')[0].value);
    canvas.renderAll();
}

function writeTextLine() {
    Bottomstring.set('text', $('#bottomLine')[0].value);
    canvas.renderAll();
}