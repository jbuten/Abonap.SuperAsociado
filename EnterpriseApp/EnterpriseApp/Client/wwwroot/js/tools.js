export const startMedia = () => {

    var video = document.getElementById("player");
    var foto = document.getElementById("foto");

    if (!("mediaDevices" in navigator)) {
        navigator.mediaDevices = {};
    }

    if (!("getUserMedia" in navigator.mediaDevices)) {
        navigator.mediaDevices.getUserMedia = constraints => {
            const getUserMedia =
                navigator.webkitGetUserMedia || navigator.mozGetUserMedia;

            if (!getUserMedia) {
                return Promise.reject(new Error("getUserMedia is not supported"));
            } else {
                return new Promise((resolve, reject) =>
                    getUserMedia.call(navigator, constraints, resolve, reject)
                );
            }
        };
    }

    navigator.mediaDevices
        .getUserMedia({ video: true })
        .then(stream => {
            video.srcObject = stream;
            video.style.display = "block";
        })
        .catch(err => {
            alert('imagePickerArea.style.display = "block"');
        });

    video.style.display = "block";
    foto.style.display = "none";
};

export const stopMedia = () => {

    var video = document.getElementById("player");
    var canvas = document.getElementById("canvas");
    var foto = document.getElementById("foto");

    video.style.display = "none";
    canvas.style.display = "none";
    foto.style.display = "block";

    var stream = video.srcObject;
    var tracks = stream.getTracks();

    for (var i = 0; i < tracks.length; i++) {
        var track = tracks[i];
        track.stop();
    }

    video.srcObject = null;

}

export const captureImage = (owner) => {

    var video = document.getElementById("player");
    var canvas = document.getElementById("canvas");
    var foto = document.getElementById("foto");

    canvas.style.display = "block";
    canvas.width = 400;
    canvas.height = 300;

    const context = canvas.getContext("2d");
    context.drawImage(video, 0, 0, canvas.width, canvas.height);
    context.drawImage(video, 0, 0, canvas.width, canvas.height, 0, 0, 0, 300);

    video.srcObject.getVideoTracks().forEach(track => {
        // track.stop();
    });

    let picture = canvas.toDataURL('image/png');

    canvas.toBlob(function (blob) {
        const formData = new FormData();
        formData.append('file', blob, 'filename.png');
        upload(formData, owner);
    });

    foto.src = picture;
    setImage(picture);
    stopMedia();
};

const upload = (file, owner) => {
    console.log(file);
    fetch('./api/User/' + owner, {
        method: 'POST',
        body: file,
    }).then(
        response => console.log("then") //console.log(response)
    ).then(
        success => console.log("capture img success")//console.log(success)
    ).catch(
        error => console.log(error)
    );
};

export function getImage() {
    document.getElementById('infile').click();
    return true;
}

export function setImage(imgsrc) {
    document.getElementById("photo1").src = imgsrc;
    document.getElementById("photo2").src = imgsrc;
    return true;
}

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

export function confirmar(title, text, icon) {

    return new Promise(
        resolve => {

            Swal.fire({
                title,
                text,
                icon,
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'SI'
            }).then((result) => {
                resolve(result.isConfirmed);
            });

        });

}