// Обновление отображения цен при изменении ползунков
document.getElementById('adult-min-price').addEventListener('input', updatePriceValues);
document.getElementById('adult-max-price').addEventListener('input', updatePriceValues);

function updatePriceValues() {
    const adultMin = document.getElementById('adult-min-price').value;
    const adultMax = document.getElementById('adult-max-price').value;

    document.getElementById('adult-price-values').innerText = `${adultMin} - ${adultMax}`;
}


const sortSelect = document.getElementById('sort-options'); // Селектор сортировки
const toursContainer = document.querySelector('.catalog-grid'); // Контейнер с турами

// Событие на изменение выбора сортировки
sortSelect.addEventListener('change', () => {
    const sortOption = sortSelect.value;

    // Получаем все элементы туров
    const tours = Array.from(toursContainer.querySelectorAll('.product-card'));

    // Сортируем элементы на основе выбранной опции
    tours.sort((a, b) => {
        const priceA = parseFloat(a.querySelector('.product-price').textContent.replace('₽', '').trim()) || 0;
        const priceB = parseFloat(b.querySelector('.product-price').textContent.replace('₽', '').trim()) || 0;

        switch (sortOption) {
            case 'price-adult-asc':
                return priceA - priceB; // Сортировка по возрастанию
            case 'price-adult-desc':
                return priceB - priceA; // Сортировка по убыванию
            default:
                return 0; // Никакой сортировки
        }
    });

    // Упорядочиваем элементы в DOM
    tours.forEach(tour => toursContainer.appendChild(tour));
});



// Отправка данных через fetch запрос
document.getElementById('apply-filter').addEventListener('click', () => {
    // Сбор данных из ползунков
    const categoryId = document.getElementById('CategoryId').value;
    const minPrice = document.getElementById('adult-min-price').value;
    const maxPrice = document.getElementById('adult-max-price').value;
    const sortOrder = document.getElementById('sort-options').value;
    // Сбор данных из чекбоксов
    const mealTypes = Array.from(document.querySelectorAll('.meal-types input[type="checkbox"]:checked'))
        .map((checkbox) => checkbox.value);

    // Формирование данных для отправки
    const filterData = {
        CategoryId: categoryId,
        priceMin: minPrice,
        priceMax: maxPrice,
    };

    console.log('Отправляемые данные:', filterData);

    // Отправка данных через fetch
    fetch('/Medicines/Filter', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(filterData),
    })
        .then((response) => {
            if (!response.ok) {
                throw new Error('Ошибка при фильтрации данных');
            }
            return response.json(); // Преобразуем ответ в JSON
        })
        .then((data) => {
            console.log('Результаты фильтрации:', data);
            dataDisplay(data); // Функция для отображения данных (если есть)
        })
        .catch((error) => {
            console.error('Ошибка:', error);
        });
});

function dataDisplay(data) {
    // Найти контейнер для списка туров
    const toursList = document.querySelector('.catalog-grid');
    toursList.innerHTML = ''; // Очистить старые данные
    const catalogcon = document.querySelector('.catalog-product');
    if (!data || data.length === 0) {
        // Если нет данных, отображаем сообщение
        const noToursMessage = '<h2>По данному фильтру нет туров</h2>';
        catalogcon.innerHTML = noToursMessage;
    } else {
        // Если данные есть, создаем элементы для туров
        data.forEach((medicines) => {
            const tourItem = `
        <div class="product-card">
        <!-- Изображение -->
        <div class="product-img-container">
            <img src="${medicines.image}" class="product-img" alt="Medicine Image"/>
        </div>

        <!-- Информация о медикаменте -->
        <h2 class="product-title">${medicines.name}</h2>

        <!-- Цена -->
        <div class="product-price">
            ${medicines.price} ₽
        </div>

        <!-- Кнопка подробнее -->
        <div class="product-details">
                    <button class="details-button" onclick="window.location.href='/medicines/medicinepage/${medicines.id}'">
                        Подробнее
                    </button>
        </div>

        <!-- Скрытые поля -->
        <input type="hidden" value="${medicines.id}" />
        <input type="hidden" value="${medicines.catalogId}" />
    </div>
        `;
            // Добавить тур в список
            toursList.innerHTML += tourItem;
        });
        catalogcon.innerHTML = '';
        catalogcon.appendChild(toursList);
    }
}
