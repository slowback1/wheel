import { beforeEach } from 'vitest';
import { fireEvent, render, type RenderResult } from '@testing-library/svelte';
import AlertUiTestHelpers from '$lib/testHelpers/uiTestHelpers/alertUiTestHelpers';
import LoginUser from '$lib/partials/user/loginUser/LoginUser.svelte';
import type { IUserLoginService } from '$lib/partials/user/loginUser/LoginUser.types';

describe('LoginUser', () => {
	let mockUserRegistrationService: IUserLoginService;
	let result: RenderResult<LoginUser>;

	const renderComponent = (overrides: Partial<IUserLoginService> = {}) => {
		mockUserRegistrationService = {
			error: '',
			onLogin: vi.fn(),
			password: '',
			showError: false,
			username: '',
			onErrorAlertClose: vi.fn(),
			...overrides
		};

		if (result) result.unmount();

		result = render(LoginUser, {
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
		let fullTestId = `login-user-${testId}`;

		expect(result.getByTestId(fullTestId)).toBeInTheDocument();
		expect(result.getByLabelText(label)).toBeInTheDocument();
	});

	it("the password field should be of type 'password'", () => {
		expect(result.getByTestId('login-user-password')).toHaveAttribute('type', 'password');
	});

	it.each([
		['username', 'username'],
		['password', 'password']
	])('should render the %s field with the given value', (field, key: keyof IUserLoginService) => {
		renderComponent({ [key]: 'Test Value' });

		expect(result.getByTestId(`login-user-${field}`)).toHaveValue('Test Value');
	});

	it("should have a button with the text 'login'", () => {
		let loginButton = result.getByTestId('login-user-button');

		expect(loginButton).toBeInTheDocument();
		expect(loginButton).toHaveTextContent('Login');
	});

	it('should call onLogin when clicking the button', () => {
		let onLogin = vi.fn();
		renderComponent({ onLogin });

		let loginButton = result.getByTestId('login-user-button');

		fireEvent.click(loginButton);

		expect(onLogin).toHaveBeenCalled();
	});

	it('should call onLogin when submitting the form', () => {
		let onLogin = vi.fn();
		renderComponent({ onLogin });

		let form = result.container.querySelector('form');

		fireEvent.submit(form);

		expect(onLogin).toHaveBeenCalled();
	});

	it('should render the error message when showError is true', () => {
		renderComponent({ showError: true, error: 'Test Error' });

		expect(result.getByTestId('login-user-error')).toHaveTextContent('Test Error');
	});

	it('should not render the error message when showError is false', () => {
		expect(result.queryByTestId('login-user-error')).not.toBeInTheDocument();
	});

	it('clicking the close button on the error message should call onErrorAlertClose', () => {
		let onErrorAlertClose = vi.fn();
		renderComponent({ showError: true, error: 'Test Error', onErrorAlertClose });

		AlertUiTestHelpers.closeAlert(result, 'login-user-error');

		expect(onErrorAlertClose).toHaveBeenCalled();
	});
});
