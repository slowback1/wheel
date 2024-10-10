import ApiService, { type Api } from '$lib/services/Api/ApiService';
import MessageBus from '$lib/bus/MessageBus';
import { Messages } from '$lib/bus/Messages';
import BaseApi from '$lib/api/baseApi';

describe('ApiService', () => {
	it('should send a message in the message bus when the initializer is called', () => {
		ApiService.initialize();

		let api = MessageBus.getLastMessage(Messages.Api);

		expect(api).toBeTruthy();
	});

	const apiKeys: (keyof Api)[] = ['Wheel'];

	it.each([apiKeys])('should have the key %s', (key) => {
		ApiService.initialize();

		let api = MessageBus.getLastMessage(Messages.Api);

		expect(api).toHaveProperty(key);
	});

	it.each([apiKeys])('should have the key %s which is part of the BaseApi class', (key) => {
		ApiService.initialize();

		let api = MessageBus.getLastMessage(Messages.Api);

		expect(api[key]).toBeInstanceOf(BaseApi);
	});
});
