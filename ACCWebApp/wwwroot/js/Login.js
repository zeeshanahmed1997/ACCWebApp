$(document).ready(function () {
    $("#loginForm").submit(function (e) {
        e.preventDefault(); // Prevent form submission

        var email = $("#email").val();
        var password = $("#password").val();
        var userCategory = $("#userCategory").val();

        var data = {
            Email: email,
            Password: password,
            UserCategory: userCategory
        };

        $.ajax({
            url: 'https://localhost:7241/api/login',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                // Handle successful login
                console.log(response);
                // Redirect to SalesDashboard page
                window.location.href = '@Url.Action("SalesDashboard", "Home")';
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }

        });
    });
});