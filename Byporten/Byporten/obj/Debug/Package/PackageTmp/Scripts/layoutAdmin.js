/*
*  =========================================
*   JavaScript document for _LayoutAdmin
*  =========================================
*   Authors: Gruppe 27, Westerdals OSLO ACT
*  =========================================
*/

(function () {
    function mobile_menu() {
        $('.menu-button-mobile').click(function () {
            $('.menu-wrap').slideToggle(300);
        });
    }

    var search_articles = function () {
        $("#filter").keyup(function () {

            // Retrieve the input field text and reset the count to zero
            var filter = $(this).val(), count = 0;

            // Loop through the comment list
            $(".last-article-wrap").each(function () {

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

    var display_article_content = function () {
        var check = false;
        $('.article-title').click(function () {
            if (check == false) {
                check = true;
                $(this).nextAll(".article-content").first().slideDown("fast");
            } else {
                check = false;
                $(this).nextAll(".article-content").first().slideUp("fast");
            }
            console.log(check);
        });
    }

    var lineBreakOnNewArticle = function(){
        $('textarea').bind('keyup', function (e) {
            var data = $('textarea').val();
            $('.result').html(data.replace(/\n/g, "<br />"));
        });
    }

    var responseArticle = function () {
        $('.createArticle').on('submit', function () {
            $.ajax({
                url: 'Create',
                type: 'POST',
                beforeSend: function () {
                    $('.added').fadeIn();
                    $('.publish-loading').append('<i style="font-size: 7em !important;" class="fa fa-circle-o-notch fa-spin"></i>');
                },
                success: function () {
                    $('.added').fadeOut();
                },
                error: function (err) {
                    return err;
                }
            })

        });
    }

    var responseOffer = function () {
        $('.createOffer').on('submit', function () {
            $.ajax({
                url: 'CreateOffer',
                type: 'POST',
                beforeSend: function () {
                    $('.added').fadeIn();
                    $('.publish-loading').append('<i style="font-size: 7em !important;" class="fa fa-circle-o-notch fa-spin"></i>');
                },
                success: function () {
                    $('.added').fadeOut();
                },
                error: function (err) {
                    return err;
                }
            })

        });
    }

    var responsePosition = function () {
        $('.createPosition').on('submit', function () {
            $.ajax({
                url: 'createNewPosition',
                type: 'POST',
                beforeSend: function () {
                    $('.added').fadeIn();
                    $('.publish-loading').append('<i style="font-size: 7em !important;" class="fa fa-circle-o-notch fa-spin"></i>');
                },
                success: function () {
                    $('.added').fadeOut();
                },
                error: function (err) {
                    return err;
                }
            })

        });
    }

    var responseStore = function () {
        $('.createStore').on('submit', function () {
            $.ajax({
                url: 'CreateStore',
                type: 'POST',
                beforeSend: function () {
                    $('.added').fadeIn();
                    $('.publish-loading').append('<i style="font-size: 7em !important;" class="fa fa-circle-o-notch fa-spin"></i>');
                },
                success: function () {
                    $('.added').fadeOut();
                },
                error: function (err) {
                    return err;
                }
            })

        });
    }

    var init = function () {
        mobile_menu();
        search_articles();
        display_article_content();
        lineBreakOnNewArticle();
        responseArticle();
        responseOffer();
        responsePosition();
        responseStore();
    }

    //calling init
    init();
})();
