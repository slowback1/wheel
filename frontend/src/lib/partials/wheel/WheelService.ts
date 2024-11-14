import type { WheelSlice } from '$lib/api/types';
import {
	type Arc,
	arc,
	type DefaultArcObject,
	interpolatePlasma,
	pie,
	type PieArcDatum,
	quantize
} from 'd3';

interface SpinnerWheelOptions {
	strokeWidth: number;
	strokeLinejoin: string;
	arcLabel: Arc<any, DefaultArcObject>;
	xVals: any[];
	arcPath: Arc<any, DefaultArcObject>;
	stroke: string;
	percent: boolean;
	colors: string[];
	wedges: Array<PieArcDatum<number | { valueOf(): number }>>;
	width: number;
	fontSize: number;
	yVals: number[];
	height: number;
}

export default class WheelService {
	getWheel(slices: WheelSlice[]): SpinnerWheelOptions | null {
		if (!slices || slices.length === 0) return null;

		const width = 788; // outer width of the chart, in pixels
		const height = width; // outer height of the chart, in pixels
		const percent = false; // format values as percentages (true/false)
		const fontSize = 10; // font size of the x and y values
		const strokeWidth = 1; // width of stroke separating wedges
		const strokeLinejoin = 'round'; // line join style of stroke separating wedges
		const outerRadius = Math.min(width, height) * 0.5 - 60; // outer radius of the circle, in pixels
		const innerRadius = 0; // inner radius of the chart, in pixels
		const labelRadius = innerRadius * 0.2 + outerRadius * 0.8; // center radius of labels
		const strokeColorWOR = 'white'; // stroke color when inner radius is greater than 0
		const strokeColorWIR = 'none'; // stroke color when inner radius is 0
		const stroke = innerRadius > 0 ? strokeColorWIR : strokeColorWOR; // stroke separating widths
		const padAngle = stroke === 'none' ? 1 / outerRadius : 0; // angular separation between wedges

		const x = Object.keys(slices[0])[0];
		const y = Object.keys(slices[0])[1];
		const xVals = slices.map((el) => el[x]);
		let yVals = slices.map((el) => Number(el[y]));
		if (percent) {
			const total = yVals.reduce((a, b) => a + b, 0);
			yVals = yVals.map((el) => el / total);
		}
		const iVals = slices.map((el, i) => i);

		// colors can be adjusted manually by creating a color array which length matches length of data set.
		let colors;
		if (!colors) colors = quantize((t) => interpolatePlasma(t * 0.7 + 0.3), xVals.length);

		const wedges = pie()
			.padAngle(padAngle)
			.sort(null)
			.value((i) => yVals[i as number])(iVals);

		const arcPath = arc().innerRadius(innerRadius).outerRadius(outerRadius);

		const arcLabel = arc().innerRadius(labelRadius).outerRadius(labelRadius);

		return {
			width,
			height,
			wedges,
			colors,
			arcPath,
			stroke,
			strokeWidth,
			strokeLinejoin,
			arcLabel,
			fontSize,
			xVals,
			yVals,
			percent
		};
	}
}
