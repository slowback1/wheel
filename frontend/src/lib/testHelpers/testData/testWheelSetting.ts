import type { WheelSetting } from '$lib/api/wheelApi';

export default function getTestWheelSetting(): WheelSetting {
	return {
		name: 'Test Wheel',
		slices: [
			{
				label: 'Slice 1',
				size: 1
			}
		]
	};
}
