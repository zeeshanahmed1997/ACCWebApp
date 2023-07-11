$(document).ready(function () {
  // Populate fabric dropdown
        $.ajax({
            url: '/Sales/FetchFabrics',
            type: 'GET',
            success: function (data) {
                console.log(data); // Check the received data in the console
                var fabricDropdown = $('#FabricId');
               // fabricDropdown.empty(); // Remove existing options before populating

                $.each(data, function (index, fabric) {
                    fabricDropdown.append($('<option></option>').val(fabric.fabricId).text(fabric.fabricName));
                    console.log(fabric.fabricId);
                });
            }
        });

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

        // Populate clothing type dropdown
        $.ajax({
            url: '/Sales/FetchClothingTypes',
            type: 'GET',
            success: function (data) {
                console.log(data); // Check the received data in the console
                var clothingTypeDropdown = $('#TypeId');
                //  clothingTypeDropdown.empty(); // Remove existing options before populating

                $.each(data, function (index, type) {
                    clothingTypeDropdown.append($('<option></option>').val(type.typeId).text(type.typeName));
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
        $('#saleForm').submit(function (e) {
            e.preventDefault(); // Prevent form submission

            var formData = $(this).serialize(); // Serialize form data

            $.ajax({
                url: '/Sales/Create',
                type: 'POST',
                data: formData,
                success: function (response) {
                    // Handle success
                    console.log(data);
                    //console.log(response);
                    // Redirect or perform any other action as needed
                },
                error: function (xhr, status, error) {
                    // Handle error
                    console.log(xhr.responseText);
                }
            });
        });
    });