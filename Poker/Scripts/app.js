var ViewModel = function () {
    var self = this;
    self.availableCards = ko.observableArray();
    self.error = ko.observable();
    self.Play = {
        Name: ko.observable(),
        Hand: ko.observableArray()
    };
    self.GroupPlay = {
        Plays: ko.observableArray()
    };
    self.player1Name = ko.observable();
    self.player2Name = ko.observable();

    self.submit = function () {
        alert(self.player1Name());
    }

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            dataType: 'json',
            url: uri,
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function load () {
        ajaxHelper('/api/Deck', 'GET').done(function (xml) {
            var cards = [];
            $(xml).children('Card').each( function (i, elem) {
                cards.push($(elem).text());
            });
            self.availableCards(cards);
        });
        
    }

    load();
    self.availableCards.valueHasMutated;
};
$(document).ready(function () {
    ko.applyBindings(new ViewModel());
});

/*function validateCards(children) {
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
}*/