window.onload = function () {
    var oret = 00;
    var minutat = 00;
    var sekondat = 00;
    var milisekondat = 00;

    var eoret = document.getElementById("oret");
    var eminutave = document.getElementById("minutat");
    var esekondave = document.getElementById("sekondat");
    var emilisekondave = document.getElementById("milisekondat");
    var fillo = document.getElementById("fillo");
    var ndalo = document.getElementById("ndalo");
    var rifillo = document.getElementById("rifillo");

    var intervali;

    fillo.onclick = function () {
        clearInterval(intervali);
        intervali = setInterval(startTimer, 0)
    }
    ndalo.onclick = function () {
        clearInterval(intervali);
    }
    rifillo.onclick = function () {
        clearInterval(intervali);
        oret = "00";
        minutat = "00";
        sekondat = "00";
        milisekondat = "00";

        eoret.innerHTML = oret;
        eminutave.innerHTML = minutat;
        esekondave.innerHTML = sekondat;
        emilisekondave.innerHTML = milisekondat;
    }

    function startTimer() {
        milisekondat++;

        if (milisekondat <= 9) {
            emilisekondave.innerHTML = "0" + milisekondat;
        }
        if (milisekondat > 9) {
            emilisekondave.innerHTML = milisekondat;
        }
        if (milisekondat > 90) {
            console.log("sekondat");
            sekondat++;
            esekondave.innerHTML = + sekondat;
            if (sekondat <= 9) {
                esekondave.innerHTML = "0" + sekondat;
            }
            milisekondat = 0;
            emilisekondave.innerHTML = + 0;
        }



        if (esekondave > 9) {
            emilisekondave.innerHTML = sekondat;
        }

        if (sekondat > 59) {
            console.log("minutat");
            minutat++;
            eminutave.innerHTML = + minutat;
            if (minutat <= 9) {
                eminutave.innerHTML = "0" + minutat;
            }
            sekondat = 0;
            esekondave.innerHTML = "0" + 0;
        }

        if (minutat > 59) {
            console.log("oret");
            oret++;
            eoret.innerHTML = + oret;
            if (oret <= 9) {
                eoret.innerHTML = "0" + oret;
            }
            minutat = 00;
            eminutave.innerHTML = "0" + 0;
        }
    }
}
