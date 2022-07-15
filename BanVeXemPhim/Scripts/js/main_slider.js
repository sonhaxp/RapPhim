function Slider() {
    const container = document.getElementById('slider')
    const btnPrev = document.getElementById('btn-prev')
    const btnNext = document.getElementById('btn-next')
    const length = document.querySelectorAll('.slider_item').length
    const btnControl = document.querySelectorAll('.slider_item-control')
    let index = 1, lastindex = 1;

    function handleClick(isNext) {
        container.style.transition = 'transform 0.5s ease-out'
        if (isNext) {
            index++
        }
        else {
            index--
        }
        container.style.transform = `translateX(${-1 * index * container.clientWidth}px)`
        if (btnControl[lastindex - 1]) {
            btnControl[lastindex - 1].classList.remove('active')
        }
        if (btnControl[index - 1]) {
            btnControl[index - 1].classList.add('active')
        }
        lastindex = index
    }

    btnPrev.onclick = function (e) {
        handleClick(false)
    }

    btnNext.onclick = function (e) {
        handleClick(true)
    }

    container.addEventListener('transitionrun', (e) => {
        btnPrev.onclick = null
        btnNext.onclick = null
    })

    container.addEventListener('transitionend', () => {
        if (index <= 0) {
            container.style.transition = 'none'
            index = length - 2
            container.style.transform = `translateX(${-1 * index * container.clientWidth}px)`
            if (btnControl[lastindex - 1]) {
                btnControl[lastindex - 1].classList.remove('active')
            }
            if (btnControl[index - 1]) {
                btnControl[index - 1].classList.add('active')
            }
            lastindex = index
        }
        else if (index >= length - 1) {
            container.style.transition = 'none'
            index = 1
            container.style.transform = `translateX(${-1 * index * container.clientWidth}px)`
            if (btnControl[lastindex - 1]) {
                btnControl[lastindex - 1].classList.remove('active')
            }
            if (btnControl[index - 1]) {
                btnControl[index - 1].classList.add('active')
            }
            lastindex = index
        }
        btnNext.onclick = function (e) {
            handleClick(true)
        }

        btnPrev.onclick = function (e) {
            handleClick(false)
        }

    })

    btnControl.forEach((e, i) => {
        e.onclick = function (e) {
            container.style.transition = 'transform 0.5s ease-out'
            index = i + 1
            container.style.transform = `translateX(-${index * container.clientWidth}px)`
            btnControl[lastindex - 1].classList.remove('active')
            btnControl[index - 1].classList.add('active')
            lastindex = index
        }
    })

    setInterval(() => {
        handleClick(true)
    }, 5000);
}

function showFilm() {
    const btnCurrent = document.getElementById('current-film-btn')
    const btnCommingUp = document.getElementById('coming-up-film-btn')
    const containers = document.querySelectorAll('.film.section_content')
    const headers = document.querySelectorAll('.section_header-title')

    btnCurrent.onclick = function (e) {
        e.preventDefault()
        if (!containers[0].classList.contains('active')) {
            containers[0].classList.add('active')
            containers[1].classList.remove('active')
            headers[0].classList.add('active')
            headers[1].classList.remove('active')
        }
    }

    btnCommingUp.onclick = function (e) {
        if (!containers[1].classList.contains('active')) {
            containers[1].classList.add('active')
            containers[0].classList.remove('active')
            headers[1].classList.add('active')
            headers[0].classList.remove('active')
        }
    }
}
Slider()
showFilm()