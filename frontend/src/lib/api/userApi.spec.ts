import UserApi from '$lib/api/userApi';
import { mockApi } from '$lib/testHelpers/getFetchMock';

describe('UserApi', () => {
	let api: UserApi = new UserApi();

	it('should call the correct url and method when calling createUser', async () => {
		let mock = mockApi({
			'User/Register': 'test'
		});

		const result = await api.createUser('test', 'test');

		expect(result).toBe('test');

		expect(mock).toHaveBeenCalledTimes(1);

		const [url, options] = mock.mock.calls[0];

		expect(url).toBe('/User/Register');

		expect(options.method).toBe('POST');
	});

	it('should call the correct url and method when calling login', async () => {
		let mock = mockApi({
			'User/Login': 'test'
		});

		const result = await api.login('test', 'test');

		expect(result).toBe('test');

		expect(mock).toHaveBeenCalledTimes(1);

		const [url, options] = mock.mock.calls[0];

		expect(url).toBe('/User/Login');

		expect(options.method).toBe('POST');
	});
});
