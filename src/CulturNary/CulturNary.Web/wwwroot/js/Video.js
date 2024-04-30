var personId;
var allVideoTypes = []; // Global variable to store all restaurants
var videoElements = []; // Global variable to store jQuery elements of restaurants

$(document).ready(function() {
    function getPerson() {
        $.ajax({
            url: '/api/Person/GetCurrentPerson',
            type: 'GET',
            success: function (data) {
                personId = data.id;
                getVideosForUser(personId);
            }
        });
    }
    getPerson(); // get the personId.

    $("#createVideoForm").submit(function(event) {
        event.preventDefault();
    
        // Create Video object from form data
        var video = {
            PersonId: personId,
            VideoName: $("#videoName").val(),
            VideoType: $("#videoType").val(),
            VideoLink: $("#videoLink").val(),
            VideoNotes: $("#videoNotes").val()
        };
    
        // Send data to API using AJAX
        $.ajax({
            url: "/api/video/",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(video),
            success: function (response) {
                // Handle successful response
                console.log("Video created successfully:", response);
                $("#createVideoForm")[0].reset();
                // Refresh the video list
                getVideosForUser(personId);
            },
            error: function (error) {
                // Handle errors during API call
                console.error("Error creating video:", error);
            }
        });
    });

    function getVideosForUser(personId) {
        $.ajax({
            url: "/api/Video/" + personId,
            type: "GET",
            contentType: "application/json",
            success: function (videos) {
                console.log(videos)
                populateVideos(videos);
            },
            error: function (error) {
                // Handle errors during retrieving videos
                console.error("Error retrieving restaurants:", error);
            }
        });
    }

    function populateVideos(videos) {
        var userVideoList = $("#UserVideoList");
        userVideoList.empty(); // Clear existing content
    
        // Check if there are no videos
        if (videos.length === 0) {
            userVideoList.append("<p>You currently have no restaurants added.</p>");
            $("#MealTypeFilter").hide(); // Hide the filter dropdown
            return; // Exit the function
        }
        $("#MealTypeFilter").show(); 
        // Loop through the received restaurants and append them to the list
        videos.forEach(function(video) {
            var videoId = '';
            var videoSrc = '';
            
            // Check if the link is a YouTube link
            if (isYouTubeLink(video.videoLink)) {
                // Extract the video ID from the YouTube link
                videoId = extractYouTubeVideoId(video.videoLink);
                videoSrc = `https://www.youtube.com/embed/${videoId}`;
            } else {
                // If it's not a YouTube link, use the original link
                videoSrc = video.videoLink;
            }
            
            var videoItem = `
            <div class="card mb-3 col-md-4" style="max-width: 100%;">
                <div class="card-body">
                    <h2 class="videoName">${video.videoName}</h2>
                    <h5 class="videoType">${video.videoType}</h5>
                    <div class="video-container" style="position: relative; overflow: hidden; padding-top: 56.25%; display: flex; justify-content: center; align-items: center;">
                        <iframe style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;" src="${videoSrc}" frameborder="0" allowfullscreen></iframe>
                    </div>
                    <h5 class="videoNotes">${video.videoNotes}</h5>
                </div>
            </div>`;
            if(video.videoType != ""){
                allVideoTypes.push(video.videoType);
            }
            var videoElement = $(videoItem); // Convert the HTML string to jQuery element
            userVideoList.append(videoElement);
            videoElements.push(videoElement); // Push the jQuery element to global array
        });
        allVideoTypes = removeVideoDuplicatesFromArray(allVideoTypes);
        generateVideoFilterOptions()
    }

    // Function to check if a link is from YouTube
    function isYouTubeLink(link) {
        return link.includes('youtube.com') || link.includes('youtu.be');
    }
    function extractYouTubeVideoId(url) {
        var videoId = url.split('v=')[1];
        var ampersandPosition = videoId.indexOf('&');
        if (ampersandPosition !== -1) {
            videoId = videoId.substring(0, ampersandPosition);
        }
        return videoId;
    }

    function removeVideoDuplicatesFromArray(array) {
        return [...new Set(array.filter(item => item !== ""))];
    }

    function generateVideoFilterOptions() {
        var filterDropdown = $("#MealTypeFilter");
        filterDropdown.empty(); // Clear existing options
    
        // Add default option for "All"
        filterDropdown.append(`<option value="">All</option>`);
    
        // Iterate over restaurant types and generate options
        allVideoTypes.forEach(function(type) {
            var option = `<option value="${type}">${type}</option>`;
            filterDropdown.append(option);
        });
    }

    $(document).on("change", "#MealTypeFilter", function() {
        var selectedType = $(this).val(); // Get the selected restaurant type
    
        // Show or hide videos based on the selected type
        videoElements.forEach(function(videoElement) {
            var videoType = videoElement.find(".videoType").text();
            if (selectedType === "" || videoType === selectedType) {
                videoElement.show();
            } else {
                videoElement.hide();
            }
        });
    });
});