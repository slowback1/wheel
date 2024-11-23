import WheelPageService from './WheelPageService.svelte';
import type { WheelSlice } from '$lib/api/types';
import type ApiContext from '$lib/api/apiContext';
import { getNSliceWheel } from '$lib/testHelpers/testData/testWheelSetting';

describe('WheelPageService', () => {
	let apiContextMock: ApiContext;
	let wheelPageService: WheelPageService;

	beforeEach(() => {
		apiContextMock = {
			spinApi: {
				spin: vi.fn().mockResolvedValue({ sliceLanded: 2 })
			}
		} as unknown as ApiContext;

		wheelPageService = new WheelPageService(apiContextMock);
		wheelPageService.spinDurationInSections = 1;
	});

	test('should initialize with default values', () => {
		expect(wheelPageService.wheelSlices).toEqual([]);
		expect(wheelPageService.landedSlice).toBeNull();
	});

	test('should update wheel slices on change', () => {
		const slices: WheelSlice[] = getNSliceWheel(2).slices;
		wheelPageService.onWheelSliceChange(slices);
		expect(wheelPageService.wheelSlices).toEqual(slices);
	});

	test('should spin the wheel and update landed slice', async () => {
		const slices: WheelSlice[] = getNSliceWheel(2).slices;
		wheelPageService.onWheelSliceChange(slices);

		await wheelPageService.spin();

		expect(apiContextMock.spinApi.spin).toHaveBeenCalledWith({
			wheelSetting: { slices, name: 'Wheel' }
		});
		expect(wheelPageService.landedSlice).toBe(2);
	});

	test('should reset the landed slice', () => {
		wheelPageService.landedSlice = 2;
		wheelPageService.reset();
		expect(wheelPageService.landedSlice).toBeNull();
	});

	it('should indicate that the wheel is spinning when the spin method is called', async () => {
		const slices: WheelSlice[] = getNSliceWheel(2).slices;
		wheelPageService.onWheelSliceChange(slices);

		const spinPromise = wheelPageService.spin();

		expect(wheelPageService.isSpinning).toBe(true);

		await spinPromise;

		expect(wheelPageService.isSpinning).toBe(false);
	});
});
