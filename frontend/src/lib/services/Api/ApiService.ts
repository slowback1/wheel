import WheelApi from '$lib/api/wheelApi';
import MessageBus from '$lib/bus/MessageBus';
import { Messages } from '$lib/bus/Messages';

export default class ApiService {
	public static initialize() {
		MessageBus.configure.doNotStoreDataForMessage(Messages.Api);
		MessageBus.sendMessage(Messages.Api, this.BuildApi());
	}

	private static BuildApi(): Api {
		return {
			Wheel: new WheelApi()
		};
	}
}

export type Api = {
	Wheel: WheelApi;
};
