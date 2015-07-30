var activeSection = "format";

$(document).ready(function () {
    $('nav').affix({
        offset: {
            top: $('header').height()
        }
    });

    $('nav').on('affix.bs.affix', function () {
        $('body').css('margin-top', $('nav').height());
    });

    $('nav').on('affix-top.bs.affix', function () {
        $('body').css('margin-top', 0);
    });
});

$(".format-btn").click(function () {
    if (activeSection != "format") {
        HideActiveSection();
        $("#id-format").fadeIn(300);
        activeSection = "format";
    }
})

$(".formula-btn").click(function () {
    if (activeSection != "formula") {
        HideActiveSection();
        $("#id-formula").fadeIn(300);
        activeSection = "formula";
    }
})

$(".generate-btn").click(function () {
    if (activeSection != "generate") {
        HideActiveSection();
        $("#id-generate").fadeIn(300);
        activeSection = "generate";
        $(".generate-id-number").click(function () {
            $.ajax({
                method: "GET",
                url: "http://localhost:57402/api/idnumbers"
            })
            .success(function (data, status, settings) {
                $(".generated-id-number").html("");
                $(".generated-id-number").html(data);
            })
            .error(function (request, status, error) {
                $(".generated-id-number").html("");
                $(".generated-id-number").html(request.responseText);
            });
        })
    }
})

$(".validate-btn").click(function () {
    if (activeSection != "validate") {
        HideActiveSection();
        $("#id-validate").fadeIn(300);
        activeSection = "validate";
        $(".validate-id-number").click(function () {
            var input = $(".input-id-number").val();
            if (input == "") {
                $(".id-validation-result").html("");
                $(".id-validation-result").html("ID Number is required");
                return;
            }
            $.ajax({
                method: "POST",
                url: "http://localhost:57402/api/idnumbers",
                data: { idnumber: input }
            })
            .success(function (data, status, settings) {
                $(".id-validation-result").html("");
                $(".id-validation-result").html("ID Number is valid");
            })
            .error(function (request, status, error) {
                $(".id-validation-result").html("");
                $(".id-validation-result").html("ID Number is not valid");
            });
        })
    }
})

$(".validate-id").click(function () {
    //alert($(".txt-validate-id").val());
    $.ajax({
        method: "POST",
        url: "http://localhost:57402/api/idnumbers",
        data: { idnumber: $("#txt-validate-id").val() }
    })
    .done(function (msg) {
        alert("Data Saved: " + msg);
    });
})

function HideActiveSection() {
    //alert(activeSection);
    switch (activeSection) {
        case "format":
            $("#id-format").fadeOut(300);
            break;
        case "formula":
            $("#id-formula").fadeOut(300);
            break;
        case "generate":
            $("#id-generate").fadeOut(300);
            break;
        case "validate":
            $("#id-validate").fadeOut(300);
            break;
        default:
            alert("activeSection not found...")
            break;
    }
}

function EnableButtons() {

}

