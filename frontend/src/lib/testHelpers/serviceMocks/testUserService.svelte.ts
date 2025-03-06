import type { IUserService } from '$lib/services/User/UserService.svelte';

export default class TestUserService implements IUserService {
	public shouldBeLoggedIn: boolean = $state(false);
	public userId: string = $state('testUserId');

	constructor() {}

	isLoggedIn(): boolean {
		return this.shouldBeLoggedIn;
	}

	getUserId(): string {
		return this.userId;
	}

	logOut() {}
}
