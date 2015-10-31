﻿# CoffeeScript
canvas = new fabric.Canvas("canvas")

document.getElementById('textin').onclick = ->
  str1 = document.getElementById("str11").value
  canvas.add new (fabric.IText)(str1,
    fontFamily: 'Georgia'
    originX: 'center'
    left: 250
    top: 0
    z_index: 1
    fontSize: 50
    lockScalingX: true
    lockScalingY: true
    editable: false
    hasRotatingPoint: false
    transparentCorners: false)
  str2 = document.getElementById("str22").value
  canvas.add new (fabric.IText)(str2,
    fontFamily: 'Georgia'
    top: 250
    fontSize: 30
    originX: 'center'
    left: 250
    editable: false
    lockScalingX: true
    lockScalingY: true
    hasRotatingPoint: false
    transparentCorners: false)
  imgsrc = document.getElementById("url2").value
  fabric.Image.fromURL imgsrc, (oImg) ->
      canvas.setBackgroundImage oImg
      canvas.backgroundImage.width = canvas.getWidth() - 10
      canvas.backgroundImage.height = canvas.getHeight() - 10
      canvas.renderAll()
