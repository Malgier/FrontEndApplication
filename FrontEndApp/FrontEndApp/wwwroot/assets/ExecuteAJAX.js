function getToken() {
    var value = "; " + document.cookie;
    var parts = value.split("; access_token=");
    if (parts.length == 2) return parts.pop().split(";").shift();
}

function Execute(url, type) {
    if (url && type)
        $.ajax({
            url: url,
            type: type,
            headers: {
                "Authorization": "Bearer " + getToken()
            },
            contentType: "application/json"
        });
};