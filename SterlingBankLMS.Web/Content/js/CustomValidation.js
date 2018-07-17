//// Custom Validation Rules
$.validator.addMethod("dropdown", function (value, element) {
    //if all values are selected

    if (value == "0" || value == null || value == '') {
        return false;
    } else {
        return true;
    }
}, 'Please select from list');

$.validator.addMethod("timepicker", function (value, element) {

    var validTime = $(element).val().match(/^(0?[1-9]|1[012])(:[0-5]\d) [APap][mM]$/);
    if (validTime) {
        return true;
    } else {
        return false;
    }
}, 'Invalid Time');

$.validator.addMethod('less', function (value, element, param) {
    return this.optional(element) || parseInt(value) <= parseInt($(param).val());
}, '');

$.validator.addMethod('greater', function (value, element, param) {
    return this.optional(element) || parseInt(value) >= parseInt($(param).val());
}, '');

//$.validator.addMethod("checkForDuplicate", function (value, element, param) {
//    var textValues = [],
//        //initially mark as valid state
//        valid = true;

//    $('.page-content').find("input." + param + "").each(function () {

//        if (String($(this).val()).trim() !== "") {
//            var doesExisit = ($.inArray($(this).val(), textValues) === -1) ? false : true;
//            if (doesExisit === false) {
//                textValues.push($(this).val());
//            } else {
//                valid = false;
//                return false;
//            }
//        }

//    });
//    //return the valid state
//    return valid;
//});

$.validator.addMethod('DateGreater', function (value, element, param) {
    var start = [];
    if (param == "currentDate") {
        date = new Date();
        start.push(date.getDate());
        start.push(date.getMonth() + 1);
        start.push(date.getFullYear());
    }
    else {
        start = $(param).val().split("/");
    }
        

    var StartDate = new Date(start[2], parseInt(start[1]) - 1, parseInt(start[0]), 0, 0, 0, 0);

    var End = value.split("/");
    var EndDate = new Date(End[2], parseInt(End[1]) - 1, parseInt(End[0]), 0, 0, 0, 0);

    return this.optional(element) || EndDate > StartDate;
}, 'Date must be greater than Start Date');

$.validator.addMethod('DateGreaterOrEqual', function (value, element, param) {
    var start = [];
    if (param == "currentDate") {
        date = new Date();
        start.push(date.getDate());
        start.push(date.getMonth() + 1);
        start.push(date.getFullYear());
    }
    else {
        start = $(param).val().split("/");
    }
    var StartDate = new Date(start[2], parseInt(start[1]) - 1, parseInt(start[0]), 0, 0, 0, 0);

    var End = value.split("/");
    var EndDate = new Date(End[2], parseInt(End[1]) - 1, parseInt(End[0]), 0, 0, 0, 0);

    return this.optional(element) || EndDate >= StartDate;
}, function (value, element) {

    if (value == 'currentDate')
        return 'Date must be greater than or equal to current date';
    else
        return 'Date must be greater than or equal to start date';
});


