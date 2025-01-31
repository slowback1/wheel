import { fireEvent, render, type RenderResult } from '@testing-library/svelte';
import SimpleWheelForm from '$lib/partials/wheelForm/simple/SimpleWheelForm.svelte';
import type { WheelFormProps } from '$lib/partials/wheelForm/WheelForm.types';
import { getNSliceWheel } from '$lib/testHelpers/testData/testWheelSetting';
import type { WheelSlice } from '$lib/api/types';

describe('SimpleWheelForm', () => {
	let result: RenderResult<SimpleWheelForm>;

	function renderComponent(overrides: Partial<WheelFormProps> = {}) {
		const props: WheelFormProps = {
			onWheelSliceChange: vi.fn(),
			slices: getNSliceWheel(2).slices,
			...overrides
		};

		if (result) result.unmount();

		result = render(SimpleWheelForm, { props: props as any });
	}

	beforeEach(() => {
		renderComponent();
	});

	it('should render a textarea', () => {
		expect(result.getByRole('textbox')).toBeInTheDocument();
	});

	it("the textarea's default value is equal to each of the slice's labels separated by a newline", () => {
		const slices = getNSliceWheel(2).slices;
		const expectedValue = slices.map((slice) => slice.label).join('\n');
		expect(result.getByRole('textbox')).toHaveValue(expectedValue);
	});

	it('updates the value of the textarea when changing the textarea', () => {
		const newValue = 'new value';
		const textarea = result.getByRole('textbox');

		fireEvent.change(textarea, { target: { value: newValue } });

		expect(result.getByRole('textbox')).toHaveValue(newValue);
	});

	it('calls onWheelSliceChange with the new slices when changing the textarea', () => {
		const onWheelSliceChange = vi.fn();
		renderComponent({ onWheelSliceChange });

		const newValue = 'new value';
		const textarea = result.getByRole('textbox');

		const newSlices: WheelSlice[] = [{ label: 'new value', size: 1 }];

		fireEvent.input(textarea, { target: { value: newValue } });

		expect(onWheelSliceChange).toHaveBeenCalledWith(newSlices);
	});

	it('will split slices by newline when calling onWheelSliceChange', () => {
		const onWheelSliceChange = vi.fn();
		renderComponent({ onWheelSliceChange });

		const newValue = 'new value\nanother value';
		const textarea = result.getByRole('textbox');

		const newSlices: WheelSlice[] = [
			{ label: 'new value', size: 1 },
			{ label: 'another value', size: 1 }
		];

		fireEvent.input(textarea, { target: { value: newValue } });

		expect(onWheelSliceChange).toHaveBeenCalledWith(newSlices);
	});

	it("the textarea has a placeholder equal to 'Enter each slice on a new line'", () => {
		expect(result.getByRole('textbox')).toHaveAttribute(
			'placeholder',
			'Enter each slice on a new line'
		);
	});

	it('the textarea has a label that is hooked up via id', () => {
		expect(result.getByLabelText('Wheel Slices')).toBeInTheDocument();
	});
});
