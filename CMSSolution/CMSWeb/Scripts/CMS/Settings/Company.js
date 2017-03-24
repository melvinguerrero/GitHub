var dialog,
    form = $('#formData'),
    filterName = $('#filterName'),
    formName = $('#formName'),
    formABN = $('#formABN'),
    formDescription = $('#formDescription'),
    formWebsite = $('#formWebsite'),
    hID = $('#hID'),
    allFields = $([]).add(formName).add(formABN).add(formWebsite),
    tips = $(".validateTips");

ReInitDialog("Create Company");

function ReInitDialog(title) {
    dialog = $("#dialog-form").dialog({
        autoOpen: false,
        height: 400,
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
    ReInitDialog("Update Company");

    $.get("/Setting/GetCompany", {
        id: id
    }, function (data) {
        if (data != null) {
            hID.val(data.CompanyID);
            formName.val(data.Name);
            formABN.val(data.ABN);
            formDescription.val(data.Description);
            formWebsite.val(data.Website);
        }

        dialog.dialog("open");
    });
}

function Delete(id) {
    if (confirm("Are you sure you want to delete this Company?")) {
        $.post("/Setting/DeleteCompany",
            {
                id:id
            },
            function (reponse) {
                if (reponse.ErrMsg != null && reponse.ErrMsg.length > 0) {
                    alert(reponse.ErrMsg);
                }
                else {
                    Reload();
                }
            });
    }
}

function Save() {
    var valid = true;
    var urlRegex = /(http|ftp|https):\/\/[\w-]+(\.[\w-]+)+([\w.,@?^=%&amp;:\/~+#-]*[\w@?^=%&amp;\/~+#-])?/;

    allFields.removeClass("ui-state-error");

    valid = valid && checkLength(formName, "Company Name", 1);
    valid = valid && checkLength(formABN, "ABN", 1);
    valid = valid && checkLength(formWebsite, "Website", 5);

    valid = valid && checkRegexp(formWebsite, urlRegex, "eg. http://www.google.com");

    if (valid) {
        $.post("/Setting/CompanySave",
        {            
            CompanyID:hID.val(),
            Name: formName.val(),
            ABN: formABN.val(),
            Description: formDescription.val(),
            Website: formWebsite.val()
        },
        function (reponse) {
            if (reponse.ErrMsg != null && reponse.ErrMsg.length > 0) {
                formName.addClass("ui-state-error");
                updateTips(reponse.ErrMsg);
            }
            else {
                dialog.dialog("close");
                Reload();
            }
        });
    }
    return valid;
}

function ResetForm() {
    hID.val("");
    formName.val("");
    formABN.val("");
    formDescription.val("");
    formWebsite.val("");
}

function Reload() {
    window.location = "/Setting/Company?filterName=" + filterName.val();
}

$(document).ready(function () {
    var companies = null;

    $.get("/Setting/CompanyList", function (data) {
        companies = data;

        $("#filterName").autocomplete({
            source: companies
        });
    });

    $("#lnkNew").button().on("click", function () {
        ReInitDialog("Create Company");
        dialog.dialog("open");
    });
});