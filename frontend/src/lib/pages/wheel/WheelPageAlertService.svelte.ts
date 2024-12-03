import MessageBus from '$lib/bus/MessageBus';
import { Messages } from '$lib/bus/Messages';
import type { WheelSlice } from '$lib/api/types';

export default class WheelPageAlertService {
	shouldShowAlert = $state(false);
	unsubscribe: () => void;
	private landedName = $state('');
	alertText = $derived(!!this.landedName ? `You landed on '${this.landedName}'!` : '');

	constructor() {
		this.initialize();
	}

	public onClose() {
		this.shouldShowAlert = false;
		this.landedName = '';
	}

	private initialize() {
		this.unsubscribe = MessageBus.subscribe<WheelSlice>(Messages.WheelSpinResult, (result) => {
			if (!result) return;

			this.shouldShowAlert = true;
			this.landedName = result.label;
		});
	}
}
