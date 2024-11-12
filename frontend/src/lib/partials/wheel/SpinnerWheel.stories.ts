import type { Meta } from '@storybook/svelte';
import SpinnerWheel from '$lib/partials/wheel/SpinnerWheel.svelte';
import { getNSliceWheel, getTestWheelSetting } from '$lib/testHelpers/testData/testWheelSetting';

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
