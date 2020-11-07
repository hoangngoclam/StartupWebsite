function filterGlobal() {
	$('#advanced_table').DataTable().search(
		$('#global_filter').val()
	).draw();
}
function filterColumn(i) {
	$('#advanced_table').DataTable().column(i).search(
		$('#col' + i + '_filter').val()
	).draw();
}
$(document).ready(function () {
	   var t = $('#data_table').DataTable({
	       responsive: false,
	       select: true,
	       'aoColumnDefs': [{
	           'bSortable': false,
			   'aTargets': ['nosort']
		   }],
		   language: {
			   "sProcessing": "Đang xử lý...",
			   "sLengthMenu": "Xem _MENU_ mục",
			   "sZeroRecords": "Không tìm thấy dòng nào phù hợp",
			   "sInfo": "Đang xem _START_ đến _END_ trong tổng số _TOTAL_ mục",
			   "sInfoEmpty": "Đang xem 0 đến 0 trong tổng số 0 mục",
			   "sInfoFiltered": "(được lọc từ _MAX_ mục)",
			   "sInfoPostFix": "",
			   "sSearch": "Tìm:",
			   "sUrl": "",
			   "oPaginate": {
				   "sFirst": "Đầu",
				   "sPrevious": "Trước",
				   "sNext": "Tiếp",
				   "sLast": "Cuối"
			   }
		   }

	});
	t.on('order.dt search.dt', function () {
		t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
			cell.innerHTML = i + 1;
		});
	}).draw();
	$('#data_table tbody').on('click', 'tr', function () {
		if ($(this).hasClass('selected')) {
			$(this).removeClass('selected');
		}
		else {
			table.$('tr.selected').removeClass('selected');
			$(this).addClass('selected');
		}
	});

	$("#advanced_table").DataTable({
		responsive: true,
		select: true,
		'aoColumnDefs': [{
			'bSortable': false,
			'aTargets': ['nosort']
		}]
	});
	$('input.global_filter').on('keyup click', function () {
		filterGlobal();
	});
	$('input.column_filter').on('keyup click', function () {
		filterColumn($(this).attr('data-column'));
	});
});