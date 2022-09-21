function loadGraph() {
    $(document).ready(function () {
        $.ajax({
            headers: { RequestVerificationToken: $("#RequestCsrfToken").val() },
            type: 'GET',
            dataType: "json",
            contentType: "application/json",
            url: '/E170961/Statistics/GetStats',
            success: function (result) {
                google.charts.load('current', {
                    'packages': ['corechart']
                });
                google.charts.setOnLoadCallback(function () {
                    drawChart(result);
                });
            }
        });

        function drawChart(result) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'helicopter');
            data.addColumn('number', 'value');
            var dataArray = [];
            $.each(result.value, function (i, obj) {
                dataArray.push([obj.key, obj.value]);
            });
            data.addRows(dataArray);
            var barchart_options = {
                title: 'Flights fill percentage',
                width: 600,
                height: 500,
                legend: 'none'

            };
            var barchart = new google.visualization.ColumnChart(document
                .getElementById('barchart_div'));
            barchart.draw(data, barchart_options);
        }
    });
}