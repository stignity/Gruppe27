/*
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
            $('.master').animate({ right: '290px' }, 250);
            $('.master').css('max-width', '100%');
            $('.master').css('overflow-y', 'hidden');
            $('.menu-trigger').hide('fast');
        });
        $('.menu-header').click(function () {
            $('.hidden-menu-wrap').animate({ right: '-290px' }, 250);
            $('.master').animate({ right: '0px' }, 250);
            $('.menu-trigger').show('fast');
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

    var to_top = function () {
        $(document).scroll(function () {
            var y = $(this).scrollTop();
            if (y > 800) {
                $('.to-top').fadeIn();
            } else {
                $('.to-top').fadeOut();
            }
        });

        $('.to-top').click(function () {
            $("html, body").animate({ scrollTop: 0 }, "slow");
        })
    }

    var isOpen = false;
    function showSearch() {
        $('.search-top-button').click(function () {
            if (isOpen === false) {
                isOpen = true;
                $('.hideOnSearch').fadeOut(100);
                $('.searchbar-top').show(100, function () {
                    $('.searchbar-top').animate({width: '400px'}, 100)
                })
            } else {
                isOpen = false;
                $('.searchbar-top').animate({ width: '0px' }, 100, function () {
                    $('.searchbar-top').hide(100, function () {
                        $('.hideOnSearch').show();
                    });
                })
                
            }
            console.log(isOpen)
        })
    }   

  
    //pageload function
    var init = function () {
        toggle_main_menu();
        carousel();
        to_top();
        showSearch();
    }    

    
    init(); // calling init on page-load
})();