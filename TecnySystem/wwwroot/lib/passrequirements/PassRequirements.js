if (typeof jQuery === 'undefined') {
    throw new Error('PassRequirements requires jQuery')
}

+(function ($) {

    $.fn.PassRequirements = function (options) {

        /*
         * TODO
         * ====
         * 
         * store regexes in variables so they can be used by users through string 
         * specifications,ex 'number', 'special', etc
         * 
         */

        var defaults = {
            //            defaults: true
        };

        if (
            !options ||                     //if no options are passed                                  /*
            options.defaults == true ||     //if default option is passed with defaults set to true      * Extend options with default ones
            options.defaults == undefined   //if options are passed but defaults is not passed           */
        ) {
            if (!options) {                   //if no options are passed, 
                options = {};               //create an options object
            }
            defaults.rules = $.extend({
                minlength: {
                    text: "Use al menos minLength caracteres",
                    minLength: 8,
                },
                containSpecialChars: {
                    text: "Use al menos minLength caracter especial </br> por ejemplo <span class='negrita'>! .  , ; : $ @ < > _</span>",
                    minLength: 1,
                    //regex: new RegExp('([^!,%,&,@,#,$,^,*,?,_,~,/,.,-,|,°,\,:,{,}])', 'g')
                    //regex: new RegExp('([^!,&,@,#,$,*,_,~,/,.,-,:,,,;,&,+,=,<,>,~])', 'g')
                    regex: new RegExp('([^!,$,@,_,.,:,,,;,<,>])', 'g')
                },
                containLowercase: {
                    text: "Use al menos minLength caracter en min\u00FAsculas",
                    minLength: 1,
                    regex: new RegExp('[^a-z,\u00F1,\u00C1,\u00E1,\u00C9,\u00E9,\u00CD,\u00ED,\u00D3,\u00F3,\u00DA,\u00FA,\u00D1]', 'g')
                },
                containUppercase: {
                    text: "Use al menos minLength caracter en may\u00FAscula",
                    minLength: 1,
                    regex: new RegExp('[^A-Z]', 'g')
                },
                containNumbers: {
                    text: "Use al menos minLength n\u00FAmero",
                    minLength: 1,
                    regex: new RegExp('[^0-9]', 'g')
                }
            },

                options.rules);
            ////AGREGADO POR DANIEL
            //document.getElementById("btnCambiarClave").style.display = "none";

        } else {
            defaults = options;     //if options are passed with defaults === false
            //document.getElementById("btnCambiarClave").style.display = "display";
        }


        var i = 0;

        return this.each(function () {

            if (!defaults.defaults && !defaults.rules) {
                console.error('You must pass in your rules if defaults is set to false. Skipping this input with id:[' + this.id + '] with class:[' + this.classList + ']');
                return false;

                //console.log('error');
            }

            var requirementList = "";
            $(this).data('pass-req-id', i++);



            $(this).keyup(function () {
                var this_ = $(this);

                Object.getOwnPropertyNames(defaults.rules).forEach(function (val, idx, array) {
                    if (this_.val().replace(defaults.rules[val].regex, "").length > defaults.rules[val].minLength - 1) {
                        //this_.next('.popover').find('#' + val).css('color', 'red');
                        $(".popover").find("li#" + val).css("color", "#218f8b");
                        $(".popover").find(".icono-" + val).css("display", "inline-block");
                        //var hola = "Holadaniel NO funciono"



                    } else {
                        //this_.next('.popover').find('#' + val).css('color', 'blue');
                        $(".popover").find("li#" + val).css("color", "#98002e");
                        $(".popover").find(".icono-" + val).css("display", "none");
                        //var hola = "Holadaniel NO funciono";

                    }

                })

            });




            Object.getOwnPropertyNames(defaults.rules).forEach(function (val, idx, array) {
                requirementList += (("<li  id='" + val + "'><i id='icono-validar' class='fas fa-check-circle px-1 icono-" + val + "'></i>" + defaults.rules[val].text).replace("minLength", defaults.rules[val].minLength));


            });




            try {
                $(this).popover({
                    title: 'Seguridad de la Contrase\u00F1a',
                    trigger: options.trigger ? options.trigger : 'focus',
                    html: true,
                    placement: options.popoverPlacement ? options.popoverPlacement : 'bottom',
                    content: 'Requerimientos:<ul >' + requirementList + '</ul>',
                    //                        '<p>The confirm field is actived only if all criteria are met</p>'
                });

            } catch (e) {
                throw new Error('PassRequirements requires Bootstraps Popover plugin');

            }
            $(this).focus(function () {
                $(this).keyup();
            });
        });
    };

}(jQuery));
