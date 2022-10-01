$('.dropdown-item').click(function (e) {
    e.preventDefault()
    $.ajax({
        type: "POST",
        url: "/Admin/Startups/StartupAjaxEdit",
        data: { StartupId: this.dataset.id, Status: this.dataset.type },
        dataType: "json",
        success: function (data) {
            $('#status-' + data.StartupId).removeClass();
            $('#status-' + data.StartupId).addClass("btn-status btn ");
            $('#status-' + data.StartupId).html(`${data.Status}<i class="ik ik-chevron-down mr-0 align-middle"></i>`)
            ResetStyleStatus();
        },
        error: function (e) {
            alert("Error occured!!")
        }
    });
})

$('.delete-startup').click(function (e) {
    e.preventDefault();
    let confirmForm = confirm("Bạn có muốn xóa startup này ko?");
    if (confirmForm == true) {
        $.ajax({
            type: "POST",
            url: "/Admin/Startups/StartupAjaxDelete",
            data: { StartupId: this.dataset.id },
            dataType: "json",
            success: function (data) {
                $('#item-' + data.StartupId).remove();
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