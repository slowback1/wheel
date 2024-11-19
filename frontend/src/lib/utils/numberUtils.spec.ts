import { getRandomNumber } from '$lib/utils/numberUtils';

describe('numberUtils', () => {
	describe('getRandomNumber', () => {
		it.each([
			[0, 1],
			[1, 10],
			[10, 100],
			[100, 1000]
		])('should return a number between %s and %s', (min, max) => {
			const randomNumber = getRandomNumber(min, max);
			expect(randomNumber).toBeGreaterThanOrEqual(min);
			expect(randomNumber).toBeLessThanOrEqual(max);
		});
	});
});
