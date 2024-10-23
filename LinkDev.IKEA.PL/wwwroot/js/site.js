var Input = document.getElementById("searchInp");

Input.addEventListener("keyup", function () {
    var value = Input.value.length > 0 ? Input.value : "";

    fetch(`${controllerName}/Index?name=${value}`, {
        method: 'GET'
    })
    .then(response => response.text()) // Since the response is HTML
    .then(html => {

        // Optionally, insert the HTML content into the page dynamically
        if (value.length > 0)
            document.getElementById("tbody").innerHTML = html;
        else
            document.body.innerHTML = html;
    });
});