import { getNSliceWheel } from '$lib/testHelpers/testData/testWheelSetting';
import WheelService from '$lib/partials/wheel/WheelService';

describe('WheelService', () => {
	it.each([
		[4, 0, -180, -90],
		[8, 1, -135, -90]
	])(
		'When given %s slices, and the selected slice is %s, the angle should be between %s and %s',
		(numberOfSlices, landedSlice, startAngle, endAngle) => {
			const slices = getNSliceWheel(numberOfSlices).slices;
			const selectedSlice = landedSlice;
			const service = new WheelService();
			const angle = service.getLandedAngle(slices, selectedSlice);

			expect(angle).toBeGreaterThanOrEqual(startAngle);
			expect(angle).toBeLessThanOrEqual(endAngle);
		}
	);
});
