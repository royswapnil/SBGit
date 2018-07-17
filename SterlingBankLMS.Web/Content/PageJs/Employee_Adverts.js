$(document).ready(function () {
    getAd();
})

setInterval(getAd, 2000);

function getAd() {
    var url = "/Common/Home/GetAdvert";
    $.get(url, null, function (data) {
        var ad_url = "Content/img/" + data.FileUrl;
        $("#adverts").attr("src", ad_url);
        $("#ad_link").attr("href", data.AdvertLink);
        $("#ad_link").attr("title", data.Description);
    })
}