
    var doc = new jsPDF();

    function saveDiv(divId, title) {
        doc.fromHTML(`<html><head><title>invoice</title></head><body>` + document.getElementById(divId).innerHTML + `</body></html>`);
    doc.save('div.pdf');
}

function AjaxFormSubmit(id) {
    //var addtc = {};

    //    addtc.ProductId = $("#ProductId").val();
    //    console.log($("#ProductId").val());
    //    addtc.ProductQuantity = $("#sst").val();
    console.log(id)
    $.ajax({
        type: "POST",
        url: `/Cart/AddToCart?id=${id}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == true) {
                var x = document.getElementById('cartNum').innerHTML;
                document.getElementById('cartNum').innerHTML = parseInt(x) + 1;
                console.log(document.getElementById('cartNum').innerHTML);
                alert("This product has been successfully added to your cart !");

            }
            else if (response == "checkQuantity") {
                alert("The quantity demanded is greater than the quantity supplied !");
            }
            else if (response == "success") {
                alert("This product has been successfully added to your cart !");
            }
            if (response == false) {
                alert("Please login first !");
            }
        }
    }

    );

}

function WishList(id) {
    //var addtc = {};

    //    addtc.ProductId = $("#ProductId").val();
    //    console.log($("#ProductId").val());
    //    addtc.ProductQuantity = $("#sst").val();
    console.log(id)
    $.ajax({
        type: "POST",
        url: `/Favorite/AddFavorite?productId=${id}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == true) {
                alert("This product has been successfully added to your WishList !");

            }
            else if (response == "success") {
                alert("This product has been successfully added to your WishList !");
            }
            if (response == false) {
                alert("Please login first !");
            }
        }
    }

    );

}


function DeleteWishList(id) {
 
    $.ajax({
        type: "POST",
        url: `/Favorite/DeleteFavorite?id=${id}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == true) {
                //alert("This product has been successfully added to your WishList !");
                document.location.reload(true);
            }
            
            if (response == false) {
                alert("Please login first !");
            }
        }
    }

    );

}