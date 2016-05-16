$(document).ready(function () {
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
                //return true;
                CheckAuthAjax(id, pw);
            }
        }

        
    });
});

function CheckAuthAjax(id, pw) {
    $.ajax({
        url: '/Authorize/Login',
        dataType: 'post',
        data: { id: id, password: pw },
        success: function (data) {
            alert(data);
        },
        error: function () {
            alert('error');
        }
    });
}