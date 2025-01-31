import UserTokenParser from '$lib/partials/user/token/userTokenParser';

describe('userTokenParser', () => {
	const VALID_TOKEN =
		'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IlRlc3RVc2VyMSIsIm5iZiI6MTczODI5MTkxNiwiZXhwIjoxNzM4ODk2NzE2LCJpYXQiOjE3MzgyOTE5MTZ9.Rar4MuSCcS2Kob5xgHIGXKVYWCj0DJ5FPjrn-irm7OQ';
	const INVALID_TOKEN = 'NOT_VALID';
	const BLANK_TOKEN = '';

	const parser = new UserTokenParser();

	it('should return null with a blank token', () => {
		expect(parser.parse(BLANK_TOKEN)).toBeNull();
	});

	it('should return a valid user session data object with a valid token', () => {
		const result = parser.parse(VALID_TOKEN);

		expect(result.id).toEqual('TestUser1');
	});

	it('should return a valid user session data object with a valid expire date', () => {
		const result = parser.parse(VALID_TOKEN);

		expect(result.expires).toEqual('2025-02-07T02:51:56.000Z');
	});

	it('returns null for an invalid token', () => {
		expect(parser.parse(INVALID_TOKEN)).toBeNull();
	});
});
