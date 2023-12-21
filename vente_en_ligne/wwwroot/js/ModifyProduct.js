const { Alert } = require("bootstrap");

function chooseFile() {
    alert('chooseFile function called');
        document.getElementById('imageFile').click();
                }

    function handleImageSelect(input) {
                    const file = input.files[0];

    if (file) {
                        const imgElement = document.getElementById('imagePreview');

    // Display the selected image preview
    imgElement.src = URL.createObjectURL(file);
    imgElement.style.display = 'block';
                    }
                }

function loadContent(action) {
    $.ajax({
        url: '/Produits/' + action,
        type: 'GET',
        success: function (result) {
            $('#main-content').html(result);

            // Si l'action est 'ModifyForm', récupérer et afficher les données dans le formulaire
            if (action === 'ModifyProductForm') {
                // Appeler une nouvelle fonction pour remplir le formulaire avec les données
                loadFormData();

            }

        }
    });
}// Déclarez une variable pour stocker l'ID
var currentProductId;
function searchAndLoadForm() {
    var searchInputValue = $('#searchInput').val();
    $.ajax({
        type: 'GET',
        url: '/Produits/CheckIfIdExists?searchId=' + searchInputValue,
        success: function (data) {
            if (data.exists) {
                // Stockez l'ID récupéré dans la variable
                currentProductId = searchInputValue;
                loadContent('ModifyProductForm');

            } else {
                alert('ID not found. Please try again.');
            }
        },
        error: function () {
            alert('An error occurred while checking the ID.');
        }
    });
}

function loadFormData() {
    // Utilisez l'ID stocké dans la variable
    var productId = currentProductId;

    $.ajax({
        type: 'GET',
        url: '/Produits/GetProductData?id=' + productId,
        success: function (data) {
            console.log(data.iDP);
            // Remplissez le formulaire avec les données récupérées
            $('#id').val(data.idPr);
            $('#Name').val(data.name);
            $('#Price').val(data.prix);
            $('#Date').val(data.dateDepot);
            $('#stock').val(data.stock);
            $('#detail').val(data.description);
            $('#Category').val(data.iDC);

            // Affichez l'image à partir de l'URL
            if (data.imageFile) {
                $('#imagePreview').attr('src', data.imageFile);
                $('#imagePreview').show();
            }
        },
        error: function () {
            alert('An error occurred while fetching owner data.');
        }
    });
}

function saveModifiedData() {
    var productId = currentProductId; // Utilisez l'ID stocké dans la variable

    var formData = {
        IdPr: $('#id').val(),
        Name: $('#Name').val(),
        prix: $('#Price').val(),
        DateDepot: $('#Date').val(),
        stock: $('#stock').val(),
        Description: $('#detail').val(),
        IDC: $('#Category').val(),
        ImageData: $('#imageFile').val()
    };

    $.ajax({
        type: 'POST',
        url: '/Produits/InsertProductData?id=' + productId,
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function (response) {
            if (response.success) {
                alert('Les modifications ont été enregistrées avec succès.');
                // Vous pouvez également mettre à jour l'affichage sur la page si nécessaire
            } else {
                alert('Une erreur s\'est produite lors de la sauvegarde des modifications: ' + response.message);
            }
        },
        error: function (xhr) {
            alert('Une erreur s\'est produite lors de la sauvegarde des modifications: ' + xhr.responseText);
        }
    });
}

