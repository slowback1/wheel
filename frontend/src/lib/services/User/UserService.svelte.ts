import type { UserSessionData } from '$lib/partials/user/userTypes';
import MessageBus from '$lib/bus/MessageBus';
import { Messages } from '$lib/bus/Messages';

export interface IUserService {
	isLoggedIn(): boolean;
	getUserId(): string;
}

export default class UserService implements IUserService {
	isLoggedIn(): boolean {
		let session = this.getUserSession();

		if (session) {
			return this.sessionIsStillActive(session);
		}

		return false;
	}

	private sessionIsStillActive(session: UserSessionData) {
		let expiration = new Date(session.expires);
		let now = new Date();

		return expiration >= now;
	}

	private getUserSession(): UserSessionData | null {
		return MessageBus.getLastMessage<UserSessionData>(Messages.UserSession);
	}

	getUserId(): string {
		let session = this.getUserSession();

		if (session) {
			return session.id;
		}

		return '';
	}
}
