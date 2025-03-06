import { fireEvent, render, type RenderResult } from '@testing-library/svelte';
import SpinnerWheel from '$lib/partials/wheel/traditional/SpinnerWheel.svelte';
import type { SpinnerWheelProps } from '$lib/partials/wheel/traditional/SpinnerWheel.types';
import { getNSliceWheel, getTestWheelSetting } from '$lib/testHelpers/testData/testWheelSetting';
import { beforeEach } from 'vitest';

describe('SpinnerWheel', () => {
	let result: RenderResult<SpinnerWheel>;

	function renderComponent(overrides: Partial<SpinnerWheelProps> = {}) {
		let props: SpinnerWheelProps = {
			slices: getTestWheelSetting().slices,
			onSpin: vi.fn(),
			isSpinning: false,
			...overrides
		};

		if (result) {
			result.unmount();
		}

		result = render(SpinnerWheel, { props: props as any });
	}

	beforeEach(() => {
		renderComponent();
	});

	it('should render the given slices', () => {
		const slices = result.getAllByTestId('wheel-slice');

		expect(slices.length).toEqual(getTestWheelSetting().slices.length);
	});

	it("should not have the 'wheel__spinning' class when the wheel is not spinning", () => {
		let wheel = result.getByTestId('wheel');

		expect(wheel.classList).not.toContain('wheel__spinning');
	});

	it("should have the 'wheel__spinning' class when the wheel is spinning", () => {
		renderComponent({ isSpinning: true });

		let wheel = result.getByTestId('wheel');

		expect(wheel.classList).toContain('wheel-spinning');
	});

	it("should have a 'data-spinning' attribute set to 'true' when the wheel is spinning", () => {
		renderComponent({ isSpinning: true });

		let wheel = result.getByTestId('wheel');

		expect(wheel.getAttribute('data-spinning')).toBe('true');
	});

	it("should have a 'data-spinning' attribute set to 'false' when the wheel is not spinning", () => {
		let wheel = result.getByTestId('wheel');

		expect(wheel.getAttribute('data-spinning')).toBe('false');
	});

	it('clicking the wheel calls the onSpin function', () => {
		let onSpin = vi.fn();

		renderComponent({ onSpin });

		let wheel = result.getByTestId('wheel');

		fireEvent.click(wheel);

		expect(onSpin).toHaveBeenCalled();
	});

	it('should render a message indicating to add a wheel slice before spinning if given no slices', () => {
		renderComponent({ slices: [] });

		let message = result.getByTestId('no-slices-message');

		expect(message).toBeInTheDocument();

		expect(message).toHaveTextContent('Add a slice to spin the wheel');
	});

	it('should not render a message indicating to add a wheel slice before spinning if given slices', () => {
		let message = result.queryByTestId('no-slices-message');

		expect(message).not.toBeInTheDocument();
	});

	it('should not render the wheel if there are no slices', () => {
		renderComponent({ slices: [] });

		let wheel = result.queryByTestId('wheel');

		expect(wheel).not.toBeInTheDocument();
	});
});
