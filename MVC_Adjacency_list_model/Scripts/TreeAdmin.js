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

function deleteAll(){
    bootbox.confirm("Are you sure you want to delete EVERY node and its children?", function (bootboxResult) {
        if (bootboxResult) {
            $.ajax({
                url: "/api/category/DeleteAllCategries",
                method: "POST",
                success: function (bootboxSucces) {
                    location.reload();
                }
            });
        }
    });
}
