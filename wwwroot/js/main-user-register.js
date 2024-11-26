
const users = [];

function showCadastroForm() {
    const modal = new bootstrap.Modal(document.getElementById('modalCadastro'));
    modal.show();
}

function showForm() {
    const selectType = document.getElementById('tipoUsuario');
    const selectedValue = selectType.value;
    const additionalFields = document.getElementById('additionalFields');
    additionalFields.innerHTML = '';

    if (selectedValue === 'aluno') {
        additionalFields.innerHTML = `
            <div class="mb-3">
                <label for="cpfAluno" class="form-label">CPF</label>
                <input type="text" class="form-control" id="cpfAluno" required>
            </div>
            <div class="mb-3">
                <label for="rgAluno" class="form-label">RG</label>
                <input type="text" class="form-control" id="rgAluno" required>
            </div>
            <div class="mb-3">
                <label for="rmAluno" class="form-label">RM</label>
                <input type="text" class="form-control" id="rmAluno" required>
            </div>
            <div class="mb-3">
                <label for="emailAluno" class="form-label">Email</label>
                <input type="email" class="form-control" id="emailAluno" required>
            </div>
            <div class="mb-3">
                <label for="senhaAluno" class="form-label">Senha</label>
                <input type="password" class="form-control" id="senhaAluno" required>
            </div>
        `;
    } else if (selectedValue === 'funcionario') {
        additionalFields.innerHTML = `
            <div class="mb-3">
                <label for="emailFuncionario" class="form-label">Email</label>
                <input type="email" class="form-control" id="emailFuncionario" required>
            </div>
            <div class="mb-3">
                <label for="senhaFuncionario" class="form-label">Senha</label>
                <input type="password" class="form-control" id="senhaFuncionario" required>
            </div>
        `;
    }
}

function addUser() {
    const nome = document.getElementById('nomeUsuario').value;
    const tipo = document.getElementById('tipoUsuario').value;
    let additionalInfo = {};

    if (tipo === 'aluno') {
        additionalInfo = {
            cpf: document.getElementById('cpfAluno').value,
            rg: document.getElementById('rgAluno').value,
            rm: document.getElementById('rmAluno').value,
            email: document.getElementById('emailAluno').value,
            senha: document.getElementById('senhaAluno').value
        };
    } else if (tipo === 'funcionario') {
        additionalInfo = {
            email: document.getElementById('emailFuncionario').value,
            senha: document.getElementById('senhaFuncionario').value
        };
    }

    if (nome && tipo) {
        users.push({ nome, tipo, ...additionalInfo });
        document.getElementById('formCadastro').reset();
        updateUserTable();
        const modal = bootstrap.Modal.getInstance(document.getElementById('modalCadastro'));
        modal.hide();
    }
}

function updateUserTable() {
    const tbody = document.getElementById('userTableBody');
    tbody.innerHTML = '';
    users.forEach((user, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${user.nome}</td>
            <td>${user.tipo}</td>
            <td>
                <button class="btn btn-info" onclick="viewUser(${index})">Visualizar</button>
                <button class="btn btn-danger" onclick="deleteUser(${index})">Excluir</button>
            </td>
        `;
        tbody.appendChild(row);
    });
}

function viewUser(index) {
    const user = users[index];
    let userInfo = `<h4>Informações do Usuário</h4>
                    <p><strong>Nome:</strong> ${user.nome}</p>
                    <p><strong>Tipo:</strong> ${user.tipo}</p>`;
    if (user.tipo === 'aluno') {
        userInfo += `<p><strong>CPF:</strong> ${user.cpf}</p>
                     <p><strong>RG:</strong> ${user.rg}</p>
                     <p><strong>RM:</strong> ${user.rm}</p>
                     <p><strong>Email:</strong> ${user.email}</p>`;
    } else if (user.tipo === 'funcionario') {
        userInfo += `<p><strong>Email:</strong> ${user.email}</p>`;
    }
    document.getElementById('userInfo').innerHTML = userInfo;
}

function deleteUser(index) {
    users.splice(index, 1);
    updateUserTable();
    document.getElementById('userInfo').innerHTML = '';
}