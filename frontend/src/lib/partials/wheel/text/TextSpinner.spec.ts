import type { TextSpinnerProps } from '$lib/partials/wheel/text/textSpinner.types';
import { fireEvent, render, type RenderResult } from '@testing-library/svelte';
import TextSpinner from '$lib/partials/wheel/text/TextSpinner.svelte';
import { getTestWheelSetting } from '$lib/testHelpers/testData/testWheelSetting';
import { beforeEach } from 'vitest';

describe('TextSpinner', () => {
	let result: RenderResult<TextSpinner>;

	function renderComponent(overrides: Partial<TextSpinnerProps> = {}) {
		let props: TextSpinnerProps = {
			slices: getTestWheelSetting().slices,
			onSpin: vi.fn(),
			isSpinning: false,
			...overrides
		};

		if (result) result.unmount();

		result = render(TextSpinner, { props: props as any });
	}

	beforeEach(() => {
		renderComponent();
	});

	it('renders the slices', () => {
		expect(result.container.querySelectorAll('.text-spinner__list-item')).toHaveLength(
			getTestWheelSetting().slices.length
		);
	});

	it('has a button to spin the wheel', () => {
		expect(result.getByText('Spin')).toBeInTheDocument();
	});

	it('clicking the spin button calls the onSpin function', async () => {
		let mockOnSpin = vi.fn();
		renderComponent({ onSpin: mockOnSpin });

		let button = result.getByText('Spin');

		await fireEvent.click(button);

		expect(mockOnSpin).toHaveBeenCalled();
	});

	it('disables the spin button when the wheel is spinning', async () => {
		renderComponent({ isSpinning: true });
		let button = result.getByText('Spin');

		expect(button).toBeDisabled();
	});

	it("adds a 'landed-slice' class to the winning slice", async () => {
		renderComponent({ isSpinning: false, landedSlice: 0 });
		let slice = result.getByTestId('wheel-slice-0');
		expect(slice).toHaveClass('wheel-slice__landed');
	});
});
