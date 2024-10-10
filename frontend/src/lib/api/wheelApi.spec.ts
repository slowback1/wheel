import { vi, type Mock, expect } from 'vitest';
import { getFetchMock } from '$lib/testHelpers/getFetchMock';
import getTestWheelSetting from '$lib/testHelpers/testData/testWheelSetting';
import WheelApi from '$lib/api/wheelApi';

describe('wheelApi', () => {
	let mockFetch: Mock;

	it('GetWheelSetting calls the correct url', async () => {
		mockFetch = getFetchMock(getTestWheelSetting());
		const wheelApi = new WheelApi();
		await wheelApi.GetWheelSetting('id');
		expect(mockFetch).toHaveBeenCalledWith('/Wheel/id', expect.anything());
	});

	it('GetWheelSettings calls the correct url', async () => {
		mockFetch = getFetchMock([getTestWheelSetting()]);
		const wheelApi = new WheelApi();
		await wheelApi.GetWheelSettings();
		expect(mockFetch).toHaveBeenCalledWith('/Wheel', expect.anything());
	});
});
