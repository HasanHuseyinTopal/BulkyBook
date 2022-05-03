var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#productTable').dataTable({

        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "productDescription", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "category.categoryName", "width": "15%" },
            { "data": "cover.coverName", "width": "15%" },
            {
                "data": "productID",
                "render": function (data) {
                    return `
                     <div class="w-75 btn-group" role="group">
                     <a href="/Admin/Product/ProductUpSert?productID=${data}" class="btn btn-primary mx-2" > Edit</a >
                     </div>
                    `
                }
                , "width": "15%"
            },
            {
                "data": "productID",
                "render": function (data) {
                    return `
                     <div class="w-75 btn-group" role="group">
                     <a onclick="Javascript : Del('ProductDelete?productID=${data}')" class="btn btn-warning mx-2">Delete</a>
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
        
