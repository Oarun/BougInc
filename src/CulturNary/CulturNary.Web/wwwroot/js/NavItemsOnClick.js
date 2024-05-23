window.onload = function() {
    var navItems = document.querySelectorAll('.nav-item a');

    navItems.forEach(function(navItem) {
        navItem.addEventListener('click', function() {
            navItems.forEach(function(item) {
                item.style.setProperty("background-color", "", "important");
                item.style.setProperty("color", "", "important");
            });
            //this.style.setProperty("background-color", "#407A3B", "important");
            this.style.setProperty("color", "white", "important");
        });
    });
};