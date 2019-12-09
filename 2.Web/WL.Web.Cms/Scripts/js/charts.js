/// <reference path="../assets/plugins/jquery-1.10.2.min.js" />


//画出曲线图
function showFlot_1(pageviews, tabl, str1, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews,
        label: str1
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return (d.getUTCMonth() + 1) + "月" + d.getUTCDate() + "日";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getUTCFullYear() + "年" + (dateTime.getUTCMonth() + 1) + "月" + day + "日";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlot_2(pageviews_1, pageviews_2, tabl, str_1, str_2, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return (d.getUTCMonth() + 1) + "月" + d.getUTCDate() + "日";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getUTCFullYear() + "年" + (dateTime.getUTCMonth() + 1) + "月" + day + "日";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlot_3(pageviews_1, pageviews_2, pageviews_3, tabl, str_1, str_2, str_3, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return (d.getUTCMonth() + 1) + "月" + d.getUTCDate() + "日";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getUTCFullYear() + "年" + (dateTime.getUTCMonth() + 1) + "月" + day + "日";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlot_4(pageviews_1, pageviews_2, pageviews_3, pageviews_4, tabl, str_1, str_2, str_3, str_4, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return (d.getUTCMonth() + 1) + "月" + d.getUTCDate() + "日";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getUTCFullYear() + "年" + (dateTime.getUTCMonth() + 1) + "月" + day + "日";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlot_5(pageviews_1, pageviews_2, pageviews_3, pageviews_4, pageviews_5, tabl, str_1, str_2, str_3, str_4, str_5, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }
    , {
        data: pageviews_5,
        label: str_5
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return (d.getUTCMonth() + 1) + "月" + d.getUTCDate() + "日";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getUTCFullYear() + "年" + (dateTime.getUTCMonth() + 1) + "月" + day + "日";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlot_6(pageviews_1, pageviews_2, pageviews_3, pageviews_4, pageviews_5, pageviews_6, tabl, str_1, str_2, str_3, str_4, str_5, str_6, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }, {
        data: pageviews_5,
        label: str_5
    }, {
        data: pageviews_6,
        label: str_6
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return (d.getUTCMonth() + 1) + "月" + d.getUTCDate() + "日";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getUTCFullYear() + "年" + (dateTime.getUTCMonth() + 1) + "月" + day + "日";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlot_7(pageviews_1, pageviews_2, pageviews_3, pageviews_4, pageviews_5, pageviews_7, tabl, str_1, str_2, str_3, str_4, str_5, str_6, str_7, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }, {
        data: pageviews_5,
        label: str_5
    }, {
        data: pageviews_6,
        label: str_6
    }, {
        data: pageviews_7,
        label: str_7
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return (d.getUTCMonth() + 1) + "月" + d.getUTCDate() + "日";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getUTCFullYear() + "年" + (dateTime.getUTCMonth() + 1) + "月" + day + "日";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlot_8(pageviews_1, pageviews_2, pageviews_3, pageviews_4, pageviews_5, pageviews_7, pageviews_8, tabl, str_1, str_2, str_3, str_4, str_5, str_6, str_7, str_8, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }, {
        data: pageviews_5,
        label: str_5
    }, {
        data: pageviews_6,
        label: str_6
    }, {
        data: pageviews_7,
        label: str_7
    }
    , {
        data: pageviews_8,
        label: str_8
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return (d.getUTCMonth() + 1) + "月" + d.getUTCDate() + "日";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getUTCFullYear() + "年" + (dateTime.getUTCMonth() + 1) + "月" + day + "日";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotDay_1(pageviews, tabl, str1, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews,
        label: str1
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + ":00" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotDay_2(pageviews_1, pageviews_2, tabl, str_1, str_2, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + ":00" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotDay_3(pageviews_1, pageviews_2, pageviews_3, tabl, str_1, str_2, str_3, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + ":00" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotDay_4(pageviews_1, pageviews_2, pageviews_3, pageviews_4, tabl, str_1, str_2, str_3, str_4, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + ":00" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotDay_5(pageviews_1, pageviews_2, pageviews_3, pageviews_4, pageviews_5, tabl, str_1, str_2, str_3, str_4, str_5, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }
    , {
        data: pageviews_5,
        label: str_5
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + ":00" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotDay_6(pageviews_1, pageviews_2, pageviews_3, pageviews_4, pageviews_5, pageviews_6, tabl, str_1, str_2, str_3, str_4, str_5, str_6, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }, {
        data: pageviews_5,
        label: str_5
    }, {
        data: pageviews_6,
        label: str_6
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + ":00" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotDay_7(pageviews_1, pageviews_2, pageviews_3, pageviews_4, pageviews_5, pageviews_7, tabl, str_1, str_2, str_3, str_4, str_5, str_6, str_7, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }, {
        data: pageviews_5,
        label: str_5
    }, {
        data: pageviews_6,
        label: str_6
    }, {
        data: pageviews_7,
        label: str_7
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + ":00" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotDay_8(pageviews_1, pageviews_2, pageviews_3, pageviews_4, pageviews_5, pageviews_7, pageviews_8, tabl, str_1, str_2, str_3, str_4, str_5, str_6, str_7, str_8, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }, {
        data: pageviews_5,
        label: str_5
    }, {
        data: pageviews_6,
        label: str_6
    }, {
        data: pageviews_7,
        label: str_7
    }
    , {
        data: pageviews_8,
        label: str_8
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + ":00" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotHour_1(pageviews, tabl, str1, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews,
        label: str1
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + "分" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotHour_2(pageviews_1, pageviews_2, tabl, str_1, str_2, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + "分" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotHour_3(pageviews_1, pageviews_2, pageviews_3, tabl, str_1, str_2, str_3, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + "分" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotHour_4(pageviews_1, pageviews_2, pageviews_3, pageviews_4, tabl, str_1, str_2, str_3, str_4, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + "分" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotHour_5(pageviews_1, pageviews_2, pageviews_3, pageviews_4, pageviews_5, tabl, str_1, str_2, str_3, str_4, str_5, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }
    , {
        data: pageviews_5,
        label: str_5
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + "分" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotHour_6(pageviews_1, pageviews_2, pageviews_3, pageviews_4, pageviews_5, pageviews_6, tabl, str_1, str_2, str_3, str_4, str_5, str_6, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }, {
        data: pageviews_5,
        label: str_5
    }, {
        data: pageviews_6,
        label: str_6
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + "分" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotHour_7(pageviews_1, pageviews_2, pageviews_3, pageviews_4, pageviews_5, pageviews_7, tabl, str_1, str_2, str_3, str_4, str_5, str_6, str_7, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }, {
        data: pageviews_5,
        label: str_5
    }, {
        data: pageviews_6,
        label: str_6
    }, {
        data: pageviews_7,
        label: str_7
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + "分" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotHour_8(pageviews_1, pageviews_2, pageviews_3, pageviews_4, pageviews_5, pageviews_7, pageviews_8, tabl, str_1, str_2, str_3, str_4, str_5, str_6, str_7, str_8, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }, {
        data: pageviews_5,
        label: str_5
    }, {
        data: pageviews_6,
        label: str_6
    }, {
        data: pageviews_7,
        label: str_7
    }
    , {
        data: pageviews_8,
        label: str_8
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            ticks: 11,
            tickDecimals: 0
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + x + "分" + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}

//画出曲线图
function showFlotTime_3(pageviews_1, pageviews_2, pageviews_3, tabl, str_1, str_2, str_3, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return d.getHours() + "点" + d.getMinutes() + "分";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getHours() + "点" + dateTime.getMinutes() + "分";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}
//画出曲线图
function showFlotTime_1(pageviews, tabl, str_1, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews,
        label: str_1
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return d.getHours() + "点" + d.getMinutes() + "分";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getHours() + "点" + dateTime.getMinutes() + "分";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}
//画出曲线图
function showFlotTime_2(pageviews_1, pageviews_2, tabl, str_1, str_2, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return d.getHours() + "点" + d.getMinutes() + "分";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getHours() + "点" + dateTime.getMinutes() + "分";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}
//画出曲线图
function showFlotTime_4(pageviews_1, pageviews_2, pageviews_3, pageviews_4, tabl, str_1, str_2, str_3, str_4, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }, {
        data: pageviews_4,
        label: str_4
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#00db00", "#f75000", "#007500", "#642100", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return d.getHours() + "点" + d.getMinutes() + "分";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getHours() + "点" + dateTime.getMinutes() + "分";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}
//画出曲线图
function showFlot_colour(pageviews, tabl, str1, str2, str3, colour) {
    var plot = $.plot($(tabl), [{
        data: pageviews,
        label: str1
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: [colour],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return (d.getUTCMonth() + 1) + "月" + d.getUTCDate() + "日";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getUTCFullYear() + "年" + (dateTime.getUTCMonth() + 1) + "月" + day + "日";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}
//画出曲线图
function showFlotMonth_1(pageviews, tabl, str1, str2, str3, colors) {
    var plot = $.plot($(tabl), [{
        data: pageviews,
        label: str1
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: [colors],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return (d.getMonth() + 1) + "月";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getFullYear() + "年" + (dateTime.getMonth() + 1) + "月";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}
//画出曲线图
function showFlotMonth_3(pageviews_1, pageviews_2, pageviews_3, tabl, str_1, str_2, str_3, str2, str3) {
    var plot = $.plot($(tabl), [{
        data: pageviews_1,
        label: str_1
    }, {
        data: pageviews_2,
        label: str_2
    }, {
        data: pageviews_3,
        label: str_3
    }
    ], {
        series: {
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.05
                    }, {
                        opacity: 0.01
                    }
                    ]
                }
            },
            points: {
                show: true
            },
            shadowSize: 2
        },
        grid: {
            hoverable: true,
            clickable: true,
            tickColor: "#eee",
            borderWidth: 0
        },
        colors: ["#d12610", "#37b7f3", "#52e136", "#783f04", "#6aa84f", "#ffff00", "#ff00ff", "#434343"],
        xaxis: {
            mode: 'time',
            tickFormatter: function (val, axis) {
                var d = new Date(val);
                //alert(d.getUTCDate());
                return (d.getUTCMonth() + 1) + "月";
            }
        },
        yaxis: {
            ticks: 11,
            tickDecimals: 0
        }
    });
    $("#loading").hide();

    function showTooltip(x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 15,
            border: '1px solid #333',
            padding: '4px',
            color: '#fff',
            'border-radius': '3px',
            'background-color': '#333',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(tabl).bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if (item) {
            if (previousPoint != item.dataIndex) {
                previousPoint = item.dataIndex;

                $("#tooltip").remove();
                var x = item.datapoint[0],
                    y = item.datapoint[1];
                var dateTime = new Date();
                dateTime.setTime(x);
                var day = dateTime.getDate();
                var dateTimeStr = dateTime.getUTCFullYear() + "年" + (dateTime.getUTCMonth() + 1) + "月";

                showTooltip(item.pageX, item.pageY, item.series.label + "：" + dateTimeStr + str2 + y + str3);

            }
        } else {
            $("#tooltip").remove();
            previousPoint = null;
        }
    });
}