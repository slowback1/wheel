import type { Meta } from '@storybook/svelte';
import SimpleWheelForm from '$lib/partials/wheelForm/simple/SimpleWheelForm.svelte';
import type { WheelFormProps } from '$lib/partials/wheelForm/WheelForm.types';
import { getNSliceWheel } from '$lib/testHelpers/testData/testWheelSetting';

const meta: Meta = {
	title: 'Partials/Wheel Form/Simple Wheel Form',
	component: SimpleWheelForm
};

export default meta;

function makeProps(overrides: Partial<WheelFormProps> = {}): WheelFormProps {
	return {
		onWheelSliceChange: () => {},
		slices: getNSliceWheel(5).slices,
		...overrides
	};
}

export const Default = {
	args: makeProps()
};

export const WithEmptySlices = {
	args: makeProps({ slices: [] })
};

export const With50Slices = {
	args: makeProps({ slices: getNSliceWheel(50).slices })
};
