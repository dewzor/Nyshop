function getCategories() {
    $.ajax({
        url: '/admin/CategoryList/',
        type: 'POST',
        dataType: 'json',
        success: function(data) {
            var categories = $('#Category_CategoryId');
            categories.empty();
            $.each(JSON.parse(data), function (idx, obj) {
                categories.append($('<option />').val(this.CategoryId).text(this.Name));
            });
        }
    })
};

$(document).ready(function() {
    getCategories();
});

$('#addcategory').click(function () {
    var text = $("#Newcategory").val();
    $.post("/admin/addcategory", { jsonData: text }).error(function (data) { });
    $('.product-added').empty();
    setTimeout(function () {
        getCategories();
        $('.product-added').append(text + " has been added to Categories");
    }, 600);
    
});