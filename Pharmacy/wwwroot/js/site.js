document.addEventListener('DOMContentLoaded', function () {
    let header = document.getElementById('header-top');
    let lastScrollTop = 0; // Для отслеживания последнего положения прокрутки

    window.addEventListener('scroll', function () {
        let scrollTop = window.scrollY; // Получаем текущую позицию прокрутки

        // Проверяем, прокручивается ли вниз или вверх
        if (scrollTop > lastScrollTop) {
            // Прокрутка вниз
            header.style.transform = 'translateY(-100%)'; // Скрываем заголовок
        } else {
            // Прокрутка вверх
            header.style.transform = 'translateY(0)'; // Показываем заголовок
        }

        lastScrollTop = scrollTop; // Обновляем последнее положение прокрутки
    });
});
