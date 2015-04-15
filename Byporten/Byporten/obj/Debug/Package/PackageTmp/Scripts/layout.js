/*
*  =========================================
*   JavaScript document for _LayoutClient
*  =========================================
*   Authors: Gruppe 27, Westerdals OSLO ACT
*  =========================================
*/

(function () {

    var toggle_main_menu = function () {
        $('.menu-button').click(function () {
            $('.hidden-menu-wrap').animate({ right: '0px' }, 250);
            $('.master').animate({ right: '250px' }, 250);
            $('.master').css('max-width', '100%');
            $('.master').css('overflow-y', 'hidden');
            $('.menu-button').fadeOut();
        });
        $('.menu-header').click(function () {
            $('.hidden-menu-wrap').animate({ right: '-250px' }, 250);
            $('.master').animate({ right: '0px' }, 250);
            $('.menu-button').fadeIn();
        });

        
    }

    var carousel = function () {
        var $els = $('div[id^=quote]'),
        i = 0,
        len = $els.length;

        $els.slice(1).hide();
        setInterval(function () {
            $els.eq(i).fadeOut(function () {
                i = (i + 1) % len
                $els.eq(i).fadeIn();
            })
        }, 7000)
    }

    //pageload function
    var init = function () {
        toggle_main_menu();
        carousel();
    }    

    
    init(); // calling init on page-load
})();