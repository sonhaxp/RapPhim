// Show Phim
$(document).ready(function () {
    GetListPhim();
    const modalTheater = document.querySelector('.content_list-item-theater')
    const modalRequestTheater = document.querySelector('.select-theater')
    let selectFilmsActived = document.getElementsByClassName("js-select-film active-color");
    $(document).on("click", ".js-select-film", function () { 
        if (selectFilmsActived.length != 0) {
            selectFilmsActived[0].classList.remove('active-color');
        }
        id = $(this).attr("id");
        GetListRap(id);
        modalTheater.classList.add('open')
        modalRequestTheater.classList.add('hide')
        $(this).addClass('active-color');
        if (selectTheaterActived.length!=0)
            selectTheaterActived[0].classList.remove('active-color')
        modalTime.classList.remove('open')
        modalRequestTime.classList.remove('hide')
    });
    $(document).on("click", ".js-select-theater", function () {
        marap = $(this).attr("id");
        maphim = document.getElementsByClassName('js-select-film active-color')[0].id;
        GetListSuat(marap,maphim)
        selectTheaterActived = document.getElementsByClassName('js-select-theater active-color')
        modalTime.classList.add('open')
        modalRequestTime.classList.add('hide')
        if (selectTheaterActived.length != 0) {
            selectTheaterActived[0].classList.remove('active-color')
        }
        $(this).addClass('active-color')
    });
    $(document).on("click", ".showtimes-list_item", function () {
        var id = $(this).attr('id');
        var xhr = new XMLHttpRequest();
        xhr.open("POST", '/MemBer/CheckSession', true);
        xhr.Lineout = 30000;
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var r = xhr.responseText;
                var j = JSON.parse(r);
                if (j.Data.status == "OK") {
                    window.location = "/chon-ve?id=" + id;
                } else {
                    document.getElementById('btn-showModel').click();
                }
                IsRunning = false;
            }
        }
        xhr.send();
    });
    let selectTheaterActived = document.getElementsByClassName('js-select-theater active-color')
    const modalTime = document.querySelector('.content_list-group-time')
    const modalRequestTime = document.querySelector('.select-time')
});
GetListPhim = function () {
    $.ajax({
        url: "/all-film",
        method: "GET",
        contentType: "json",
        crossDomain: true,
        dataType: 'json',
        success: function (res) {
            var length = res.length;
            var tableHtml = '';
            for (var i = 0; i < length; i++) {
                tableHtml = tableHtml + '<li class="content-item js-select-film " id = "'+res[i].MaPhim+'">';
                tableHtml = tableHtml + '<a href="javascript:;" class="content-link content__link-film c'+res[i].Tuoi+' ">';
                tableHtml = tableHtml + '<img src="https://localhost:44314/Content/img/' + res[i].Anh + '" alt="ảnh phim ' + res[i].GhiChu +'" class="content-link-img">';
                tableHtml = tableHtml + '<div class="content-link-info ">';
                tableHtml = tableHtml + '<span class="content-link-info_name ">' + res[i].TenPhim + '</span>';
                tableHtml = tableHtml + '<span class="content-link-info_description">' + res[i].GhiChu + '</span >';
                tableHtml = tableHtml + '</div>';
                tableHtml = tableHtml + '</a>';
                tableHtml = tableHtml + '</li>';
            }
            $(".content_list-item").html(tableHtml);
            $(".content_list-group-time").html('');
        },
        error: function () {
            console.log("Load api get thất bại");
        }
    });
}
GetListRap = function (id) {
    $.ajax({
        url: "/api/Rap?id=" + id,
        method: "GET",
        contentType: "json",
        crossDomain: true,
        dataType: 'json',
        success: function (res) {
            var length = res.length;
            var tableHtml = '';
            for (var i = 0; i < length; i++) {
                tableHtml = tableHtml + '<li class="content-item js-select-theater" id = "' + res[i].MaRap + '">';
                tableHtml = tableHtml + '<a href="javascript:;" class="content-link">';
                tableHtml = tableHtml + '<p class="content-link-address">' + res[i].TenRap + '</p>';
                tableHtml = tableHtml + '</a>';
                tableHtml = tableHtml + '</li>';
            }
            $(".content_list-item-theater").html(tableHtml);
            $(".content_list-group-time").html('');
        },
        error: function () {
            console.log("Load api get thất bại");
        }
    });
}
GetListSuat = function (marap, maphim) {
    $.ajax({
        url: "/api/suatchieu?marap="+marap+"&maphim=" + maphim,
        method: "GET",
        contentType: "json",
        crossDomain: true,
        dataType: 'json',
        success: function (res) {
            var length = res.length;
            const ngay = [];
            ngay[0] = res[0].ThoiGianChieu;
            var count = 0;
            const suat1ngay = [];
            suat1ngay[0] = 1;
            for (var i = 1; i < length; i++) {
                if (ngay[count] != res[i].ThoiGianChieu) { 
                    count++;
                    suat1ngay[count] = 1;
                    ngay[count] = res[i].ThoiGianChieu;
                }
                else {
                    suat1ngay[count]++;
                }
            }
            var tableHtml = '';
            var index = 0;
            var day = ['Chủ nhật', 'Thứ hai', 'Thứ ba', 'Thứ tư', 'Thứ năm', 'Thứ sáu', 'Thứ bảy']
            for (var i = 0; i < ngay.length; i++) {
                tableHtml = tableHtml + '<li class="content_list-group-item">';
                tableHtml = tableHtml + '<div class="showtimes-row">';
                a = new Date(ngay[i]);
                tableHtml = tableHtml + '<p class="ng-binding">' + day[a.getDay()] + ', ' + dateFormat(ngay[i]) + '</p>';
                tableHtml = tableHtml + '<div class="showtimes-time">';
                tableHtml = tableHtml + '<p class="showtimes-time-subtitles">2D-Phụ đề</p>';
                tableHtml = tableHtml + '<div class="showtimes-list-wrapper">';
                for (var j = 0; j < suat1ngay[i]; j++) {
                    tableHtml = tableHtml + '<a class="showtimes-list_item" id="' + res[index].MaSuatChieu + '">' + res[index].GioChieu + '</a>';
                    index++;
                }
                tableHtml = tableHtml + '</div>';
                tableHtml = tableHtml + '</div>';
                tableHtml = tableHtml + '</div>';
                tableHtml = tableHtml + '</li>';
            }
            $(".content_list-group-time").html(tableHtml);
        },
        error: function () {
            console.log("Load api get thất bại");
        }
    });
}
dateFormat = function (str) {
    var s = str.split('T')
    var t = s[0].split('-')
    var output = t[2] + '/' + t[1] + '/' + t[0];
    return output
}
