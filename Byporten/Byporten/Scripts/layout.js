﻿/*
*  =========================================
*   JavaScript document for _LayoutClient
*  =========================================
*   Authors: Gruppe 27, Westerdals OSLO ACT
*  =========================================
*/

(function () {

    var toggle_main_menu = function () {
        $('.menu-trigger').click(function () {
            $('.hidden-menu-wrap').animate({ right: '0px' }, 250);
            $('.master').animate({ right: '200px' }, 250);
            $('.master').css('max-width', '100%');
            $('.master').css('overflow-y', 'hidden');
            $('.menu-trigger').fadeOut();
        });
        $('.menu-header').click(function () {
            $('.hidden-menu-wrap').animate({ right: '-290px' }, 250);
            $('.master').animate({ right: '0px' }, 250);
            $('.menu-trigger').fadeIn();
        });   
    }

    var carousel = function () {
        $("#slideshow > div:gt(0)").hide();

        setInterval(function () {
            $('#slideshow > div:first')
              .fadeOut(1000)
              .next()
              .fadeIn(1000)
              .end()
              .appendTo('#slideshow');
        }, 3000);
    }

    //pageload function
    var init = function () {
        toggle_main_menu();
        carousel();
    }    

    
    init(); // calling init on page-load
})();