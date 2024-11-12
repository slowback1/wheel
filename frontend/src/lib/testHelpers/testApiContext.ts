import ApiContext from '$lib/api/apiContext';
import MessageBus from '$lib/bus/MessageBus';
import { Messages } from '$lib/bus/Messages';

export default function createTestApiContext(): ApiContext {
	let context = new ApiContext();

	MessageBus.sendMessage(Messages.ApiContext, context);

	return context;
}
