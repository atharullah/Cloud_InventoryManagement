function PrintDiv() {
    var divToPrint = document.getElementById('printDiv');
    var popupWin = window.open('', '_blank', 'location=no,left=200px');
    popupWin.document.open();
    popupWin.document.write('<html><head><link href="/CSS/bootstrap.min.css" rel="stylesheet" /></head><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
    popupWin.document.close();
    $("#lblMessage").text("");
}

function makingRow() {
    if ($("#chkIsMakingRequired").is(":checked"))
        $(".rowMaking").show("slow");
    else
        $(".rowMaking").hide("slow");
}

function setTotalOrderAmount() {
    var total = 0;
    for (i = 0; i < $(".rptrTotalAmtClass").length; i++) {
        total += Number($(".rptrTotalAmtClass")[i].innerText);
    }
    var makingCost = $("#txtMakingCost").val() == "" ? 0 : Number($("#txtMakingCost").val());
    total += makingCost;
    $("#txtTotalOrderAmount").val(total.toFixed(2));

    var paidAmt = $("#txtPaidAmount").val() == "" ? 0 : Number($("#txtPaidAmount").val());
    var RemainingAmt = total - paidAmt;
    $("#txtBalance").val(RemainingAmt.toFixed(2));
}

function setPrevBalance(customerID) {
    $.ajax({
        url: "SaleOrder.aspx/GetCustomerBalance?CustomerID=" + customerID,
        dataType: "json",
        type: "Get",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
            if (data.d.length == 0)
                $("#txtPreviousBalance").val("0");
            else
                $("#txtPreviousBalance").val(data.d.Balance);
        },
        error: function (data) {
            console.log("Error Occured");
        },
    });
}

function setTotalAmount() {
    var sellingRate = $("#txtSellingRate").val();
    var quantity = $("#txtQuantity").val();
    var VAT = $("#txtVAT").val();
    if (sellingRate != "" && quantity != "") {
        var vatToAdd = 1;
        if (VAT != "")
            vatToAdd = (100 + Number(VAT)) / 100;
        $("#txtTotalAmount").val((sellingRate * quantity * vatToAdd).toFixed(2));
    }
}

$(function () {

    $("#txtSellingRate,#txtQuantity,#txtVAT").on("keyup focusout", function () {
        setTotalAmount();
    });

    setTotalOrderAmount();
    $("#txtPaidAmount,#txtTotalOrderAmount,#txtMakingCost").on("keyup focusout", function () {
        setTotalOrderAmount();
    });

    $('#txtAutoCustomerName').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "SaleOrder.aspx/GetCustomerName?searchText='" + request.term + "'",
                dataType: "json",
                type: "Get",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                            return {
                                label: item.CustomerName + "-" + item.CustomerMobile,
                                value: item.CustomerName,
                                CustomerID: item.CustomerID
                            }
                    }));
                },
                error: function (data) {
                    console.log("Error Occured");
                },
            });
        },
        select: function (event, ui, data2, data3) {
            var item = ui.item;
            $("#txtCustomerID").val("");
            $("#txtCustomerID").val(item.CustomerID);
            setPrevBalance(item.CustomerID);
        }
    });

    $('#txtAutoMakingInventoryType').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "SaleOrder.aspx/GetItemDetail?searchText='" + request.term + "'",
                dataType: "json",
                type: "Get",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.InventoryCode + " - " + item.InventoryTypeName,
                            value: item.InventoryTypeName,
                            InventoryTypeName: item.InventoryTypeName,
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
            $("#txtMakingInventoryType").val("");
            $("#txtMakingInventoryType").val(currentItem.InventoryTypeId);
        }
    });

    $('#txtAutoItemCode').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "SaleOrder.aspx/GetItemDetail?searchText='" + request.term + "'",
                dataType: "json",
                type: "Get",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.InventoryCode + " - " + item.InventoryTypeName,
                            value: item.InventoryCode,
                            InventoryCode: item.InventoryCode,
                            InventoryTypeName: item.InventoryTypeName,
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

            $("#txtItemCode").val("");
            $("#txtItemName").val("");
            $("#txtSellingRate").val("");
            $("#txtVAT").val("");
            $(".hdndivItemID input").val("");

            $("#txtItemCode").val(currentItem.InventoryCode);
            $("#txtItemName").val(currentItem.InventoryTypeName);
            $("#txtSellingRate").val(currentItem.SellingRate);
            $("#txtVAT").val(currentItem.VAT);
            $(".hdndivItemID input").val(currentItem.InventoryTypeId);
        }
    });

    makingRow();

    $("#chkIsMakingRequired").change(function () {
        makingRow();
    });

    if ($("#printlblBillNo").text() != "")
        $(".printBtnDiv").show();
    else
        $(".printBtnDiv").hide();

    $("#btnPrintOrder").click(function () {
        if ($("#printlblBillNo").text() != "")
            PrintDiv();
    });

    $(".newCustomer").click(function () {
        $(".divAddCustomer").modal();
    });

    $(".lnkbtnNewItem").click(function () {
        $(".divAddInventoryType").modal();
    });

    $("#txtPreviousBalance").attr('readonly', true);

});