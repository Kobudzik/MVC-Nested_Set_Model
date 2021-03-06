﻿$(document).ready(function () {
    addListClasses();
});

//BUTTONS
$("#toggleAllButton").click(function () {
    toggleAllCategories();
});

$('ul li').click(function (e) {
    e.stopPropagation();
    $(this).children('.subLink').toggle('slow');
});

$("#listDiv a").click(function (e) {
    e.stopPropagation();
});

$(".glyphicon-eye-open").click(function () {
    $(this).toggleClass("glyphicon glyphicon-eye-open");
    $(this).toggleClass("glyphicon glyphicon-eye-close");

});

//FUNCTIONS
function toggleAllCategories() {
    if ($('.subLink:visible').length)
        $('.subLink').hide("slow");
    else
        $('.subLink').show("fast");
};

function addListClasses() {

    $('li:has(ul)').addClass('link');
    $('ul ul').addClass('subLink');
};

$('.sortButton, #sortAllButton').click(function () {
    var parents = $(this).parents().eq(1);
    var parent = parents.first("li");
    var parentDepth = parseInt(parent.attr("data-my-level"));
    var intChildDepth = parseInt(parentDepth) + 1;
    var lowerChildren = parents.find("li[data-my-level=" + intChildDepth + "]");

    if ($(this).attr("id") == "sortAllButton") {
        lowerChildren = $("#listDiv").find("li[data-my-level='1']");
        parent = lowerChildren;
    }

    lowerChildren.sort(compareAlphabetically).each(function () {
        this.parentNode.appendChild(this);
    });

    function uCase(param) {
        return $(param).text().toUpperCase();
    }


    function compareAlphabetically(a, b) {
        var firstString = uCase($(a).first());
        var secondString = uCase($(b).first());

        if (parent.attr("order") == "database" || parent.attr("order") == "desc") {
            return (firstString > secondString) ? 1 : -1; //ASC ORDER
        }

        else {
            return (firstString > secondString) ? -1 : 1; //DESC ORDER
        }
    }

    //przełączanie na desc
    if (parent.attr("order") == "asc") {
        parent.attr("order", "desc");
        return;
    }

    //przełączanie na asc
    if (parent.attr("order") == "database" || parent.attr("order") == "desc")
        parent.attr("order", "asc");
})