var currentTab = 0;
showTab(currentTab);

function showTab(n) {
    var x = document.getElementsByClassName("step");
    x[n].style.display = "block";

    if (n == 0) {
        document.getElementById("prevBtn").style.display = "none";
    } else {
        document.getElementById("prevBtn").style.display = "inline";
    }

    if (n == (x.length - 1)) {
        document.getElementById("nextBtn").innerHTML = "Submit";
    } else {
        document.getElementById("nextBtn").innerHTML = "Next";
    }

    fixStepIndicator(n);
}

function nextPrev(n) {
    var x = document.getElementsByClassName("step");

    if (n == 1 && !validateForm()) return false;

    x[currentTab].style.display = "none";
    currentTab = currentTab + n;

    if (currentTab >= x.length) {
        document.getElementById("signUpForm").submit();
        return false;
    }

    showTab(currentTab);
}

function validateForm() {
    var x, y, i, valid = true;
    x = document.getElementsByClassName("step");
    y = x[currentTab].getElementsByTagName("input");

    for (i = 0; i < y.length; i++) {
        if (y[i].value == "") {
            y[i].className += " invalid";
            valid = false;
        } else if (y[i].name === "Price") {
            var price = parseFloat(y[i].value);
            if (price < 250 || price > 100000) {
                y[i].className += " invalid";
                valid = false;
            }
        } else if (y[i].name === "Size") {
            var size = parseInt(y[i].value);
            if (size < 40 || size > 500) {
                y[i].className += " invalid";
                valid = false;
            }
        }
    }

    var dropdowns = x[currentTab].getElementsByTagName("select");
    for (i = 0; i < dropdowns.length; i++) {
        if (dropdowns[i].value === "") {
            dropdowns[i].className += " invalid";
            valid = false;
            showRequiredText(dropdowns[i]);
        }
    }


    if (valid) {
        document.getElementsByClassName("stepIndicator")[currentTab].className += " finish";
    }

    return valid;
}

function resetValidationIndicators() {
    var requiredTexts = document.getElementsByClassName("required-text");
    for (var i = 0; i < requiredTexts.length; i++) {
        requiredTexts[i].style.display = "none";
    }
}

function showRequiredText(element) {
    var nextElement = element.nextElementSibling;
    if (nextElement && nextElement.classList.contains("required-text")) {
        nextElement.style.display = "block";
    }
}

function fixStepIndicator(n) {
    var i, x = document.getElementsByClassName("stepIndicator");

    for (i = 0; i < x.length; i++) {
        x[i].className = x[i].className.replace(" active", "");
    }

    x[n].className += " active";
}
