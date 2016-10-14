function GetUrl() {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf(')/') + 2);
    return (prePath + postPath); //如果发布IIS，有虚假目录用用这句
}

function AjaxJson(url, params, callBack) {
    params = params || {};
    try {
        params.MobieCode = plus.device.uuid;
    }
    catch (e) { }

    var str = "?start=start";
    for (var i in params) {
        str += ("&" + i + "=" + params[i]);
    }

    $.ajax({
        url: GetUrl() + url + str,
        async: true,
        type: "get",                           //http的请求方式
        dataType: "json",                       //预期数据库返回的数据类型
        success: function (data) {              //请求成功后调用的回调函数
            callBack(data);
        },
        error: function () {
            alert("加载错误，请稍后重试");
        }
    });
}