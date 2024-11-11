document.addEventListener("DOMContentLoaded", function() {
    const navLinks = document.querySelectorAll(".nav-link");

    // Добавляем активный класс для текущей страницы
    navLinks.forEach(link => {
        if (link.href === window.location.href) {
            link.classList.add("active");
        }

        // Эффект при клике на кнопку
        link.addEventListener("click", () => {
            navLinks.forEach(link => link.classList.remove("active"));
            link.classList.add("active");
        });
    });
});
