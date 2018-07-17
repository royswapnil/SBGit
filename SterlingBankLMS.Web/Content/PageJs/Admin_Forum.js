$("#add_category_btn").click(function () {
    $(this).addClass("hide");
    $("#remove_category_btn").removeClass("hide");
    $("#add_cat_form").removeClass("hide");
})

$("#remove_category_btn").click(function () {
    $(this).addClass("hide");
    $("#add_category_btn").removeClass("hide");
    $("#add_cat_form").addClass("hide");
})