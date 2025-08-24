// Lógica para el modal de confirmación de eliminación
var confirmDeleteModal = document.getElementById('confirmDeleteModal');

confirmDeleteModal.addEventListener('show.bs.modal', function (event) {
    // Botón que activó el modal
    var button = event.relatedTarget;
    // Extrae la información de los atributos data-
    var homeworkId = button.getAttribute('data-id');
    var homeworkDescription = button.getAttribute('data-description');

    // Actualiza el input oculto en el formulario del modal
    var modalInput = confirmDeleteModal.querySelector('#homeworkIdInput');
    modalInput.value = homeworkId;

    // Actualiza la descripción en el cuerpo del modal
    var modalDescriptionSpan = confirmDeleteModal.querySelector('#homeworkDescriptionSpan');
    modalDescriptionSpan.textContent = homeworkDescription;
});