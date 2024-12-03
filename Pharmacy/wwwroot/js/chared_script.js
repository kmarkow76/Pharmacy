function toggleSideMenu() {
    document.querySelector(".side-menu").classList.toggle("active");
}

document.getElementById("hamburger-icon").addEventListener("click", toggleSideMenu);

// Управление гамбургер-меню
document.getElementById('hamburger-menu').addEventListener('click', function() {
    const menu = document.getElementById('main-menu');
    menu.classList.toggle('show');  // Переключаем класс "show" для отображения/скрытия меню
});
document.getElementById("send-button").addEventListener("click", async () => {
    const messageText = document.getElementById("message-textarea").value;

    if (!messageText.trim()) {
        alert("Сообщение не может быть пустым!");
        return;
    }

    try {
        const response = await fetch('/api/messages/send', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ text: messageText })
        });

        if (response.ok) {
            alert("Сообщение успешно отправлено!");
            document.getElementById("message-textarea").value = ""; // Очистка поля
        } else {
            const error = await response.text();
            alert(`Ошибка: ${error}`);
        }
    } catch (error) {
        alert(`Ошибка соединения: ${error.message}`);
    }
});
