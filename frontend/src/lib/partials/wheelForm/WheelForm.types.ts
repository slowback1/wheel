import type { WheelSlice } from '$lib/api/types';

export interface WheelFormProps {
	slices: WheelSlice[];
	onWheelSliceChange: (slices: WheelSlice[]) => void;
}
