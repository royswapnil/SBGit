var mainApp = function () {

    var handleExpiredSession = function (appVars) {
        $(document).ajaxError(function (e, xhr, settings, err) {
            if (xhr.status === 401) {
                var refferer = window.encodeURIComponent(location.href.replace(window.location.origin, ''));
                window.location.replace(appVars.loginUrl + refferer);
                return false;
            }
        });
    }

    return {
        init: function (appVars, $) {
            if (!appVars || !$) {
                alert('App not fully initialized');
                return;
            }
            handleExpiredSession(appVars);
        },

        getUrlQueryStringValue: function (name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }
    };
}();