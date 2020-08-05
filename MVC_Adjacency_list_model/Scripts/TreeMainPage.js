$(document).ready(function () {
    AddListClasses();
});

$("#toggleButton").click(function () {
    toggleAllCategories();
});

$('ul li').click(function (e) {
    e.stopPropagation();
    $(this).children('.subLink').toggle('slow');
});

$("#listDiv a").click(function (e) {
    e.stopPropagation();
});


//FUNCTIONS
function deleteCategory(ID) {
    bootbox.confirm("Are you sure you want to delete this node and its children?", function (bootboxResult) {
        if (bootboxResult) {
            $.ajax({
                url: "/api/category/" + ID,
                method: "DELETE",
                success: function (bootboxSucces) {
                    location.reload();
                }
            });
        }
    });
}

function toggleAllCategories() {
    if ($('.subLink:visible').length)
        $('.subLink').hide("slow");
    else
        $('.subLink').show("fast");
};

function AddListClasses() {

    $('li:has(ul)').addClass('link');
    $('ul ul').addClass('subLink');
};



//$(this).parents().eq(1).find("li[data-my-level='2']").addClass("red");


$('.newClick').click(function () {  // inserted callback param EVENT

    //wybranie rodziców 2 poziomy wyżej
    var parents = $(this).parents().eq(1);

    //current depth of clicked object
    var parentDepth = parseInt(parents.first("li").attr("data-my-level"));

    //depth of clicked object's child
    var intChildDepth = parseInt(parentDepth) + 1;

    //only lower LI children
    var lowerLiChildren = parents.find("li[data-my-level=" + intChildDepth + "]");

    function uCase(param) {
        return $(param).text().toUpperCase();
    }

    function compareAlphabetically(a, b) {
        var firstString = uCase($(a).first());
        var secondString = uCase($(b).first());
        return (firstString > secondString) ? 1 : -1; //ORDER
    }

    lowerLiChildren.sort(compareAlphabetically).each(function () {
        this.parentNode.appendChild(this);
    });
})