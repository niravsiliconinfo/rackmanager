/* account-deletion.js
   Client controller for the Account Deletion Request page.
   Posts form data to the Web API endpoint (AccountDeletionApiController)
   and renders success/error feedback in-place, matching the RackManager theme.
*/
(function ($) {
    "use strict";

    var API_URL = "/api/accountdeletion/submit"; // routes to AccountDeletionApiController.Submit

    function getAntiForgeryToken() {
        var $token = $('input[name="__RequestVerificationToken"]');
        return $token.length ? $token.val() : null;
    }

    function showAlert(type, message) {
        var $alert = $("#resultAlert");
        $alert
            .removeClass("alert-success alert-danger")
            .addClass(type === "success" ? "alert-success" : "alert-danger")
            .text(message)
            .stop(true, true)
            .fadeIn(150);
    }

    function setSubmitting(isSubmitting) {
        var $btn = $("#btnSubmitDeletion");
        $btn.prop("disabled", isSubmitting);
        $btn.text(isSubmitting ? "Submitting..." : "Confirm Permanent Deletion");
    }

    function validateForm(data) {
        if (!data.FullName || !data.FullName.trim()) {
            return "Please enter your full name.";
        }
        if (!data.UserName || !data.UserName.trim()) {
            return "Please enter your user name.";
        }
        if (!data.Email || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(data.Email)) {
            return "Please enter a valid email address.";
        }
        if (!data.ConfirmDelete) {
            return "Please confirm that you understand this action is permanent.";
        }
        return null;
    }

    function collectFormData() {
        return {
            FullName: $("#FullName").val(),
            UserName: $("#UserName").val(),
            Email: $("#Email").val(),
            Reason: $("#Reason").val(),
            ConfirmDelete: $("#confirmDelete").is(":checked")
        };
    }

    $(function () {
        $("#accountDeletionForm").on("submit", function (e) {
            e.preventDefault();

            var formData = collectFormData();
            var validationError = validateForm(formData);

            if (validationError) {
                showAlert("error", validationError);
                return;
            }

            setSubmitting(true);
            $("#resultAlert").hide();

            $.ajax({
                url: API_URL,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(formData),
                beforeSend: function (xhr) {
                    var token = getAntiForgeryToken();
                    if (token) {
                        xhr.setRequestHeader("__RequestVerificationToken", token);
                    }
                },
                success: function (response) {
                    setSubmitting(false);

                    if (response && response.success) {
                        showAlert(
                            "success",
                            "Your account deletion request has been submitted successfully. Our support " +
                            "team will review and verify your information within 24-48 hours."
                        );
                        $("#accountDeletionForm")[0].reset();
                    } else {
                        showAlert("error", (response && response.message) || "Something went wrong. Please try again.");
                    }
                },
                error: function (xhr) {
                    setSubmitting(false);
                    var message = "Unable to submit your request right now. Please try again later.";
                    if (xhr && xhr.responseJSON && xhr.responseJSON.message) {
                        message = xhr.responseJSON.message;
                    }
                    showAlert("error", message);
                }
            });
        });
    });
})(jQuery);
