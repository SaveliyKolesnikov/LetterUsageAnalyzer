const generateChart = (callback, title, charCount) => {
    var { ChartJSNodeCanvas } = require("chartjs-node-canvas");
    var fs = require("fs");

    const width = 1200;
    const height = 1200;

    const configuration = {
        type: "bar",
        data: {
            labels: Object.keys(charCount),
            datasets: [
                {
                    label: title,
                    data: Object.values(charCount),
                    backgroundColor: bgColors,
                    borderColor: bgColors.map(c => c.replace("0.2", "1")),
                    borderWidth: 1
                }
            ]
        },
        options: {
            plugins: {
                datalabels: {
                    color: 'blue',
                    labels: {
                        title: {
                            font: {
                                weight: 'bold'
                            }
                        },
                        value: {
                            color: 'green'
                        }
                    }
                }
            }
        },
        plugins: [
            {
                id: "background-colour",
                beforeDraw: (chart) => {
                    const ctx = chart.ctx;
                    ctx.save();
                    ctx.fillStyle = "white";
                    ctx.fillRect(0, 0, width, height);
                    ctx.restore();
                }
            },
            {
                id: "add-labels",
                afterDraw: (chart) => {
                    const ctx = chart.ctx;
                    ctx.textAlign = 'center';
                    ctx.fillStyle = "rgba(0, 0, 0, 1)";
                    ctx.textBaseline = 'bottom';

                    chart.data.datasets.forEach(function (dataset, i) {
                        var meta = chart.getDatasetMeta(i);
                        meta.data.forEach(function (bar, index) {
                            var data = parseFloat(dataset.data[index]).toFixed(2);
                            ctx.fillText(data, bar.x, bar.y);
                        });
                    });
                }
            },

        ],
    };
    const chartCallback = (ChartJS) => {
        ChartJS.defaults.responsive = true;
        ChartJS.defaults.maintainAspectRatio = false;
    };
    const chartJSNodeCanvas = new ChartJSNodeCanvas({ width, height, chartCallback });

    chartJSNodeCanvas.renderToDataURL(configuration).then((buffer) => {
        callback(null, buffer);
    }).catch((e) => callback(e));
};

// TODO: add color generator for more colors.
// Color scheme: mpn65.
var bgColors = [
    "rgba(255, 0, 41, 0.2)",
    "rgba(55, 126, 184, 0.2)",
    "rgba(102, 166, 30, 0.2)",
    "rgba(152, 78, 163, 0.2)",
    "rgba(0, 210, 213, 0.2)",
    "rgba(255, 127, 0, 0.2)",
    "rgba(175, 141, 0, 0.2)",
    "rgba(127, 128, 205, 0.2)",
    "rgba(179, 233, 0, 0.2)",
    "rgba(196, 46, 96, 0.2)",
    "rgba(166, 86, 40, 0.2)",
    "rgba(247, 129, 191, 0.2)",
    "rgba(141, 211, 199, 0.2)",
    "rgba(190, 186, 218, 0.2)",
    "rgba(251, 128, 114, 0.2)",
    "rgba(128, 177, 211, 0.2)",
    "rgba(253, 180, 98, 0.2)",
    "rgba(252, 205, 229, 0.2)",
    "rgba(188, 128, 189, 0.2)",
    "rgba(255, 237, 111, 0.2)",
    "rgba(196, 234, 255, 0.2)",
    "rgba(207, 140, 0, 0.2)",
    "rgba(27, 158, 119, 0.2)",
    "rgba(217, 95, 2, 0.2)",
    "rgba(231, 41, 138, 0.2)",
    "rgba(230, 171, 2, 0.2)",
    "rgba(166, 118, 29, 0.2)",
    "rgba(0, 151, 255, 0.2)",
    "rgba(0, 208, 103, 0.2)",
    "rgba(0, 0, 0, 0.2)",
    "rgba(37, 37, 37, 0.2)",
    "rgba(82, 82, 82, 0.2)",
    "rgba(115, 115, 115, 0.2)",
];

module.exports = { generateChart };

module.exports.generateChart(() => { }, "test", { 'q': 123123 });