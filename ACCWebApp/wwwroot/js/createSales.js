$(document).ready(function () {
    // Populate gender dropdown
    $.ajax({
        url: '/Sales/FetchGenders',
        type: 'GET',
        success: function (data) {
            console.log(data); // Check the received data in the console
            var clothingTypeDropdown = $('#GenderId');
            //  clothingTypeDropdown.empty(); // Remove existing options before populating

            $.each(data, function (index, type) {
                clothingTypeDropdown.append($('<option></option>').val(type.genderId).text(type.genderName));
            });
        }
    });

    $.ajax({
        url: '/Sales/FetchClothings',
        type: 'GET',
        success: function (data) {
            console.log(data); // Check the received data in the console
            var clothingTypeDropdown = $('#ClothingId');
            //  clothingTypeDropdown.empty(); // Remove existing options before populating

            $.each(data, function (index, type) {
                clothingTypeDropdown.append($('<option></option>').val(type.clothingId).text(type.clothingName));
            });
        }
    });
    // Handle form submission
    // Handle form submission
    $('#saleForm').submit(function (event) {
        event.preventDefault(); // Prevent the form from submitting normally

        var formData = $(this).serialize(); // Serialize the form data

        $.ajax({
            url: '/Sales/Create',
            type: 'POST',
            data: formData,
            success: function (response) {
                console.log(response); // Handle the response from the server
                // Perform any additional actions after successful submission
                //window.location.href = '/Home/SalesDashboard';
            },
            error: function (error) {
                console.log(error); // Handle the error case
                // Perform any additional actions for error handling
            }
        });
    });

});
