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

    var toggle_searchbar = function () {

    }



    //pageload function
    var init = function () {
        toggle_main_menu();
        toggle_searchbar();
    }    

    
    init(); // calling init on page-load
})();