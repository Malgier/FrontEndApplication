function Execute(url, type) {

    if (
        url != undefined
        && url != null
        && type != undefined
        && type != null
    )
        $.ajax({
            url: url,
            type: type,
            done: function () { },
            fail: function () { },
            always: function () { },
            contentType: "application/json"
        });
};