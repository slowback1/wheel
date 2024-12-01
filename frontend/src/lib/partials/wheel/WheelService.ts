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
import { getRandomNumber } from '$lib/utils/numberUtils';
import { getColorOfSlice, type SliceColor } from '$lib/partials/wheel/wheelColors';

interface SpinnerWheelOptions {
	strokeWidth: number;
	strokeLinejoin: string;
	arcLabel: Arc<any, DefaultArcObject>;
	xVals: any[];
	arcPath: Arc<any, DefaultArcObject>;
	stroke: string;
	percent: boolean;
	colors: SliceColor[];
	wedges: Array<PieArcDatum<number | { valueOf(): number }> | any>;
	width: number;
	fontSize: number;
	yVals: number[];
	height: number;
}

export default class WheelService {
	private readonly DEGREES_IN_CIRCLE = 360;
	/**
	 * The angle offset is used to ensure that the selected angle lines up with the pointer that is rendered on top of the wheel.
	 * @private
	 */
	private readonly ANGLE_OFFSET = -180;
	/**
	 * The text rotation offset is used to ensure that the text is rotated correctly.  The text should be "pointing" to the center of the wheel.
	 * @private
	 */
	private readonly TEXT_ROTATION_OFFSET = 92;

	getWheel(slices: WheelSlice[]): SpinnerWheelOptions | null {
		if (!slices || slices.length === 0) return null;

		const width = 788; // outer width of the chart, in pixels
		const height = width; // outer height of the chart, in pixels
		const percent = false; // format values as percentages (true/false)
		const fontSize = 24; // font size of the x and y values
		const strokeWidth = 1; // width of stroke separating wedges
		const strokeLinejoin = 'round'; // line join style of stroke separating wedges
		const outerRadius = Math.min(width, height) * 0.5 - 60; // outer radius of the circle, in pixels
		const innerRadius = 0; // inner radius of the chart, in pixels
		const labelRadius = innerRadius * 0.2 + outerRadius * 0.6; // center radius of labels
		const strokeColorWOR = 'white'; // stroke color when inner radius is greater than 0
		const strokeColorWIR = 'none'; // stroke color when inner radius is 0
		const stroke = innerRadius > 0 ? strokeColorWIR : strokeColorWOR; // stroke separating widths
		const padAngle = stroke === 'none' ? 1 / outerRadius : 0; // angular separation between wedges

		const xVals = slices.map((el) => this.truncateLabelText(el.label));
		let yVals = slices.map((el) => Number(el.size));

		if (percent) {
			const total = yVals.reduce((a, b) => a + b, 0);
			yVals = yVals.map((el) => el / total);
		}

		const iVals = slices.map((el, i) => i);

		let colors: SliceColor[] = slices.map((_, i) => getColorOfSlice(i));

		const wedges = pie()
			.padAngle(padAngle)
			.sort(null)
			.value((i) => yVals[i.valueOf()])(iVals);

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

	getLandedAngle(slices: WheelSlice[], slice: number): number {
		const degreesPerSlice = this.DEGREES_IN_CIRCLE / slices.length;

		const startAngle = slice * degreesPerSlice;
		const endAngle = startAngle + degreesPerSlice;

		return getRandomNumber(startAngle, endAngle) + this.ANGLE_OFFSET;
	}

	getTextRotationAngle(slices: WheelSlice[], slice: number): number {
		const degreesPerSlice = this.DEGREES_IN_CIRCLE / slices.length;
		const startAngle = slice * degreesPerSlice;
		const endAngle = startAngle + degreesPerSlice;
		const middleAngle = (startAngle + endAngle) / 2;

		return middleAngle + this.TEXT_ROTATION_OFFSET;
	}

	private truncateLabelText(label: string): string {
		const maxLength = 20;
		return label.length > maxLength ? label.substring(0, maxLength) + '...' : label;
	}
}
