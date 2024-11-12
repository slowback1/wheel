import { render, type RenderResult } from '@testing-library/svelte';
import SpinnerWheel from '$lib/partials/wheel/SpinnerWheel.svelte';
import type { SpinnerWheelProps } from '$lib/partials/wheel/SpinnerWheel.types';
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

		getTestWheelSetting().slices.forEach((slice) => {
			expect(slices.some((s) => s.textContent === slice.label)).toBe(true);
		});
	});

	it("should not have the 'wheel__spinning' class when the wheel is not spinning", () => {
		let wheel = result.getByTestId('wheel');

		expect(wheel.classList).not.toContain('wheel__spinning');
	});

	it("should have the 'wheel__spinning' class when the wheel is spinning", () => {
		renderComponent({ isSpinning: true });

		let wheel = result.getByTestId('wheel');

		expect(wheel.classList).toContain('wheel__spinning');
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

		wheel.click();

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

	it.each([
		[0, 'wheel-slice__0'],
		[1, 'wheel-slice__1'],
		[2, 'wheel-slice__2'],
		[3, 'wheel-slice__3'],
		[4, 'wheel-slice__4'],
		[5, 'wheel-slice__5'],
		[6, 'wheel-slice__6'],
		[7, 'wheel-slice__7'],
		[8, 'wheel-slice__8'],
		[9, 'wheel-slice__0'],
		[10, 'wheel-slice__1'],
		[11, 'wheel-slice__2'],
		[12, 'wheel-slice__3'],
		[13, 'wheel-slice__4'],
		[14, 'wheel-slice__5'],
		[15, 'wheel-slice__6'],
		[16, 'wheel-slice__7'],
		[17, 'wheel-slice__8']
	])('should add the correct child class based on the index', (index, expectedClass) => {
		renderComponent({ slices: getNSliceWheel(18).slices });

		const slices = result.getAllByTestId('wheel-slice');

		expect(slices[index].classList).toContain(expectedClass);
	});
});
