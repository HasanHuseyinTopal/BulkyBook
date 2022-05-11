var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#companyTable').dataTable({

        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "companyName", "width": "15%" },
            { "data": "streetAdress", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "state", "width": "15%" },
            { "data": "postalCode", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "companyID",
                "render": function (data) {
                    return `
                     <div class="w-75 btn-group" role="group">
                     <a href="/Admin/Company/CompanyUpSert?id=${data}" class="btn btn-primary mx-2" > Edit</a >
                     </div>
                    `
                }
                , "width": "15%"
            },
            {
                "data": "companyID",
                "render": function (data) {
                    return `
                     <div class="w-75 btn-group" role="group">
                     <a onclick="Javascript : Del('CompanyDelete?id=${data}')" class="btn btn-warning mx-2">Delete</a>
                     </div>
                    `
                }
                , "width": "15%"
            },
        ]
    });
}

function Del(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = url;
        }
    }
    )
}

