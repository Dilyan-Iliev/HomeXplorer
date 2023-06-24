document.addEventListener('DOMContentLoaded', function () {
    const countryDropdown = document.getElementById('countryDropdown');
    const cityDropdown = document.getElementById('cityDropdown');

    countryDropdown.addEventListener('change', async function () {
        const selectedCountryId = this.value;
        if (selectedCountryId) {
            try {
                const response = await fetch('/City/CityBasedOnCountry?countryId=' + selectedCountryId);

                if (response.ok) {
                    const cities = await response.json();

                    cityDropdown.innerHTML = '';
                    if (cities.length > 0) {
                        cityDropdown.disabled = false;
                        cities.forEach(function (city) {
                            const option = document.createElement('option');
                            option.value = city.id;
                            option.text = city.name;
                            cityDropdown.appendChild(option);
                        });
                    } else {
                        cityDropdown.disabled = true;
                        const option = document.createElement('option');
                        option.text = 'No cities found';
                        cityDropdown.appendChild(option);
                    }
                } else {
                    throw new Error('Error: ' + response.status);
                }
            } catch (error) {
                console.log(error);
            }
        } else {
            cityDropdown.disabled = true;
            cityDropdown.innerHTML = '<option value="">Select City</option>';
        }
    });
});