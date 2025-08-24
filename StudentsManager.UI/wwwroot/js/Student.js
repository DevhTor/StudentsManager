
// Lógica para el modal de confirmación de eliminación
var confirmDeleteModal = document.getElementById('confirmDeleteModal');

confirmDeleteModal.addEventListener('show.bs.modal', function (event) {
    // Botón que activó el modal
    var button = event.relatedTarget;
    // Extrae la información de los atributos data-
    var studentId = button.getAttribute('data-id');
    var studentName = button.getAttribute('data-name');

    // Actualiza el input oculto en el formulario del modal
    var modalInput = confirmDeleteModal.querySelector('#studentIdInput');
    modalInput.value = studentId;

    // Actualiza la descripción en el cuerpo del modal
    var modalNameSpan = confirmDeleteModal.querySelector('#studentNameSpan');
    modalNameSpan.textContent = studentName;
});