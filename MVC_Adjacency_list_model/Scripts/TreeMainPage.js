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


    //<a> (display menu) toggling fix



        //$('#newClick').click(function (toggle) {  // inserted callback param EVENT
        //    toggle.stopPropagation();
        //    $(this).first(".subLink").addClass("red");
        //    var inside = $("li:first-child");

        //    inside.each(function () {
        //        alert($(this).attr("data-my-content"));
        //    });

        //});




