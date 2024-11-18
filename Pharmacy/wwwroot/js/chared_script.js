function toggleSideMenu() {
    document.querySelector(".side-menu").classList.toggle("active");
}

document.getElementById("hamburger-icon").addEventListener("click", toggleSideMenu);

// Управление гамбургер-меню
document.getElementById('hamburger-menu').addEventListener('click', function() {
    const menu = document.getElementById('main-menu');
    menu.classList.toggle('show');  // Переключаем класс "show" для отображения/скрытия меню
});
