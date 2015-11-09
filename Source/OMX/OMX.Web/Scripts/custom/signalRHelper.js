$(document).ready(function () {
    $.connection.hub.start();
    var notificationHub = $.connection.notification;

    notificationHub.client.OnConnected = signalR.getNotificationsCount;
    notificationHub.client.getNotificationsCount = signalR.getNotificationsCount;
    
});

var signalR = (function() {

    function getNotificationsCount() {
        $.get("/Users/GetUnreadNotifications", function(unreadNotificationsCount) {
            $("#notificationsCount").text(unreadNotificationsCount.unreadMsgs);
        });
    }
    return {
        getNotificationsCount: getNotificationsCount
    }
})();