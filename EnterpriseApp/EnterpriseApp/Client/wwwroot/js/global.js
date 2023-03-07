const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 5000
});

export function showMsg(_icon, msg) {
    Toast.fire({
        icon: _icon,
        title: msg
    });
    return true;
}

export function loadSearch() {
    $('#inputSearch').keyup(function () {
        console.log($(this).val());
        var rex = new RegExp($(this).val(), 'i');
        $('#tbdata tbody tr').hide();
        $('#tbdata tbody tr').filter(function () {
            return rex.test($(this).text());
        }).show();
    });
    return true;
}

export function select2() {
    $(".iselect2").select2();

    $(".iselect2").on('change', function () {
        var data = $(".iselect2 option:selected").val();
        document.getElementById("ufoto").src = "img/profile/" + data + ".png?" + Math.floor(Math.random() * 1000000) + 1;
        data = document.getElementById("imageid").src;
        //$("#sol").val(data);
        alert(data);
    })

    return true;
}

export function confirm(title, text, icon) {
    return new Promise(resolve => {
        Swal.fire({
            title, text, icon,
            showDenyButton: false,
            showCancelButton: true,
            confirmButtonColor: icon != "danger" ? '#3085d6' : '#DC3545',
            confirmButtonText: 'Continuar',
            cancelButtonText: 'Cancelar', focusConfirm: false,
        }).then((result) => {
            resolve(result.isConfirmed);
        })
    })
}

export function messageOk(_hide, _title) {
    if (_hide.length > 0) { document.getElementById(_hide).style.display = 'none'; }
    return new Promise(resolve => {
        Swal.fire({
            icon: 'success',
            title: _title,
            showConfirmButton: false //,timer: 3000
        })
    })
}



export function msgReload(msg) {
    Swal.fire({
        title: msg,
        showDenyButton: false,
        showCancelButton: false,
        confirmButtonText: 'Ok',
    }).then((result) => {
        if (result.isConfirmed) {
            document.location.reload();
        }
    });

    return true;
}
