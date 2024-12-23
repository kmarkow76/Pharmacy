// Обновление отображения цен при изменении ползунков
document.getElementById('adult-min-price').addEventListener('input', updatePriceValues);
document.getElementById('adult-max-price').addEventListener('input', updatePriceValues);

function updatePriceValues() {
    const adultMin = document.getElementById('adult-min-price').value;
    const adultMax = document.getElementById('adult-max-price').value;

    document.getElementById('adult-price-values').innerText = `${adultMin} - ${adultMax}`;
}

// Селектор сортировки
const sortSelect = document.getElementById('sort-options');
const toursContainer = document.querySelector('.catalog-grid');

// Событие на изменение выбора сортировки
sortSelect.addEventListener('change', () => {
    const sortOption = sortSelect.value;

    // Получаем все элементы товаров
    const products = Array.from(toursContainer.querySelectorAll('.product-card'));

    // Сортируем элементы на основе выбранной опции
    products.sort((a, b) => {
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
    products.forEach(product => toursContainer.appendChild(product));
});

// Отправка данных через fetch запрос
document.getElementById('apply-filter').addEventListener('click', () => {
    // Сбор данных из ползунков
    const categoryId = document.getElementById('CategoryId').value;
    const minPrice = document.getElementById('adult-min-price').value;
    const maxPrice = document.getElementById('adult-max-price').value;
    const sortOrder = document.getElementById('sort-options').value;
    const search = document.getElementById('search-input').value;

    // Сбор данных из чекбоксов
    const mealTypes = Array.from(document.querySelectorAll('.meal-types input[type="checkbox"]:checked'))
        .map((checkbox) => checkbox.value);

    // Формирование данных для отправки
    const filterData = {
        CategoryId: categoryId,
        priceMin: minPrice,
        priceMax: maxPrice,
        Search: search,
        MealTypes: mealTypes,
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

// Функция для отображения отфильтрованных данных
function dataDisplay(data) {
    const productsList = document.querySelector('.catalog-grid');
    productsList.innerHTML = ''; // Очистить старые данные

    if (!data || data.length === 0) {
        const noProductsMessage = '<h2>По данному фильтру нет товаров</h2>';
        productsList.innerHTML = noProductsMessage;
    } else {
        // Создаем элементы для товаров
        data.forEach((medicine) => {
            const productItem = `
                <div class="product-card">
                    <div class="product-img-container">
                        <img src="${medicine.image}" class="product-img" alt="Medicine Image"/>
                    </div>
                    <h2 class="product-title">${medicine.name}</h2>
                    <div class="product-price">${medicine.price} ₽</div>
                    <div class="product-details">
                        <button class="details-button" onclick="window.location.href='/medicines/medicinepage/${medicine.id}'">
                            Подробнее
                        </button>
                    </div>
                </div>
            `;
            productsList.innerHTML += productItem;
        });
    }
}
