var canvas = new fabric.Canvas("canvas");
$(document).ready(function () {
    $("#uploadFileBtn")[0].onchange = UploadImageFromComputer;
    $("#PreviewBtn")[0].onclick = UploadImageFromUrl;
});

function UploadImageFromComputer()
{
    var temp = $('#uploadFileBtn')[0].files[0];
    var Url = URL.createObjectURL(temp);
    fabric.Image.fromURL(Url, function (oImg) {
        canvas.clear();
        canvas.setBackgroundImage(oImg);
        InitializeCanvas();
        canvas.backgroundImage.setWidth(canvas.getWidth() - 10);
        canvas.backgroundImage.setHeight(canvas.getHeight() - 10);
        canvas.renderAll.bind(canvas);
    });
}
function UploadImageFromUrl()
{
    fabric.Image.fromURL($('#ImageUrl')[0].value, function (oImg) {
        canvas.clear();
        InitializeCanvas();
        canvas.setBackgroundImage(oImg, canvas.renderAll.bind(canvas));
        canvas.backgroundImage.setWidth(canvas.getWidth() - 10);
        canvas.backgroundImage.setHeight(canvas.getHeight() - 10);
        canvas.renderAll.bind(canvas);
    });
}
function InitializeCanvas()
{
    canvas.setWidth(500);
    canvas.setHeight(300);
    //canvas.setbackgroundColor('#00000');
    canvas.renderAll.bind(canvas);
}