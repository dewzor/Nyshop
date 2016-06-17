function getCategories() {
    $.ajax({
        url: 'Admin/CategoryList/',
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
    alert("hej");
    $.post("localhost/Webshop/admin/addcategory", { jsonData: JSON.stringify(text) });
    //alert($("#Newcategory").val());
    //var text = $("#Newcategory").val();
    //$.ajax({
    //    type: 'POST',
    //    dataType: 'json',
    //    url: 'AddCategory',
    //    data: JSON.stringify(text),
    //    // contentType: 'application/json', <-- no need this.
    //    success: function(json) {
    //        if (json) {
    //            alert('ok');
    //        } else {
    //            alert('failed');
    //        }
    //    },
    //});


});