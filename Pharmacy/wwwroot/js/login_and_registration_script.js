// Функции для отображения форм входа и регистрации
function showLoginForm() {
    document.getElementById('form_signin').style.display = 'block';
    document.getElementById('form_signup').style.display = 'none';
    document.getElementById('login-tab').classList.add('active');
    document.getElementById('register-tab').classList.remove('active');
}

function showRegisterForm() {
    document.getElementById('form_signin').style.display = 'none';
    document.getElementById('form_signup').style.display = 'block';
    document.getElementById('register-tab').classList.add('active');
    document.getElementById('login-tab').classList.remove('active');
}

// Функция для логина
async function login() {
    const username = document.getElementById("username").value.trim();
    const password = document.getElementById("password").value.trim();
    const errorContainer = document.getElementById("error-messages-signin");

    errorContainer.innerHTML = ''; // Очищаем ошибки

    try {
        const response = await fetch("/Home/Login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ Email: username, Password: password })
        });

        const result = await response.json();

        if (!response.ok) {
            displayErrors(result, errorContainer, "form_signin");
        } else {
            location.reload(); // Успешный вход, обновляем страницу
        }
    } catch (error) {
        console.error("Ошибка авторизации:", error);
    }
}

// Функция для регистрации
async function register() {
    const username = document.getElementById("reg-username").value.trim();
    const email = document.getElementById("reg-email").value.trim();
    const password = document.getElementById("reg-password").value.trim();
    const confirmPassword = document.getElementById("reg-password-confirm").value.trim();
    const errorContainer = document.getElementById("error-messages-signup");

    errorContainer.innerHTML = ''; // Очистка ошибок

    if (password !== confirmPassword) {
        errorContainer.innerHTML = "<div class='error'>Пароли не совпадают</div>";
        return;
    }

    try {
        const response = await fetch("/Home/Register", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                Login: username,
                Email: email,
                Password: password,
                PasswordConfirm: confirmPassword
            })
        });

        const result = await response.json();

        if (!response.ok) {
            displayErrors(result, errorContainer, "form_signup");
        } else {
            console.log("Регистрация успешна:", result);

            // Очищаем и закрываем форму регистрации
            cleaningAndClosingForm("#signup-form", errorContainer);

            // Информируем пользователя, что проверка почты требуется
            errorContainer.innerHTML = "<div class='success'>На вашу почту отправлена ссылка для подтверждения регистрации. Пожалуйста, проверьте почту!</div>";
        }
    } catch (error) {
        console.error("Ошибка регистрации:", error);
    }
}

// Функция для отображения ошибок
function displayErrors(errors, errorContainer, formId) {
    errorContainer.innerHTML = '';
    if (!Array.isArray(errors)) {
        errors = [errors]; // Преобразуем в массив, если это не массив
    }
    errors.forEach(error => {
        const errorMessage = document.createElement('div');
        errorMessage.classList.add('error');
        errorMessage.textContent = error;
        errorContainer.appendChild(errorMessage);
    });
}

// Функция для очистки формы и её закрытия
function cleaningAndClosingForm(form, errorContainer) {
    errorContainer.innerHTML = '';
    const formElement = document.querySelector(form);
    const inputs = formElement.querySelectorAll('input');
    inputs.forEach(input => input.value = ''); // Очистка полей формы
}

// Функция для показа и скрытия пароля
document.addEventListener("DOMContentLoaded", () => {
    const togglePasswordButtons = document.querySelectorAll(".toggle-password");

    togglePasswordButtons.forEach(button => {
        button.addEventListener("click", () => {
            const passwordInput = button.previousElementSibling;
            if (passwordInput.type === "password") {
                passwordInput.type = "text";
                button.querySelector("img").src = "icons/Hide.png";
            } else {
                passwordInput.type = "password";
                button.querySelector("img").src = "icons/Hide.svg";
            }
        });
    });
});

// Функция для переключения видимости контейнера
function hiddenOpen_Closeclick(container, forceDisplay = null) {
    const element = document.querySelector(container);
    if (element) {
        if (forceDisplay !== null) {
            element.style.display = forceDisplay;
        } else {
            element.style.display = element.style.display === "none" || element.style.display === "" ? "block" : "none";
        }
    }
}
