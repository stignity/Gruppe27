/*
*  =========================================
*   JavaScript document for all clients, with plugins
*  =========================================
*   Authors: Gruppe 27, Westerdals OSLO ACT
*  =========================================
*/


//facebook feed
(function(d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/nb_NO/sdk.js#xfbml=1&version=v2.3";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));


(function () {

    var search_stores = function () {
        $("#filter").keyup(function () {

            // Retrieve the input field text and reset the count to zero
            var filter = $(this).val(), count = 0;

            // Loop through the comment list
            $(".store-link").each(function () {

                // If the list item does not contain the text phrase fade it out
                if ($(this).text().search(new RegExp(filter, "i")) < 0) {
                    $(this).fadeOut();

                    // Show the list item if the phrase matches and increase the count by 1
                } else {
                    $(this).show();
                    count++;
                }
            });

            

            // Update the count
            var numberItems = count;
        });
    }

    function checkURL() {
        // Can 'fix' many imgs at once (with the appropriate logic in the body)
        $('.contain-image img').attr('src', function (i, src) {
            // If it's a JPG, leave it alone, otherwise...
            return /\.(jpg|png|pdf)$/.test(src) ? src : '/images/web/image_error.jpg';
        });

        $('.link img').attr('src', function (i, src) {
            // If it's a JPG, leave it alone, otherwise...
            return /\.(jpg|png|pdf)$/.test(src) ? src : '/images/web/image_error.jpg';
        });
    }

    function activeNav() {
        var path = window.location.pathname;
        path = path.replace(/\/$/, "");
        path = decodeURIComponent(path);

        $('.top-nav-most-used').each(function () {
            var href = $(this).attr('href');
            if (path.substring(0, href.length) === href) {
                $(this).closest('li').addClass('active');
            }
        });

    }

    checkURL();
    search_stores();
    
})();