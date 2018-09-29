// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    if ($.isFunction($.fn.datetimepicker)) {
        $("#OpDate-datetimepicker").datetimepicker({
            format: 'YYYY/MM/DD HH:mm',
            useCurrent: true
        });
        $("#CloseDate-datetimepicker").datetimepicker({
            format: 'YYYY/MM/DD HH:mm',
            useCurrent: false
        });
        $("#ETD-datetimepicker").datetimepicker({
            format: 'YYYY/MM/DD',
            useCurrent: false
        });
    }

    // 侧栏user菜单，设置为网站首页
    if (window.location.pathname === "/") {
        var user_menu = $(".side-menu a[href='/Users']")
            .closest("li").addClass("current-page")
            .closest("ul").css({ "display": "block" })
            .closest("li").addClass("active");
    }
});


//len: 多余len行开始滚动
var doscroll2 = function (len) {
    var $parent = $('.table > tbody');
    var $first = $parent.find('tr:first');
    var height = $first.find('a').css('line-height');

    $first.children('td').css("padding-top", 0);
    $first.children('td').css("padding-bottom", 0);

    $first.children('td')
        .not(':has(div.td-slider)')
        .wrapInner('<div class="td-slider" />');

    if ($(".vessels-table tbody tr").length > len) {
        $first.children('td')
            .children(".td-slider")
            .slideUp(
                1000,
                function () {
                    $(this).closest('tr').appendTo($parent);
                    $(this).show();
                });
    };

};

var setColor = function () {
    $('tbody tr td:nth-child(4)').each(function () {
        var OpDate = new Date($(this).text());
        var tr = $(this).closest('tr');
        var CloseDate = new Date(tr.find("td:nth-child(5)").text());
        var CurrentDate = new Date();
        if (OpDate <= CurrentDate && CloseDate > CurrentDate) {
            tr.css("color", "#7cfc00");
        }
    });

    $('tbody tr td:nth-child(5)').each(function () {
        var CloseDate = new Date($(this).text());
        if (CloseDate <= new Date()) {
            $(this).closest('tr').css("color", "#ffff00");
        }
    });
}