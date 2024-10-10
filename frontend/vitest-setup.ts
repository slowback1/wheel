import '@testing-library/jest-dom/vitest';
import ApiService from './src/lib/services/Api/ApiService';
import { Messages } from './src/lib/bus/Messages';
import MessageBus from './src/lib/bus/MessageBus';
import { vi } from 'vitest';

function mockTheApi() {
	ApiService.initialize();

	let api = MessageBus.getLastMessage(Messages.Api);

	Object.values(api).forEach((cls) => {
		Object.keys(cls).forEach((method) => {
			cls[method] = vi.fn();
		});
	});

	global.api = api;
}

mockTheApi();
