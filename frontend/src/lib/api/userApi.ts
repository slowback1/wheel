import BaseApi from '$lib/api/baseApi';
import type { CreateUserRequest, LoginRequest } from '$lib/api/types';

export default class UserApi extends BaseApi {
	async createUser(username: string, password: string): Promise<string> {
		const body: CreateUserRequest = {
			password,
			username
		};

		return await this.Post('User/Register', body);
	}

	async login(username: string, password: string): Promise<string> {
		const body: LoginRequest = {
			password,
			username
		};

		return await this.Post('User/Login', body);
	}
}
