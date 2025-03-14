import type { WheelSlice } from '$lib/api/types';

export interface TextSpinnerProps {
	slices: WheelSlice[];
	landedSlice?: number;
	isSpinning: boolean;
	onSpin: () => void;
}
