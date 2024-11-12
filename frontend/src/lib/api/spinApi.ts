import BaseApi from '$lib/api/baseApi';
import type { SpinResult, WheelRequest } from '$lib/api/types';

export default class SpinApi extends BaseApi {
	async spin(request: WheelRequest): Promise<SpinResult> {
		return await this.Post('Spin/Spin', request);
	}
}
