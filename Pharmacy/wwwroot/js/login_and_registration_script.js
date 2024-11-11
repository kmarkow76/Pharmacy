// Функция для отображения формы входа
function showLoginForm() {
    // Скрыть форму регистрации
    document.getElementById("form_signup").style.display = "none";

    // Показать форму входа
    document.getElementById("form_signin").style.display = "block";

    // Изменить стили для активной кнопки
    document.getElementById("login-tab").classList.add("active");
    document.getElementById("register-tab").classList.remove("active");
}

// Функция для отображения формы регистрации
function showRegisterForm() {
    // Скрыть форму входа
    document.getElementById("form_signin").style.display = "none";

    // Показать форму регистрации
    document.getElementById("form_signup").style.display = "block";

    // Изменить стили для активной кнопки
    document.getElementById("register-tab").classList.add("active");
    document.getElementById("login-tab").classList.remove("active");
}

// Функция для отображения ошибок
function displayErrors(errors, container, formId) {
    container.innerHTML = ""; // Очищаем контейнер ошибок

    // Проходим по всем ошибкам и выводим их
    errors.forEach(error => {
        const errorDiv = document.createElement("div");
        errorDiv.classList.add("error");
        errorDiv.innerText = error;
        container.appendChild(errorDiv);
    });

    // Подсвечиваем поля с ошибками
    const form = document.getElementById(formId);
    const inputs = form.querySelectorAll("input");
    inputs.forEach(input => input.classList.remove("error")); // Снимаем класс ошибки

    errors.forEach(error => {
        // Пример: если ошибка связана с конкретным полем, добавляем класс
        const errorField = form.querySelector(`[name="${error}"]`);
        if (errorField) {
            errorField.classList.add("error");
        }
    });
}



async function login() {
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    const errorContainer = document.getElementById("error-messages-signin");

    // Очистить контейнер с ошибками перед отправкой запроса
    errorContainer.innerHTML = '';

    const response = await fetch("/Home/Login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ Email: username, Password: password })
    });

    const result = await response.json();

    if (!response.ok) {
        // Если есть ошибки, отображаем их
        displayErrors(result, errorContainer, "form_signin");
    } else {
        // Если запрос успешен, перезагружаем страницу
        location.reload();
    }
}

async function register() {
    const username = document.getElementById("reg-username").value.trim(); // Очистка пробела с начала и конца
    const email = document.getElementById("reg-email").value.trim(); // Очистка пробела с начала и конца
    const password = document.getElementById("reg-password").value.trim(); // Очистка пробела с начала и конца
    const confirmPassword = document.getElementById("reg-password-confirm").value.trim(); // Очистка пробела с начала и конца
    const errorContainer = document.getElementById("error-messages-signup");

    // Проверка на совпадение паролей
    if (password !== confirmPassword) {
        errorContainer.innerHTML = "<div class='error'>Пароли не совпадают</div>";
        return;
    }

    const response = await fetch("/Home/Register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            Login: username,
            Email: email,
            Password: password,
            PasswordConfirm: confirmPassword // Используем правильное имя поля
        })
    });

    const result = await response.json();

    if (!response.ok) {
        displayErrors(result, errorContainer, "form_signup");
    } else {
        location.reload(); // Если успех, обновляем страницу
    }
}



