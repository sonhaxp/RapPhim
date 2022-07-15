

//Modal
const btnNextTicket = document.querySelector('.js-btn-next-ticket')
const btnBackSeat = document.querySelector('.js-btn-back-seat')
const btnNextSeat = document.querySelector('.js-btn-next-seat')
const btnBackPayment = document.querySelector('.js-btn-back-payment')

const modalTicket = document.getElementById('modal-ticket')
const modalSeat = document.getElementById('modal-seat')
const modalPayment = document.getElementById('modal-payment')

const modalBtnSeat = document.querySelector('.ticket-btn-seat')
const modalBtnTicket = document.querySelector('.ticket-btn')

var inputTickets = document.querySelectorAll('.input-ticket-js')

//Show Seat
function showSeat() {
    let isValid = false
    for (let index = 0; index < inputTickets.length; index++) {
        if (inputTickets[index].value != '0') {
            isValid = true;
            break;
        }
    }
    if (!isValid) {
        alert('Vui lòng chọn vé')
    }
    else {
        modalSeat.classList.add('open')
        modalTicket.classList.add('hide')
        modalBtnSeat.classList.add('open')
        modalBtnTicket.classList.add('hide')
    }
}
btnNextTicket.addEventListener('click', showSeat)

//Show Ticket
function showTicket() {
    modalSeat.classList.remove('open')
    modalTicket.classList.remove('hide')
    modalBtnSeat.classList.remove('open')
    modalBtnTicket.classList.remove('hide')
}
btnBackSeat.addEventListener('click', showTicket)


//Show SeatBack
function showSeatBack() {
    modalSeat.classList.add('open')
    modalPayment.classList.remove('open')
    modalBtnSeat.classList.add('open')
}
btnBackPayment.addEventListener('click', showSeatBack)
var tongTien;
var doan123 = []
$.ajax({
        url: "/api/DoAn",
        method: "Get",
        contentType: "json",
        crossDomain: true,
        dataType: 'json',
    success: function (res) {
        doan123 = res
        },
        error: function () {
            console.log("Load api get thất bại");
        }
});
// Tính tiền mua vé và đồ ăn
function tinhTien() {
    var soVeNguoiLon = document.getElementById('ve_nguoilon').value
    var tongTienNguoiLon = soVeNguoiLon * 100000
    soluong_DA = []
    for (var i = 0; i < doan123.length; i++) {
        soluong_DA[i] = document.getElementById('sl_'+doan123[i].MaDoAn).value
    }
    //var cbFamily = document.getElementById('cb_family').value
    //var cb2big = document.getElementById('cb_2big').value
    //var svb1big = document.getElementById('cb_1big').value

    
    tongtien_DA = []
    var tongTienDoAn = 0
    for (var i = 0; i < doan123.length; i++) {
        tongtien_DA[i] = soluong_DA[i] * doan123[i].GiaBan
        tongTienDoAn += tongtien_DA[i]
    }
    //var tongTienFamily = cbFamily * 199000
    //var tongTien2big = cb2big * 92000
    //var tongTien1big = svb1big * 79000
    /*var tongTienDoAn = tongTienFamily + tongTien2big + tongTien1big*/
    

    tongTien = tongTienDoAn + tongTienNguoiLon
    document.getElementById('tongtien_nguoilon').innerHTML = tongTienNguoiLon.toLocaleString('vi', { style: 'currency', currency: 'VND' })
    document.getElementById('tongtien_ve').innerHTML = tongTienNguoiLon.toLocaleString('vi', { style: 'currency', currency: 'VND' })
    var Food_price_total = document.getElementsByClassName('food-price-total')
    for (var i = 0; i < Food_price_total.length; i++) {
        Food_price_total[i].innerHTML = tongtien_DA[i].toLocaleString('vi', { style: 'currency', currency: 'VND' })
    }
    //document.getElementById('tongtien_family').innerHTML = tongTienFamily.toLocaleString('vi', { style: 'currency', currency: 'VND' })
    //document.getElementById('tongtien_2big').innerHTML = tongTien2big.toLocaleString('vi', { style: 'currency', currency: 'VND' })
    //document.getElementById('tongtien_1big').innerHTML = tongTien1big.toLocaleString('vi', { style: 'currency', currency: 'VND' })
    document.getElementById('tongtien_doan').innerHTML = tongTienDoAn.toLocaleString('vi', { style: 'currency', currency: 'VND' })

    document.getElementById('tongtien').innerHTML = tongTien.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
}

// Combo Food
var elementCombo = document.querySelectorAll(".col__des-b")
var inputChange = document.querySelectorAll(".input-quantity-combo")
var comboAleart = document.querySelector('#list-combo')
var msg = ['', '', '']
var da = [0,0,0]

inputChange.forEach((elem, index) => {
    elem.addEventListener('change', (e) => {
        da[index] = Number.parseInt(elem.value);
        if (Number.parseInt(elem.value) == 1) {
            msg[index] = elementCombo[index].innerHTML + ', '
            comboAleart.innerHTML = msg.join('')
        }
        else if (Number.parseInt(elem.value) > 1) {
            msg[index] = `${elementCombo[index].innerHTML} (${elem.value}), `
            comboAleart.innerHTML = msg.join('')
        }
        else {
            msg[index] = ''
            comboAleart.innerHTML = msg.join('')
        }
    })
})



//lay so luong ve
var count = 0
let lastValue = new Array(inputTickets.length)
var countSeatDisplay = document.querySelector('.option-seat__heading>span')
lastValue.fill(0)
inputTickets.forEach((elem, index) => {
    elem.addEventListener('change', () => {
        let tmp = Number.parseInt(elem.value)
        count += tmp - lastValue[index]
        lastValue[index] = tmp
        countSeatDisplay.innerHTML = `0/${count}`
    })
})

// hien thi so ve


var btnSeats = document.querySelectorAll('.seat-row__number-item')
var showActiveSeat = document.getElementById('seatLog')
var countSeat = 0
var countrow = document.getElementsByClassName('seat-row').length
var countcolumn = document.getElementsByClassName('seat-row__number-item').length / countrow;
let seats = new Map()
var hang = [ "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" ];
btnSeats.forEach((elem, index) => {
    elem.addEventListener('click', (e) => {
        if (e.target.classList.contains('seat-active-color') || countSeat < count) {
            if (elem.classList.contains('seat-selected-color')) {
                alert('Ghế này đã có người đặt rổi! Vui lòng chọn ghế khác')
            }
            else if (!elem.classList.contains('seat-active-color')) {
                elem.classList.add('seat-active-color')
                seats.set(elem.getAttribute("id"), `${hang[countrow - Math.floor(index / countcolumn)]}${index % countcolumn + 1}, `)
                let msg = ''
                seats.forEach((elem) => {
                    msg += elem
                })
                showActiveSeat.innerHTML = msg
                countSeat++
                countSeatDisplay.innerHTML = `${countSeat}/${count}`
            }
            else {
                elem.classList.remove('seat-active-color')
                seats.delete(elem.getAttribute("id"))
                let msg = ''
                seats.forEach((elem) => {
                    msg += elem
                })
                showActiveSeat.innerHTML = msg
                countSeat--
                countSeatDisplay.innerHTML = `${countSeat}/${count}`
            }
        }
    })
})

//Thanh toan
function valid() {
    let btn = document.querySelector('.ticket-btn-pay')
    let option = document.getElementById('payment')
    let name = document.getElementById('name')
    let email = document.getElementById('email')
    let phone = document.getElementById('phoneNum')

    btn.onclick = function (e) {
        if (option.value === "") {
            alert('Vui lòng chọn hình thức thanh toán')
        }
        else if (name.value.length < 2 || /\d/g.test(name.value)) {
            alert('Tên không hợp lệ')
        }
        else if (!/^\w{2,}@([A-Za-z\d]+\.)+[A-Za-z\d]{2,3}$/.test(email.value)) {
            alert('email không hợp lệ')
        }
        else if (!/^0\d{9}$/.test(phone.value)) {
            alert('Số điện thoại không hợp lệ')
        }
        else {
            addHoaDon()
        }
    }
}

valid()

//Show Payment
function showPayment() {
    if (countSeat < count) {
        alert('Vui lòng chọn đủ ghế')
        return
    }
    modalSeat.classList.remove('open')
    modalPayment.classList.add('open')
    modalBtnSeat.classList.remove('open')
}
btnNextSeat.addEventListener('click', showPayment)
function showSeatSelected() {
    $.ajax({
        url: "/api/VeBan?masc=" + document.getElementsByClassName('buy-ticket_main')[0].getAttribute("id"),
        method: "Get",
        contentType: "json",
        crossDomain: true,
        dataType: 'json',
        success: function (res) {
            for (var i = 0; i < res.length; i++) {
                $('#' + res[i].MaGheNgoi).addClass('seat-selected-color')
            }
        },
        error: function () {
            console.log("Load api get thất bại");
        }
    });          
}
showSeatSelected()
function showMember() {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", '/Choose_Ticket/ShowMember', true);
    xhr.Lineout = 30000;
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            var r = xhr.responseText;
            var j = JSON.parse(r);
            if (j.Data.status == "OK") {
                $('#name').val(j.Data.name);
                $('#email').val(j.Data.email);
                $('#phoneNum').val(j.Data.sdt);
            } else {
                alert('Lỗi');
            }
        }
    }
    xhr.send();
}
showMember()
function addHoaDon() {

    var form = new FormData();
    var maghe = [];
    var ghengoi = [];
    seats.forEach((elem, id) => {
        maghe.push(id)
        ghengoi.push(elem)
    })
    form.append("maghe", maghe.toString());
    form.append("doan", da.toString());
    form.append("tongtien", tongTien);
    form.append("tendoan", msg.join(''));
    form.append("ghengoi", ghengoi);
    var xhr = new XMLHttpRequest();
    xhr.open("POST", '/Choose_Ticket/LapHD', true);
    xhr.Lineout = 30000;
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            var r = xhr.responseText;
            var j = JSON.parse(r);
            if (j.Data.status == "OK") {
                alert('Bạn đã mua vé thành công. Vui lòng vào trang thành viên để xem lại giao dịch');
                window.location='/trang-chu';
            } else if (j.Data.status == "F") {
                alert('Lỗi không xác định');
                window.location='/trang-chu';
            }
        }
    }
    xhr.send(form);
}