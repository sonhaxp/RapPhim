function Model() {
    const btnShowModel = document.getElementById('btn-showModel')
    const model = document.querySelector('.model')
    const btnClose = document.getElementById('btn-close')
    const form = document.querySelector('.model_body')
    const headerBtns = document.querySelectorAll('.model_heading')
    const formContents = document.querySelectorAll('.form')
    const linkForgot = document.querySelector('.model_login-forgot-pass')
    const modelForgot = document.querySelector('.model_forgot-password')

    btnShowModel.onclick = function (e) {
        if (!model.classList.contains('model-open')) {
            model.classList.add('model-open')
        }
        if (!form.classList.contains('active')) {
            form.style.display = 'block'
            form.classList.add('active')
        }
    }

    btnClose.onclick = function (e) {
        if (form.classList.contains('active')) {
            form.classList.remove('active')
        }
        if (model.classList.contains('model-open')) {
            model.classList.remove('model-open')
        }
        if (modelForgot.classList.contains('open')) {
            modelForgot.classList.remove('open')
        }
    }

    model.onclick = function (e) {
        if (form.classList.contains('active')) {
            form.classList.remove('active')
        }
        if (model.classList.contains('model-open')) {
            model.classList.remove('model-open')
        }
        if (modelForgot.classList.contains('open')) {
            modelForgot.classList.remove('open')
        }
    }

    form.onclick = function (e) {
        e.stopPropagation()
    }

    let lastindex = 0

    headerBtns.forEach((elem, i) => {
        elem.onclick = function (e) {
            if (!this.classList.contains('active')) {
                this.classList.add('active')
                formContents[i].classList.add('form-display')
            }
            headerBtns[lastindex].classList.remove('active')
            formContents[lastindex].classList.remove('form-display')
            lastindex = i
        }
    })

    linkForgot.onclick = function (e) {
        if (!modelForgot.classList.contains('open')) {
            form.classList.remove('active')
            setTimeout(() => {
                form.style.display = 'none'
            }, 400);
            setTimeout(() => {
                modelForgot.classList.add('open')
                modelForgot.classList.add('active')
            }, 600);

        }
    }

    modelForgot.onclick = function (e) {
        e.stopPropagation()
        if (e.target.id === 'btn-close') {
            if (form.classList.contains('active')) {
                form.classList.remove('active')
            }
            if (model.classList.contains('model-open')) {
                model.classList.remove('model-open')
            }
            if (modelForgot.classList.contains('open')) {
                modelForgot.classList.remove('open')
            }
        }
    }
}

function Validate() {
    const inputEmail = document.querySelector('.model_register-email')
    const inputPass = document.querySelector('.model_register-password')
    const inputReEnterPass = document.querySelector('.model_register-repassword')
    const inputName = document.querySelector('.model_register-name')
    const inputAddres = document.querySelector('.model_register-addres')
    const inputGioitinh= document.querySelector('.model_register-sex')
    const inputNgaySinh = document.querySelector('.model_register-birthday')
    const inputPhone = document.querySelector('.model_register-phone')
    const btnRegister = document.getElementById('btn-register')
    const form = document.querySelector('.form-register')
    const inputSex = document.querySelector('.model_register-sex')
    let alertMess = form.querySelector('.model_aleart')

    btnRegister.onclick = function (e) {
        let isValid = true
        if (!/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/.test(inputEmail.value)) {
            if (!alertMess) {
                alertMess = document.createElement('p')
                alertMess.classList.add('model_aleart')
                alertMess.classList.add('model_aleart--error')
                form.insertBefore(alertMess, form.firstChild)
            }
            alertMess.innerHTML = `Email không hợp lệ`
            isValid = false
        }
        else if (!/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/.test(inputPass.value)) {
            if (!alertMess) {
                alertMess = document.createElement('p')
                alertMess.classList.add('model_aleart')
                alertMess.classList.add('model_aleart--error')
                form.insertBefore(alertMess, form.firstChild)
            }
            alertMess.innerHTML = `Mật khẩu ít nhất 8 ký tự bao gồm số,chữ hoa và chữ thường`
            isValid = false
        }
        else if (inputPass.value !== inputReEnterPass.value) {
            if (!alertMess) {
                alertMess = document.createElement('p')
                alertMess.classList.add('model_aleart')
                alertMess.classList.add('model_aleart--error')
                form.insertBefore(alertMess, form.firstChild)
            }
            alertMess.innerHTML = `Hai mật khẩu không khớp nhau`
            isValid = false
        }
        else if (inputSex.value === "") {
            if (!alertMess) {
                alertMess = document.createElement('p')
                alertMess.classList.add('model_aleart')
                alertMess.classList.add('model_aleart--error')
                form.insertBefore(alertMess, form.firstChild)
            }
            alertMess.innerHTML = `Vui lòng chọn giới tính`
            isValid = false
        } else {

            $.ajax({
                url: "/api/taikhoan?username=" + inputEmail.value + "&password=" + inputPass.value + "&quyen=user",
                method: "POST",
                contentType: "json",
                crossDomain: true,
                dataType: 'json',
                success: function (res) {
                    if (res == true) {
                        $.ajax({
                            url: "/api/thanhvien?hoten=" + inputName.value + "&sdt=" + inputPhone.value + "&email=" + inputEmail.value + "&diachi=" + inputAddres.value + "&ngaysinh=" + inputNgaySinh.value + "&gioitinh=" + inputGioitinh.value,
                            method: "POST",
                            contentType: "json",
                            crossDomain: true,
                            dataType: 'json',
                            success: function (res) {
                                if (res == true) {
                                    if (!alertMess) {
                                        alertMess = document.createElement('p')
                                        alertMess.classList.add('model_aleart')
                                        alertMess.classList.add('model_aleart--error')
                                        form.insertBefore(alertMess, form.firstChild)
                                    }
                                    alertMess.innerHTML = `Đăng kí thành công`;
                                    isValid = false
                                } else {
                                    if (!alertMess) {
                                        alertMess = document.createElement('p')
                                        alertMess.classList.add('model_aleart')
                                        alertMess.classList.add('model_aleart--error')
                                        form.insertBefore(alertMess, form.firstChild)
                                    }
                                    alertMess.innerHTML = `Lỗi không xác định.Vui lòng thử lại sau!`;
                                    isValid = false
                                    
                                }
                            },
                            error: function () {
                                console.log("Load api get thất bại");
                            }
                        });
                    } else {
                        if (!alertMess) {
                            alertMess = document.createElement('p')
                            alertMess.classList.add('model_aleart')
                            alertMess.classList.add('model_aleart--error')
                            form.insertBefore(alertMess, form.firstChild)
                        }
                        alertMess.innerHTML = `Email đã được sử dụng`;
                        isValid = false
                    }
                },
                error: function () {
                    console.log("Load api get thất bại");
                }
            });
        }
        if (!isValid) {
            e.preventDefault()
        }
    }
}

function showNav() {
    const btnMenu = document.getElementById('btn-menu')
    const nav = document.querySelector('.header-nav')

    btnMenu.onclick = function (e) {
        nav.classList.toggle('open')
    }
}
function showUser() {
    const container = document.getElementById('user')
    const dropDown = document.querySelector('.user-option')

    container.onclick = function (e) {
        dropDown.classList.toggle('open')
    }
}
$(document).ready(function () {
    Model()
    Validate()
    showNav()
    showUser()
    $(document).on("click", "#logout", function () {
        window.location = "/member/logout";
    });
    $(document).on("click", "#goto_member", function () {
        window.location = "/member";
    });
});

var IsRunning = false;
Login = function () {
    if (!IsRunning) {
        IsRunning = true;
        var username = document.getElementsByClassName('model_login-email model_input')[0].value;
        var password = document.getElementsByClassName('model_login-password model_input')[0].value;
        if (username == '' || password == '') {
            alert('Vui lòng nhập tên đăng nhập và mật khẩu');
            IsRunning = false;
        }
        else {
            var form = new FormData();
            form.append("username", username);
            form.append("password", password);

            var xhr = new XMLHttpRequest();
            xhr.open("POST", 'https://localhost:44314/MemBer/CheckLogin', true);
            xhr.Lineout = 30000;
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    var r = xhr.responseText;
                    var j = JSON.parse(r);
                    if (j.Data.status == "OK") {
                        document.querySelector('.model').classList.remove('model-open')
                        document.getElementById('btn-showModel').style = "display:none;"
                        document.querySelector('.user').style = "display:block;"
                        document.querySelector('.user-name').textContent = j.Data.name;
                        document.querySelector('.user-name').setAttribute("id") = j.Data.id;
                    } else {
                        alert('Sai tên đăng nhập hoặc mật khẩu');
                    }
                    IsRunning = false;
                }
            }
            xhr.send(form);
        }
    }
};
timkiem = function () {
    keyword = document.querySelector('.header_search-input').value;
    if (keyword == '') {
        window.location = "/tim-kiem";
    }
    else window.location = "/tim-kiem?keyword="+keyword;
};
function statusChangeCallback(response) {  // Called with the results from FB.getLoginStatus().
    if (response.status === 'connected') {   // Logged into your webpage and Facebook.
        testAPI();
    } else {                                 // Not logged into your webpage or we are unable to tell.
        alert('Bạn đã hủy đăng nhập bằng facebook')
    }
}
function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

window.fbAsyncInit = function () {
    FB.init({
        appId: '3219659421686983',
        cookie: true,                     // Enable cookies to allow the server to access the session.
        xfbml: true,                     // Parse social plugins on this webpage.
        version: 'v13.0'           // Use this Graph API version for this call.
    });
};
function makeid(length) {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < length; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
}
function testAPI() {                      // Testing Graph API after login.  See statusChangeCallback() for when this call is made.
    FB.api(
        '/me',
        'GET',
        { "fields": "id,name,email" },
        function (response) {
            $.ajax({
                url: '/api/taikhoan?username=' + response.email,
                method: "GET",
                contentType: "json",
                crossDomain: true,
                dataType: 'json',
                success: function (res) {
                    if (res == null) {
                        $.ajax({
                            url: "/api/taikhoan?username=" + response.email + "&password=" + makeid(15) + "&quyen=user",
                            method: "POST",
                            contentType: "json",
                            crossDomain: true,
                            dataType: 'json',
                            success: function (res) {
                                if (res == true) {
                                    $.ajax({
                                        url: "/api/thanhvien?hoten=" + response.name + "&sdt=0936822123" + "&email=" + response.email + "&diachi=Hà Nội" + "&ngaysinh=1980-01-01" + "&gioitinh=1",
                                        method: "POST",
                                        contentType: "json",
                                        crossDomain: true,
                                        dataType: 'json',
                                        success: function (res1) {
                                            if (res1 == true) {
                                                var form = new FormData();
                                                form.append("username", response.email);
                                                var xhr = new XMLHttpRequest();
                                                xhr.open("POST", 'https://localhost:44314/MemBer/CheckLoginFb', true);
                                                xhr.Lineout = 30000;
                                                xhr.onreadystatechange = function () {
                                                    if (xhr.readyState == 4 && xhr.status == 200) {
                                                        var r = xhr.responseText;
                                                        var j = JSON.parse(r);
                                                        if (j.Data.status == "OK") {
                                                            document.querySelector('.model').classList.remove('model-open')
                                                            document.getElementById('btn-showModel').style = "display:none;"
                                                            document.getElementById('login_fb').style = "display:none;"
                                                            document.querySelector('.user').style = "display:block;"
                                                            document.querySelector('.user-name').textContent = response.name;
                                                            document.querySelector('.user-name').setAttribute("id") = response.id;
                                                        } else {
                                                            
                                                        }
                                                    }
                                                }
                                                xhr.send(form);
                                            }
                                        },
                                        error: function () {
                                            console.log("Load api get thất bại");
                                        }
                                    });
                                } else {
                                    console.log("Email đã được dùng");
                                    alert('Lỗi')
                                }
                            },
                            error: function () {
                                console.log("Load api get thất bại");
                            }
                        });
                    }
                    else {
                        var form = new FormData();
                        form.append("username", response.email);
                        var xhr = new XMLHttpRequest();
                        xhr.open("POST", 'https://localhost:44314/MemBer/CheckLoginFb', true);
                        xhr.Lineout = 30000;
                        xhr.onreadystatechange = function () {
                            if (xhr.readyState == 4 && xhr.status == 200) {
                                var r = xhr.responseText;
                                var j = JSON.parse(r);
                                if (j.Data.status == "OK") {
                                    document.querySelector('.model').classList.remove('model-open')
                                    document.getElementById('btn-showModel').style = "display:none;"
                                    document.getElementById('login_fb').style = "display:none;"
                                    document.querySelector('.user').style = "display:block;"
                                    document.querySelector('.user-name').textContent = response.name;
                                    document.querySelector('.user-name').setAttribute("id") = response.id;
                                } else {
                                }
                            }
                        }
                        xhr.send(form)
                    }
                },
                error: function (res) {
                    console.log('load api thất bại')
                }
            })
        }
    );
}