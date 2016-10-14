var rowSelected;
$(function () {
    GetEatSystemUserInfoDatagrid();
});


function btn_stop(flag_status, pk_id, text) {
    if (!EndEdit()) {
        $.messager.alert("系统提醒", "当前页存在未填写完整的数据，请填写完整");
        return;
    }
    $.messager.confirm("系统提醒", "确认" + text + "吗", function (r) {
        if (r) {
            AjaxJson("/Pages/EatSystemUserInfoManagement/data_ashx/EatSystemUserInfo.ashx",
                { "action": "StopEatSystemUserInfo", "pk_id": pk_id, "flag_status": flag_status },
                function (data) {
                    $.messager.alert("系统提醒", data[0].Message, "", function () {
                        if (data[0].Success == 1) {
                            GetEatSystemUserInfoDatagrid();
                        }
                    });
                });
        }
    });
}

function btn_del_base(pk_id) {
    if (!EndEdit()) {
        $.messager.alert("系统提醒", "当前页存在未填写完整的数据，请填写完整");
        return;
    }
    $.messager.confirm("系统提醒", "确认删除吗", function (r) {
        if (r) {
            AjaxJson("/Pages/EatSystemUserInfoManagement/data_ashx/EatSystemUserInfo.ashx",
                { "action": "DeleteEatSystemUserInfoBase", "pk_id": pk_id },
                function (data) {
                    $.messager.alert("系统提醒", data[0].Message, "", function () {
                        if (data[0].Success == 1) {
                            GetEatSystemUserInfoDatagrid();
                        }
                    });
                });
        }
    });
}
function GetEatSystemUserInfoDatagrid() {
    $("#EatSystemUserInfoTable").datagrid({
        url: "data_ashx/EatSystemUserInfo.ashx",
        queryParams: { "action": "GetEatSystemUserInfoDatagrid" },
        loadMsg: "",
        rownumbers: true,
        singleSelect: true,
        fitColumns: true,
        fit: true,
        checkOnSelect: false,
        selectOnCheck: false,
        onDblClickRow: function (index, row) {
            if (rowSelected != undefined) {
                if (!EndEdit()) {
                    return;
                }
            }
            rowSelected = index;
            $("#EatSystemUserInfoTable").datagrid("beginEdit", rowSelected);
        },
        onClickRow: function () {
            if (rowSelected != undefined) {
                EndEdit();
            }
        },
        onLoadError: function () {
            $.messager.alert("系统提醒", "数据查询失败，请重试");
        },
        onBeforeLoad: function () { },
        onLoadSuccess: function () {

        },
        columns:
            [[
                  {
                      field: "operation", width: 100, align: "center", title: "操作",
                      formatter: function (value, row, index) {
                          var str = "";
                          var text = "";
                          if (row.flag_status == "0") {
                              text = "禁用";
                          }
                          else if (row.flag_status == "-1") {
                              text = "启用";
                          }
                          str += "<a href='javascript:void(0)' onclick=btn_stop('" + row.flag_status + "','" + row.pk_id + "','" + text + "')>" + text + "</a>";
                          str += "<a href='javascript:void(0)' onclick=btn_del_base('" + row.pk_id + "') style='margin-left:6px'>删除</a>";
                          return str;
                      }
                  },
                  {
                      title: "用户名", halign: "center", field: "pk_id", width: 100,
                      editor: { type: "textbox", options: { required: true } }
                  },
                  {
                      title: "用户昵称", halign: "center", field: "user_nick_name", width: 100,
                      editor: { type: "textbox", options: {} }
                  },
                  {
                      title: "用户权限", align: "center", field: "flag_power", width: 100,
                      editor: {
                          type: "combobox", options:
                              {
                                  data: [{ "id": "0", "text": "系统用户" }, { "id": "1", "text": "普通用户" }],
                                  valueField: "id",
                                  textField: "text",
                                  editable: false,
                                  panelHeight: 'auto'
                              }
                      },
                      formatter: function (value) {
                          if (value == "0") {
                              return "系统用户";
                          }
                          else if (value == "1") {
                              return "普通用户";
                          }
                      }
                  },
                  {
                      title: "状态", align: "center", field: "flag_status", width: 100,
                      editor: {
                          type: "combobox", options: {
                              data: [{ "id": "0", "text": "启用" }, { "id": "-1", "text": "禁用" }],
                              valueField: "id",
                              textField: "text",
                              editable: false,
                              panelHeight: 'auto'
                          }
                      },
                      formatter: function (value) {
                          if (value == "0") {
                              return "启用";
                          }
                          else if (value == "-1") {
                              return "禁用";
                          }
                      }
                  },
                  {
                      title: "备注", halign: "center", field: "remark", width: 100,
                      editor: { type: "textbox", options: {} }
                  }
            ]]
    });
}

function EndEdit() {
    if (!$("#EatSystemUserInfoTable").datagrid("validateRow", rowSelected)) {
        return false;
    }
    $("#EatSystemUserInfoTable").datagrid("endEdit", rowSelected); rowSelected = undefined;
    return true;
}

function btn_refresh() {
    GetEatSystemUserInfoDatagrid();
}

function btn_add() {
    if (!EndEdit()) {
        $.messager.alert("系统提醒", "当前页存在未填写完整的数据，请填写完整");
        return;
    }
    rowSelected = 0;
    $("#EatSystemUserInfoTable").datagrid("insertRow", { index: 0, row: { flag_power: 1, flag_status: 0 } });
    $("#EatSystemUserInfoTable").datagrid("beginEdit", rowSelected);
}

function btn_save() {
    if (!EndEdit()) {
        $.messager.alert("系统提醒", "当前页存在未填写完整的数据，请填写完整");
        return;
    }
    var url = "/Pages/EatSystemUserInfoManagement/data_ashx/EatSystemUserInfo.ashx";
    var postData = {
        "action": "SaveEatSystemUserInfoTable",
        "insertRows": JSON.stringify($("#EatSystemUserInfoTable").datagrid("getChanges", "inserted")),
        "updateRows": JSON.stringify($("#EatSystemUserInfoTable").datagrid("getChanges", "updated"))
    };
    AjaxJson(url, postData, function (data) {
        $.messager.alert("系统提醒", data[0].Message, "", function () {
            if (data[0].Success == "1") {
                GetEatSystemUserInfoDatagrid();
            }
        });
    });
}

