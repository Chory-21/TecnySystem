// oh man so much spaghetti code don't read it pls
const pinWrapper = document.querySelector('.pin-wrapper'),
    inputPin = pinWrapper.querySelector('.focus'),
    digits = Array.from(pinWrapper.querySelectorAll('.digit')),
    caret = pinWrapper.querySelector('.caret'),
    mainEl = document.querySelector('main')
let currentIndex = 0;
let prevLeft = 0;
let prevX = 0;
let inputLock = false;
let pinLock = false;

function moveCaret(newWidth, newLeft) {
    anime({
        targets: caret,
        width: newWidth,
        left: newLeft,
        easing: 'easeInOutQuad',
        duration: 450,
        begin: () => {
            caret.classList.remove('blink')
        },
        complete: () => {
            caret.classList.add('blink')
        }
    });
}

inputPin.oninput = e => {
    if (pinLock) return;

    let digit = parseInt(e.data);
    insertValueInput(digit);
};

function insertValueInput(digit) {
    this.value = "";
    if (!isNaN(digit) && !inputLock && currentIndex <= digits.length - 1) {
        // inputLock = true;
        let left, x, newWidth, newLeft;

        if (currentIndex < digits.length - 1) {
            x = digits[currentIndex + 1].offsetLeft;
            left = x + (digits[currentIndex + 1].offsetWidth - 4) / 2;

            newWidth = [
                { value: x - prevX + 5 },
                { value: 4 }
            ];
            newLeft = [
                { value: prevLeft },
                { value: left }
            ];
        }

        else if (currentIndex === digits.length - 1) {
            x = 100;
            left = pinWrapper.offsetWidth;
            newWidth = [
                { value: x },
                { value: 70 }
            ];
            newLeft = [
                { value: prevLeft },
                { value: left }
            ];
            x = pinWrapper.offsetWidth;
        }

        moveCaret(newWidth, newLeft);

        prevLeft = left;
        prevX = x;

        digits[currentIndex].dataset.digit = digit;
        digits[currentIndex].classList.add('shown');

        currentIndex++;
        // setTimeout(() => {
        //   inputLock = false;

        //   if (currentIndex === digits.length) {
        //     mainEl.classList.add('show-button');
        //   }
        // }, 450);

        if (currentIndex == 4) {
            var text = "";
            $("#validar-pin").prop("disabled", false);
            for (var i = 0; i < 4; i++) {
                text += digits[i].dataset.digit;
            }
            inputPin.value = text;
            $("#validar-pin").trigger("click");
        }

    }
}

function deleteValueInput() {
    inputLock = true;
    currentIndex--;
    if (currentIndex < 0) {
        currentIndex = 0;
        return;
    }

    let x = digits[currentIndex].offsetLeft;
    let left = x + (digits[currentIndex].offsetWidth - 4) / 2;

    let newWidth = [
        { value: prevX - x + 5 },
        { value: 4 }
    ],
        newLeft = [
            { value: left },
            { value: left }
        ];
    moveCaret(newWidth, newLeft);

    prevLeft = left;
    prevX = x;

    digits[currentIndex].classList.remove('shown');

    if (currentIndex < 4) {
        $("#validar-pin").prop("disabled", true)
    }

    //setTimeout(() => {
    digits[currentIndex].dataset.digit = "";
    inputLock = false;
    mainEl.classList.remove('show-button');
        //}, 450);
}

function clearInputPIN() {
    for (var i = 0; i < 4; i++) {
        deleteValueInput();
    }
}

inputPin.onkeydown = e => {

    if (pinLock) return;

    if (e.key === "Backspace" && !inputLock && currentIndex !== 0) {
        deleteValueInput();
    }
};

inputPin.onfocus = e => {
    if (pinLock) return;

    if (!currentIndex) {
        inputLock = true;
        let x = digits[0].offsetLeft;
        let left = x + (digits[currentIndex].offsetWidth - 4) / 2;

        let newWidth = [
            { value: left + 5 },
            { value: 4 }
        ],
            newLeft = [
                { value: 0 },
                { value: left }
            ];
        moveCaret(newWidth, newLeft);

        prevLeft = left;
        prevX = x;
        setTimeout(() => { inputLock = false; }, 450)
    }
};

inputPin.onblur = e => {
    if (pinLock) return;

    if (!currentIndex) {
        let q = digits[0].offsetLeft;
        let left = q + (digits[currentIndex].offsetWidth - 4) / 2;

        let newWidth = [
            { value: left + 5 },
            { value: 0 }
        ],
            newLeft = [
                { value: 0 },
                { value: 0 }
            ];
        moveCaret(newWidth, newLeft);
    }
};


inputPin.addEventListener('paste', (e) => {

    if (pinLock) return;

    e.preventDefault();

    let clipboardData = (e.originalEvent || e).clipboardData || window.clipboardData;
    let pastedData = clipboardData.getData('text/plain');

    for (var i = 0; i < 4; i++) {
        if (pastedData[i] == null) {
            break;
        }

        insertValueInput(pastedData[i])
    }
});