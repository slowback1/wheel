import { WheelSpinMode, type WheelSpinOptions } from '$lib/api/types';

export function getTestSpinOptions(overrides: Partial<WheelSpinOptions> = {}): WheelSpinOptions {
	return {
		mode: WheelSpinMode.Random,
		...overrides
	};
}
