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

$(document).ready(function() {
    // Adiciona a classe 'show' aos botões após um pequeno atraso
    setTimeout(function() {
        $('.btn').addClass('show');
    }, 100); // 100ms de atraso
});

function loadImage(event) {
    const image = document.getElementById('profileImage');
    const navbarImage = document.getElementById('navbarProfileImage');
    image.src = URL.createObjectURL(event.target.files[0]);
    navbarImage.src = image.src; // Atualiza a imagem na navbar
}

function editName() {
    const nameDisplay = document.getElementById('nameDisplay');
    const nameInput = document.getElementById('nameInput');
    nameInput.value = nameDisplay.innerText; // Preenche o campo de entrada com o nome atual
    nameDisplay.style.display = 'none'; // Esconde o nome exibido
    nameInput.style.display = 'block'; // Mostra o campo de entrada
    nameInput.focus(); // Foca no campo de entrada
}

function saveName() {
    const nameDisplay = document.getElementById('nameDisplay');
    const nameInput = document.getElementById('nameInput');
    nameDisplay.innerText = nameInput.value; // Atualiza o nome exibido
    document.getElementById('navbarUserName').innerText = nameInput.value; // Atualiza o nome na navbar
    nameDisplay.style.display = 'block'; // Mostra o nome exibido novamente
    nameInput.style.display = 'none'; // Esconde o campo de entrada
}

function logout() {
    alert('Você saiu do perfil.');
    // Aqui você pode adicionar a lógica para redirecionar ou encerrar a sessão
}

function deleteProfile() {
    alert('Perfil excluído.');
    // Aqui você pode adicionar a lógica para excluir o perfil do usuário
}
const documents = document.querySelectorAll('#documentsList .card');
        const docCountMessage = document.getElementById('docCountMessage');
        docCountMessage.textContent = `Total de requisições: ${documents.length}`;