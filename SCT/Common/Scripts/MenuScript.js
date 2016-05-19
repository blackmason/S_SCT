$(document).ready(function () {

    $.ajax({
        url: '/Base/SetMenus',
        success: function (data) {
            SetDropdownMenu(data);
        },
        error: function () {
            alert('error');
        }
    });
});

function SetDropdownMenu(obj) {
    var menus = [];
    var child = [];
    var k = 0;

    // isolation the parents and child
    $.each(obj, function (i) {
        if (obj[i].ParentCode == '0') {
            menus[i] = {
                code: obj[i].Code,
                parentCode: obj[i].ParentCode,
                name: obj[i].Name,
                url: obj[i].Url,
                role: obj[i].Role,
                enabled: obj[i].Enabled,
                child: []
            }
        }
        else {
            child[k] = {
                code: obj[i].Code,
                parentCode: obj[i].ParentCode,
                name: obj[i].Name,
                url: obj[i].Url,
                role: obj[i].Role,
                enabled: obj[i].Enabled,
                child: []
            }
            k++;
        }
    });

    for (var i = 0; i <= child.length - 1; i++) {
        for (var k = 0; k <= menus.length - 1; k++) {
            if (child[i].parentCode == menus[k].code) {
                menus[k].child.push(child[i]);
            } 
        }
    }

    ObjToHTML(menus, $('#main-menus'));

}

function ObjToHTML(obj, target) {
    $.each(obj, function (i) {
        //var li = $('<li>' + this.name + '</li>');
        var li = $('<li><a href="' + this.url + '">' + this.name + '</a></li>');
        //href.appendTo(li);
        li.appendTo(target).trigger('create');

        if (this.child && this.child.length > 0) {
            var ul = $('<ul class="sub_menus"></ul>');
            ul.appendTo(li);
            ObjToHTML(this.child, ul);
        }
    });

}