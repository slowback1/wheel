import type { WheelSetting } from '$lib/api/types';

export function getTestWheelSetting(overrides: Partial<WheelSetting> = {}): WheelSetting {
	return {
		name: 'Test Wheel Setting',
		slices: [
			{
				label: 'Test Slice 1',
				size: 1
			},
			{
				label: 'Test Slice 2',
				size: 1
			}
		],
		...overrides
	};
}

export function getNSliceWheel(numberOfSlices: number) {
	const sliceToClone = getTestWheelSetting().slices[0];

	let slices = [];

	for (let i = 0; i < numberOfSlices; i++) {
		slices.push({ ...sliceToClone, label: `${sliceToClone.label} ${i}` });
	}

	return getTestWheelSetting({ slices });
}
