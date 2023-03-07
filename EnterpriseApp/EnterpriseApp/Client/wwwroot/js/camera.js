const videoPlayer = document.querySelector("#player");
const canvasElement = document.querySelector("#canvas");
const captureButton = document.querySelector("#capture-btn");
const foto = document.querySelector("#foto");

// Image dimensions

const createImage = (src, alt, title, width, height, className) => {
    let newImg = document.createElement("img");

    if (src !== null) newImg.setAttribute("src", src);
    if (alt !== null) newImg.setAttribute("alt", alt);
    if (title !== null) newImg.setAttribute("title", title);
    if (width !== null) newImg.setAttribute("width", width);
    if (height !== null) newImg.setAttribute("height", height);
    if (className !== null) newImg.setAttribute("class", className);

    return newImg;
};

export const stopMedia = () => {

    document.getElementById("btnStart").style.display = "block";
    document.getElementById("btnGet").style.display = "block";
    document.getElementById("foto").style.display = "block";

    document.getElementById("btnStop").style.display = "none";
    captureButton.style.display = "none";
    videoPlayer.style.display = "none";
    canvasElement.style.display = "none";

    var stream = videoPlayer.srcObject;
    var tracks = stream.getTracks();

    for (var i = 0; i < tracks.length; i++) {
        var track = tracks[i];
        track.stop();
    }

    videoPlayer.srcObject = null;

}

export const startMedia = () => {

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
            videoPlayer.srcObject = stream;
            videoPlayer.style.display = "block";
        })
        .catch(err => {
            alert('imagePickerArea.style.display = "block"');
        });

    document.getElementById("btnStart").style.display = "none";
    document.getElementById("btnGet").style.display = "none";
    document.getElementById("foto").style.display = "none";

    document.getElementById("btnStop").style.display = "block";
    captureButton.style.display = "block";
    videoPlayer.style.display = "block";
    //canvasElement.style.display = "block";

};

export const captureImage = (owner) => {
    canvasElement.style.display = "block";
    //canvasElement.classList.add("profileUserImg");
    canvasElement.width = 400;
    canvasElement.height = 300;

    const context = canvasElement.getContext("2d");
    context.drawImage(videoPlayer, 0, 0, canvas.width, canvas.height);
    context.drawImage(videoPlayer, 0, 0, canvas.width, canvas.height, 0, 0, 0, 300);

    videoPlayer.srcObject.getVideoTracks().forEach(track => {
        // track.stop();
    });

    let picture = canvasElement.toDataURL('image/png');

    canvasElement.toBlob(function (blob) {
        const formData = new FormData();
        formData.append('file', blob, 'filename.png');
        upload(formData, owner);
    });
    
    foto.src = picture;
    document.getElementById("photo1").src = picture;
    document.getElementById("photo2").src = picture;
    stopMedia(); 
    
};

const upload = (file, owner) => {
    //console.log(file);
    fetch('./api/User/' + owner, {
        method: 'POST',
        body: file ,
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