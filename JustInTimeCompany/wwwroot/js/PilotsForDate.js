function loadPilots(value) {
    $.ajax({
        headers: { RequestVerificationToken: $("#RequestCsrfToken").val() },
        type: 'GET',
        dataType: "json",
        contentType: "application/json",
        url: '/E170961/Flights/GetPilotsFor?date=' + value,
        success: function (result) {
            console.log(result);
            $('#pilots').empty();
            result.value.forEach(pilot => {
                $('#pilots').append(`<option value="${pilot.id}">${pilot.name}</option>`);
            });
        }
    });
}

$('#selectedDate').change(function () {
    loadPilots(this.value);
}
);

$('#selectedDate').valueAsDate = new Date().toLocaleDateString();
