/*
*  =========================================
*   JavaScript document for _LayoutClient
*  =========================================
*   Authors: Gruppe 27, Westerdals OSLO ACT
*  =========================================
*/

(function () {

    var Toggled = false;
    var toggle_main_menu = function () {
        $('.menu-button').click(function () {
            if (Toggled == false) {
                Toggled = true;
                $('.hidden-menu-wrap').animate({ left: '0px' }, 250);
                $('body').animate({ left: '230px' }, 250);
                $('body').css('max-width', '100%');
                $('body').css('overflow-x', 'hidden');
                $('.menu-button').removeClass('fa fa-bars');
                $('.menu-button').addClass('fa fa-remove');

            } else {
                Toggled = false;
                $('.hidden-menu-wrap').animate({ left: '-230px' }, 250);
                $('body').animate({ left: '0px' }, 250);
                $('.menu-button').removeClass('fa fa-remove');
                $('.menu-button').addClass('fa fa-bars');
            }
        });
    }

    var searchbar = false;
    var toggle_searchbar = function () {
        $('.search-button').click(function () {
            if (searchbar == false) {
                searchbar = true;
                $('.search-wrapper').slideDown(100);
            } else {
                searchbar = false;
                $('.search-wrapper').slideUp(100);
            }
           
        })
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
        toggle_searchbar();
        carousel();
    }    

    
    init(); // calling init on page-load
})();