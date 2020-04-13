import React, { Component } from 'react';
import * as signalR from '@aspnet/signalr';

type SimpleTemperatureComponentState = { temperature: number | null, loading: boolean };

export class SimpleTemperatureComponent extends Component<{}, SimpleTemperatureComponentState> {
    constructor(props: {}) {
        super(props);
        this.state = { temperature: null, loading: true };
    }

    public componentDidMount() {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("/temperatureHub")
            .configureLogging(signalR.LogLevel.Trace)
            .build();

        connection.on("ReceiveTemperature", (data) => {
            console.log("ReceiveTemperature");
            console.log(data);
            this.setState({ temperature: data, loading: false });
        });

        connection.start().catch(err => console.error(err.toString())).then(function () {
            console.log("Streaming connected");
        });
    }

    public render() {
        return <p>{this.state.temperature}</p>;
    }

    private async populate() {
        const response = await fetch('temperature');
        const data = await response.json();
        this.setState({ temperature: data, loading: false });
    }
}