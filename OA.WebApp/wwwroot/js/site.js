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

var vessel = {
    //len: 多余len行开始滚动
    doScroll: function (len) {
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

    },

    setColor: function () {
        $('tbody tr td:nth-child(4)').each(function () {
            var OpDate = moment($(this).text(), 'YYYY/MM/DD HH:mm');
            var tr = $(this).closest('tr');
            var CloseDate = moment(tr.find("td:nth-child(5)").text(), 'YYYY/MM/DD HH:mm');
            var CurrentDate = moment().format('YYYY/MM/DD HH:mm');
            if (OpDate <= CurrentDate && CloseDate > CurrentDate) {
                tr.css("color", "#98ff35");
            }
        });

        $('tbody tr td:nth-child(5)').each(function () {
            var CloseDate = moment($(this).text(), 'YYYY/MM/DD HH:mm');
            if (CloseDate <= moment().format('YYYY/MM/DD HH:mm')) {
                $(this).closest('tr').css("color", "#ffff33");
            }
        });

        $('tbody tr td:nth-child(6)').each(function () {
            var ETD = moment($(this).text(), 'YYYY/MM/DD');
            var day = moment().format('YYYY/MM/DD');
            if (ETD.isValid() && ETD.isSame(day)) {
                $(this).closest('tr').css("color", "#ff3333");
            }
        });
    }
}

var t1_data = {
    clock: function () {
        var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        var dayNames = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"]

        // Create a newDate() object
        var newDate = new Date();

        // Extract the current date from Date object
        newDate.setDate(newDate.getDate());

        // Output the day, date, month and year
        $('#date').html(newDate.getFullYear() + ' 年 ' + (newDate.getMonth() + 1) + ' 月 ' + newDate.getDate() + ' 日 ' + dayNames[newDate.getDay()]);

        setInterval(function () {

            // Create a newDate() object and extract the seconds of the current time on the visitor's
            var seconds = new Date().getSeconds();

            // Add a leading zero to seconds value
            $("#sec").html((seconds < 10 ? "0" : "") + seconds);
        }, 1000);

        setInterval(function () {

            // Create a newDate() object and extract the minutes of the current time on the visitor's
            var minutes = new Date().getMinutes();

            // Add a leading zero to the minutes value
            $("#min").html((minutes < 10 ? "0" : "") + minutes);
        }, 1000);

        setInterval(function () {

            // Create a newDate() object and extract the hours of the current time on the visitor's
            var hours = new Date().getHours();

            // Add a leading zero to the hours value
            $("#hours").html((hours < 10 ? "0" : "") + hours);
        }, 1000);
    }
}
