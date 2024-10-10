import type { Api } from '$lib/services/Api/ApiService';
import MessageBus from '$lib/bus/MessageBus';
import { Messages } from '$lib/bus/Messages';

export default function getTestApi(): Api {
	return MessageBus.getLastMessage(Messages.Api);
}
