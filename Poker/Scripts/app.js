
function validateCards(children) {
    for (var i = 0; i < children.length; ++i) {
        if (!$(children[i]).val())
            return false;
    }
    return true;
}

function getCards(children) {
    var cards = [];
    for (var i = 0; i < children.length; ++i) {
        cards.push($(children[i]).val());
    }
    return cards;
}

function submit() {
    // Simple data validation to make sure that all fields have been entered.
    if (!$("#p1").val() || !$("#p2").val() || $("#p1").val() == $("#p2").val()) {
        $('#winner').text("All players must have unique names.");
        return;
    }
    if (!validateCards($("#p1cards input")) || !validateCards($("#p2cards input"))) {
        $('#winner').text("All cards must be entered.");
        return;
    }

    var input = {};
    input["hands"] = [];
    var p1 = $('#p1').val();
    var hand1 = getCards($("#p1cards input"));
    var p2 = $('#p2').val();
    var hand2 = getCards($("#p2cards input"));
    input["hands"] = [{ "name": p1, "hand": hand1 }, { "name": p2, "hand": hand2 }];
    $.ajax({
        url: '/api/Poker',
        type: "POST",
        data: input,
        dataType: 'json',
        success: function (data) {
            $('#winner').text(data);
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            $('#winner').text("Call failed with status: " + err.Message);
        }
    });
}