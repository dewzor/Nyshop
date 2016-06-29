$('#sendreview').click(function () {
    var text = $("#Comment").val();
    var userid ='@User.Identity.Name';
    var productitem = @Model.ProductId;
    var review = {'comment' : text, 'customerid': userid, 'productid': productitem };

    $.ajax({
        url: "/api/review",
        type: "POST",
        data: JSON.stringify(review),
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) { location.reload(); },
        error: function(xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert("ERROR:"+err.Message);
        }
    });
});

