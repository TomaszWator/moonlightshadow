// var velocity = 0.00000;

// $(document).ready()
// {
//     function afterScroll() {
//         var position = $(window).scrollTop();
//         $('.content').each(function () {
//             var $element = $(this);

//             var offsetTop = $element.offset().top;

//             $(this).css('background-position-y', (Math.round(position - offsetTop) * velocity) + 18.4 + 'px');
//         });
//     }

//     $(window).bind('scroll', afterScroll);
// }

var activeTab = 1;

$(document).ready(function () {
    showTab(1);
    hideTab(2);
    hideTab(3);
    hideTab(4);

    $('#filter-menu').on('click', function (event) {
        var tabId = event.target.id.match(/\d+/).toString(); 

        if($.isNumeric(tabId)){
            hideTab(activeTab);
            showTab(tabId);
        } 
    });

    function showTab(tabId)
    {   
        $('#filter-tab-item-' + tabId).show();
        $('#filter-menu-item-' + tabId).addClass("active");
        activeTab = tabId;
    }

    function hideTab(tabId)
    {   
        $('#filter-tab-item-' + tabId).hide();
        $('#filter-menu-item-' + tabId).removeClass("active");
    }
});
