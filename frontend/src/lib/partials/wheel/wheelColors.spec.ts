import { getColorOfSlice } from '$lib/partials/wheel/wheelColors';

describe('getColorOfSlice', () => {
	it.each([
		[0, '#137121', '#FFFFFF'],
		[1, '#4271cf', '#050303'],
		[2, '#b12525', '#FFFFFF'],
		[3, '#8930b5', '#ffffff'],
		[4, '#cf961c', '#151515'],
		[5, '#03154c', '#FFFFFF'],
		[6, '#1aa828', '#FFFFFF'],
		[7, '#a50cb6', '#FFFFFF'],
		[8, '#ec0000', '#2c2525'],
		[9, '#e1bd1f', '#050303'],
		[10, '#137121', '#FFFFFF'],
		[11, '#4271cf', '#050303'],
		[12, '#b12525', '#FFFFFF'],
		[13, '#8930b5', '#ffffff'],
		[14, '#cf961c', '#151515'],
		[15, '#03154c', '#FFFFFF'],
		[16, '#1aa828', '#FFFFFF'],
		[17, '#a50cb6', '#FFFFFF'],
		[18, '#ec0000', '#2c2525'],
		[19, '#e1bd1f', '#050303']
	])('should return the correct color for slice %i', (index, background, text) => {
		const color = getColorOfSlice(index);

		expect(color.background).toBe(background);
		expect(color.text).toBe(text);
	});
});
