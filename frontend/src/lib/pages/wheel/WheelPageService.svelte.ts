import type ApiContext from '$lib/api/apiContext';
import type { WheelSetting, WheelSlice } from '$lib/api/types';

export default class WheelPageService {
	wheelSlices: WheelSlice[] = $state([]);
	landedSlice: number | null = $state(null);
	private spinResultPromises: Promise<any>[] = $state([]);
	isSpinning = $state(false);
	spinDurationInSections = $state(5);

	constructor(private apiContext: ApiContext) {}

	onWheelSliceChange(slices: WheelSlice[]) {
		this.wheelSlices = slices;
	}

	async spin() {
		let wheel: WheelSetting = {
			slices: this.wheelSlices,
			name: 'Wheel'
		};

		let spinPromise = this.apiContext.spinApi.spin({ wheelSetting: wheel });
		let waitPromise = this.waitForSeconds(this.spinDurationInSections);

		this.spinResultPromises = [spinPromise, waitPromise];
		this.isSpinning = true;

		let result = await spinPromise;

		this.landedSlice = result.sliceLanded;

		await Promise.all(this.spinResultPromises);

		this.isSpinning = false;
	}

	reset() {
		this.landedSlice = null;
	}

	private async waitForSeconds(seconds: number) {
		return new Promise((resolve) => setTimeout(resolve, seconds * 1000));
	}
}
