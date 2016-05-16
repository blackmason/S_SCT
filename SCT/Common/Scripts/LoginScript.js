$(document).ready(function () {
    EmptyFormCheck();
    Logout();
});

/* 빈 폼 검사 */
function EmptyFormCheck() {
    $('#btn-submit').click(function () {
        var id = $('#user-id').val();
        var pw = $('#user-pass').val();

        if (id == null || id == '') {
            alert('이메일 주소를 입력해주세요.');
            return false;
        } else {
            if (pw == null || pw == '') {
                alert('암호를 입력해주세요.');
                return false;
            } else {
                return true;
            }
        }
    });
}

/* 로그인-사용안함 */
function CheckAuthAjax(id, pw) {
    $.ajax({
        url: '/Authorize/Login',
        data: { id: id, password: pw },
        success: function (data) {
            alert(data);
        },
        error: function (request, status, error) {
            alert("code:" + request.status + "\n" + "message:" + request.responseText + "\n" + "error:" + error);
        }
    });
}

/* 로그아웃 */
function Logout() {
    $('#btn-logout').click(function () {
        $.ajax({
            url: '/Authorize/Logout',
            success: function () {
                alert("로그아웃 되었습니다.");
                window.location.reload();
            }
        })
    });
    
}