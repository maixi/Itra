var canvas = new fabric.StaticCanvas("canvas");
var canvascontainer = $("#canvas-container")[0];
var image;
var rectangle;
var formData;
var img;
var form;
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
    $("#vertical-btn").click(VerticalOrientation);
    $("#horizont-btn").click(HorizontalOrientation);
    form = $("#Form");
});

function UploadImageFromComputer() {
    UploadImage(URL.createObjectURL($('#uploadFileBtn')[0].files[0]));

}
function UploadImageFromUrl() {
    img = new Image();
    img.setAttribute('crossOrigin', 'anonymous');
    img.src = $("#ImageUrl")[0].value;
    UploadImage($("#ImageUrl")[0].value)
}
function UploadImage(Url) {
    fabric.Image.fromURL(Url, function (oImg) {
        canvas.clear();
        image = oImg;
        var prop = 1, imgwidth, imgheight;
        var imgwidth = image.getWidth()
        imgheight = image.getHeight();
        if (imgwidth > 640 || imgwidth < 490) {
            if (imgwidth < 490) prop = 490 / imgwidth;
            else prop = 640 / imgwidth;
        }
        InitializeCanvas((imgwidth * prop) + 60, (imgheight * prop) + 200);
        image.setWidth(imgwidth * prop);
        image.setHeight(imgheight * prop);
        if (image.getHeight() > 1000) image.setHeight(1000);
        Topstring.set('top', image.getHeight() + 50);
        Topstring.set('left', canvas.getWidth() / 2);
        Bottomstring.set('top', image.getHeight() + 120);
        Bottomstring.set('left', canvas.getWidth() / 2);
        canvas.add(Topstring);
        canvas.add(Bottomstring);
        image.set('top', 40);
        image.set('left', 30);
        rectangle = new fabric.Rect({
            width: image.getWidth() + 5,
            height: image.getHeight() + 5,
            left: 25,
            top: 35,
            stroke: 'white',
            strokeWidth: 5
        })
        canvas.add(rectangle);
        canvas.add(image);
        canvas.renderAll();
        if (Url != null) {
            $("#buttons-div").removeClass('invisible');
        }
        else $("#buttons-div").addClass('invisible');
    }, { crossOrigin: 'Anonymous' });
}
function InitializeCanvas(Width, Height) {
    canvas.setBackgroundColor("#000000");
    canvas.setWidth(Width);
    canvas.setHeight(Height);
    canvas.renderAll();
}
function writeHeadLine() {
    var str = $('#topLine')[0].value;
    if (str == "") str = "Head";
    Topstring.set('text', str);
    canvas.renderAll();
}

function writeTextLine() {
    var str = $('#bottomLine')[0].value;
    Bottomstring.set('text', str);
    canvas.renderAll();
}

function VerticalOrientation() {
    $(this).addClass("disabled");
    $(this).addClass("btn-info");
    $("#horizont-btn").removeClass("disabled");
    $("#horizont-btn").removeClass("btn-info");
    canvas.setWidth(canvas.getWidth() / 1.2);
    canvas.setHeight(canvas.getHeight() * 1.2);
    image.setWidth(image.getWidth() / 1.2);
    image.setHeight(image.getHeight() * 1.2);
    rectangle.setWidth(rectangle.getWidth() / 1.2);
    rectangle.setHeight(rectangle.getHeight() * 1.2);
    image.set('top', 90);
    rectangle.set('top', 85)
    Topstring.set('top', 20)
    Topstring.set('left', Topstring.get('left') / 1.2);
    Bottomstring.set('left', Bottomstring.get('left') / 1.2);
    Bottomstring.set('top', Bottomstring.get('top') * 1.2);
    canvas.renderAll();
}
function HorizontalOrientation() {
    $(this).addClass("disabled");
    $(this).addClass("btn-info");
    $("#vertical-btn").removeClass("disabled");
    $("#vertical-btn").removeClass("btn-info");
    canvas.setWidth(canvas.getWidth() * 1.2);
    canvas.setHeight(canvas.getHeight() / 1.2);
    image.setWidth(image.getWidth() * 1.2);
    image.setHeight(image.getHeight() / 1.2);
    rectangle.setWidth(rectangle.getWidth() * 1.2);
    rectangle.setHeight(rectangle.getHeight() / 1.2);
    image.set('top', 40);
    rectangle.set('top', 35)
    Topstring.set('top', image.getHeight() + 50);
    Topstring.set('left', Topstring.get('left')*1.2);
    Bottomstring.set('left', Bottomstring.get('left') * 1.2);
    Bottomstring.set('top', Bottomstring.get('top') / 1.2);
    canvas.renderAll();
}


$('#Create').on('click', function (e) {

    e.preventDefault();
    if (window.FormData !== undefined) {
        var data = new FormData();
        if (image.getSrc() == $("#ImageUrl")[0].value) data.append("source", $("#ImageUrl")[0].value);
        else data.append("image", $('#uploadFileBtn')[0].files[0]);
        data.append("canvas", document.getElementById('canvas').toDataURL());
        $.ajax({
            type: "POST",
            url: 'Upload',
            async: false,
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                document.getElementById('OriginalImageUrl').setAttribute('value', result[0].Uri);
                document.getElementById('DemotivatorUrl').setAttribute('value', result[1].Uri);
                form.submit();
                return;
            },
            error: function (xhr, status, p3) {
                alert(xhr.responseText);
            }
        });
    } else {
        alert("Браузер не поддерживает загрузку файлов HTML5!");
    }
    return;
});

