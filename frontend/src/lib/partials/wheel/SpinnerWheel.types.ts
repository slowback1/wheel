import type { WheelSlice } from '$lib/api/types';

export interface SpinnerWheelProps {
	slices: WheelSlice[];
	onSpin: () => void;
	isSpinning: boolean;
	selectedSlice?: number;
}
