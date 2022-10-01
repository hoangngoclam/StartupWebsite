
//Product category

$('#sub-category').hide();
$('#discountOptions').hide();
$('#main-category li').click(() => {
    $('#sub-category').show();
})
if ($('#checkboxDiscount').prop('checked')) {
    $('#discountOptions').show();
}
$('#attributeTags').tagsinput('items');
$('#inputDescription').summernote({
    height: 200,
    tabsize: 2
});
$('#inputDescriptionGilf').summernote({
    height: 100,
    tabsize: 2
});
$(".shopCategory").select2();

$(document).ready((e) => {
    var subImagesPathStr = changeSubImagesPath();
    $('input#SubImages').val(subImagesPathStr).trigger("change");

})

//End Product category


//Images
//anhat:
function onChangeUploadImage(event) {
    event.preventDefault();
    var file = event.target.files[0];
    //Only pictures
    if (file.type.match('image')) {
        //Check extend image
        var fileName = event.target.value;
        var idxDot = fileName.lastIndexOf(".") + 1;
        var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
        if (extFile == "jpg" || extFile == "jpeg" || extFile == "png") {
        
            var childWrapper = event.path[2];
        var inputWrapper = event.path[1];
        var picReader = new FileReader();
        picReader.addEventListener("load", function (event) {
            var picFile = event.target;
            var div = document.createElement("div");
            div.classList.add('image-wrapper');
            var btnDelete = `<button type="button" onclick="deleteImage(event)" class="btn-delete btn-icon btn-danger">X</button>`
            div.innerHTML = "<img id='image' class='image rounded' src='" + picFile.result + "'" +
                "title='" + file.name + "'/>" + btnDelete;
            inputWrapper.style.display = "none";
            childWrapper.insertBefore(div, null);
        });
        //Read the image
        picReader.readAsDataURL(file);
    } else {
        alert("Định dạng ảnh phải là jpg, jpeg, png");
    }
    }
    else {
        alert('Định dạng ảnh phải là image')
    }
}
//anhat
function deleteImage(event) {
	console.log(event);
    var childWrapper = event.path[2];
    var imageWrapper = event.path[1];
    childWrapper.firstElementChild.style.display = "block";
    $(imageWrapper).remove();
    if (childWrapper.id != "") {
        $('#mainImage .adding-box input').prop('required', true);    }
    var subImagesPathStr = changeSubImagesPath();
    console.log(subImagesPathStr);
    $('input#SubImages').val(subImagesPathStr).trigger("change");
}
//anhat
function changeSubImagesPath() {
    var subImagesPathArr = [];
    var path, subImagesPathStr = "";
    for (i = 0; i < $(".subImages .child-wrapper").length; i++) {
        var imageSrc = $(".subImages .child-wrapper")[i].lastElementChild.firstElementChild.src;
        let parser = document.createElement('a');
        parser.href = imageSrc;
        if (imageSrc != "") {
            path = parser.pathname;
        }
        else {
            path = "''";
        }
        subImagesPathArr.push(path);

    }
    subImagesPathStr = subImagesPathArr.join(',');
    console.log(subImagesPathStr);
    return subImagesPathStr;
}

//Attribute



