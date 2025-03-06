import type { Mock } from '@storybook/test';
import type ApiContext from '$lib/api/apiContext';
import createTestApiContext from '$lib/testHelpers/testApiContext';
import MessageBus from '$lib/bus/MessageBus';
import { Messages } from '$lib/bus/Messages';
import type { UserSessionData } from '$lib/partials/user/userTypes';
import UserLoginService from '$lib/partials/user/loginUser/userLoginService.svelte';

describe('userRegistrationService', () => {
	let onLogin: Mock;
	let testApi: ApiContext;
	let registrationService: UserLoginService;

	beforeEach(() => {
		onLogin = vi.fn();
		testApi = createTestApiContext();
		testApi.userApi.login = vi.fn(() => Promise.resolve({ token: 'yes' }));

		registrationService = new UserLoginService(onLogin);
	});

	it("should call the userApi's createUser method with the correct parameters", async () => {
		registrationService.username = 'username';
		registrationService.password = 'password';

		await registrationService.onLogin();

		expect(testApi.userApi.login).toHaveBeenCalledWith('username', 'password');
	});

	it("should call the onRegisterSuccessful callback when the userApi's createUser method resolves", async () => {
		await registrationService.onLogin();

		expect(onLogin).toHaveBeenCalled();
	});

	it("should not call the onRegisterSuccessful callback when the userApi's createUser method rejects", async () => {
		testApi.userApi.login = vi.fn(() => Promise.reject('no'));

		await registrationService.onLogin();

		expect(onLogin).not.toHaveBeenCalled();
	});

	it("should set the error message when the userApi's createUser method rejects", async () => {
		testApi.userApi.login = vi.fn(() => Promise.reject(new Error('no')));

		await registrationService.onLogin();

		expect(registrationService.error).toBe('no');
	});

	it('a successful registration should set the user token to the response', async () => {
		await registrationService.onLogin();

		let token = MessageBus.getLastMessage<string>(Messages.UserToken);

		expect(token).toBe('yes');
	});

	it('successful registration should set the session data to the message bus', async () => {
		const validToken =
			'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IlRlc3RVc2VyMSIsIm5iZiI6MTczODI5MTkxNiwiZXhwIjoxNzM4ODk2NzE2LCJpYXQiOjE3MzgyOTE5MTZ9.Rar4MuSCcS2Kob5xgHIGXKVYWCj0DJ5FPjrn-irm7OQ';

		testApi.userApi.login = vi.fn(() => Promise.resolve({ token: validToken }));

		await registrationService.onLogin();

		let sessionData = MessageBus.getLastMessage<UserSessionData>(Messages.UserSession);

		expect(sessionData.id).toEqual('TestUser1');
	});

	it('erroring sets showError to true', async () => {
		testApi.userApi.login = vi.fn(() => Promise.reject('no'));

		await registrationService.onLogin();

		expect(registrationService.showError).toBe(true);
	});

	it('erroring, then trying again, should reset the error message and set showError to false', async () => {
		testApi.userApi.login = vi.fn(() => Promise.reject('no'));

		await registrationService.onLogin();

		expect(registrationService.showError).toBe(true);

		testApi.userApi.login = vi.fn(() => Promise.resolve({ token: 'yes' }));

		await registrationService.onLogin();

		expect(registrationService.showError).toBe(false);
		expect(registrationService.error).toBe('');
	});

	it('onErrorAlertClose should reset the error state', () => {
		registrationService.showError = true;
		registrationService.error = 'Test Error';

		registrationService.onErrorAlertClose();

		expect(registrationService.showError).toBe(false);
		expect(registrationService.error).toBe('');
	});
});
