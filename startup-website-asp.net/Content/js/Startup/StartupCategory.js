

function editStatus(type, linkUrl, dataJson) {
    $('.dropdown-item').click(function (e) {
        e.preventDefault()
        $.ajax({
            type: type,
            url: `${linkUrl}/UpdateStatus`,
            //data: { CategoryId: this.dataset.id, Status: this.dataset.type },
            data: dataJson,
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
}
function deleteRecord(typeInput, linkUrl, dataJson) {
    let confirmForm = confirm("Bạn có muốn xóa danh mục này ko?");
    if (confirmForm == true) {
        $.ajax({
            type: typeInput,
            url: `${linkUrl}/Delete`,
            data: dataJson,
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
}


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
            case "Chưa duyệt":
                $(element).addClass('btn-warning');
                break;
            case "Bị ẩn":
                $(element).addClass('btn-light');
                break;
            case "Đang hoạt động":
                $(element).addClass('btn-success');
                break;
            case "Ngừng Hoạt động":
                $(element).addClass('btn-secondary');
                break;
            case "Bị Khóa":
                $(element).addClass('btn-danger');
                break;
            default:
                $(element).addClass('btn-danger');
                break;
        }
    }
    );
}

$(document).ready(function () {
    ResetStyleStatus();
});