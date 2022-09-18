
function fAlertFront(message, type, url) {
    try {

        if (type == "error") {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: message
            }).then(
                function () {
                    if (url != "stay") {
                        window.location.href = url;
                    }
                }
            )
        }
        else if (type == "success") {
            Swal.fire({
                icon: 'success',
                title: 'Success!',
                text: message
            }).then(
                function () {
                    if (url != "stay") {
                        window.location.href = url;
                    }
                   

                }
            )
        }
        else if (type == "question") {
            Swal.fire({
                icon: 'question',
                title: 'Question',
                text: message
            }).then(
                function () {
                    if (url != "stay") {
                        window.location.href = url;
                    }

                }
            )
        }
        else if (type == "info") {
            Swal.fire({
                icon: 'info',
                title: 'Information',
                text: message
            }).then(
                function () {
                    if (url != "stay") {
                        window.location.href = url;
                    }

                }
            )
        }
        else if (type == "warning") {
            Swal.fire({
                icon: 'warning',
                title: 'Warning',
                text: message
            }).then(
                function () {
                    if (url != "stay") {
                        window.location.href = url;
                    }

                }
            )
        }
        else {
            alert('Sweetalert "type" is not right! ')
        }

    }
    catch (err) {
        alert(err);

    }

}



