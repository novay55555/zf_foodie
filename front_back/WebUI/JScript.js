$(function () {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf(')/') + 2);
    return (prePath + postPath); //如果发布IIS，有虚假目录用用这句

    $.ajax({
        url: "data_ashx/Handler_Log.ashx?action=logout&LoginName=admin&Password=1234&MobieCode=1221",
        async: true,
        type: "get",                           //http的请求方式
        dataType: "text",                       //预期数据库返回的数据类型
        success: function (data) {              //请求成功后调用的回调函数
            console.info(data);
        },
        error: function () {
            alert("加载错误，请稍后重试")
        }
    });
});