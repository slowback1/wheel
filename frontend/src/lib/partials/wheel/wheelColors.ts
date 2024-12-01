export interface SliceColor {
	background: string;
	text: string;
}

const sliceColors: SliceColor[] = [
	{
		background: '#137121',
		text: '#FFFFFF'
	},
	{
		background: '#4271cf',
		text: '#050303'
	},
	{
		background: '#b12525',
		text: '#FFFFFF'
	},
	{
		background: '#8930b5',
		text: '#ffffff'
	},
	{
		background: '#cf961c',
		text: '#151515'
	},
	{
		background: '#03154c',
		text: '#FFFFFF'
	},
	{
		background: '#1aa828',
		text: '#FFFFFF'
	},
	{
		background: '#a50cb6',
		text: '#FFFFFF'
	},
	{
		background: '#ec0000',
		text: '#2c2525'
	},
	{
		background: '#e1bd1f',
		text: '#050303'
	}
];

export const getColorOfSlice = (index: number): SliceColor => {
	return sliceColors[index % sliceColors.length];
};
