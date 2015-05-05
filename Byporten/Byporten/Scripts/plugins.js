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

    function validateUser(checkUser) {

        $('.regForm').on('submit', function () {
            $.ajax({
                url: 'Registrering',
                type: 'POST',
                contentType: 'application/json;',
                beforeSend: function () {
                    $('#loading').addClass('fa fa-circle-o-notch fa-spin');
                },
                success: function (valid) {
                    $('#loading').addClass('fa fa-circle-o-notch fa-spin');
                },
                error: function () {
                } 
            })
        });
    }
    
    function loginUser() {
        $('form').on('submit', function (e) {
            $.ajax({
                url: 'Kundeklubb',
                type: 'POST',
                beforeSend: function () {
                    $('#loading').addClass('fa fa-circle-o-notch fa-spin');
                    console.log('LOADING THIS REQUEST OF LOG IN')    
                    
                },
                success: function (valid) {
                    $('#loading').addClass('fa fa-circle-o-notch fa-spin');
                    console.log('success');
                },
                error: function (err) {
                    console.log('Error i logg inn')
                    e.preventDefault();
                }
            })
        })
    }
    

    checkURL();
    validateUser();
    loginUser();
})();