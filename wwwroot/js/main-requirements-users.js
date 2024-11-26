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
// Dados dos documentos (simule dados reais vindo de um banco de dados)
const documents = [
  { title: "Autorização do Vitor", requester: "Aluno", status: "Aprovado" },
  { title: "Comunicado do Vitor", requester: "Funcionário", status: "Pendente" },
  { title: "Relatório do João", requester: "Aluno", status: "Aprovado" },
  { title: "Solicitação da Maria", requester: "Funcionário", status: "Rejeitado" }
];

// Função para renderizar os documentos
function renderDocuments() {
  const documentsList = document.getElementById('documentsList');
  documentsList.innerHTML = ''; // Limpa a lista

  if (documents.length === 0) {
      // Se não houver documentos, mostra uma mensagem
      const emptyMessage = document.createElement('div');
      emptyMessage.className = 'col-12 text-center';
      emptyMessage.textContent = 'Você ainda não possui documentos.';
      documentsList.appendChild(emptyMessage);
  } else {
      // Renderiza cada documento
      documents.forEach(document => {
          const documentDiv = document.createElement('div');
          documentDiv.className = 'col-md-6 col-lg-3 mb-4';

          const card = document.createElement('div');
          card.className = 'card';

          const img = document.createElement('img');
          img.src = "../img/images.jpg"; // Substitua pela imagem real
          img.className = "img-thumbnail w-25";
          img.alt = "Requerimento";

          const cardBody = document.createElement('div');
          cardBody.className = 'card-body';

          const title = document.createElement('h5');
          title.className = 'card-title';
          title.textContent = document.title;

          const statusLabel = document.createElement('span');
          statusLabel.className = 'badge badge-pill';
          if (document.status === "Aprovado") {
              statusLabel.classList.add('badge-success');
          } else if (document.status === "Pendente") {
              statusLabel.classList.add('badge-warning');
          } else if (document.status === "Rejeitado") {
              statusLabel.classList.add('badge-danger');
          }
          statusLabel.textContent = document.status;

          const requesterText = document.createElement('p');
          requesterText.textContent = "Requerido por: " + document.requester;

          cardBody.appendChild(title);
          cardBody.appendChild(statusLabel);
          cardBody.appendChild(requesterText);

          card.appendChild(img);
          card.appendChild(cardBody);

          documentDiv.appendChild(card);
          documentsList.appendChild(documentDiv);
      });
  }

  // Atualiza o contador de documentos
  document.getElementById('docCount').textContent = documents.length;
}

// Carrega os dados e renderiza a tela ao carregar a página
window.onload = () => {
  renderDocuments();
};