$('.dropdown-item').click(function (e) {
    e.preventDefault()
    $.ajax({
        type: "POST",
        url: "/Startup/StartupCategory/Edit",
        data: { CategoryId: this.dataset.id, Status: this.dataset.type },
        dataType: "json",
        success: function (data) {
            $('#status-' + data.CategoryId).removeClass();
            $('#status-' + data.CategoryId).addClass("btn-status btn ");
            $('#status-' + data.CategoryId).html(`${data.Status}<i class="ik ik-chevron-down mr-0 align-middle"></i>`)
            ResetStyleStatus();
        },
        error: function (e) {
            alert("Error occured!!")
        }
    });
})

$('.delete-startup').click(function (e) {
    e.preventDefault();
    let confirmForm = confirm("Bạn có muốn xóa danh mục này ko?");
    if (confirmForm == true) {
        $.ajax({
            type: "POST",
            url: "/Startup/StartupCategory/delete",
            data: { CategoryId: this.dataset.id },
            dataType: "json",
            success: function (data) {
                if (data.CategoryId == -1 || data.result == false) {
                    alert(data.error);
                    return;
                }
                $('#item-' + data.CategoryId).remove();
            },
            error: function (e) {
                alert("Error occured!!")
            }
        });
    }

})


function ResetStyleStatus() {
    let arrayDropdown = $('.btn-status[data-toggle="dropdown"]').toArray();
    arrayDropdown.forEach(element => {
        switch (element.innerText.trim()) {
            case "Ẩn":
                $(element).addClass('btn-danger');
                break;
            case "Hiện":
                $(element).addClass('btn-success');
                break;
        }

    }
    );
}

$(document).ready(function () {
    ResetStyleStatus();
});