function getToken() {
    var value = "; " + document.cookie;
    var parts = value.split("; access_token=");
    if (parts.length == 2) return parts.pop().split(";").shift();
}

function Execute(url, type, success, failure) {
    if (url && type)
        $.ajax({
            url: url,
            type: type,
            headers: {
                "Authorization": "Bearer " + getToken()
            },
            contentType: "application/json",
            success: success ? success : null,
            error: failure ? failure : null
        });
};