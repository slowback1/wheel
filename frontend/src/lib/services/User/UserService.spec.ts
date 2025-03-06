import { beforeEach } from 'vitest';
import MessageBus from '$lib/bus/MessageBus';
import { Messages } from '$lib/bus/Messages';
import type { UserSessionData } from '$lib/partials/user/userTypes';
import UserService from '$lib/services/User/UserService.svelte';

describe('UserService', () => {
	beforeEach(() => {
		MessageBus.clearAll();
	});

	function logInUser(expired: boolean = false) {
		let date = new Date();

		if (expired) {
			date.setTime(date.getTime() - 1000);
		} else {
			date.setTime(date.getTime() + 1000);
		}

		let testSessionData: UserSessionData = {
			expires: date.toISOString(),
			id: 'testUserId'
		};

		MessageBus.sendMessage(Messages.UserSession, testSessionData);
		MessageBus.sendMessage(Messages.UserToken, 'testToken');
	}

	describe('isLoggedIn', () => {
		it('returns false if the user session data is not in the message bus', () => {
			let userService = new UserService();
			expect(userService.isLoggedIn()).toBe(false);
		});

		it('returns true if the user session data ', () => {
			logInUser();
			let userService = new UserService();
			expect(userService.isLoggedIn()).toBe(true);
		});

		it('returns false if the user session data is expired', () => {
			logInUser(true);
			let userService = new UserService();
			expect(userService.isLoggedIn()).toBe(false);
		});
	});

	describe('getUserId', () => {
		it('returns an empty string if no user session data is present', () => {
			let userService = new UserService();
			expect(userService.getUserId()).toBe('');
		});

		it('returns the user id if user session data is present', () => {
			logInUser();
			let userService = new UserService();
			expect(userService.getUserId()).toBe('testUserId');
		});
	});

	describe('logOut', () => {
		beforeEach(() => {
			logInUser();
		});

		it('clears the user session data', () => {
			let userService = new UserService();
			userService.logOut();

			expect(MessageBus.getLastMessage(Messages.UserSession)).toBeNull();
		});

		it('clears the user token data', () => {
			let userService = new UserService();
			userService.logOut();

			expect(MessageBus.getLastMessage(Messages.UserToken)).toBeNull();
		});
	});
});
