import React, { Component } from 'react';
import * as signalR from '@aspnet/signalr';
import ReactApexChart from 'react-apexcharts'
import ApexCharts from 'apexcharts';

type SimpleTemperatureComponentState = { temperature: number[] | null, loading: boolean };

export class SimpleTemperatureComponent extends Component<{}, any> {
    private hubConnection: signalR.HubConnection;

    private XAXISRANGE: number = 1000*60; // 00
    private TICKINTERVAL: number = 86400 // 00
    private data: { x: number, y: number }[] = [];
    private lastDate: number = 0;

    private getNewSeries(baseval: number, data: number) {
        var newDate = Date.now();
        this.lastDate = newDate

        // for (var i = 0; i < this.data.length - 10; i++) {
        //     // IMPORTANT
        //     // we reset the x and y of the data which is out of drawing area
        //     // to prevent memory leaks
        //     this.data[i].x = newDate - this.XAXISRANGE - this.TICKINTERVAL
        //     this.data[i].y = 0
        // }

        this.data.push({
            x: newDate,
            y: data
        })
    }

    constructor(props: {}) {
        super(props);

        this.lastDate = Date.now();

        this.state = {
            temperature: [],
            loading: true,
            series: [{
                name: "Temperature",
                data: this.data.slice()
            }],
            options: {
                chart: {
                    id: 'realtime',
                    height: 350,
                    type: 'line',
                    animations: {
                        enabled: true,
                        easing: 'linear',
                        dynamicAnimation: {
                            speed: 1000
                        }
                    },
                    toolbar: {
                        show: false
                    },
                    zoom: {
                        enabled: false
                    }
                },
                dataLabels: {
                    enabled: false
                },
                stroke: {
                    curve: 'smooth'
                },
                title: {
                    text: 'Dynamic Updating Chart',
                    align: 'left'
                },
                markers: {
                    size: 0
                },
                xaxis: {
                    type: 'datetime',
                    range: this.XAXISRANGE,
                },
                yaxis: {
                    max: 40
                },
                legend: {
                    show: false
                },
            },
        };

        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl("/temperatureHub")
            .configureLogging(signalR.LogLevel.Trace)
            .build();

        this.hubConnection.on("ReceiveTemperature", (data) => {
            console.log("ReceiveTemperature");
            console.log(data);

            this.getNewSeries(this.lastDate, data);

            ApexCharts.exec('realtime', 'updateSeries', [{
                data: this.data
            }]);

            ApexCharts.exec('mychart', 'updateOptions', {
                yaxis: {
                    min: 0,
                    max: 40
                },
            });
        });
    }

    public componentWillUnmount() {
        this.hubConnection.stop();
    }

    public componentDidMount() {
        this.hubConnection.start().catch(err => console.error(err.toString())).then(function () {
            console.log("Streaming connected");
        });
    }

    public render() {
        return <div>
            <p>{this.state.temperature}</p>
            <div id="chart">
                <ReactApexChart options={this.state.options} series={this.state.series} type="line" height={350} />
            </div>
        </div>;
    }

    private async populate() {
        const response = await fetch('temperature');
        const data = await response.json();
        this.setState({ temperature: data, loading: false });
    }
}