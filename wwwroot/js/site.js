// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//$body = $("body");
//$(document).on({
//    ajaxStart: function () { $body.addClass("loading"); },
//    ajaxStop: function () { $body.removeClass("loading"); }
//});

$body = $(".content-page .content");
$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});

function GetPatientCareComponentView(viewName, patientId) {
    //alert(patientId);
    $("#patientCareInfoBody").empty();
    $("#infoTitle").empty();

    $.ajax({
        type: "Post",
        url: "/Registration/PatientRegistrationAKT/PatientCareComponent",
        dataType: "html",
        data: { "view": viewName, "patientId": patientId },
        success: function (data) {
            //alert("Success");
            //console.log(data);

            $("#patientCareInfoBody").html(data);
            $("#infoTitle").html($("#titleId").val());
        },
        error: function (object) {
        }
    });
}

function ContentPageLoading() {
    $(".content-page .content").addClass("loadingPage");
}

function RemoveContentPageLoading() {
    $(".content-page .content").removeClass("loadingPage");
}

var sid = 0;
function getst(coid) {
    var url = "/Administration/Employee/GetStates";
    var cid = $("#CountryId").val();
    var defaultSelect = "<option value='" + "" + "'>" + "--Select State--" + "</option>";
    $("#StateId").empty();
    $("#StateId").html(defaultSelect);
    $.getJSON(url, { CountryId: cid }, function (data) {
        var items = '';
        $.each(data, function (i, states) {
            items += "<option value='" + states.value + "'>" + states.text + "</option>";
        });
        $("#StateId").append(items);

        if (coid != null) {
            $("#StateId").val(coid);

        }
    });
}

function getts(tsid) {
    var url = "/Administration/Employee/GetTownships";
    var cid = sid;
    var defaultSelect = "<option value='" + "" + "'>" + "--Select Township--" + "</option>";
    $("#TownshipId").empty();
    $("#TownshipId").html(defaultSelect);
    $.getJSON(url, { StateId: cid }, function (data) {
        var items = '';
        $.each(data, function (i, tsp) {
            items += "<option value='" + tsp.value + "'>" + tsp.text + "</option>";
        });
        $("#TownshipId").append(items);

        if (tsid != null) {
            $("#TownshipId").val(tsid);

        }
    });
}


function TableEditClick(data) {

    $(".field-validation-error, .field-validation-valid, .validation-summary-errors, .validation-summary-valid").empty();
    //console.log(data);
    $(".btnSaveSubmitToggle").hide();
    $(".btnUpdateSubmitToggle").show();

    $.each(data, function (key, value) {

        var modelToId = key.replace(/\.|\[|\]/g, "_");
        var dynamicId = "#" + modelToId;

        if (dynamicId == "#StateId") {
            getst(value);
            sid = value;
        }

        else if (dynamicId == "#TownshipId") {
            getts(value);

        }


        if (dynamicId != "#StateId" && dynamicId != "#TownshipId") {
            $(dynamicId).val(value);
        }

    });
}


function TableRowEditClick(data) {

    $(".field-validation-error, .field-validation-valid, .validation-summary-errors, .validation-summary-valid").empty();
    //console.log(data);
    $(".btnSaveSubmitToggle").hide();
    $(".btnUpdateSubmitToggle").show();
    $.each(data, function (key, value) {
        var modelToId = key.replace(/\.|\[|\]/g, "_");
        var dynamicId = "#" + modelToId;
        if (dynamicId == "#OrganisationMasterId") {
            sessionStorage.setItem("omid", value);
        }


        if (dynamicId == "#OrganisationMasterId") {


            $("#showlist").show();

        }

        if (dynamicId == "#OrganisationCode") {
            $("#parent").text(value);
        }
        if (dynamicId == "#ContactFirstName") {
            $("#contactname").val(value);
        }
        if (dynamicId == "#OrganisationTypeId") {

            if (value == 2) {
                $("#orgType").text("Group");
                $("#office").text("Group Office");
            }
            else if (value == 3) {
                $("#orgType").text("Branch");
                $("#office").text("Head Office");
            }
            else {
                $("#orgType").text("Department");
                $("#office").text("Sub Department");
            }


        }
        //console.log(dynamicId);
        if (!$(dynamicId).is(":checkbox")) {
            //if (dynamicId == "#EmployeeId") {
            //    getid(parseInt(value));
            //}
            $(dynamicId).val(value);
        } else {
            if (value) {
                $(dynamicId).attr("checked", "checked").prop("checked", true);
            } else {
                $(dynamicId).removeAttr("checked").prop("checked", false);
            }
        }
    });

    var count = $(".Master1").length;
    var incCount = 0;
    var master1Id = parseInt(document.getElementById("OrganisationMasterId").value);
    for (var i = 0; i < count; i++) {

        var checkId = parseInt(document.getElementsByClassName("Master1")[i].innerText);
        if (master1Id != checkId) {
            document.getElementsByClassName("ContactTable")[0].rows[i + 1].style.display = "none";
            incCount += 1;
        }
        else {
            document.getElementsByClassName("ContactTable")[0].rows[i + 1].removeAttribute("style");

        }
    }
    //count = count - incCount;
}

function getid(data) {
    return data;
}

function TableRowDeleteClick(deleteRecordId, deleteConfirmWarningText = "Are You Sure To Delete This Record ?", popupSelectorId = "DeleteConfirmModal") {
    //console.log(deleteRecordId);
    $("#deleteConfirmWarningText").html(deleteConfirmWarningText);
    $(".field-validation-error, .field-validation-valid, .validation-summary-errors, .validation-summary-valid").empty();
    ResetFormAndShowHideBtnSubmit();
    //$("#txtToDeleteRecordId").val("");
    $("#txtToDeleteRecordId").val(deleteRecordId);
    $("#" + popupSelectorId).modal('show');
}

function ResetFormAndShowHideBtnSubmit(formSelectorName = "form") {
    $(".btnSaveSubmitToggle").show();
    $(".btnUpdateSubmitToggle").hide();
    $(formSelectorName).trigger("reset");
    $(".resetInputValue").val("");
    //$(formSelectorName + " " + "input").val("");
    $(".field-validation-error, .field-validation-valid, .validation-summary-errors, .validation-summary-valid").empty();
}

function StringCompareFormat(inputString) {
    if (inputString == null) {
        return inputString;
    } else {
        inputString = inputString.toString();
        return inputString.replace(" ", "").toLowerCase().trim();
    }
}

InputEnterBtnSubmitDefined();
function InputEnterBtnSubmitDefined() {
    $("form input:not([type='submit']), form input:not([type='button']), form input:not([type='button'])").on("keyup keypress", function (e) {
        var keyCode = e.keyCode || e.which;
        if (keyCode === 13) {
            e.preventDefault();
            if ($("#btnSaveSubmit").css("display") == "none") {
                $("#btnUpdateSubmit").trigger("click");
            } else {
                $("#btnSaveSubmit").trigger("click");
            }
        }
    });
}

MinAndMaxCompareValueValidator();
function MinAndMaxCompareValueValidator() {
    jQuery.validator.addMethod("comparedminmax",
        function (value, element, param) {
            var comparedId = $(element).data("val-comparedproperty");
            var comparedValue = $(comparedId).val();
            if ($.isNumeric(value) && comparedValue != null && comparedValue.trim() != "" && $.isNumeric(comparedValue.trim())) {
                var isMax = $(element).data("val-ismax");
                setTimeout(function () {
                    $(comparedId).trigger("focusout");
                }, 300);
                return (isMax && parseFloat(value) > parseFloat(comparedValue)) || (!isMax && parseFloat(value) < parseFloat(comparedValue));
            } else {
                return true;
            }
        });
    jQuery.validator.unobtrusive.adapters.addBool('comparedminmax');
}

RequiredFieldWhenUpdate();
function RequiredFieldWhenUpdate() {
    jQuery.validator.addMethod("requiredwhenupdate",
        function (value, element, param) {
            var refId = $(element).attr("data-val-referenceproperty");
            var refValue = $(refId).val();
            if (refValue == null || refValue.toString().trim() == "" || refValue.toString().trim() == "0") {
                return true;
            } else {
                if (value == null || value.toString().trim() == "") {
                    return false;
                } else {
                    return true;
                }
            }
        });
    jQuery.validator.unobtrusive.adapters.addBool("requiredwhenupdate");
}

function FormSubmitLoadingPage(selectorForm) {
    $(selectorForm).submit(function () {
        if ($(selectorForm).valid())
            ContentPageLoading();
        else
            RemoveContentPageLoading();
    });
}
