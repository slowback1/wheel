import BaseApi from '$lib/api/baseApi';

export type WheelSetting = {
	name: string;
	slices: WheelSlice[];
};

export type WheelSlice = {
	label: string;
	size: number;
};

export default class WheelApi extends BaseApi {
	constructor() {
		super();
	}

	public async GetWheelSetting(id: string): Promise<WheelSetting> {
		return this.Get(`Wheel/${id}`);
	}

	public async GetWheelSettings(): Promise<WheelSetting[]> {
		return this.Get('Wheel');
	}
}
