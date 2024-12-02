$(function() {
	'use strict';

  $('.form-control').on('input', function() {
	  var $field = $(this).closest('.form-group');
	  if (this.value) {
	    $field.addClass('field--not-empty');
	  } else {
	    $field.removeClass('field--not-empty');
	  }
	});

});

function viewPDF(pdfFile) {
  window.open(pdfFile, '_blank');
}

document.addEventListener('DOMContentLoaded', function() {
    const documents = [
        'Autorização do Vito',
        'Comunicado do Vito',
        'Relatório do João',
        'Solicitação da Maria'
    ];

    const docCountMessage = document.getElementById('docCountMessage');
    const userTypeSelect = document.getElementById('userType');

    function updateDocCount() {
        const userType = userTypeSelect.value;
        docCountMessage.innerText = `Existem ${documents.length} documentos requeridos por ${userType === 'aluno' ? 'alunos' : 'funcionários'}.`;
    }

    userTypeSelect.addEventListener('change', updateDocCount);
    updateDocCount(); // Initialize message
});

document.addEventListener('DOMContentLoaded', function() {
    // Lista de documentos e seus requisitantes
    const documents = [
        { title: 'Autorização do Vito', requester: 'Aluno' },
        { title: 'Comunicado do Vito', requester: 'Funcionário' },
        { title: 'Relatório do João', requester: 'Aluno' },
        { title: 'Solicitação da Maria', requester: 'Funcionário' }
    ];

    // Contar quantas pessoas requisitaram documentos
    const uniqueRequesters = new Set(documents.map(doc => doc.requester));
    const docCountMessage = document.getElementById('docCountMessage');
    docCountMessage.innerText = `Existem ${uniqueRequesters.size} pessoas que requisitaram documentos.`;
});
