import type { Meta } from '@storybook/svelte';
import TextSpinner from '$lib/partials/wheel/text/TextSpinner.svelte';
import { getNSliceWheel, getTestWheelSetting } from '$lib/testHelpers/testData/testWheelSetting';
import type { TextSpinnerProps } from '$lib/partials/wheel/text/textSpinner.types';

const meta: Meta = {
	title: 'Partials/Wheel/Text Wheel',
	component: TextSpinner
};

export default meta;

const makeProps = (overrides: Partial<TextSpinnerProps> = {}): TextSpinnerProps => ({
	isSpinning: false,
	slices: getNSliceWheel(5).slices,
	onSpin: () => {},
	...overrides
});

export const Default = {
	args: makeProps()
};

export const Spinning = {
	args: makeProps({ isSpinning: true })
};

export const LandedOnSlice = {
	args: makeProps({ landedSlice: 0 })
};
