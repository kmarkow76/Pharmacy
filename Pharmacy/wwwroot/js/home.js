document.addEventListener('DOMContentLoaded', () => {
    const categoryLinks = document.querySelectorAll('.category-link');
    const priceRange = document.getElementById('price-range');
    const priceValue = document.getElementById('price-value');
    const catalogGrid = document.querySelector('.catalog-grid');

    // Фильтр по категориям
    categoryLinks.forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            const category = e.target.dataset.category;
            // Заглушка: здесь будет фильтрация каталога
            alert(`Фильтруем по категории: ${category}`);
        });
    });

    // Фильтр по ценам
    priceRange.addEventListener('input', () => {
        priceValue.textContent = `${priceRange.value}₽`;
    });
});
// Обработчик для категорий
const categoryItems = document.querySelectorAll('.category-item');
categoryItems.forEach(item => {
    item.addEventListener('click', (event) => {
        event.preventDefault();
        const categoryName = item.textContent;
        alert(`Вы выбрали категорию: ${categoryName}`);
    });
});


document.addEventListener('DOMContentLoaded', function () {
    const toggler = document.querySelector('.navbar-toggler');
    const menu = document.querySelector('.navbar-collapse');

    toggler.addEventListener('click', function () {
        menu.classList.toggle('show'); // Переключение класса 'show'
    });
});