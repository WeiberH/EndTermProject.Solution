let formHelper = {
    /**
     * 清空下拉選單的內容
     * @param {HtmlEelement} ddl
     */
    emptyDDL: function (ddl) {
        // 判斷 ddl 是 select 元素
        if (ddl.tagName != "SELECT") throw new Error("ddl 必需是 select 元素");
        ddl.innerHTML = "";
    },

    /**
     * 繫結陣列資料到 select 元素, 不包含設定值
     * @param ddl
     * @param datasource
     * @param valueMember
     * @param textMember
     */
    bindDDL: function (ddl, datasource, valueMember, textMember) {
        // 判斷 ddl 是 select 元素
        if (ddl.tagName != "SELECT") throw new Error("ddl 必需是 select 元素");
        // 判斷 datasource 是陣列
        if (!Array.isArray(datasource)) throw new Error("datasource 必需是陣列");
        // valueMember, textMember 必需是字串,且不能是null
        if (typeof valueMember != "string" || valueMember == null || typeof valueMember === 'undefined') throw new Error("valueMember 必需是字串");
        if (typeof textMember != "string" || textMember == null || typeof valueMember === 'undefined') throw new Error("textMember 必需是字串");

        // 先清空 ddl 的內容
        ddl.innerHTML = "";

        datasource.forEach(function (item) {
            let option = document.createElement("option");
            option.value = item[valueMember];
            option.innerText = item[textMember];
            ddl.appendChild(option);
        });
    },
};