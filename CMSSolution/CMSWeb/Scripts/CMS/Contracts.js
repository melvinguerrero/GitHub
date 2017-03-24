var tbodyContract = $("#tbodyContract")
    selFilterCompany = $("#Company")
    selFilterContractType = $("#ContractType")
    selFilterValidContract = $("#selValidContract")
    selFilterNearRenewal = $("#selNearRenewal");

var dialog, 
    form = $('#formData'),
    formCompany = $('#formCompany'),
    formContractType = $('#formContractType'),
    formSignedDate = $('#formSignedDate'),
    formEndDate = $('#formEndDate'),
    formRenewalDate = $('#formRenewalDate'),
    formPrice = $('#formPrice'),
    hID = $('#hID'),
    allFields = $([]).add(formCompany).add(formEndDate).add(formContractType).add(formPrice),
    tips = $(".validateTips");

var dialogView,
    divViewContract = $('#divViewContract')
    pagerIndex = 1;

function LoadContracts() {
    $("#tbodyContract").html("");

    var template = "<tr>"
    template += "<td><a href='/Setting/Company?filterName=#Company'>#Company</a></td>";
    template += "<td>#ContractType</td>";
    template += "<td>#SignedDate</td>";
    template += "<td>#EndDate</td>";
    template += "<td>#RenewDate</td>";
    template += "<td>#Price</td>";
    template += "<td style='color:#IsValidColor'>#IsValid</td>";
    template += "<td style='text-align:center;'><a href='#' onclick='View(#ContractID);'>View</a> <a href='#' onclick='Edit(#ContractID);'>Edit</a> <a href='#' onclick='Delete(#ContractID);'>Delete</a></td>";
    template += "<tr>";

    $.get("/Contract/GetContracts", {
        companyID: selFilterCompany.val(), 
        contractTypeID: selFilterContractType.val(), 
        isValid: selFilterValidContract.val(), 
        isNearRenewal: selFilterNearRenewal.val(),
        pageIndex: pagerIndex
    }, function (data) {
        if (data != null) {
            var htmlText = "";
            for (var i = 0; i < data.ItemList.length; i++) {
                var element = template;

                element = element.replace("#Company", data.ItemList[i].Company);
                element = element.replace("#Company", data.ItemList[i].Company);
                element = element.replace("#ContractType", data.ItemList[i].ContractType);
                element = element.replace("#SignedDate", data.ItemList[i].SignedDateText);
                element = element.replace("#EndDate", data.ItemList[i].EndDateText);
                element = element.replace("#RenewDate", data.ItemList[i].RenewalDateText);
                element = element.replace("#IsValidColor", data.ItemList[i].IsValidText == "Yes" ? "Green" : "Red");
                element = element.replace("#IsValid", data.ItemList[i].IsValidText);
                element = element.replace("#Price", data.ItemList[i].PriceText);
                element = element.replace("#ContractID", data.ItemList[i].ContractID);
                element = element.replace("#ContractID", data.ItemList[i].ContractID);
                element = element.replace("#ContractID", data.ItemList[i].ContractID);

                htmlText += element;
            }
            tbodyContract.html(htmlText);

            LoadPager(data.PageSize, data.RecordCount, data.PageIndex);
        }
    });
}


function LoadPager(pageSize, recordCount, currentPage) {
    var pageCount = recordCount < pageSize ? 1 : Math.round(recordCount / pageSize);
    currentPage = currentPage == 0 ? 1 : currentPage;

    $('#pagination-top').twbsPagination('destroy');
    $('#pagination-bot').twbsPagination('destroy');

    $('#pagination-top').twbsPagination({
        totalPages: pageCount,
        startPage: currentPage,
        visiblePages: 5,
        paginationClass: 'pagination',
        initiateStartPageClick: false,
        first: '',
        prev: '',
        next: '',
        last: '',
        onPageClick: function (event, next_page) {
            pagerIndex = next_page;
            LoadContracts();
        }

    });
    $('#pagination-bot').twbsPagination({
        totalPages: pageCount,
        startPage: currentPage,
        visiblePages: 5,
        paginationClass: 'pagination',
        initiateStartPageClick: false,
        first: '',
        prev: '',
        next: '',
        last: '',
        onPageClick: function (event, next_page) {
            pagerIndex = next_page;
            LoadContracts();
        }
    });
}

function View(id) {
    divViewContract.html("");
    var template = "<table class='table'>";
    template += "<tr><td><label>Company<label></td><td>#Company</td></tr>";
    template += "<tr><td><label>ContractType<label></td><td>#ContractType</td></tr>";
    template += "<tr><td><label>SignedDate<label></td><td>#SignedDate</td></tr>";
    template += "<tr><td><label>EndDate<label></td><td>#EndDate</td></tr>";
    template += "<tr><td><label>RenewDate<label></td><td>#RenewDate</td></tr>";
    template += "<tr><td><label>Price<label></td><td>#Price</td></tr>";
    template += "<tr><td><label>IsValid<label></td><td>#IsValid</td></tr>";
    template += "</table >";

    $.get("/Contract/GetContract", {
        id: id
    }, function (data) {
        if (data != null) {
            var element = template;
            element = element.replace("#Company", data.Company);
            element = element.replace("#ContractType", data.ContractType);
            element = element.replace("#SignedDate", data.SignedDateText);
            element = element.replace("#EndDate", data.EndDateText);
            element = element.replace("#RenewDate", data.RenewalDateText);
            element = element.replace("#IsValid", data.IsValidText);
            element = element.replace("#Price", data.PriceText);
            
            divViewContract.html(element);
        }

        dialogView.dialog("open");
    });
}

function ReInitDialog(title) {
    dialog = $("#dialog-form").dialog({
        autoOpen: false,
        height: 500,
        width: 350,
        modal: true,
        title: title,
        buttons: {
            "Submit": Save,
            Cancel: function () {
                dialog.dialog("close");
            }
        },
        close: function () {
            ResetForm();
            allFields.removeClass("ui-state-error");
            updateTips('');
            tips.removeClass("ui-state-highlight");
        }
    });
}

function updateTips(t) {
    tips
        .text(t)
        .addClass("ui-state-highlight");
    setTimeout(function () {
        tips.removeClass("ui-state-highlight", 1500);
    }, 500);
}

function checkLength(o, n, min) {
    if (o.val().length < min) {
        o.addClass("ui-state-error");
        updateTips(n + " is required.");
        return false;
    } else {
        return true;
    }
}

function checkRegexp(o, regexp, n) {
    if (!(regexp.test(o.val()))) {
        o.addClass("ui-state-error");
        updateTips(n);
        return false;
    } else {
        return true;
    }
}

function Edit(id) {
    ReInitDialog("Update Contract");

    $.get("/Contract/GetContract", {
        id: id
    }, function (data) {
        if (data != null) {
            hID.val(data.ContractID);
            formCompany.val(data.CompanyID);
            formContractType.val(data.ContractTypeID);
            formSignedDate.val(data.SignedDateText);
            formEndDate.val(data.EndDateText);
            formRenewalDate.val(data.RenewalDateText);
            formPrice.val(data.PriceText);
        }

        dialog.dialog("open");
    });
}

function Delete(id) {
    if (confirm("Are you sure you want to delete this Contract?")) {
        $.post("/Contract/DeleteContract",
            {
                id: id
            },
            function (reponse) {
                if (reponse.ErrMsg != null && reponse.ErrMsg.length > 0) {
                    alert(reponse.ErrMsg);
                }
                else {
                    LoadContracts();
                }
            });
    }
}

function Save() {
    var valid = true;

    allFields.removeClass("ui-state-error");

    valid = valid && checkLength(formCompany, "Company Name", 1);
    valid = valid && checkLength(formContractType, "Contract Type", 1);
    valid = valid && checkLength(formPrice, "Price", 1);
    

    if (valid) {
        $.post("/Contract/ContractSave",
            {
                ContractID: hID.val(),
                CompanyID: formCompany.val(),
                ContractTypeID: formContractType.val(),
                SignedDate: formSignedDate.val(),
                Description: formEndDate.val(),
                EndDate: formEndDate.val(),
                RenewalDate: formRenewalDate.val(),
                Price: formPrice.val().replace(',', '').replace(',', '')
            },
            function (reponse) {
                if (reponse.ErrMsg != null && reponse.ErrMsg.length > 0) {
                    formEndDate.addClass("ui-state-error");
                    updateTips(reponse.ErrMsg);
                }
                else {
                    dialog.dialog("close");
                    LoadContracts();
                }
            });
    }
    return valid;
}

function ResetForm() {
    hID.val("");
    formCompany.val("");
    formContractType.val("");
    formSignedDate.val("");
    formEndDate.val("");
    formRenewalDate.val("");
    formPrice.val("");
}

$(document).ready(function () {
    ReInitDialog("Create Contract");
    LoadContracts();

    $(formSignedDate).datepicker();
    formEndDate.datepicker();
    formRenewalDate.datepicker();


    $("#btnSearch").on("click", function () {
        pagerIndex = 1;
        LoadContracts();
    });

    $("#lnkNew").button().on("click", function () {
        ReInitDialog("Create Contract");
        dialog.dialog("open");
    });

    dialogView = $("#dialog-form-view").dialog({
        autoOpen: false,
        height: 400,
        width: 350,
        modal: true
    });
});