/* Funcion para mostrar u ocultar paswword */
function mostrarPassword() {

    var cambio = document.getElementById("txtPassword");
    if (cambio.className == "form__field pw") {
        cambio.className = "form__field pwnone ";
        $('.icon-mostrar').removeClass('fa fa-eye-slash').addClass('fa fa-eye color-charcoal');
    } else {
        cambio.className = "form__field pw";
        $('.icon-mostrar').removeClass('fa fa-eye').addClass('fa fa-eye-slash color-charcoal');
    }
}

function mostrarPassNew() {

    var cambio = document.getElementById("txtNewPassword");
    if (cambio.className == "form__field pw ") {
        cambio.className = "form__field pwnone";
        $('.icon-pass-new').removeClass('fa fa-eye-slash').addClass('fa fa-eye color-charcoal');
    } else {
        cambio.className = "form__field pw ";
        $('.icon-pass-new').removeClass('fa fa-eye').addClass('fa fa-eye-slash color-charcoal');
    }
}

function mostrarConfPassNew() {

    var cambio = document.getElementById("txtConfirmNewPassword");
    if (cambio.className == "form__field pw col-sm-11") {
        cambio.className = "form__field pwnone";
        $('.icon-conf-pass-new').removeClass('fa fa-eye-slash').addClass('fa fa-eye color-white');
    } else {
        cambio.className = "form__field pw ";
        $('.icon-conf-pass-new').removeClass('fa fa-eye').addClass('fa fa-eye-slash color-white');
    }
}


function mostrarPassAnt() {

    var cambio = document.getElementById("txtPasswordAnterior");
    if (cambio.className == "form__field pw") {
        cambio.className = "form__field pwnone";
        $('.icon-contraseña-pass').removeClass('fa fa-eye-slash').addClass('fa fa-eye color-charcoal');
    } else {
        cambio.className = "form__field pw";
        $('.icon-contraseña-pass').removeClass('fa fa-eye').addClass('fa fa-eye-slash color-charcoal');
    }
}

function mostrarPassNew2() {

    var cambio = document.getElementById("txtNewPassword2");
    if (cambio.className == "form__field pw") {
        cambio.className = "form__field pwnone";
        $('.icon-contraseña-pass-new').removeClass('fa fa-eye-slash').addClass('fa fa-eye color-charcoal');
    } else {
        cambio.className = "form__field pw";
        $('.icon-contraseña-pass-new').removeClass('fa fa-eye').addClass('fa fa-eye-slash color-charcoal');
    }
}



