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
