//Should be used directly in aspx file,not in separate file


// p = past; c = current;
function RunnerMain() {
    var textPass_p = "";
    var textFname_p = "";
    var textDob_p = "";
    var textPnum_p = "";
    var textEmail_p = "";
    var textCname_p = "";
    var textPcode_p = "";
    var textFaddr_p = "";
    var textUname_p = "";

    var currentProgress = 0;
    var passStrengthName = ["Too guessable", "Very guessable", "Weak", "Medium", "Strong"];
    setInterval(
        function () {
            var textFname_c = document.getElementById('<%=TextBox1.ClientID%>').value;
            var textDob_c = document.getElementById('<%=TextBox2.ClientID%>').value;
            var textPnum_c = document.getElementById('<%=TextBox3.ClientID%>').value;
            var textEmail_c = document.getElementById('<%=TextBox4.ClientID%>').value;
            var textCname_c = document.getElementById('<%=TextBox5.ClientID%>').value;
            var textPcode_c = document.getElementById('<%=TextBox6.ClientID%>').value;
            var textFaddr_c = document.getElementById('<%=TextBox7.ClientID%>').value;
            var textUname_c = document.getElementById('<%=TextBox8.ClientID%>').value;
            var textPass_c = document.getElementById('<%=TextBox9.ClientID%>').value;

            if (textFname_c != textFname_p || textDob_c != textDob_p || textPnum_c != textPnum_p || textEmail_c != textEmail_p || textCname_c != textCname_p || textPcode_c != textPcode_p || textFaddr_c != textFaddr_p || textUname_c != textUname_p || textPass_c != textPass_p) {
                if (textPass_c.length != 0) {
                    if (textPass_c.length <= 256) {

                        var string = zxcvbn(textPass_c, user_inputs = [textFname_c, textDob_c, textPnum_c, textEmail_c, textCname_c, textPcode_c, textFaddr_c, textUname_c, "library", "openlib", "Open-Lib", "Library Management System"]);

                        if (string.score == 0) {
                            currentProgress = 20;
                            $("#dynamic")
                                .css("width", currentProgress + "%")
                                .css("background-color", "#ff0000")
                                .attr("aria-valuenow", currentProgress)
                                .text(passStrengthName[string.score]);
                        }
                        if (string.score == 1) {
                            currentProgress = 40;
                            $("#dynamic")
                                .css("width", currentProgress + "%")
                                .css("background-color", "#f04747")
                                .attr("aria-valuenow", currentProgress)
                                .text(passStrengthName[string.score]);
                        }
                        if (string.score == 2) {
                            currentProgress = 60;
                            $("#dynamic")
                                .css("width", currentProgress + "%")
                                .css("background-color", "#ff6100")
                                .attr("aria-valuenow", currentProgress)
                                .text(passStrengthName[string.score]);
                        }
                        if (string.score == 3) {
                            currentProgress = 80;
                            $("#dynamic")
                                .css("width", currentProgress + "%")
                                .css("background-color", "#43b581")
                                .attr("aria-valuenow", currentProgress)
                                .text(passStrengthName[string.score]);
                        }
                        if (string.score == 4) {
                            currentProgress = 100;
                            $("#dynamic")
                                .css("width", currentProgress + "%")
                                .css("background-color", "green")
                                .attr("aria-valuenow", currentProgress)
                                .text(passStrengthName[string.score]);
                        }



                    }
                    else {
                        currentProgress = 50;
                        $("#dynamic")
                            .css("width", currentProgress + "%")
                            .attr("aria-valuenow", currentProgress)
                            .text("Pretty weak");

                    }

                }
                else {
                    currentProgress = 0;
                    $("#dynamic")
                        .css("width", currentProgress + "%")
                        .attr("aria-valuenow", currentProgress)
                }

            }

            textFname_p = textFname_c;
            textDob_p = textDob_c;
            textPnum_p = textPnum_c;
            textEmail_p = textEmail_c;
            textCname_p = textCname_c;
            textPcode_p = textPcode_c;
            textFaddr_p = textFaddr_c;
            textUname_p = textUname_c;
            textPass_p = textPass_c;
        }, 1000);

}

window.onload = function () {
    RunnerMain();
};

