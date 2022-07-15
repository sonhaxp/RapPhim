
// transaction
const information = document.getElementById('js-information')
const modalInformation = document.querySelector('.member-body-wrap')
const modalInformationBoder = document.querySelector('.tab-member__link--info')
const modalInformationPointer = document.querySelector('.tab-member__link--info')
// Hàm hiện modal
function showInformation(){
    modalInformation.classList.remove('hide')
    modalTransaction.classList.remove('open')   
    //
    modalTransactionBoder.classList.remove('tab-member-color-active')
    modalInformationBoder.classList.add('tab-member-color-active') 
    //
    modalInformationPointer.classList.remove('cursor-pointer')
    modalTransactionPointer.classList.add('cursor-pointer')
}
// xử lý hành vi click hiện
information.addEventListener('click',showInformation)

const transaction = document.getElementById('js-transaction')
const modalTransaction = document.querySelector('.modal')
const modalTransactionBoder = document.querySelector('.tab-member__link--trans')
const modalTransactionPointer = document.querySelector('.tab-member__link--trans')
// Hàm hiện modal
function showTransaction(){
    modalTransaction.classList.add('open')
    modalInformation.classList.add('hide')
    //
    modalTransactionBoder.classList.add('tab-member-color-active')
    modalInformationBoder.classList.remove('tab-member-color-active')
      //
    modalInformationPointer.classList.add('cursor-pointer')
    modalTransactionPointer.classList.remove('cursor-pointer')
    load_hd()
}
dateFormat = function (str) {
    var s = str.split('T')
    var t = s[0].split('-')
    var output = t[2] + '/' + t[1] + '/' + t[0];
    return output
}

// xử lý hành vi click hiện
transaction.addEventListener('click', showTransaction)
load_hd = function () {
    $.ajax({
        url: "/api/hoadon?makh=" + document.querySelector('.user-name').getAttribute("id") + "&datefrom=" + document.getElementById('date_from').value + "&dateto=" + document.getElementById('date_to').value,
        method: "GET",
        contentType: "json",
        crossDomain: true,
        dataType: 'json',
        success: function (res) {
            var table = ''
            for (var i = 0; i < res.length; i++){
                table = table + '<tr>';
                table = table + '<th>' + dateFormat(res[i].NgayLap) + '</th>';
                table = table + '<th>' + res[i].MaHoaDon + '</th>';
                table = table + '<th>' + res[i].Ghe + '</th>';
                table = table + '<th>' + res[i].Rap + '</th>';
                table = table + '<th>' + res[i].Phim + '</th>';
                table = table + '<th>' + res[i].SuatChieu + '</th>';
                table = table + '<th>' + res[i].DoAn + '</th>';
                table = table + '<th>' + res[i].TongTien + '</th>';
                table = table + '</tr>';
            }
            console.log(table)
            $("#data-table").html(table);
        },
        error: function () {
            console.log("Load api get thất bại");
        }
    });
}
Save_ThanhVien = function () {
    var a = $('#changepass__checkbox').is(':checked');
    if (a == false) {
        $.ajax({
            url: "/api/thanhvien?email=" + $('.input-member')[5].value + "&diachi=" + $('.input-member--enable')[0].value,
            method: "PUT",
            contentType: "json",
            crossDomain: true,
            dataType: 'json',
            success: function (res) {
                alert('Cập nhật thành công');
            },
            error: function () {
                console.log("Load api get thất bại");
            }
        });
    }
    else {
        if ((!/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/.test(document.getElementById('pass_older').value)
            && !/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/.test(document.getElementById('pass_new').value)
            && !/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/.test(document.getElementById('pass_new2').value))) {
            alert('Mật khẩu ít nhất 8 ký tự bao gồm số,chữ hoa và chữ thường')
        }
        else if (document.getElementById('pass_new').value != document.getElementById('pass_new2').value) {
            alert('Mật khẩu mới không khớp')
        }
        else {
            $.ajax({
                url: "/api/taikhoan?username=" + $('.input-member')[5].value + "&oldpassword=" + document.getElementById('pass_older').value + "&newpassword=" + document.getElementById('pass_new').value,
                method: "PUT",
                contentType: "json",
                crossDomain: true,
                dataType: 'json',
                success: function (res) {
                    if (res == false) {
                        alert('Mật khẩu cũ không đúng')
                    }
                    else {
                        $.ajax({
                            url: "/api/thanhvien?email=" + $('.input-member')[5].value + "&diachi=" + $('.input-member--enable')[0].value,
                            method: "PUT",
                            contentType: "json",
                            crossDomain: true,
                            dataType: 'json',
                            success: function (res) {
                                alert('Cập nhật thành công');
                            },
                            error: function () {
                                console.log("Load api get thất bại");
                            }
                        });
                    }
                },
                error: function () {
                    console.log("Load api get thất bại");
                }
            });
        }
    }
}


