import type { Meta } from '@storybook/svelte';
import SpinnerWheel from '$lib/partials/wheel/SpinnerWheel.svelte';
import { getNSliceWheel, getTestWheelSetting } from '$lib/testHelpers/testData/testWheelSetting';
import { getRandomNumber } from '$lib/utils/numberUtils';

const meta: Meta = {
	title: 'Partials/Spinner Wheel',
	component: SpinnerWheel
};

export default meta;

export const Default = {
	args: {
		slices: getTestWheelSetting().slices,
		onSpin: () => {},
		isSpinning: false
	}
};

export const With20Slices = {
	args: {
		slices: getNSliceWheel(20).slices,
		onSpin: () => {},
		isSpinning: false
	}
};

export const Spinning = {
	args: {
		slices: getNSliceWheel(5).slices,
		onSpin: () => {},
		isSpinning: true
	}
};

export const NoSlices = {
	args: {
		slices: [],
		onSpin: () => {},
		isSpinning: false
	}
};

export const LandingOnSlice = {
	args: {
		slices: getNSliceWheel(5).slices,
		onSpin: () => {},
		isSpinning: false,
		selectedSlice: getRandomNumber(0, 4)
	}
};

const slicesWithLongLabels = getNSliceWheel(5).slices.map((slice, index) => {
	slice.label = `This is a really really really long label that should be truncated ${index}`;
	return slice;
});

export const WithReallyLongLabels = {
	args: {
		slices: slicesWithLongLabels,
		isSpinning: false,
		onSpin: () => {}
	}
};
