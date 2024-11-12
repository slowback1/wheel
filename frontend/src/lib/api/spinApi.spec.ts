import SpinApi from '$lib/api/spinApi';
import { getFetchMock } from '$lib/testHelpers/getFetchMock';
import { getTestWheelSetting } from '$lib/testHelpers/testData/testWheelSetting';

describe('spinApi', () => {
	let api = new SpinApi();

	it('should be defined', () => {
		expect(api).toBeDefined();
	});

	it('should have a method for spinning a wheel', async () => {
		let mockFetch = getFetchMock({});

		await api.spin({ wheelSetting: getTestWheelSetting() });

		expect(mockFetch).toHaveBeenCalled();

		const [url, options] = mockFetch.mock.calls[0] as unknown as [string, RequestInit];

		expect(url).toEqual('/Spin/Spin');
		expect(options.method).toEqual('POST');
	});
});
