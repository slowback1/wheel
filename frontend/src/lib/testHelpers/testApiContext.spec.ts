import ApiContext from '$lib/api/apiContext';
import createTestApiContext from '$lib/testHelpers/testApiContext';
import MessageBus from '$lib/bus/MessageBus';
import { Messages } from '$lib/bus/Messages';

describe('testApiContext', () => {
	it('is an api context', () => {
		const apiContext = createTestApiContext();
		expect(apiContext).toBeInstanceOf(ApiContext);
	});

	it('sends the ApiContext as a message in the message bus', () => {
		const apiContext = createTestApiContext();
		const stored = MessageBus.getLastMessage(Messages.ApiContext);
		expect(stored).toBe(apiContext);
	});
});
