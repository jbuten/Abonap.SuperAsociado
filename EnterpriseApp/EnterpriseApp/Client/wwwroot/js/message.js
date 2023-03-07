export function showMsg(_icon, msg) {

    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 5000
    });

    Toast.fire({
        icon: _icon,
        title: msg
    });
    return true;
}

export function moveBottom() {
    var scrollingElement = (document.scrollingElement || document.body);
    scrollingElement.scrollTop = scrollingElement.scrollHeight;
    return true;
}

export function loadSearchUl() {
    $('#inputSearch').keyup(function () {
        var rex = new RegExp($(this).val(), 'i');
        $('#uldata li').hide();
        $('#uldata li').filter(function () {
            return rex.test($(this).text());
        }).show();
    });
    return true;
}

export function loadSearch() {
    $('#inputSearch').keyup(function () {
        var rex = new RegExp($(this).val(), 'i');
        $('#tbdata tbody tr').hide();
        $('#tbdata tbody tr').filter(function () {
            return rex.test($(this).text());
        }).show();
    });
    return true;
}