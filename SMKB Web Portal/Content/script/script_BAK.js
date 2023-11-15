// <-- PROFILE BUTTON -->

function menuToggle() {
    const toggleMenu = document.querySelector(".menu");
    toggleMenu.classList.toggle("active");
}

// <-- SIDE NAV ACTIVE SCRIPT -->

//var header = document.getElementById("myDIV");
//var btns = header.getElementsByClassName("btn");
//for (var i = 0; i < btns.length; i++) {
//    btns[i].addEventListener("click", function () {
//        var current = document.getElementsByClassName("active");
//        current[0].className = current[0].className.replace(" active", "");
//        this.className += " active";
//    });
//}

//   <-- TAB SCRIPT -->

function openTab(evt, cityName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
      tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
      tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
  }

  // < -- RESPONSIVE SIDENAV -- >

  function openNav() {
    document.getElementById("mySidenav").style.width = "270px";
  }
  
  function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
  }

  // <-- SEARCHABLE DROPDOWN -->
  $(document).ready(function () {
    $('select').selectize({
        sortField: 'text'
    });
});