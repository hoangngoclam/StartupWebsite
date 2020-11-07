
function editStatus(type, linkUrl, dataJson) {
    $.ajax({
        type: type,
        url: linkUrl,
        //data: { CategoryId: this.dataset.id, Status: this.dataset.type },
        data: dataJson,
        dataType: "json",
        success: function (data) {
            $('#status-' + data.Id).removeClass();
            $('#status-' + data.Id).addClass("btn-status btn ");
            $('#status-' + data.Id).html(`${data.Status} <i class="ik ik-chevron-down mr-0 align-middle"></i>`)
            ResetStyleStatus();
        },
        error: function (e) {
            alert("Error occured!!");
        }
    });
}
function deleteRecord(typeInput, linkUrl, dataJson) {
    let confirmForm = confirm("Bạn có muốn xóa danh mục này ko?");
    if (confirmForm == true) {
        $.ajax({
            type: typeInput,
            url: linkUrl,
            data: dataJson,
            dataType: "json",
            success: function (data) {
                if (data.Id == -1 || data.Result == false) {
                    alert(data.Message);
                    showSuccessToast(data.Type, data.Message)
                    return;
                }
                $('#item-' + data.Id).remove();
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
                $(element).addClass('btn-danger');
                break;
            case "ChuaDuyet":
                $(element).addClass('btn-danger');
                break;
            case "Đã duyệt":
                $(element).addClass('btn-success');
                break;
            case "DaDuyet":
                $(element).addClass('btn-success');
                break;
            case "Đã xong":
                $(element).addClass('btn-success');
                break;
            case "Đã giao":
                $(element).addClass('btn-success');
                break;
            case "DaGiao":
                $(element).addClass('btn-success');
                break;
            case "Đã hủy":
                $(element).addClass('btn-light');
                break;
            case "DaHuy":
                $(element).addClass('btn-light');
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
            case "Nháp":
                $(element).addClass('btn-light');
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