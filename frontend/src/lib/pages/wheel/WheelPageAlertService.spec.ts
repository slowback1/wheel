import { afterEach, beforeEach } from 'vitest';
import MessageBus from '$lib/bus/MessageBus';
import { Messages } from '$lib/bus/Messages';
import { getNSliceWheel } from '$lib/testHelpers/testData/testWheelSetting';
import type { WheelSlice } from '$lib/api/types';
import WheelPageAlertService from '$lib/pages/wheel/WheelPageAlertService.svelte';

function getTestSlice(): WheelSlice {
	return getNSliceWheel(1).slices[0];
}

describe('WheelPageAlertService', () => {
	let service: WheelPageAlertService;

	beforeEach(() => {
		service = new WheelPageAlertService();
	});

	afterEach(() => {
		MessageBus.clearAll();
	});

	it('should initially not show an alert', () => {
		expect(service.shouldShowAlert).toEqual(false);
	});

	describe('reacting to messages', () => {
		it('should indicate to show an alert when receiving a message that the wheel has spun', () => {
			MessageBus.sendMessage(Messages.WheelSpinResult, getTestSlice());

			expect(service.shouldShowAlert).toEqual(true);
		});

		it("shouldn't do anything if the message is null", () => {
			MessageBus.sendMessage(Messages.WheelSpinResult, null);

			expect(service.shouldShowAlert).toEqual(false);
		});

		it('should expose the text to show in the alert', () => {
			let slice = getTestSlice();
			slice.label = 'Rathalos';
			MessageBus.sendMessage(Messages.WheelSpinResult, slice);

			expect(service.alertText).toEqual("You landed on 'Rathalos'!");
		});

		it('unsubscribing from the message bus should prevent the service from reacting to messages', () => {
			service.unsubscribe();
			MessageBus.sendMessage(Messages.WheelSpinResult, getTestSlice());

			expect(service.shouldShowAlert).toEqual(false);
		});
	});

	describe('closing the alert', () => {
		beforeEach(() => {
			MessageBus.sendMessage(Messages.WheelSpinResult, getTestSlice());
		});

		it('Closing the alert should hide the alert', () => {
			service.onClose();

			expect(service.shouldShowAlert).toEqual(false);
		});

		it('Closing the alert unsets the name', () => {
			service.onClose();

			expect(service.alertText).toEqual('');
		});
	});
});
