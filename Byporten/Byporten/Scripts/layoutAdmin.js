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
            $(".article-title").each(function () {

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

    var init = function () {
        mobile_menu();
        search_articles();
        display_article_content();
    }

    //calling init
    init();
})();
