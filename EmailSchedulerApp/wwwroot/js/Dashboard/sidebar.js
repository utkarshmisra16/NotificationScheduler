$(function () {
    $(".sidebar-wrapper").on("mouseenter", function () {
        if ($(this).hasClass("expanded")) 
        return;
        $(this).find(".sidebar-logo").addClass("d-none");
        $(this).find(".sidebar-toggle-icon").removeClass("d-none");
    });
    $(".sidebar-wrapper").on("mouseleave", function () {
        if ($(this).hasClass("expanded")) 
        return;
        $(this).find(".sidebar-toggle-icon").addClass("d-none");
        $(this).find(".sidebar-logo").removeClass("d-none");
    });
});

$(document).on("click", "#sidebarToggleIcon", function () {
    $(".sidebar-wrapper").toggleClass("expanded");
    $(".sidebar-brand-sub, .sidebar-brand-text").addClass("d-block");
    $(".sidebar-toggle-icon").addClass("d-none");
    $(".sidebar-logo").removeClass("d-none");
    $("#sidebarToggleClose").removeClass("d-none");
}); 

$(document).on("click", "#sidebarToggleClose", function(){
    $(".sidebar-wrapper").toggleClass("expanded");
    $(".sidebar-brand-sub, .sidebar-brand-text").removeClass("d-block");
});