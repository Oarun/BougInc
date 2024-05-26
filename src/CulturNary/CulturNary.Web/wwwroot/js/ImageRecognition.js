// Get references to HTML elements
const video = document.getElementById('video');
const canvas = document.getElementById('canvas');
const startCameraButton = document.getElementById('startCameraButton');
const captureButton = document.getElementById('captureButton');
const uploadButton = document.getElementById('uploadButton2');
const preview = document.getElementById('preview');

document.addEventListener("DOMContentLoaded", function() {

    document.getElementById('startCameraButton').addEventListener('click', function() {
        // Get user media
        navigator.mediaDevices.getUserMedia({ video: {facingMode: 'environment'} })
            .then(function(stream) {
                // Display video stream in the video element
                var video = document.getElementById('video');
                video.srcObject = stream;
                video.play();

                document.getElementById('video').style.display = 'block';
                document.getElementById('startCameraButton').style.display = 'none';
                document.getElementById('canvas').style.display = 'none';
                document.getElementById('captureButton').style.display = 'block';
            })
            .catch(function(err) {
                console.error('Error accessing the camera.', err);
            });
    });

    document.getElementById('captureButton').addEventListener('click', function() {
        
        var video = document.getElementById('video');
        var canvas = document.getElementById('canvas');

        // Draw video frame on canvas
        var context = canvas.getContext('2d');
        context.drawImage(video, 0, 0, canvas.width, canvas.height);

        // Show canvas and upload button
        canvas.style.display = 'block';
        document.getElementById('video').style.display = 'none';
        document.getElementById('uploadButton2').style.display = 'block';
    });

    document.getElementById('uploadButton2').addEventListener('click', function() {
        // Get canvas data
        var canvas = document.getElementById('canvas');
        var dataURL = canvas.toDataURL();

        document.getElementById('canvas').style.display = 'none';
        document.getElementById('startCameraButton').style.display = 'block';
        document.getElementById('captureButton').style.display = 'none';
        document.getElementById('uploadButton2').style.display = 'none';

        // Convert dataURL to Blob
        var blobBin = atob(dataURL.split(',')[1]);
        var array = [];
        for (var i = 0; i < blobBin.length; i++) {
            array.push(blobBin.charCodeAt(i));
        }
        var file = new Blob([new Uint8Array(array)], { type: 'image/jpg' });
        //var file = new File([blob], "image.jpg", { type: 'image/jpg' })

        var zipCode = document.getElementById('zipCode').value;

        // Create FormData object
        var formData = new FormData();
        formData.append('file', file, 'image.jpg');
        formData.append('zipCode', zipCode);

        // Use fetch API to upload image
        fetch('/ImageRecognition/ImageRecognitionCam', {
            method: 'POST',
            body: formData
        })
        .then(async function(response) {
            console.log('Image uploaded successfully.');
            // Handle response
            let data = await response.json();
            console.log(data);

            var imageUrl = data.imageUrl;
            var result = data.response.choices[0].message.content;

            const aiImage = document.getElementById('aiImage');
            const aiContent = document.getElementById('aiContent');

            aiImage.innerHTML = '<h3>Submitted Image:</h3> <img src="' + imageUrl + '" />';
            aiContent.innerHTML = '<h3>AI Response:</h3> <p>' + result + '</p>';


        })
        .catch(function(error) {
            console.error('Error uploading image.', error);
        });

    });
});
