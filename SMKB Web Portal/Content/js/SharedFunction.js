

function to2Decimal(val) { 
    try {
        return parseFloat(val).toFixed(2)
    } catch (e) {
        return 0.00
    }
}


function readInt(val) {
    try {
        const parsedValue = parseInt(val);
        console.log(parsedValue)
        if (!isNaN(parsedValue)) {
            return parsedValue;
        } else {
            return 0;
        }
    } catch (error) {
        console.log('err' + error)
        return 0;
    }
}


function toSqlDateString(date) {
    let dd = date.getDate();
    let mm = date.getMonth() + 1; // January is 0!
    let yyyy = date.getFullYear();

    if (dd < 10) {
        dd = '0' + dd;
    }

    if (mm < 10) {
        mm = '0' + mm;
    }

    return yyyy + '-' + mm + '-' + dd;
}

function formatPrice(val) { 
    if (typeof val === "string" && val instanceof String === false) {
        val = val.replace(/,/g, "")
    }
    if (!val || isNaN(val)) {
        return '0.00'
    }
    let formatted = to2Decimal(val).toString()  
    let strprice = formatted.split('.')
    let ringgit = strprice[0]
    if (ringgit.length > 3) {
        formatted = ""
        let last = ringgit.length - 1
        let start = last - 2
        while (start > 0) {
            formatted = ringgit.substring(start, start + 3) + ',' + formatted 
            start = start - 3
        }
        formatted = ringgit.substring(0, (3 + start)) + ',' + formatted
        if (formatted.charAt(formatted.length - 1) == ',') {

            formatted = formatted.substring(0, formatted.length - 1)
        }
    }
    else {
        formatted = ringgit
    }
    return formatted + '.' + strprice[1]
}

function formatPriceKeyUp(input) {
    let elem = $(input)[0]
    let tmps = elem.selectionStart
    let tmpe = elem.selectionEnd
    let tmpval = $(input).val().split('.')[0]
    let formated = formatPrice($(input).val())
    $(input).val(formated)
    let front = formated.split('.')[0]
    if (tmps == tmpe) {
        if (tmpval.length < front.length) {
            tmps += 1
        }
        if (tmpval.length > front.length) {
            tmps -= 1
        }
        if (tmps < 0) {
            tmps = 0
        }
        elem.selectionStart = tmps
        elem.selectionEnd = tmps

    }
}

function formattedPriceToFloat(val) {
    if (typeof val === "string" && val instanceof String === false) {
        val = val.replace(/,/g, "")
    }
    if (!val || isNaN(val)) {
        return  0.0
    }
    try {
        val = parseFloat(val)
        return val
    } catch (e) {
        console.log('format price to float parse error')
        return 0.0
    }
}

function dateStrFromSQl(dateString) {
    if (dateString == "" || !dateString) {
        return ''
    }
    var dateComponents = dateString.split("T")[0].split("-");
    var year = dateComponents[0];
    var month = dateComponents[1];
    var day = dateComponents[2];

    return formattedDate = year + "-" + month + "-" + day;
}


function todayString() {
    // en-CA format yyyy-mm-dd
    // hour cycle , jam terakhir =23,  supaya 12 am= jam 24 malam mula hari baharu
    return new Date().toLocaleDateString('en-CA', { hourCycle: 'h23' } ).replace(',', '') 
}

//pass classname to initialize only group of date picker with the class name
function initializeAllDatePickerToday(classname) {
    let today = todayString() 
    if (classname) {
        try {
            $('.' + classname).val(today)

        } catch (e) {
            console.log('date init',e)
        }
        return
    }
    $('input[type="date"]').val(today)
}


function roundToTwoDecimalPlaces(val) {
    if (typeof val === "string" && val instanceof String === false) {
        val = val.replace(/,/g, "")
    }
    if (!val || isNaN(val)) {
        return  0.00
    }
    try {
        val =  Math.round(val * 100) / 100;
        return val
    } catch (e) {
        console.log('float round error')
        return 0.0
    } 
}


function toNearest5Cents(val){
    if (typeof val === "string" && val instanceof String === false) {
        val = val.replace(/,/g, "")
    }
    if (!val || isNaN(val)) {
        return  0.00
    }
    try { 
        let lastdec = Math.floor(val * 100) % 10
        if(lastdec >2 && lastdec < 8){
            val =  Math.floor(val * 100) - lastdec + 5 //remove last number replace with 5
            val = val/100 // move 2 decimal place
        }
        else{
            // else round the last decimal only 
            val =  Math.round(val * 10) / 10; 

        }
        return val
    } catch (e) {
        console.log('float round error')
        return 0.0
    } 
    
}