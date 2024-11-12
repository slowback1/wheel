import type { SpinResult } from '$lib/api/types';
import { getTestWheelSetting } from '$lib/testHelpers/testData/testWheelSetting';

export function getTestSpinResult(overrides: Partial<SpinResult>): SpinResult {
	return {
		sliceLanded: 0,
		wheelUsed: getTestWheelSetting(),
		...overrides
	};
}
