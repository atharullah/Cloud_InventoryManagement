function setSellingVatRate() {
    var rateObj = JSON.parse($("#hdnInvRate").val());
    $(rateObj).each(function () {
        if ($("#ddlInventoryType").val() == this.InventoryTypeID) {
            $("#txtPurchasRate").val(this.PurchaseRate);
            $("#txtVAT").val(this.VAT);
            return false;
        }
        else {
            $("#txtVAT").val("");
            $("#txtSellingRate").val("");
        }
    });
}

function SetTotalAmount() {
    var purchaseRate = Number($("#txtPurchasRate").val());
    var Quantity = Number($("#txtQuantity").val());
    var vat = $("#txtVAT").val();
    if (purchaseRate != "" && Quantity != "") {
        var vatToAdd = 1;
        if (vat != "")
            vatToAdd = (100 + Number(vat)) / 100;
        $("#txtTotalAmount").val((purchaseRate * Quantity * vatToAdd).toFixed(2));
    }
}

function setAmounts() {
    var totalOrderAmt = 0;
    $(".rptrTotalAmt").each(function () {
        totalOrderAmt += Number($(this).text());
    });
    $("#txtTotalOrderAmount").val(totalOrderAmt);

    var paidAmt = Number($("#txtPaidAmount").val());
    $("#txtBalance").val((totalOrderAmt - paidAmt).toFixed(2));
}

$(function () {
    $("#txtQuantity,#txtPurchasRate,#txtVAT").on("keyup focusout", function () {
        SetTotalAmount();
    });

    //Set Total Order amount balance amount
    setAmounts();

    //Show popup to edit inventory order
    $(".rptrEdit").click(function () {
        var parentTr = $(this).parents("tr");
        $("#txtpopupAmtPaid").val(parentTr.find(".rptrAmtPaid").text());
        $("#txtpopupTotalAmt").val(parentTr.find(".rptrTotalOrderAmt").text());
        if (parentTr.find(".rptrIsComplete").text() == "True")
            $("#chkpopupOrderComplete").prop("checked", "checked");
        else
            $("#chkpopupOrderComplete").removeProp("checked");
        document.getElementById("txtpopupamtpaiddate").valueAsDate = new Date(parentTr.find("#hdnAmountPaidDate").val());
        $("#txtpopupRemarks").val(parentTr.find("#hdnRemarks").val());
        $("#hdnOldPaidAmt").val(parentTr.find(".rptrAmtPaid").text());
        $("#hdnInventoryOrderID").val(parentTr.find("#hdnrptrinventoryID").val());
    });

    $("#txtPaidAmount,#txtTotalOrderAmount").on("keyup focusout", function () {
        setAmounts();
    });

    $('#txtAutoSeller').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "AddPurchaseOrder.aspx/GetSellerName?searchText='" + request.term + "'",
                dataType: "json",
                type: "Get",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.SellerName + " - " + item.SellerMobile,
                            value: item.SellerName,
                            SellerID: item.SellerID
                        }
                    }));
                },
                error: function (data) {
                    console.log("Error Occured");
                },
            });
        },
        select: function (event, ui, data2, data3) {
            console.log(ui);
            $("#txtSellerID").val("");
            $("#txtSellerID").val(ui.item.SellerID);
        }
    });

    $('#txtAutoItemCode').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "AddPurchaseOrder.aspx/GetItemDetail?searchText='" + request.term + "'",
                dataType: "json",
                type: "Get",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.InventoryCode + " - " + item.InventoryTypeName,
                            value: item.InventoryCode + " - " + item.InventoryTypeName,
                            InventoryCode: item.InventoryCode,
                            InventoryTypeName: item.InventoryTypeName,
                            PurchaseRate: item.PurchaseRate,
                            SellingRate: item.SellingRate,
                            VAT: item.VAT,
                            InventoryTypeId: item.InventoryTypeId
                        }
                    }));
                },
                error: function (data) {
                    console.log("Error Occured");
                },
            });
        },
        select: function (event, ui, data2, data3) {
            var currentItem = ui.item;
            $(".itemCodeTBoxClass").val("");
            $(".itemNameTBoxClass").val("");
            $(".itemPrateTBoxClass").val("");
            $(".itemSRateTBoxClass").val("");
            $(".itemVatTBoxClass").val("");
            $(".hdndivItemID input").val("");

            $(".itemCodeTBoxClass").val(currentItem.InventoryCode);
            $(".itemNameTBoxClass").val(currentItem.InventoryTypeName);
            $(".itemPrateTBoxClass").val(currentItem.PurchaseRate);
            $(".itemSRateTBoxClass").val(currentItem.SellingRate);
            $(".itemVatTBoxClass").val(currentItem.VAT);
            $(".hdndivItemID input").val(currentItem.InventoryTypeId);
        }
    });

    $(".lnkBtnAddSeller").click(function () {
        $(".divAddSeller").modal();
    });

    $(".lnkBtnAddItem").click(function () {
        $(".divAddInventoryType").modal();
    });
});
