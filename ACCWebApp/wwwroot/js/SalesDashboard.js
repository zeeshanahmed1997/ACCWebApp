var salesData = [
    { month: 'January', sales: 1000 },
    { month: 'February', sales: 1500 },
    { month: 'March', sales: 2000 },
    // Add data for other months
];

var labels = salesData.map(data => data.month);
var sales = salesData.map(data => data.sales);

var ctx = document.getElementById('salesChart').getContext('2d');
var chart = new Chart(ctx, {
    type: 'bar',
    data: {
        labels: labels,
        datasets: [{
            label: 'Monthly Sales',
            data: sales,
            backgroundColor: 'rgba(75, 192, 192, 0.5)',
            borderColor: 'rgba(75, 192, 192, 1)',
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true,
                ticks: {
                    precision: 0,
                    callback: function (value) {
                        if (value >= 1000) {
                            return '$' + value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        } else {
                            return '$' + value;
                        }
                    }
                }
            }
        },
        plugins: {
            title: {
                display: true,
                text: 'Monthly Sales',
                font: {
                    size: 18
                }
            },
            legend: {
                display: false
            }
        },
        hover: {
            mode: 'index',
            intersect: false
        },
        animation: {
            duration: 500,
            easing: 'easeInOutQuad'
        }
    }
});