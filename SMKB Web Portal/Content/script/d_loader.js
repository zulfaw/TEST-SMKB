var _x0prntobj = document;
if (window.parent !== undefined && window.parent !== null) {
    _x0prntobj = window.parent.document;
}

function createOuterDiv() {
    var tmpCss = "position:fixed;top: 0%;left: 0%;  width: 100%;";
    tmpCss = tmpCss + "height: 100%;background-color: #FFFFFF;z-index: 1001;";
    tmpCss = tmpCss + "-moz-opacity: 10;opacity: .90;filter: alpha(opacity=90);-webkit-backface-visibility: hidden;";
    var outerDiv = createButton("div", "sessionContainer", '', '', tmpCss);
    return outerDiv;
}

function createButton(elem, id, text, funcName, styleText) {
    var tmpBtn = document.createElement(elem);
    tmpBtn.innerHTML = text;
    tmpBtn.id = id;
    tmpBtn.name = id;
    if (funcName != "") {
        tmpBtn.addEventListener("click", funcName);
    }

    if (styleText != undefined) {
        tmpBtn.style.cssText = styleText;
    }

    return tmpBtn;
}

function load_loader(msg) {
    if (msg === undefined || msg === null) {
        msg = "";
    }

    if (_x0prntobj.getElementById("pg-loader-container") !== undefined && _x0prntobj.getElementById("pg-loader-container") !== null) {
        _x0prntobj.getElementById("pg-loader-container").innerHTML = "";
    }
    var load_outer = document.createElement("div");
    load_outer.id = "pg-loader-container";
    var loader = document.createElement("div");
    var lbl = document.createElement("label");

    var logo = document.createElement("img");

    var logoHolder = document.createElement("div");
    logoHolder.style.cssText = "position:relative;margin-top:10px;margin-left:0px;";

    logo.src = "https://devmis.utem.edu.my/smkbnet/images/logoUtem.png";
    logo.style.cssText = "width:90px;height:90px;z-index:1003;";
    logo.style.cssText += "-webkit-animation: breathing 5s ease-out infinite normal;";
    logo.style.cssText += "animation: breathing 5s ease-out infinite normal;";

    logoHolder.appendChild(logo);

    loader.id = "pg-loader";
    loader.style.cssText = "";
    lbl.innerHTML = msg;
    lbl.id = "pg-loader-lbl";

    load_outer.style.cssText = "width:50%;position:fixed;top:35vh;font-weight:bolder;font-size:16px;left:25%;height:230px;z-index:1002;text-align:center;";
    load_outer.className = "animate-bottom";

    load_outer.appendChild(lbl);
    load_outer.appendChild(loader);
    load_outer.appendChild(logoHolder);
    //alert(calc_body());
    //load_outer.style.height = calc_body() + "px";
    //alert(load_outer.style.height);
    return load_outer;
}

function upd_loadertext(newMsg) {
    var pgloader = _x0prntobj.getElementById("pg-loader-lbl")
    if (pgloader !== null && pgloader !== undefined) {
        pgloader.innerHTML = newMsg;
    }
}

function show_loader(msg) {
    _x0prntobj = document;
    loadLoader(msg);
}

function show_loaderP(msg) {
    _x0prntobj = window.parent.document;
    loadLoader(msg);
}

function loadLoader(msg) {
    //GoTop();
    if (_x0prntobj.getElementById("sessionContainer") === undefined || _x0prntobj.getElementById("sessionContainer") === null) {
        _x0prntobj.body.className += " noscroll";
        _x0prntobj.body.appendChild(createOuterDiv());
        _x0prntobj.body.appendChild(load_loader(msg));
    } else {
        upd_loadertext(msg);
    }
}

function GoTop() {
    _x0prntobj.body.scrollTop = 0; // For Safari
    _x0prntobj.documentElement.scrollTop = 0;
}

function close_loader(fn) {
    var outerDiv = _x0prntobj.getElementById("sessionContainer");
    var pg_loader = _x0prntobj.getElementById("pg-loader-container");
    if (pg_loader !== undefined && pg_loader !== null && outerDiv !== undefined && outerDiv !== null) {
        setTimeout(function () {
            pg_loader.className = "animate-out";
        }, 300);

        setTimeout(function () {
            _x0prntobj.body.removeChild(outerDiv);
            _x0prntobj.body.removeChild(pg_loader);
            //alert(_x0prntobj.body.className);
            _x0prntobj.body.className = _x0prntobj.body.className.replace(/\bnoscroll\b/g, "");
            if (fn !== null && fn !== undefined) {
                fn();
            }
            //var matches = /\bnoscroll\b/g.exec(_x0prntobj.body.className);
            //alert(matches);
        }, 1000);
    }
}

function calc_body() {
    var body = _x0prntobj.body,
        html = _x0prntobj.documentElement;

    var height = Math.max(body.scrollHeight, body.offsetHeight,
        html.clientHeight, html.scrollHeight, html.offsetHeight);
    return height;
}
