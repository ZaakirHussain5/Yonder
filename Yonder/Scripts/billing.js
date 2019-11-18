

/*Whole Billing Client-side Logic
  
*/

var calGST =
    function (gst, amount) {
            var taxVal = (amount * gst) / 100;
            return amount + taxVal;
    }

$(document).ready(function () {
    var vm = {
        cardNumber: "",
        billItems: []
    };

    var taxes = [];

    $('#Items').typeahead({
        hint: true,
        highlight: true,
        minLength: 1,
        source: function (request, response) {
            $.ajax({
                url: '/Billing/ItemSearch/',
                data: "{ 'query': '" + request + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    items = [];
                    map = {};
                    $.each(data, function (i, item) {
                        var id = item.Price;
                        var name = item.itemName;
                        var tax = item.GST;
                        map[name] = { id: id, name: name, tax: tax };
                        items.push(name);
                    });
                    response(items);
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        updater: function (item) {
            $('#Items').val(map[item].name);
            $('#Price').val(map[item].id);
            $('#total').val(calGST(map[item].tax, map[item].id));
            $('#gst').val(map[item].tax);
            $('#Qty').focus(function () {
                $(this).select()
            });
            $('#Qty').focus();
            return item;
        }
    })
    var t = $('#billItems').DataTable({
        scrollY: "120px",
        scrollCollapse: true,
        paging: false,
        sorting: false,
        searching: false,
        language: {
            "info": "Item Count : _TOTAL_ ",
        },
        columnDefs: [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        order: [[1, 'asc']]
    });

    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    $('#Qty').bind('keyup mouseup', function () {
        var gst = $('#gst').val();
        var total = $('#Price').val() * $('#Qty').val();
        $('#total').val(calGST(gst, total));
    })

    var rowHtml = $("#billItems").find("tr")[1].outerHTML;;
    function refreshTable(billItems) {
        t.clear();
        $.each(billItems, function (index, billItem) {
            var newRowHTML = rowHtml
                .replace(/itemName/g, billItem.itemName)
                .replace(/PriceVal/g, billItem.Price)
                .replace(/qtyVal/g, billItem.qty)
                .replace(/tax/g, billItem.gst)
                .replace(/totalVal/g, billItem.total);

            t.row.add($(newRowHTML)).draw();
        })
        $("#tableContent").show()
    }

    var getItem = function () {
        return {
            itemName: $('#Items').val(),
            qty: $('#Qty').val(),
            Price: $('#Price').val(),
            totalWithoutTax: ($('#Price').val() * $('#Qty').val()),
            total: $('#total').val(),
            gst: $('#gst').val()
        }
    }

    $('#Qty').keydown(function (e) {
               
        var keycode = e.keyCode ? e.keyCode : e.which;
        if (keycode == 13) {
            e.preventDefault();
            $('#btnAddBillItem').click();
        }
    })

    $('#btnAddBillItem').click(function () {
        if ($('#billForm').valid()) {
            var newItem = getItem();
            var contains = vm.billItems.some(function (elem) {
                return newItem.itemName === elem.itemName;
            });
            if (!contains) {
                vm.billItems.push(newItem)
                refreshTable(vm.billItems);
                displayTotal(vm.billItems);
                addTaxes(vm.billItems);
                $('#Items').val("");
                $('#Price').val("");
                $('#Qty').val("1");
                $('#gst').val("");
                $('#total').val("");
                $('#Items').focus();
            }
            else {
                toastr.warning("Item Already Exists", "Duplication", { positionClass: "toast-top-center" })
            }
            console.log(vm)
        }
    });

    $('#billItems').on("click", ".i-update", function () {
        var row = $(this).closest('tr');
        var qty = row.find('.itemUpdate').val();
        var price = $(this).attr('data-price-val');
        var gst = $(this).attr('data-gst-val');
        updateQuantity(qty, $(this).attr('data-item-name'));
        displayTotal(vm.billItems);
        row.find('.totalUpdate').val(calGST(gst, price * qty));
        toastr.success("", "Quantity Updated!", { positionClass: "toast-top-center" })
    });

    $('#billItems').on("click", ".i-delete", function () {
        deleteItem($(this).attr('data-item-name'));
        t.row($(this).parents('tr')).remove().draw();
        displayTotal(vm.billItems);
        addTaxes(vm.billItems);
    });

    var updateQuantity = function (qty, name) {
        $.each(vm.billItems, function (i, billItem) {
            if (billItem.itemName == name) {
                billItem.qty = qty;
                billItem.total = calGST(billItem.gst,billItem.Price * qty);
                billItem.totalWithoutTax = billItem.Price * qty;
            }
        })
    }

    var deleteItem = function (name) {
        var itemToDelete;
        $.each(vm.billItems, function (i, billItem) {
            if (billItem.itemName == name) {
                itemToDelete = billItem;
            }
        })
        vm.billItems.splice(vm.billItems.indexOf(itemToDelete), 1);
    }

    var addTaxes = function (billItems) {
        taxes=[];
        $.each(billItems, function (i, item) {
            var tax = {
                taxPercent: item.gst,
                taxValue: (item.totalWithoutTax * item.gst) / 100,
            }

            var contains = taxes.some(function (elem) {
                return tax.taxPercent === elem.taxPercent;
            });

            if (contains) {
                $.each(taxes, function (i, taxItem) {
                    if (taxItem.taxPercent == tax.taxPercent) {
                        taxItem.taxValue += tax.taxValue;
                    }
                })
            }
            else {
                taxes.push(tax);
            }
        });

        displayTaxes();
    }

    var displayTaxes = function () {
        $('#GSTTable tbody').html("");
        $.each(taxes, function (i, taxItem) {
            var gst = taxItem.taxPercent;
            var gstVal = taxItem.taxValue;
            $('#GSTTable tbody').append(
           "<tr>" +
           "<td class='text-center'>" + gst + " <br> Rs " + gstVal + "</td>" +
           "<td class='text-center'>" + (gst/2) + " <br> Rs " + (gstVal/2) + "</td>" +
           "<td class='text-center'>" + (gst/2) + " <br> Rs " + (gstVal/2) + "</td>" +
           "</tr>"
           );
        })
               
    }

    var totalPayable;

    var displayTotal = function (billItems) {
        var GTWT = 0;
        var GT = 0;
        $.each(billItems, function (i, item) {
            GTWT = GTWT + item.totalWithoutTax;
            GT = GT + parseFloat(item.total);
        });
        var discount = $('#discount').val();
        $('#grandTotalWithoutTax').html(GTWT);
        var discountValue = (GTWT * discount) / 100;
        var payableWithoutTaxes = GTWT - discountValue;
        var totalTaxVal = GT - GTWT;
        var payableWithTaxes = payableWithoutTaxes + totalTaxVal;
        totalPayable=payableWithTaxes;
        $('#dDiscount').html(discount + "% (Rs " + discountValue + ")");
        $('#payable').html("Rs " + payableWithTaxes.toPrecision(5));
        $('#dtotal').html("Rs " + payableWithoutTaxes.toPrecision(5));
        $('#totalTax').html("Rs " + totalTaxVal.toPrecision(5));
        $('#amountInWords').html(inWords(payableWithTaxes)+" rupees only")
    }

    $('#billForm').submit(function (e) {
        e.preventDefault();
        e.stopPropagation();
        if ($('#billForm').valid()) {
            $('.loading').show();
            vm.cardNumber = $('#CardNo').val();
            var data = JSON.stringify(vm);
            $.ajax({
                url: $('#billForm').attr('action'),
                data: data,
                type: 'POST',
                contentType: "application/json; charset=utf-8"
            }).done(function (data) {
                $('.loading').hide();
                if (data.success) {
                    $("#tableContent").hide();
                    $('#Items').val("");
                    $('#Price').val("");
                    $('#Qty').val("1");
                    $('#gst').val("");
                    $('#total').val("");
                    $('#Items').focus();
                    toastr.success(data.message, "Success", {positionClass:"toast-top-center"});
                } else {
                    toastr.error(data.message, "Error", { positionClass: "toast-top-center" });
                }
            }).fail(function () {
                $('.loading').hide();
                toastr.error("Server Error", "Error", { positionClass: "toast-top-center" });
            })
        }
    })
})
