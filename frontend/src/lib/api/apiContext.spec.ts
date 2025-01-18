import SpinApi from '$lib/api/spinApi';
import ApiContext from '$lib/api/apiContext';
import { beforeEach } from 'vitest';
import MessageBus from '$lib/bus/MessageBus';
import getLocalStorageMock from '$lib/testHelpers/localStorageMock';
import type IStorageProvider from '$lib/bus/IStorageProvider';
import { Messages } from '$lib/bus/Messages';
import UserApi from '$lib/api/userApi';

describe('ApiContext', () => {
	it.each([
		['spinApi', SpinApi],
		['userApi', UserApi]
	])('has property %s', (property, expectedClass) => {
		const apiContext = new ApiContext();
		expect(apiContext[property]).toBeInstanceOf(expectedClass);
	});

	describe('getting the api context', () => {
		beforeEach(() => {
			MessageBus.clearAll();
		});

		it('returns the message bus variation of the api context', () => {
			const context = new ApiContext();
			MessageBus.sendMessage(Messages.ApiContext, context);

			const result = ApiContext.get();

			expect(result).toBe(context);
		});

		it("returns a new instance of the api context if it hasn't been set", () => {
			const result = ApiContext.get();

			expect(result).toBeInstanceOf(ApiContext);
		});
	});

	describe('on initialization', () => {
		let storageMock: IStorageProvider;

		beforeEach(() => {
			storageMock = getLocalStorageMock();

			MessageBus.initialize(storageMock);

			ApiContext.initialize();
		});

		it('sends the ApiContext as a message in the message bus', () => {
			let context = MessageBus.getLastMessage(Messages.ApiContext);

			expect(context).toBeInstanceOf(ApiContext);
		});

		it('does not store the data for the message in the message bus', () => {
			let stored = storageMock.getItem(Messages.ApiContext);

			expect(stored).toBeUndefined();
		});
	});
});
