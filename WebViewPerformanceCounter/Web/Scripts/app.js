$(function () {
    var data = [], totalPoints = 100
    var i = 0;
    var plot;

    var PCounter = {
        getData: function () {
            $.getJSON('/api/PCounterWeb/GetData')
                .done(PCounter.onGetData)
                .fail(PCounter.onError);
        },
        onGetData: function (result) {

            $("#spanMessage").html("");

            $.each(result, function (index, val) {
                if (data.length != result.length) {
                    data.push({
                        serverName: val.Name,
                        data: [],
                        errorLabel: $("#" + val.Name + "_error"),
                        plot: $("#" + val.Name + "_ph"),
                        labels: null,
                        scale: val.Scale
                    });
                }
            });

            $.each(result, function (index, val) {
                var tData = []

                var error = null;
                $("#spanTimeServer").html(new moment(val.TimeServer).format("dddd, MMMM Do YYYY, h:mm:ss a"));
                for (var i = 0; i < val.PerformanceCounters.length; ++i) {
                    tData.push(val.PerformanceCounters[i].Value * val.PerformanceCounters[i].Scale);
                    if (val.PerformanceCounters[i].Error != null) {
                        error = val.PerformanceCounters[i].Error + "</br>";
                    }
                    $("#current_" + i + "-" + index).html((val.PerformanceCounters[i].Value * val.PerformanceCounters[i].Scale).toFixed(2));
                }
                if (error != null) {
                    data[index].errorLabel.html(error);
                } else {
                    data[index].errorLabel.html("");
                }

                data[index].data.push(tData);


                if (data[index].data.length >= totalPoints)
                    data[index].data = data[index].data.slice(1);

                if (data[index].labels == null) {
                    data[index].labels = [];
                    data[index].colors = [];
                    var tab = $("#" + val.Name + "_tc");
                    for (var i = 0; i < val.PerformanceCounters.length; ++i) {
                        data[index].labels.push(val.PerformanceCounters[i].Label);
                        data[index].colors.push(val.PerformanceCounters[i].Color);

                        var tdName = document.createElement("td");
                        var smallName = document.createElement("small");
                        $(smallName).css("white-space", "nowrap");
                        $(tdName).append(smallName);
                        //var spanColor = document.createElement("span");
                        //$(spanColor).height(10);
                        //$(spanColor).width(10);
                        $(smallName).attr("id", "color_" + i + "-" + index);
                        //$(spanColor).css('float', 'left');
                        //$(smallName).append(spanColor);

                        var spanName = document.createElement("span");
                        $(spanName).html(val.PerformanceCounters[i].Label);
                        $(smallName).append(spanName);

                        var tdScale = document.createElement("td");
                        var smallScale = document.createElement("small");
                        $(tdScale).append(smallScale);
                        $(smallScale).append(val.PerformanceCounters[i].Scale);

                        var tdAverage = document.createElement("td");
                        var smallAverage = document.createElement("small");
                        $(tdAverage).append(smallAverage);
                        var spanAverage = document.createElement("span");
                        $(spanAverage).attr("id", "counter_" + i + "-" + index);
                        $(smallAverage).append(spanAverage);

                        var tdMin = document.createElement("td");
                        var smallMin = document.createElement("small");
                        $(tdMin).append(smallMin);
                        var spanMin = document.createElement("span");
                        $(spanMin).attr("id", "min_" + i + "-" + index);
                        $(smallMin).append(spanMin);

                        var tdCurrent = document.createElement("td");
                        var smallCurrent = document.createElement("small");
                        $(tdCurrent).append(smallCurrent);
                        var spanCurrent = document.createElement("span");
                        $(spanCurrent).attr("id", "current_" + i + "-" + index);
                        $(smallCurrent).append(spanCurrent);

                        var tdMax = document.createElement("td");
                        var smallMax = document.createElement("small");
                        $(tdMax).append(smallMax);
                        var spanMax = document.createElement("span");
                        $(spanMax).attr("id", "max_" + i + "-" + index);
                        $(smallMax).append(spanMax);

                        var tr = document.createElement("tr");

                        $(tr).append(tdName);
                        $(tr).append(tdScale);
                        $(tr).append(tdAverage);
                        $(tr).append(tdMin);
                        $(tr).append(tdCurrent);
                        $(tr).append(tdMax);

                        tab.append(tr);
                    }
                }

                var res = [];
                var average = [];
                var values = [];
                for (var i = 0; i < data[index].data.length; ++i) {

                    for (var j = 0; j < data[index].data[i].length; j++) {
                        if (res[j] == null) {
                            res[j] = { label: data[index].labels[j], data: [], color: data[index].colors[j], hoverable: true };
                            average[j] = 0;
                        }
                        if (values[j] == null) {
                            values[j] = [];
                        }
                        average[j] = average[j] + data[index].data[i][j];
                        res[j].data.push([i, data[index].data[i][j]]);
                        values[j].push(data[index].data[i][j]);
                    }
                }

                for (var i = 0; i < data[index].labels.length; i++) {
                    $("#counter_" + i + "-" + index).html((average[i] / data[index].data.length).toFixed(2));
                    $("#max_" + i + "-" + index).html(Math.max.apply(null, values[i]).toFixed(2));
                    $("#min_" + i + "-" + index).html(Math.min.apply(null, values[i]).toFixed(2));
                    $("#color_" + i + "-" + index).css('color', data[index].colors[i]);
                }


                $.plot(data[index].plot, res, {
                    series: {
                        lines: { lineWidth: 2 },
                        shadowSize: 0
                    },
                    yaxis: { min: 0, max: data[index].scale },
                    xaxis: { min: 0, max: 100, show: false },
                    legend: { show: false }
                })
                .draw();
            });

            if (!websocket)
                setTimeout(update, 1000);

        },
        onError: function (error) {
            $("#spanMessage").html(error);
        },
        updateServer: function (table, plot) {

        }
    };

    var websocket = false;
    var ws;
    ws = new WebSocket("ws://" + window.location.hostname +
            "/api/PCounter");
    ws.onopen = function () {
        $("#spanMessage").text("connected");
        websocket = true;
    };
    ws.onerror = function (evt) {
        $("#spanMessage").text(evt.message);
    };
    ws.onclose = function () {
        $("#spanMessage").text("disconnected");
        websocket = false;
    };
    ws.onmessage = function (evt) {
        PCounter.onGetData(eval(evt.data));
    };

    function update() {
        PCounter.getData();
    }

    if (!websocket) {
        update();
    }

});