// Обновление отображения цен при изменении ползунков
document.getElementById('adult-min-price').addEventListener('input', updatePriceValues);
document.getElementById('adult-max-price').addEventListener('input', updatePriceValues);

function updatePriceValues() {
    const adultMin = document.getElementById('adult-min-price').value;
    const adultMax = document.getElementById('adult-max-price').value;

    document.getElementById('adult-price-values').innerText = `${adultMin} - ${adultMax}`;
}


const sortSelect = document.getElementById('sort-options'); // Селектор сортировки
const toursContainer = document.querySelector('.container-tours'); // Контейнер с турами

// Событие на изменение выбора сортировки
sortSelect.addEventListener('change', () => {
    const sortOption = sortSelect.value;

    // Получаем все элементы туров
    const tours = Array.from(toursContainer.querySelectorAll('.tour-item'));

    // Сортируем элементы на основе выбранной опции
    tours.sort((a, b) => {
        switch (sortOption) {
            case 'price-adult-asc': {
                const priceA = parseFloat(a.querySelector('table tr:nth-child(2) td:first-child').textContent.replace('$', ''));
                const priceB = parseFloat(b.querySelector('table tr:nth-child(2) td:first-child').textContent.replace('$', ''));
                return priceA - priceB; // Сортировка по возрастанию цены взрослых
            }
            case 'price-adult-desc': {
                const priceA = parseFloat(a.querySelector('table tr:nth-child(2) td:first-child').textContent.replace('$', ''));
                const priceB = parseFloat(b.querySelector('table tr:nth-child(2) td:first-child').textContent.replace('$', ''));
                return priceB - priceA; // Сортировка по убыванию цены взрослых
            }
            default:
                location.reload(); // Обновление страницы для некорректной опции
        }
    });

    // Упорядочиваем элементы в DOM
    tours.forEach(tour => toursContainer.appendChild(tour));
});


// Отправка данных через fetch запрос
document.getElementById('apply-filter').addEventListener('click', () => {
    // Сбор данных из ползунков
    const categoryId = document.getElementById('CategoryId').value;
    const minPrice = document.getElementById('min-price').value;
    const maxPrice = document.getElementById('max-price').value;

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
    const toursList = document.querySelector('.list-tours');
    toursList.innerHTML = ''; // Очистить старые данные

    if (!data || data.length === 0) {
        // Если нет данных, отображаем сообщение
        const noToursMessage = '<h2>По данному фильтру нет туров</h2>';
        toursList.innerHTML = noToursMessage;
    } else {
        // Если данные есть, создаем элементы для туров
        data.forEach((medicines) => {
            const tourItem = `
                <div class="tour-item">
                    <img src="${medicines.image}" class="item-tour-img" alt="Изображение медикамента" />
                    <div class="item-info">
                        <h2>${medicines.name}</h2>
                    </div>
                    <table>
                        <tbody>
                            <tr>
                                <td>${medicines.price}$</td>
                            </tr>
                   
                        </tbody>
                    </table>
                    <!-- Скрытые поля -->
                    <input type="hidden" value="${medicines.id}" />
                    <input type="hidden" value="${medicines.categoryId}" />
                </div>
            `;
            // Добавить тур в список
            toursList.innerHTML += tourItem;
        });
    }
}
