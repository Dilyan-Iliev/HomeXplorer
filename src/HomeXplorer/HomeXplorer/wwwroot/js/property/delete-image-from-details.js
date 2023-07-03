document.addEventListener('DOMContentLoaded', function () {
        var deletePhotoButtons = document.querySelectorAll('.delete-photo');

        deletePhotoButtons.forEach(function (button) {
            button.addEventListener('click', function () {
                var photoId = this.getAttribute('data-photo-id');
                var form = this.closest('form');

                // Update the form to include the deleted photo ID
                var deletedPhotosInput = document.createElement('input');
                deletedPhotosInput.setAttribute('type', 'hidden');
                deletedPhotosInput.setAttribute('name', 'DeletedPhotosIds');
                deletedPhotosInput.setAttribute('value', photoId);
                form.appendChild(deletedPhotosInput);

                // Remove the table row from the DOM
                var tableRow = this.closest('tr');
                tableRow.parentNode.removeChild(tableRow);
            });
        });
    });