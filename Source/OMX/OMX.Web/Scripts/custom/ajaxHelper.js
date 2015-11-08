var ajaxHelper = (function() {
    function hideNotification(id) {
        $(id).hide(300);
    }
    return {
        hideNotification: hideNotification
    }
})();