import type { IUserRegistrationService } from '$lib/partials/user/registerUser/RegisterUser.types';
import { beforeEach } from 'vitest';
import { fireEvent, render, type RenderResult } from '@testing-library/svelte';
import RegisterUser from '$lib/partials/user/registerUser/RegisterUser.svelte';
import AlertUiTestHelpers from '$lib/testHelpers/uiTestHelpers/alertUiTestHelpers';

describe('RegisterUser', () => {
	let mockUserRegistrationService: IUserRegistrationService;
	let result: RenderResult<RegisterUser>;

	const renderComponent = (overrides: Partial<IUserRegistrationService> = {}) => {
		mockUserRegistrationService = {
			error: '',
			onRegister: vi.fn(),
			password: '',
			showError: false,
			username: '',
			onErrorAlertClose: vi.fn(),
			...overrides
		};

		if (result) result.unmount();

		result = render(RegisterUser, {
			props: {
				service: mockUserRegistrationService
			} as any
		});
	};

	beforeEach(() => {
		renderComponent();
	});

	it('should render a form', () => {
		expect(result.container.querySelector('form')).toBeInTheDocument();
	});

	it.each([
		['username', 'Username'],
		['password', 'Password']
	])('should render an input for %s, with the label %s', (testId, label) => {
		let fullTestId = `register-user-${testId}`;

		expect(result.getByTestId(fullTestId)).toBeInTheDocument();
		expect(result.getByLabelText(label)).toBeInTheDocument();
	});

	it("the password field should be of type 'password'", () => {
		expect(result.getByTestId('register-user-password')).toHaveAttribute('type', 'password');
	});

	it.each([
		['username', 'username'],
		['password', 'password']
	])(
		'should render the %s field with the given value',
		(field, key: keyof IUserRegistrationService) => {
			renderComponent({ [key]: 'Test Value' });

			expect(result.getByTestId(`register-user-${field}`)).toHaveValue('Test Value');
		}
	);

	it("should have a button with the text 'Register'", () => {
		let registerButton = result.getByTestId('register-user-button');

		expect(registerButton).toBeInTheDocument();
		expect(registerButton).toHaveTextContent('Register');
	});

	it('should call onRegister when clicking the button', () => {
		let onRegister = vi.fn();
		renderComponent({ onRegister });

		let registerButton = result.getByTestId('register-user-button');

		fireEvent.click(registerButton);

		expect(onRegister).toHaveBeenCalled();
	});

	it('should call onRegister when submitting the form', () => {
		let onRegister = vi.fn();
		renderComponent({ onRegister });

		let form = result.container.querySelector('form');

		fireEvent.submit(form);

		expect(onRegister).toHaveBeenCalled();
	});

	it('should render the error message when showError is true', () => {
		renderComponent({ showError: true, error: 'Test Error' });

		expect(result.getByTestId('register-user-error')).toHaveTextContent('Test Error');
	});

	it('should not render the error message when showError is false', () => {
		expect(result.queryByTestId('register-user-error')).not.toBeInTheDocument();
	});

	it('clicking the close button on the error message should call onErrorAlertClose', () => {
		let onErrorAlertClose = vi.fn();
		renderComponent({ showError: true, error: 'Test Error', onErrorAlertClose });

		AlertUiTestHelpers.closeAlert(result, 'register-user-error');

		expect(onErrorAlertClose).toHaveBeenCalled();
	});
});
