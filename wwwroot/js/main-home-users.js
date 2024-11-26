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

// Simulação de dados dos documentos
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
            img.className = "img-thumbnail w-100";
            img.alt = "Requerimento";

            const cardBody = document.createElement('div');
            cardBody.className = 'card-body';

            const title = document.createElement('h5');
            title.className = 'card-title';
            title.textContent = document.title;

            const requesterText = document.createElement('p');
            requesterText.textContent = "Requerido por: " + document.requester;

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

            const viewButton = document.createElement('button');
            viewButton.className = 'btn btn-primary me-2';
            viewButton.textContent = 'Visualizar Informações';
            viewButton.onclick = () => alert(`Visualizando: ${document.title}`);

            const deleteButton = document.createElement('button');
            deleteButton.className = 'btn btn-danger';
            deleteButton.textContent = 'Excluir Documento';
            deleteButton.onclick = () => {
                const index = documents.indexOf(document);
                if (index > -1) {
                    documents.splice(index, 1);
                    renderDocuments(); // Re-render the documents
                }
            };

            cardBody.appendChild(title);
            cardBody.appendChild(requesterText);
            cardBody.appendChild(statusLabel);
            cardBody.appendChild(viewButton);
            cardBody.appendChild(deleteButton);

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