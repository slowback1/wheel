import BaseApi from '$lib/api/baseApi';
import type { CreateUserRequest, LoginRequest, TokenResponse } from '$lib/api/types';

export default class UserApi extends BaseApi {
	async createUser(username: string, password: string): Promise<TokenResponse> {
		const body: CreateUserRequest = {
			password,
			username
		};

		return await this.Post('User/Register', body);
	}

	async login(username: string, password: string): Promise<TokenResponse> {
		const body: LoginRequest = {
			password,
			username
		};

		return await this.Post('User/Login', body);
	}
}
