
$(document).on("click", "#Deleteanchor", function () {
    var id = $(this).attr("data-id");
    swal({
        title: "Are you sure?",
        text: "You want to delete this user ?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/Webmaster/Users/DeleteFunc",
                data: '{id:' + id + '}',
                dataType: "json",
                success: function (response) {
                    if (response.success) {
                        // This code is redirct to index page click on OK Button
                        swal({
                            title: "Deleted!",
                            text: "User has been deleted successfully.",
                            type: "success"
                        }, function (isConfirm1) {
                            if (isConfirm1) {
                                window.location.href = '/Webmaster/Users/Index';
                            }
                            else {
                                swal("Cancelled", "User is safe :)", "error");
                            }
                        });
                    }
                },
                failure: function (response) {
                    alert("You have canceled the changes");
                }
            });
        } else {
            swal("Cancelled", "User not deleted", "error");
        }
    });
});

// Delete Component From Component Page  
$(document).on("click", "#DeleteanchorComp", function () {
    var id = $(this).attr("data-id");
    swal({
        title: "Are you sure?",
        text: "Are You Sure to Delete this Component ?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        cancelButtonText: "No",
        confirmButtonText: "Yes",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/Webmaster/Component/DeleteConfirmed",
                data: '{id:' + id + '}',
                dataType: "json",
                success: function (response) {
                    if (response.success) {
                        // This code is redirct to index page click on OK Button
                        swal({
                            title: "Deleted!",
                            text: "Component has been deleted successfully.",
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
                },
                failure: function (response) {
                    alert("You have canceled the changes");
                }
            });
        } else {
            swal("Cancelled", "Component not deleted", "error");
        }
    });
});