
function clickIE4(){
	if (event.button==2){
	return false;
	}
}

function clickNS4(e){
	if (document.layers||document.getElementById&&!document.all){
		if (e.which==2||e.which==3){
			return false;
		}
	}
}

function AllTrim(strOrg) 
{
    var strt = 0, end = strOrg.length - 1, i = 0;
    var tmpstr;

    tmpstr = new String(strOrg);
    while ((tmpstr.charAt(strt) == ' ') && (strt < tmpstr.length)) 
      ++strt;
    while ((tmpstr.charAt(end) == ' ') && (end >= 0)) 
      --end;
    if (strt <= end)
      return (tmpstr.substring(strt, end + 1));
    else
      return "";
}

if (document.layers){
	document.captureEvents(Event.MOUSEDOWN);
	document.onmousedown=clickNS4;
}
else if (document.all&&!document.getElementById){
	document.onmousedown=clickIE4;
}

document.oncontextmenu=new Function("return false")

function checkCtrl(){
  if (event.ctrlKey) return false;
}
document.onkeydown = checkCtrl;
