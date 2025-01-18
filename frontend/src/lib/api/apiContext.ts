import SpinApi from '$lib/api/spinApi';
import MessageBus from '$lib/bus/MessageBus';
import { Messages } from '$lib/bus/Messages';
import UserApi from '$lib/api/userApi';

export default class ApiContext {
	public spinApi: SpinApi;
	public userApi: UserApi;

	constructor() {
		this.spinApi = new SpinApi();
		this.userApi = new UserApi();
	}

	static initialize() {
		MessageBus.configure.doNotStoreDataForMessage(Messages.ApiContext);
		MessageBus.sendMessage(Messages.ApiContext, new ApiContext());
	}

	static get(): ApiContext {
		let stored = MessageBus.getLastMessage<ApiContext>(Messages.ApiContext);

		return stored ?? new ApiContext();
	}
}
