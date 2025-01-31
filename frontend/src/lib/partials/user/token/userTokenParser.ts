import type { UserSessionData } from '$lib/partials/user/userTypes';

type JwtPayload = {
	exp: number;
	iat: number;
	id: string;
	nbf: number;
};

export default class UserTokenParser {
	parse(token: string): UserSessionData | null {
		try {
			let payload = this.parseJwt(token);

			return this.buildUserSessionData(payload);
		} catch {
			return null;
		}
	}

	private buildUserSessionData(payload: JwtPayload): UserSessionData {
		return {
			id: payload.id,
			expires: new Date(payload.exp * 1000).toISOString()
		};
	}

	private parseJwt(token: string): JwtPayload {
		const base64Url = token.split('.')[1];
		const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
		const jsonPayload = decodeURIComponent(
			atob(base64)
				.split('')
				.map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
				.join('')
		);

		return JSON.parse(jsonPayload);
	}
}
