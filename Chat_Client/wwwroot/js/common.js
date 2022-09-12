
window.ShowSwalToast = (message) => {
    Swal.fire({
        toast: true,
        position: 'bottom-end',
        icon: 'success',
        title: message,
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true
    })
}

window.ShowSwalToast2 = (message) => {
    Swal.fire({
        toast: true,
        position: 'bottom-end',
        icon: 'info',
        title: message,
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true
    })
}

window.ScrollToBottom = (elementName) => {
    element = document.getElementById(elementName);
    element.scrollTop = element.scrollHeight - element.clientHeight;
}

window.ScrollToCenter = (elementName) => {
    element = document.getElementById(elementName);
    element.scrollTop = element.clientHeight;
}

window.IsScrollTop = (elementName, dotNet) => {
    element = document.getElementById(elementName);
    if (element.scrollTop == 0) {
        return dotNet.invokeMethodAsync('GetNewMessages');
    }
}