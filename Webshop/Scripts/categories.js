function getCategories() {
    $.ajax({
        url: '/Admin/CategoryList/',
        type: 'POST',
        dataType: 'json',
        success: function(data) {
            alert('success');
            var categories = $('#Category_CategoryId');

            $.each(JSON.parse(data), function(idx, obj) {
                categories.append($('<option />').val(this.CategoryId).text(this.Name));
                //alert(this.Name+" "+this.CategoryId);
            });
        }
    })
};

$(document).ready(function() {
    getCategories();
});

$('#addcategory').click(function () {
    //alert($("#Newcategory").val());
    var text = $("#Newcategory").val();
    var data = { JSON.stringify(text), };
    $.ajax({
        type: 'post',
        dataType: 'json',
        url: 'Admin\AddCategory',
        data: data,
        // contentType: 'application/json', <-- no need this.
        success: function(json) {
            if (json) {
                alert('ok');
            } else {
                alert('failed');
            }
        },
    });

    //$.post("/Journal/SaveEntry", { jsonData: JSONstring });
    });