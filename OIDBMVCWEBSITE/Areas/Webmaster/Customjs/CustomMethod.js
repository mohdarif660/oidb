

// this method call from SetPermission Page.
$(document).ready(function () {
    $("#AddCheckAll").change(function () {
        if (this.checked) {
            $(".AddCheckBoxSingle").each(function () {
                this.checked = true;
            })
        }
        else {
            $(".AddCheckBoxSingle").each(function () {
                this.checked = false;
            })
        }
    });
    $("#EditCheckAll").change(function () {
        if (this.checked) {
            $(".EditCheckBoxSingle").each(function () {
                this.checked = true;
            })
        }
        else {
            $(".EditCheckBoxSingle").each(function () {
                this.checked = false;
            })
        }
    });

    $("#DeleteCheckAll").change(function () {
        if (this.checked) {
            $(".DeleteCheckBoxSingle").each(function () {
                this.checked = true;
            })
        }
        else {
            $(".DeleteCheckBoxSingle").each(function () {
                this.checked = false;
            })
        }
    });
    $("#NACheckAll").change(function () {
        if (this.checked) {
            $(".NACheckBoxSingle").each(function () {
                this.checked = true;
            })
        }
        else {
            $(".NACheckBoxSingle").each(function () {
                this.checked = false;
            })
        }
    });

    // This method call from mapPartialView when set commponent permission to user
    // checked All Checkbox
    $("#CheckBoxAll").change(function () {
        if (this.checked) {
            $(".PerCheckBoxSingle").each(function () {
                this.checked = true;
            })
        } else {
            $(".PerCheckBoxSingle").each(function () {
                this.checked = false;
            })
        }
    });
    // Unchecked Select All Checkbox
    $(".PerCheckBoxSingle").click(function () {
        if ($(this).is(":checked")) {
            var isAllChecked = 0;
            $(".PerCheckBoxSingle").each(function () {
                if (!this.checked)
                    isAllChecked = 1;
            })
            if (isAllChecked == 0) { $("#CheckBoxAll").prop("checked", true); }
        } else {
            $("#CheckBoxAll").prop("checked", false);
        }
    });

    // Call From User Trial Page 
    $(".DatePicker").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yy'
    });

    // This Function Work from User From Delete User 
    $(document).on("click", "#ChangePassword", function () {
        var id = $(this).attr("data-id");
        var options = {
            "backdrop": "static",
            keyboard: true
        };
        $.ajax({
            type: "POST",
            url: "/Webmaster/Users/UserChangePasswordModal",
            data: '{"id":' + id + '}',
            contentType: "application/json;charset=utf-8",
            dataType: "html",
            success: function (data) {
                $("#myModalContent").html(data);
                $("#myModal").modal(options);
                $("#myModal").modal("show");
            },
            error: function () {
                alert("Modal Popup load failed.");
            }

        });

    });

    //This Function Work From Map Component 
    $("#ddlPtypeId").change(function () {
        $.ajax({
            url: '/Webmaster/Mapcomponent/GetComponent/' + $("#ddlPtypeId").val(),
            datatype: "json",
            type: "post",
            contenttype: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                $("#partialDiv").html(data);
            },
            error: function (xhr) {
                $("#partialDiv").html(xhr);
            }
        });
    });


   // This Function Work Add And Edit Component Popup Model
    $(document).on("click", ".AddComponentclass", function () {
        var id = $(this).attr("data-id");
        var options = {
            "backdrop": "static",
            keyboard: true
        };
        $.ajax({
            type: "POST",
            url: "/Webmaster/Component/Create",
            data: '{"id":' + id + '}',
            contentType: "application/json;charset=utf-8",
            dataType: "html",
            success: function (data) {
                $("#myModalContent").html(data);
                $("#myModal").modal(options);
                $("#myModal").modal("show");
            },
            error: function () {
                alert("Modal Popup load failed.");
            }

        });

    });

});

// This Function Work from User From To  Change password
function SubmitForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: "POST",
            url: form.action,
            data: $(form).serialize(),
            success: function (data) {
                $("#myModal").modal("hide");
                var classname = "success";
                if (data.success) {
                    classname = "success";
                }
                else {
                    classname = "error";
                }
                $.notify(data.message, {
                    globalPosition: "top center",
                    className: classname
                });
            }
        });
    }
    return false;
}

// this function work on Add Component
function SubmitForm1(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: "POST",
            url: form.action,
            data: $(form).serialize(),
            success: function (data) {
                if (data.success) {
                    $("#myModal").modal("hide");
                    //alert(data.message);
                    //window.location.href = "/Webmaster/Component/Index";
                    // This code is redirct to index page click on OK Button
                    swal({
                        title: "Done!",
                        text: data.message,
                        type: "success"
                    }, function (isConfirm1) {
                        if (isConfirm1) {
                            window.location.href = '/Webmaster/Component/Index';
                        }
                        else {
                            swal("Cancelled", "Component is safe", "error");
                        }
                    });
                }
            }
        });
    }
    return false;
}
// Alert Message Automatically 
window.setTimeout(function () {
    $(".alertmsg").fadeTo(2000, 500).slideUp(500, function () {
        $(this).remove();
    });
}, 3000);

