// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    
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
});