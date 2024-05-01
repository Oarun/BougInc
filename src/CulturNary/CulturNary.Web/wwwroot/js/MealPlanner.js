$(document).ready(function () {

    $('[id^=heading]').on("click", function () {
        var id = $(this).attr('id').replace('heading', '');
        $('[id^=collapse]').hide();
        $("#collapse" + id).show();
    });

    $(document).on('click', '.res-foods, .res-recipes', function (event) {
        event.preventDefault();
        var $this = $(this);
        if (!$this.hasClass("active")) {
            $('.res-foods, .res-recipes').removeClass('active');
            $this.addClass('active');
            if ($this.hasClass('res-foods')) {
                $('.foods-area').css('display', 'inline-table');
                $('.recipes-area').css('display', 'none');
            } else {
                $('.recipes-area').css('display', 'inline-table');
                $('.foods-area').css('display', 'none');
            }
        }
        return false;
    });

    function listAllItems() {
        console.log('listAllItems');
        for (i = 0; i < localStorage.length; i++) {
            key = localStorage.key(i);
            console.log(key);

            var obj = localStorage.getItem(key);
            console.log(obj);
            var resobj = obj.split("|");
            console.log(resobj);

            if (resobj.length == 2) {
                var finalkey = resobj[0].split("=");
                var finalvalue = resobj[1].split("=");
                var objKey = finalkey[1].replaceAll('"', '');
                var objValue = finalvalue[1].replaceAll('"', '');

                var typeKey = key.substring(0, key.indexOf("["));
                var typeValue = key.substring(key.indexOf("["));
                typeValue = typeValue.replace('[', '');
                typeValue = typeValue.replace(']', '');

                console.log(finalkey[1] + '-' + finalvalue[1]);
                console.log(typeKey + ':' + typeValue + '/' + objKey + '-' + objValue);

                if (typeKey == 'diet') {
                    $(".diet-check input[type='checkbox'][value='" + objValue.toLowerCase().replace(/_/g, '-') + "']").prop('checked', true);
                    $('.all-meals').append('<span class="right-filter dietValue" data-nutrient="' + typeValue + '">' + objKey + '<span class="close_control"></span></span>');
                } else if (typeKey == 'health') {
                    $(".health-check input[type='checkbox'][value='" + objValue.toLowerCase().replace(/_/g, '-') + "']").prop('checked', true);
                    $('.all-meals').append('<span class="right-filter healthValue" data-nutrient="' + typeValue + '">' + objKey + '<span class="close_control"></span></span>');
                } else if (typeKey == 'meal') {
                    $(".health-check-meal input[type='checkbox'][value='" + typeValue + "']").prop('checked', true);
                    $('.meals').append('<span class="right-filter healthValueMeal" data-nutrient="' + typeValue + '">' + objKey + '<span class="close_control"></span></span>');
                } else if (typeKey == 'nutrients') {
                    var minVal = '';
                    var maxVal = '';

                    var rangeVal = objValue;
                    rangeVal = rangeVal.replace('μ', '');
                    rangeVal = rangeVal.replace('m', '');
                    rangeVal = rangeVal.replace('g', '');

                    if ((rangeVal.indexOf('-') != 0) && (rangeVal.indexOf('-') != -1)) {
                        var parts1 = rangeVal.split('-', 2);
                        minVal = parts1[0];
                        maxVal = parts1[1];
                    }

                    if ((rangeVal.indexOf('+') != 0) && (rangeVal.indexOf('+') != -1)) {
                        var parts2 = rangeVal.split('+');
                        minVal = parts2[0];
                    }

                    if (rangeVal.match(/^-?\d+$/)) {
                        maxVal = rangeVal;
                    }

                    $(".nutr input[type='checkbox'][value='" + typeValue + "']").prop('checked', true);
                    $('.all-meals').append('<span class="right-filter nutrientsValue" data-nutrient="' + typeValue + '" data-min="' + minVal + '" data-max="' + maxVal + '" data-range="' + rangeVal + '">' + objKey + ' - ' + objValue + '<span class="close_control"></span></span>');
                } else if (typeKey == 'calories') {
                    var minVal = '';
                    var maxVal = '';

                    var rangeVal = objValue;
                    rangeVal = rangeVal.replace('kcal', '');

                    if ((rangeVal.indexOf('-') != 0) && (rangeVal.indexOf('-') != -1)) {
                        var parts1 = rangeVal.split('-', 2);
                        minVal = parts1[0];
                        maxVal = parts1[1];
                    }

                    if ((rangeVal.indexOf('+') != 0) && (rangeVal.indexOf('+') != -1)) {
                        var parts2 = rangeVal.split('+');
                        minVal = parts2[0];
                    }

                    if (rangeVal.match(/^-?\d+$/)) {
                        maxVal = rangeVal;
                    }
                    $(".cal-check input[type='checkbox']").prop('checked', true);
                    $('.all-meals').append('<span class="right-filter caloriesValue" data-nutrient="' + typeValue + '" data-min="' + minVal + '" data-max="' + maxVal + '" data-range="' + rangeVal + '">' + objValue + '<span class="close_control"></span></span>');
                } else if (typeKey == 'breakfast') {
                    $(".dish-type-br input[type='checkbox'][value='" + typeValue + "']").prop('checked', true);
                    $('.breakfast').append('<span class="right-filter dishTypeBr" data-nutrient="' + typeValue + '">' + objKey + '<span class="close_control"></span></span>');
                } else if (typeKey == 'lunch') {
                    $(".dish-type-ln input[type='checkbox'][value='" + typeValue + "']").prop('checked', true);
                    $('.lunch').append('<span class="right-filter dishTypeLn" data-nutrient="' + typeValue + '">' + objKey + '<span class="close_control"></span></span>');
                } else if (typeKey == 'dinner') {
                    $(".dish-type-di input[type='checkbox'][value='" + typeValue + "']").prop('checked', true);
                    $('.dinner').append('<span class="right-filter dishTypeDi" data-nutrient="' + typeValue + '">' + objKey + '<span class="close_control"></span></span>');
                }
                $('#heading2, #heading3, #heading4, #heading5, #heading6, #heading7, #heading8').css('display', 'block');
                $('.breakfast, .lunch, .dinner, .analyze-demo').removeClass('d-none');
                $('.step-1, .step-2').addClass('d-none');
            }
        }
    }

    function checkFilters() {
        console.log('checkFilters');

        $(".nutr input[type='checkbox'][value=PROCNT]").prop('checked', true);
        var min = $('.procnt').find('.min').val();
        console.log(min);
        var range = min + ' ';
        console.log(range);
        $('.all-meals').append('<span class="right-filter nutrientsValue" data-nutrient="PROCNT" data-min="' + min + '" data-max data-range="' + range + '">Protein - ' + range + 'g<span class="close_control"></span></span>');
        localStorage.setItem('nutrients[PROCNT]', 'key="Protein"|value="' + range + 'g"');

        $(".cal-check input[type='checkbox']").prop('checked', true);
        var min = $('.mdesc-cal').find('.min').val();
        var max = $('.mdesc-cal').find('.max').val();
        var range = min + '-' + max;
        $('.all-meals').append('<span class="right-filter caloriesValue" data-nutrient="calories" data-min="' + min + '" data-max="' + max + '" data-range="' + range + '">' + range + 'kcal<span class="close_control"></span></span>');
        localStorage.setItem('calories[calories]', 'key="calories"|value="' + range + 'kcal"');

        $(".health-check-meal input[type='checkbox']").prop('checked', true);
        $('.health-check-meal .checkbox-button__input').each(function () {
            $('.meals').append('<span class="right-filter healthValueMeal" data-nutrient="' + this.value + '">' + $(this).closest('.checkbox-button').find('.checkbox-button__label').text() + '<span class="close_control"></span></span>');
            localStorage.setItem('meal[' + this.value + ']', 'key="' + $(this).closest('.checkbox-button').find('.checkbox-button__label').text() + '"|value="' + this.value + '"');
        });

    }

    if (localStorage.getItem("load") === null) {
        console.log('local storage found', localStorage);
        listAllItems();
    } else {
        checkFilters();
    }

    $('#collapse1').on('show.bs.collapse', function () {
        $('#heading1').css('display', 'none');
    })
    $('#collapse2').on('show.bs.collapse', function () {
        $('#heading2').css('display', 'none');
    })
    $('#collapse3').on('show.bs.collapse', function () {
        $('#heading3').css('display', 'none');
    })
    $('#collapse4').on('show.bs.collapse', function () {
        $('#heading4').css('display', 'none');
    })
    $('#collapse5').on('show.bs.collapse', function () {
        $('#heading5').css('display', 'none');
    })
    $('#collapse6').on('show.bs.collapse', function () {
        $('#heading6').css('display', 'none');
    })
    $('#collapse7').on('show.bs.collapse', function () {
        $('#heading7').css('display', 'none');
    })
    $('#collapse8').on('show.bs.collapse', function () {
        $('#heading8').css('display', 'none');
    })

    function addToHeader() {

        var searchQuery = $('.search.q').val(),
            healtHTML = '',
            healtStatus = '',
            dietHTML = '',
            dietStatus = '',
            calQuery = $('.search.cal').val(),
            nutrHTML = '',
            nutrStatus = '',
            nutrValueMin = '',
            nutrValueMax = '';

        //----------- Search -----------------
        if (searchQuery == '') {
            $('#heading1').find('.inp').html('').css('display', 'none');
            console.log('searchQuery empty')
        } else {
            $('#heading1').find('.inp').html('').html(searchQuery).css('display', 'block');
            console.log('searchQuery not empty')
        }

        //----------- Healt ------------------
        $('input.healt:checked').each(function () {
            healtStatus = $(this).parent('.checkbox-button').find('.checkbox-button__label').html();
            if (healtStatus != '') {
                healtHTML += healtStatus + ' <span>' + $(this).parent('.checkbox-button').find('.mdesc').html() + '</span></br>';
            }
        });
        if (healtHTML == '') {
            $('#heading2').find('.inp').html('').css('display', 'none');
        } else {
            $('#heading2').find('.inp').html('').html(healtHTML).css('display', 'block');
        }

        //----------- Diet -------------------
        $('input.diet:checked').each(function () {
            dietStatus = $(this).parent('.checkbox-button').find('.checkbox-button__label').html();
            if (dietStatus != '') {
                dietHTML += dietStatus + ' <span>' + $(this).parent('.checkbox-button').find('.mdesc').html() + '</span></br>';
            }
        });
        if (dietHTML == '') {
            $('#heading3').find('.inp').html('').css('display', 'none');
        } else {
            $('#heading3').find('.inp').html('').html(dietHTML).css('display', 'block');
        }
        //----------- Calories ---------------
        if (calQuery == '') {
            $('#heading4').find('.inp').html('').css('display', 'none');
        } else {
            $('#heading4').find('.inp').html('').html(calQuery + ' kcal per 100g').css('display', 'block');
        }
        //----------- Nutrients --------------
        $('input.nutrient:checked').each(function () {
            nutrStatus = $(this).parent('.checkbox-button').find('.checkbox-button__label').html();
            nutrValueMin = $(this).parent('.checkbox-button').find('.min').val();
            nutrValueMax = $(this).parent('.checkbox-button').find('.max').val();
            if (nutrStatus != '') {
                if ((nutrValueMin != '') && (nutrValueMax != '')) {
                    //nutrHTML += nutrStatus+' '+nutrValueMin+' '+$(this).parent('.checkbox-button').find('.mdesc').text()+'</br>';
                    nutrHTML += nutrStatus + ' / min <b>' + nutrValueMin + '</b> and max <b>' + nutrValueMax + '</b> quantity per serving</br>';
                } else if ((nutrValueMin == '') && (nutrValueMax != '')) {
                    nutrHTML += nutrStatus + ' / max <b>' + nutrValueMax + '</b> quantity per serving</br>';
                } else if ((nutrValueMin != '') && (nutrValueMax == '')) {
                    nutrHTML += nutrStatus + ' / min <b>' + nutrValueMin + '</b> quantity per serving</br>';
                } else if ((nutrValueMin == '') && (nutrValueMax == '')) {
                    nutrHTML += nutrStatus + '</br>';
                }
            }
        });
        if (nutrHTML == '') {
            $('#heading5').find('.inp').html('').css('display', 'none');
        } else {
            $('#heading5').find('.inp').html('').html(nutrHTML).css('display', 'block');
        }
    }

    function collapseMenu(selector) {
        $('#heading' + selector).css('display', 'block');
        $('#collapse' + selector).removeClass('in');
        addToHeader();
    }

    $(document).on('click', '.back-to', function () {
        $(".result-area").fadeOut(300, function () {
            $(".loading-area").fadeIn(200, function () {
                $('.loading-area').css('display', 'none');
                $('.content-area').css('display', 'block');
                $('.begin-area').removeClass('d-none');
                $('#collapse1').css('display', 'block');
                $('#heading1').find('.btn-link').addClass('active');
            });
        });
    });

    $('.search-back').on('click', function () {
        $(".noresult-area").fadeOut(300, function () {
            $(".loading-area").fadeIn(200, function () {
                $('.loading-area').css('display', 'none');
                $('.content-area').css('display', 'block');
            });
        });
    });

    $(".nutrient").change(function () {
        if (this.checked) {
            $(this).parent('.checkbox-button').addClass('checked');
        } else {
            $(this).parent('.checkbox-button').removeClass('checked');
            $(this).parent('.checkbox-button').find('.min').val('');
            $(this).parent('.checkbox-button').find('.max').val('');
        }
    });

    function foodDBCall(ingrData) {
        var cal, pro, fat, carbs, quantity, mlabel, flabel, brand, img;
        var errMsg = '';
        var result = '';

        $('.loading-area').css('display', 'none');

        $('#collapse1, #collapse2, #collapse3, #collapse4, #collapse5, #collapse6, #collapse7, #collapse8').css('display', 'none');
        $('.search-row').css('display', 'block');
        $('.content-area').css('display', 'block');

        $('.result-area').css('display', 'block');
        $('.begin-area').addClass('d-none');

    }

    function GoToSearch() {

        $('html, body').animate({ scrollTop: 0 }, 'fast');
        console.log('GoToSearch')

        $('.btn-link').each(function () {
            $(this).removeClass('active');
            console.log("Button class removed")
        });

        var q =
            healt = '',
            diet = '',
            calories = '',
            breakfast = '',
            lunch = '',
            dinner = '',
            dishBr = '',
            dishLn = '',
            dishDi = '',
            jsonConst = '',
            jsonBrekf = '',
            jsonLunch = '',
            jsonDinner = '',
            jsonHealth = '',
            nutrients = '';
        console.log(q)

        var breakfastMin = 10,
            breakfastMax = 30;

        var lunchMin = 30,
            lunchMax = 45;

        var dinnerMin = 20,
            dinnerMax = 45;

        var caloriesBrekfast = '',
            caloriesLunch = '',
            caloriesDinner = '';

        var nutrientsBrekfast = '',
            nutrientsLunch = '',
            nutrientsDinner = '';

        var brekfastADD = "", lunchADD = "", dinnerADD = "";

        var cal, pro, fat, carbs, quantity, mlabel, flabel, brand, img, title;

        var output = '';

        $('.healthValue').each(function () {
            healt += ' "' + $(this).data('nutrient') + '",';
            console.log(healt)
        });
        if (healt != '') {
            healt = '{"health": [' + healt.slice(0, -1) + ']},';
            console.log(healt)
        }

        $('.dietValue').each(function () {
            diet += ' "' + $(this).data('nutrient') + '",';
            console.log(diet)
        });
        if (diet != '') {
            diet = '{"diet": [' + diet.slice(0, -1) + ']},';
            console.log(diet)
        }

        if ((healt != '') || (diet != '')) {
            jsonHealth = healt +
                diet;
            jsonHealth = jsonHealth.slice(0, -1);
            console.log(jsonHealth.slice(0, -1))
        }

        $('.caloriesValue').each(function () {
            var min = $(this).data('min');
            var max = $(this).data('max');
            console.log(min)
            console.log(max)

            if ((min != '') && (max != '')) {
                calories = '"ENERC_KCAL": {"min": ' + min + ', "max": ' + max + '},';
                caloriesBrekfast = '"ENERC_KCAL": {"min": ' + (min * breakfastMin) / 100 + ', "max": ' + (max * breakfastMax) / 100 + '},';
                caloriesLunch = '"ENERC_KCAL": {"min": ' + (min * lunchMin) / 100 + ', "max": ' + (max * lunchMax) / 100 + '},';
                caloriesDinner = '"ENERC_KCAL": {"min": ' + (min * dinnerMin) / 100 + ', "max": ' + (max * dinnerMax) / 100 + '},';
                console.log(calories)
                console.log(caloriesBrekfast)
                console.log(caloriesLunch)
                console.log(caloriesDinner)
            } else if ((min == '') && (max != '')) {
                calories = '"ENERC_KCAL": {"max": ' + max + '},';
                caloriesBrekfast = '"ENERC_KCAL": {"max": ' + (max * breakfastMax) / 100 + '},';
                caloriesLunch = '"ENERC_KCAL": {"max": ' + (max * lunchMax) / 100 + '},';
                caloriesDinner = '"ENERC_KCAL": {"max": ' + (max * dinnerMax) / 100 + '},';
                console.log(calories)
                console.log(caloriesBrekfast)
                console.log(caloriesLunch)
                console.log(caloriesDinner)
            } else if ((min != '') && (max == '')) {
                calories = '"ENERC_KCAL": {"min": ' + min + '},';
                caloriesBrekfast = '"ENERC_KCAL": {"min": ' + (min * breakfastMin) / 100 + '},';
                caloriesLunch = '"ENERC_KCAL": {"min": ' + (min * lunchMin) / 100 + '},';
                caloriesDinner = '"ENERC_KCAL": {"min": ' + (min * dinnerMin) / 100 + '},';
                console.log(calories)
                console.log(caloriesBrekfast)
                console.log(caloriesLunch)
                console.log(caloriesDinner)
            }
        });

        $('.nutrientsValue').each(function () {
            var min = $(this).data('min');
            var max = $(this).data('max');
            var name = $(this).data('nutrient');
            console.log(name)

            if ((min != '') && (max != '')) {
                nutrients += '"' + name + '": {"min": ' + min + ', "max": ' + max + '},';
                //nutrientsBrekfast   += '"'+name+'": {"min": '+(min*breakfastMin)/100+', "max": '+(max*breakfastMax)/100+'},';
                //nutrientsLunch      += '"'+name+'": {"min": '+(min*lunchMin)/100+', "max": '+(max*lunchMax)/100+'},';
                //nutrientsDinner     += '"'+name+'": {"min": '+(min*dinnerMin)/100+', "max": '+(max*dinnerMax)/100+'},';        
            } else if ((min == '') && (max != '')) {
                nutrients += '"' + name + '": {"max": ' + max + '},';
                //nutrientsBrekfast   += '"'+name+'": {"max": '+(max*breakfastMax)/100+'},';
                //nutrientsLunch      += '"'+name+'": {"max": '+(max*lunchMax)/100+'},';
                //nutrientsDinner     += '"'+name+'": {"max": '+(max*dinnerMax)/100+'},';        
            } else if ((min != '') && (max == '')) {
                nutrients += '"' + name + '": {"min": ' + min + '},';
                //nutrientsBrekfast   += '"'+name+'": {"min": '+(min*breakfastMin)/100+'},';
                //nutrientsLunch      += '"'+name+'": {"min": '+(min*lunchMin)/100+'},';
                //nutrientsDinner     += '"'+name+'": {"min": '+(min*dinnerMin)/100+'},';        
            }
        });

        if ((calories != '') || (nutrients != '')) {
            jsonConst = calories +
                nutrients;
            jsonConst = jsonConst.slice(0, -1); //махане на последната запетайка
        }

        if ((caloriesBrekfast != '') || (nutrientsBrekfast != '')) {
            brekfastADD = caloriesBrekfast;
            //brekfastADD = caloriesBrekfast+
            //              nutrientsBrekfast;                  
            brekfastADD = brekfastADD.slice(0, -1); //махане на последната запетайка
        }

        if ((caloriesLunch != '') || (nutrientsLunch != '')) {
            lunchADD = caloriesLunch;
            //lunchADD = caloriesLunch+
            //           nutrientsLunch;
            lunchADD = lunchADD.slice(0, -1); //махане на последната запетайка
        }

        if ((caloriesDinner != '') || (nutrientsDinner != '')) {
            dinnerADD = caloriesDinner;
            //dinnerADD = caloriesDinner+
            //           nutrientsDinner;
            dinnerADD = dinnerADD.slice(0, -1); //махане на последната запетайка
        }


        $('.healthValueMeal').each(function () {
            if ($(this).data('nutrient') == 'Breakfast') {
                breakfast = '{"meal": [ "breakfast" ]}';
                jsonBrekf = '"Breakfast": {' +
                    '   "accept": {' +
                    '       "all": [' +
                    '           ' + breakfast + '' +
                    '       ]' +
                    '   },' +
                    '   "fit": {' +
                    '       ' + brekfastADD + '' +
                    '   }' +
                    '},';
                console.log(jsonBrekf)

            } else if ($(this).data('nutrient') == 'Lunch') {
                lunch = '{"meal": [ "lunch/dinner" ]}';
                jsonLunch = '"Lunch": {' +
                    '   "accept": {' +
                    '       "all": [' +
                    '           ' + lunch + '' +
                    '       ]' +
                    '   },' +
                    '   "fit": {' +
                    '       ' + lunchADD + '' +
                    '   }' +
                    '},';
                    console.log(jsonLunch)
            } else if ($(this).data('nutrient') == 'Dinner') {
                dinner = '{"meal": [ "lunch/dinner" ]}';
                jsonDinner = '"Dinner": {' +
                    '   "accept": {' +
                    '       "all": [' +
                    '          ' + dinner + '' +
                    '       ]' +
                    '   },' +
                    '   "fit": {' +
                    '       ' + dinnerADD + '' +
                    '   }' +
                    '},';
                console.log(jsonDinner)
            }
        });

        $('.dishTypeBr').each(function () {
            dishBr += '"' + $(this).data('nutrient') + '",';
        });
        if (dishBr != '') {
            breakfast = '{"dish": [' + dishBr.slice(0, -1) + ']},';
            breakfast += '{"meal": [ "breakfast" ]}';
            jsonBrekf = '"Breakfast": {' +
                '   "accept": {' +
                '       "all": [' +
                '           ' + breakfast + '' +
                '       ]' +
                '   },' +
                '   "fit": {' +
                '       ' + brekfastADD + '' +
                '   }' +
                '},';
            console.log(jsonBrekf)
        }

        $('.dishTypeLn').each(function () {
            dishLn += '"' + $(this).data('nutrient') + '",';
        });
        if (dishLn != '') {
            lunch = '{"dish": [' + dishLn.slice(0, -1) + ']},';
            lunch += '{"meal": [ "lunch/dinner" ]}';
            jsonLunch = '"Lunch": {' +
                '   "accept": {' +
                '       "all": [' +
                '           ' + lunch + '' +
                '       ]' +
                '   },' +
                '   "fit": {' +
                '       ' + lunchADD + '' +
                '   }' +
                '},';
        }

        $('.dishTypeDi').each(function () {
            dishDi += '"' + $(this).data('nutrient') + '",';
        });
        if (dishDi != '') {
            dinner = '{"dish": [' + dishDi.slice(0, -1) + ']},';
            dinner += '{"meal": [ "lunch/dinner" ]}';
            jsonDinner = '"Dinner": {' +
                '   "accept": {' +
                '       "all": [' +
                '          ' + dinner + '' +
                '       ]' +
                '   },' +
                '   "fit": {' +
                '       ' + dinnerADD + '' +
                '   }' +
                '},';
        }

        var jsonMealType = jsonBrekf + jsonLunch + jsonDinner;
        console.log(jsonMealType)
        jsonMealType = jsonMealType.slice(0, -1); //махане на последната запетайка
        console.log(jsonMealType)

        var jsonMeal = '{' +
            '    "size": 7,' +
            '    "plan": {' +
            '        "accept": {' +
            '            "all": [' +
            '               ' + jsonHealth + '' +
            '            ]' +
            '        },' +
            '        "fit": {' +
            '            ' + jsonConst + '' +
            '        },' +
            '        "sections": {' +
            '            ' + jsonMealType + '' +
            '        }' +
            '    }' +
            '}';
        var jsonString = JSON.stringify(jsonMeal, null, 2);
        console.log(jsonString);

        $('.content-area').css('display', 'none');
        console.log('content-area none')
        $('.loading-area').css('display', 'block');
        console.log("loading-area block")

        $(".result-area").html('');

        $(document).on('click', '#openJSON', function () {
            $('#jsonModal').find('.page-title').text('JSON request');
            $('#jsonModal').find('.modal-body').html('');
            $('#jsonModal').find('.modal-body').html('<textarea class="form-control" type="textarea" id="jsonArea" rows="12" wrap="hard">' + JSON.parse(JSON.stringify(jsonMeal, null, 2)) + '</textarea>');
            $('#jsonModal').modal('show');
        });

        function getRecipe(param) {
            var cal, pro, fat, carbs, img, ingrd, ingr, srv;

            var jsonData = JSON.stringify(jsonMeal);
            console.log(jsonData);

            $.ajax({
                url: 'https://api.edamam.com/api/recipes/v2/' + param + '?type=public&app_id=469fc797&app_key=01c8217e070a97ba8180863e94daa8e6',    //recipe
                type: 'GET',
                headers: { "Edamam-Account-User": "edamam" },
                contentType: "application/json; charset=UTF-8",
                async: false,
                success: function (res) {
                    console.log(res);
                    if (typeof (res.recipe.images.SMALL.url) != "undefined") {
                        img = '<img src="' + res.recipe.images.SMALL.url + '">';
                        console.log(img)
                    } else { img = '' }

                    if (typeof (res.recipe.label) != "undefined") {
                        title = res.recipe.label;
                    } else { title = '-' }

                    if (typeof (res.recipe.yield) != "undefined") {
                        srv = Math.round(res.recipe.yield);
                    } else { srv = 0 }

                    if (typeof (res.recipe.dietLabels) != "undefined") {
                        dietLabels = String(res.recipe.dietLabels);
                        dietLabels = dietLabels.split(',').join(' &bull; ');
                        dietLabels = dietLabels.split('-').join(' ');
                    } else { dietLabels = '' }

                    if (typeof (res.recipe.healthLabels) != "undefined") {
                        healthLabels = String(res.recipe.healthLabels);
                        healthLabels = healthLabels.split(',').join(' &bull; ');
                        healthLabels = healthLabels.split('-').join(' ');
                    } else { healthLabels = '' }

                    if ((healthLabels != '') && (dietLabels != '')) {
                        labels = dietLabels + ' &bull; ' + healthLabels;
                    } else if ((healthLabels == '') && (dietLabels != '')) {
                        labels = dietLabels;
                    } else if ((healthLabels != '') && (dietLabels == '')) {
                        labels = healthLabels;
                    } else {
                        labels = '';
                    }

                    if (typeof (res.recipe.totalNutrients.ENERC_KCAL.quantity) != "undefined") {
                        cal = Math.floor(res.recipe.totalNutrients.ENERC_KCAL.quantity / srv);
                    } else { cal = '' }
                    if (typeof (res.recipe.totalNutrients.PROCNT.quantity) != "undefined") {
                        pro = Math.floor(res.recipe.totalNutrients.PROCNT.quantity / srv);
                    } else { pro = '' }
                    if (typeof (res.recipe.totalNutrients.FAT.quantity) != "undefined") {
                        fat = Math.floor(res.recipe.totalNutrients.FAT.quantity / srv);
                    } else { fat = '' }
                    if (typeof (res.recipe.totalNutrients.CHOCDF) != "undefined") {
                        carbs = Math.floor(res.recipe.totalNutrients.CHOCDF.quantity / srv);
                    } else { carbs = '' }
                    if (typeof (res.recipe.totalNutrients.CHOLE) != "undefined") {
                        CHOLE = Math.floor(res.recipe.totalNutrients.CHOLE.quantity / srv);
                    } else { CHOLE = '' }
                    if (typeof (res.recipe.totalNutrients.NA) != "undefined") {
                        NA = Math.floor(res.recipe.totalNutrients.NA.quantity / srv);
                    } else { NA = '' }
                    if (typeof (res.recipe.totalNutrients.CA) != "undefined") {
                        CA = Math.floor(res.recipe.totalNutrients.CA.quantity / srv);
                    } else { CA = '' }
                    if (typeof (res.recipe.totalNutrients.MG) != "undefined") {
                        MG = Math.floor(res.recipe.totalNutrients.MG.quantity / srv);
                    } else { MG = '' }
                    if (typeof (res.recipe.totalNutrients.K) != "undefined") {
                        K = Math.floor(res.recipe.totalNutrients.K.quantity / srv);
                    } else { K = '' }
                    if (typeof (res.recipe.totalNutrients.FE) != "undefined") {
                        FE = Math.floor(res.recipe.totalNutrients.FE.quantity / srv);
                    } else { FE = '' }

                    output = '<div class="nutr-block">' +
                        '	<div class="row">' +
                        '		<div class="col-md-12">' +
                        '			<div class="recipe-title">' + title + '</div>' +
                        '		</div>' +
                        '		<div class="col-md-6">' +
                        '			<div class="recipe-ico"></div>' +
                        '			<div><a href="' + res.recipe.shareAs + '" target="_new">' + img + '</a></div>' +
                        '		</div>' +
                        '   	<div class="col-md-6 text-dark">' +
                        '			<div class="serv">' + srv + ' servings</div>' +
                        '			<div class="daily-cal-area">' +
                        '				<div class="daily-cal">' + cal + '</div> kcal' +
                        '			</div>' +
                        '   		<ul class="nutr-left">' +
                        '   			<li class="protein">' +
                        '   				<span>Protein</span>' +
                        '					<span>' + pro + ' g</span>' +
                        '				</li>' +
                        '				<li class="fat">' +
                        '					<span>Fat</span>' +
                        '					<span>' + fat + ' g</span>' +
                        '				</li>' +
                        '				<li class="carbs">' +
                        '					<span>Carb</span>' +
                        '					<span>' + carbs + ' g</span>' +
                        '				</li>' +
                        '   		</ul>' +
                        '		</div>' +
                        '	</div>' +
                        '</div>';

                    return output;
                    console.log(output)
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        }

        $.ajax({
            url: 'https://api.edamam.com/api/meal-planner/v1/b26fb46d/select?app_id=b26fb46d&app_key=c887a724df8435d92a76f3ec8b3ba91a',       //meal-planner
            type: 'POST',
            data: jsonMeal,
            dataType: 'JSON',
            contentType: "application/json; charset=UTF-8",
            success: function (data) {

                //$('.openJSON').css('display', 'block');

                if (data.status == "OK") {
                    console.log(data)

                    var html = '<div class="foodresult"></div>' +
                        '<div class="table-res-recipe recipes-area row">' +
                        '	<div class="r-title text-center"><button type="button" class="btn btn-green px-5 ms-3 back-to position-absolute start-0">Back to Search</button><span>Your daily meal planner</span><a id="openJSON" class="btn btn-green px-5 ms-3 back-to position-absolute end-0">view JSON request</a></div>';
                    var end = "no";

                    $.each(data.selection, function (i) {
                        if (end == "no") {
                            html += '<div class="col-md-1"></div>';
                            html += '<div class="col-md-11">';
                            html += '   <div class="row">';
                            $.each(data.selection[i], function (key, valueObj) {
                                $.each(valueObj, function (k, obj) {
                                    if (k == "Breakfast") {
                                        html += '<div class="col-md-4">';
                                        html += '   <div class="key-title">' + k + '</div>';
                                        html += '</div>';
                                    }
                                });
                                $.each(valueObj, function (k, obj) {
                                    if (k == "Lunch") {
                                        html += '<div class="col-md-4">';
                                        html += '   <div class="key-title">' + k + '</div>';
                                        html += '</div>';
                                    }
                                });
                                $.each(valueObj, function (k, obj) {
                                    if (k == "Dinner") {
                                        html += '<div class="col-md-4">';
                                        html += '   <div class="key-title">' + k + '</div>';
                                        html += '</div>';
                                    }
                                });
                                end = "yes";
                            });
                            html += '   </div>';
                            html += '</div>';
                        }
                    });

                    $.each(data.selection, function (i) {

                        html += '<div class="col-md-1">';
                        html += '   <div class="day-title">Day ' + (i + 1) + '</div>';
                        html += '</div>';

                        $.each(data.selection[i], function (key, valueObj) {
                            html += '<div class="col-md-11">';
                            html += '   <div class="row">';

                            function printMeal(mealTitle, mealType) {
                                html += '<div class="col-md-4">';
                                if (typeof (mealType["assigned"]) != "undefined") {
                                    var param = mealType["assigned"].substr(mealType["assigned"].indexOf("#") + 1);
                                    getRecipe(param);
                                    html += output;
                                }
                                html += '</div>';
                            }

                            $.each(valueObj, function (k, obj) {
                                if (k == "Breakfast") {
                                    printMeal(k, obj);
                                }
                            });
                            $.each(valueObj, function (k, obj) {
                                if (k == "Lunch") {
                                    printMeal(k, obj);
                                }
                            });
                            $.each(valueObj, function (k, obj) {
                                if (k == "Dinner") {
                                    printMeal(k, obj);
                                }
                            });

                            html += '</div>';
                            html += '</div>';
                        });

                    });

                    html += '</div>';

                    $(".result-area").append(html);

                    $('#collapse1, #collapse2, #collapse3, #collapse4, #collapse5, #collapse6, #collapse7, #collapse8').css('display', 'none');
                    $('.search-row').css('display', 'block');
                    $('.content-area').css('display', 'block');

                } else {
                    // ERROR
                    $('#errorModal').find('.modal-body').html('');
                    $('#errorModal').find('.modal-body').html('<p>Ooops, nothing in our recipes database matches what you are searching for! Please try again.</p>');
                    $('#errorModal').modal('show');

                    $('#collapse1').css('display', 'block');
                    $('#collapse1').addClass('err');

                    $('#collapse2, #collapse3, #collapse4, #collapse5, #collapse6, #collapse7, #collapse8').css('display', 'none');

                    $('.content-area').css('display', 'block');
                    $(".result-area").css('display', 'none');

                    $(".loading-area").css('display', 'none');
                    return false;
                }

                foodDBCall('ingr=' + $('.searchValue').data('query') + healt + diet + calories + nutrients);
                console.log(foodDBCall('ingr=' + $('.searchValue').data('query') + healt + diet + calories + nutrients))

            },
            statusCode: {
                401: function () {

                },
                500: function () { //Bad request
                    // ERROR
                    $('#errorModal').find('.modal-body').html('');
                    $('#errorModal').find('.modal-body').html('<p style="text-align: center;">HTTP Status 500 – Internal Server Error</p><pre>' + JSON.parse(JSON.stringify(jsonMeal)) + '</pre>');
                    $('#errorModal').modal('show');

                    $('#collapse1').css('display', 'block');
                    $('#collapse1').addClass('err');

                    $('#collapse2, #collapse3, #collapse4, #collapse5, #collapse6, #collapse7, #collapse8').css('display', 'none');

                    $('.content-area').css('display', 'block');
                    $(".result-area").css('display', 'none');

                    $(".loading-area").css('display', 'none');
                    return false;
                }
            }
        });
    }

    $('.analyze-demo').on("click", function () {
        GoToSearch();
    });

    $('.btn-link').on("click", function () {
        $('.btn-link').each(function () {
            $(this).removeClass('active');
        });
        $(this).addClass('active');
        $('.result-area').css('display', 'none');
    });

    $(".range-slider").on("click", function (e) {
        e.preventDefault;
        return false;
    });

    $(".nutr input[type='checkbox']").change(function () {
        var val = this.value;
        if (!this.checked) {
            $('.nutrientsValue').each(function () {
                if ($(this).data('nutrient') == val) {
                    $(this).remove();
                    localStorage.removeItem('nutrients[' + val + ']');
                }
            });
        }
    });

    $(".nutr-check input[type='checkbox']").change(function () {
        if (this.checked) {
            var val = this.value;
            $('.mdesc').css('display', 'none');
            $('.right-col').addClass('bgr-row');
            $('.nutrientsValue').each(function () {
                if ($(this).data('nutrient') == val) {
                    $(this).remove();
                    localStorage.removeItem('nutrients[' + val + ']');
                }
            });

            var min = $(this).closest('.mdesc').find('.min').val();
            var max = $(this).closest('.mdesc').find('.max').val();
            var range = '';

            if ((min != '') && (max != '')) {
                range = min + '-' + max;
            } else if ((min == '') && (max != '')) {
                range = max;
            } else if ((min != '') && (max == '')) {
                range = min + '+';
            } else if ((min == '') && (max == '')) {
                //err
                $(".nutr input[type='checkbox']").each(function () {
                    if ($(this).val() == val) {
                        $(this).prop('checked', false);
                    }
                });
                return false;
            }
            localStorage.setItem('nutrients[' + this.value + ']', 'key="' + $(this).closest('.mdesc').prev('.checkbox-button__label').text() + '"|value="' + range + $(this).data('measure') + '"');
            $('.all-meals').append('<span class="right-filter nutrientsValue" data-nutrient="' + this.value + '" data-min="' + min + '" data-max="' + max + '" data-range="' + range + '">' + $(this).closest('.mdesc').prev('.checkbox-button__label').text() + ' - ' + range + $(this).data('measure') + '<span class="close_control"></span></span>');
            console.log('nutrients[' + this.value + ']');
        }

    $(".cal-check input[type='checkbox']").change(function () {
        if (this.checked) {
            var val = this.value;
            $('.mdesc').css('display', 'none');
            $('.right-col').addClass('bgr-row');
            $('.caloriesValue').each(function () {
                if ($(this).data('nutrient') == val) {
                    $(this).remove();
                    localStorage.removeItem('calories[' + this.value + ']');
                }
            });

            var min = $(this).closest('.mdesc-cal').find('.min').val();
            var max = $(this).closest('.mdesc-cal').find('.max').val();
            var range = '';

            if ((min != '') && (max != '')) {
                range = min + '-' + max;
            } else if ((min == '') && (max != '')) {
                range = max;
            } else if ((min != '') && (max == '')) {
                range = min + '+';
            } else if ((min == '') && (max == '')) {
                //err
                $(this).prop('checked', false);
                return false;
            }
            localStorage.setItem('calories[' + this.value + ']', 'key="' + this.value + '"|value="' + range + $(this).data('measure') + '"');
            $('.all-meals').append('<span class="right-filter caloriesValue" data-nutrient="' + this.value + '" data-min="' + min + '" data-max="' + max + '" data-range="' + range + '">' + range + $(this).data('measure') + '<span class="close_control"></span></span>');
        } else {
            $('.caloriesValue').each(function () {
                $(this).remove();
                localStorage.removeItem('calories[' + this.value + ']');
            });
        }
    });

    $(".health-check-meal input[type='checkbox']").change(function () {
        var val = this.value;
        if (this.checked) {
            $('.right-col').addClass('bgr-row');
            $('.healthValueMeal').each(function () {
                if ($(this).data('nutrient') == val) {
                    $(this).remove();
                    localStorage.removeItem('meal[' + this.value + ']');
                }
            });
            localStorage.setItem('meal[' + this.value + ']', 'key="' + $(this).closest('.checkbox-button').find('.checkbox-button__label').text() + '"|value="' + this.value + '"');
            $('.meals').append('<span class="right-filter healthValueMeal" data-nutrient="' + this.value + '">' + $(this).closest('.checkbox-button').find('.checkbox-button__label').text() + '<span class="close_control"></span></span>');
        } else {
            $('.healthValueMeal').each(function () {
                if ($(this).data('nutrient') == val) {
                    $(this).remove();
                }
            });
            localStorage.removeItem('meal[' + this.value + ']');
        }

    $(".diet-check input[type='checkbox']").change(function () {
        var val = this.value;
        if (this.checked) {
            $('.right-col').addClass('bgr-row');
            $('.dietValue').each(function () {
                if ($(this).data('nutrient') == val) {
                    $(this).remove();
                    localStorage.removeItem('diet[' + this.value.toUpperCase().replace(/-/g, '_') + ']');
                }
            });
            localStorage.setItem('diet[' + this.value.toUpperCase().replace(/-/g, '_') + ']', 'key="' + $(this).closest('.checkbox-button').find('.checkbox-button__label').text() + '"|value="' + this.value.toUpperCase().replace(/-/g, '_') + '"');
            $('.all-meals').append('<span class="right-filter dietValue" data-nutrient="' + this.value.toUpperCase().replace(/-/g, '_') + '">' + $(this).closest('.checkbox-button').find('.checkbox-button__label').text() + '<span class="close_control"></span></span>');
        } else {
            $('.dietValue').each(function () {
                if ($(this).data('nutrient') == val.toUpperCase().replace(/-/g, '_')) {
                    $(this).remove();
                }
            });
            localStorage.removeItem('diet[' + this.value.toUpperCase().replace(/-/g, '_') + ']');
        }
    });

    $(".health-check input[type='checkbox']").change(function () {
        var val = this.value;
        if (this.checked) {
            $('.right-col').addClass('bgr-row');
            $('.healthValue').each(function () {
                if ($(this).data('nutrient') == val) {
                    $(this).remove();
                    localStorage.removeItem('health[' + this.value.toUpperCase().replace(/-/g, '_') + ']');
                }
            });
            localStorage.setItem('health[' + this.value.toUpperCase().replace(/-/g, '_') + ']', 'key="' + $(this).closest('.checkbox-button').find('.checkbox-button__label').text() + '"|value="' + this.value.toUpperCase().replace(/-/g, '_') + '"');
            $('.all-meals').append('<span class="right-filter healthValue" data-nutrient="' + this.value.toUpperCase().replace(/-/g, '_') + '">' + $(this).closest('.checkbox-button').find('.checkbox-button__label').text() + '<span class="close_control"></span></span>');
        } else {
            $('.healthValue').each(function () {
                if ($(this).data('nutrient') == val.toUpperCase().replace(/-/g, '_')) {
                    $(this).remove();
                }
            });
            localStorage.removeItem('health[' + this.value.toUpperCase().replace(/-/g, '_') + ']');
        }
    });

    $(".dish-type-br input[type='checkbox']").change(function () {
        var val = this.value;
        if (this.checked) {
            $('.right-col').addClass('bgr-row');
            $('.dishTypeBr').each(function () {
                if ($(this).data('nutrient') == val) {
                    $(this).remove();
                    localStorage.removeItem('breakfast[' + this.value + ']');
                }
            });
            localStorage.setItem('breakfast[' + this.value + ']', 'key="' + $(this).closest('.checkbox-button').find('.checkbox-button__label').text() + '"|value="' + this.value + '"');
            $('.breakfast').append('<span class="right-filter dishTypeBr" data-nutrient="' + this.value + '">' + $(this).closest('.checkbox-button').find('.checkbox-button__label').text() + '<span class="close_control"></span></span>');
        } else {
            $('.dishTypeBr').each(function () {
                if ($(this).data('nutrient') == val) {
                    $(this).remove();
                }
            });
            localStorage.removeItem('breakfast[' + this.value + ']');
        }
    });

    $(".dish-type-ln input[type='checkbox']").change(function () {
        var val = this.value;
        if (this.checked) {
            $('.right-col').addClass('bgr-row');
            $('.dishTypeLn').each(function () {
                if ($(this).data('nutrient') == val) {
                    $(this).remove();
                    localStorage.removeItem('lunch[' + this.value + ']');
                }
            });
            localStorage.setItem('lunch[' + this.value + ']', 'key="' + $(this).closest('.checkbox-button').find('.checkbox-button__label').text() + '"|value="' + this.value + '"');
            $('.lunch').append('<span class="right-filter dishTypeLn" data-nutrient="' + this.value + '">' + $(this).closest('.checkbox-button').find('.checkbox-button__label').text() + '<span class="close_control"></span></span>');
        } else {
            $('.dishTypeLn').each(function () {
                if ($(this).data('nutrient') == val) {
                    $(this).remove();
                }
            });
            localStorage.removeItem('lunch[' + this.value + ']');
        }
    });

    $(".dish-type-di input[type='checkbox']").change(function () {
        var val = this.value;
        if (this.checked) {
            $('.right-col').addClass('bgr-row');
            $('.dishTypeDi').each(function () {
                if ($(this).data('nutrient') == val) {
                    $(this).remove();
                    localStorage.removeItem('dinner[' + this.value + ']');
                    console.log('remove' + this.value)
                }
            });
            localStorage.setItem('dinner[' + this.value + ']', 'key="' + $(this).closest('.checkbox-button').find('.checkbox-button__label').text() + '"|value="' + this.value + '"');
            $('.dinner').append('<span class="right-filter dishTypeDi" data-nutrient="' + this.value + '">' + $(this).closest('.checkbox-button').find('.checkbox-button__label').text() + '<span class="close_control"></span></span>');
            console.log()
        } else {
            $('.dishTypeDi').each(function () {
                if ($(this).data('nutrient') == val) {
                    $(this).remove(this.value);
                }
            });
            localStorage.removeItem('dinner[' + this.value + ']');
        }
    });

    $(document).on('click', '.close_control', function (event) {
        event.preventDefault();
        console.log('close')

        $(this).closest('.right-filter').remove();
        var val = $(this).closest('.right-filter').data('nutrient');

        if ($(this).closest('.right-filter').hasClass('dietValue')) {
            $(".diet-check input[type='checkbox']").each(function () {
                if ($(this).val().toUpperCase().replace(/-/g, '_') == val) {
                    $(this).prop('checked', false);
                }
            });
            localStorage.removeItem('diet[' + val + ']');

        } else if ($(this).closest('.right-filter').hasClass('healthValue')) {
            $(".health-check input[type='checkbox']").each(function () {
                if ($(this).val().toUpperCase().replace(/-/g, '_') == val) {
                    $(this).prop('checked', false);
                }
            });
            localStorage.removeItem('health[' + val + ']');

        } else if ($(this).closest('.right-filter').hasClass('healthValueMeal')) {
            $(".health-check-meal input[type='checkbox']").each(function () {
                if ($(this).val() == val) {
                    $(this).prop('checked', false);
                }
            });
            localStorage.removeItem('meal[' + val + ']');

        } else if ($(this).closest('.right-filter').hasClass('dishTypeBr')) {
            $(".dish-type-br input[type='checkbox']").each(function () {
                if ($(this).val() == val) {
                    $(this).prop('checked', false);
                }
            });
            localStorage.removeItem('breakfast[' + val + ']');

        } else if ($(this).closest('.right-filter').hasClass('dishTypeLn')) {
            $(".dish-type-ln input[type='checkbox']").each(function () {
                if ($(this).val() == val) {
                    $(this).prop('checked', false);
                }
            });
            localStorage.removeItem('lunch[' + val + ']');

        } else if ($(this).closest('.right-filter').hasClass('dishTypeDi')) {
            $(".dish-type-di input[type='checkbox']").each(function () {
                if ($(this).val() == val) {
                    $(this).prop('checked', false);
                }
            });
            localStorage.removeItem('dinner[' + val + ']');

        } else if ($(this).closest('.right-filter').hasClass('caloriesValue')) {
            $(".cal-check input[type='checkbox']").each(function () {
                if ($(this).val() == val) {
                    $(this).prop('checked', false);
                }
            });
            $(".mdesc-cal .search").val('');
            localStorage.removeItem('calories[' + val + ']');

        } else if ($(this).closest('.right-filter').hasClass('nutrientsValue')) {
            $(".nutr input[type='checkbox']").each(function () {
                if ($(this).val() == val) {
                    $(this).prop('checked', false);
                }
            });
    });

});