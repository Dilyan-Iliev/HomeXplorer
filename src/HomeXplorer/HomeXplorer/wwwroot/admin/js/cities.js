document.addEventListener('DOMContentLoaded', function () {
    var countryDropdown = document.getElementById('countryDropdown');
    var citiesLabel = document.getElementById('citiesLabel');
    var cityList = document.getElementById('cityList');

    countryDropdown.addEventListener('change', function () {
        var countryId = countryDropdown.value;

        // Clear the city list
        cityList.innerHTML = '';

        if (countryId !== '') {
            citiesLabel.style.display = 'block'; // Show the "Cities:" text
            fetch('/City/CityBasedOnCountry?countryId=' + countryId)
                .then(function (response) {
                    if (response.ok) {
                        return response.json();
                    } else {
                        throw new Error('Error fetching cities. Status code: ' + response.status);
                    }
                })
                .then(function (cities) {
                    cities.forEach(function (city) {
                        var listItem = document.createElement('li');
                        listItem.className = 'list-group-item';
                        listItem.textContent = city.name;
                        cityList.appendChild(listItem);
                    });
                })
                .catch(function (error) {
                    console.log(error);
                });
        } else {
            citiesLabel.style.display = 'none'; // Hide the "Cities:" text
        }
    });
});
