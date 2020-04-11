import React, { Component } from 'react';

type SimpleTemperatureComponentState = { temperature: number | null, loading: boolean };

export class SimpleTemperatureComponent extends Component<{}, SimpleTemperatureComponentState> {
    constructor(props: {}) {
        super(props);
        this.state = { temperature: null, loading: true };
    }

    public componentDidMount() {
        this.populate();
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