import MessageBus from '$lib/bus/MessageBus';
import type ApiContext from '$lib/api/apiContext';
import { Messages } from '$lib/bus/Messages';
import UserTokenParser from '$lib/partials/user/token/userTokenParser';
import type { IUserLoginService } from '$lib/partials/user/loginUser/LoginUser.types';

export default class UserLoginService implements IUserLoginService {
	constructor(private onLoginSuccessful: () => void) {}

	username: string = $state('');
	password: string = $state('');
	error: string = $state('');
	showError: boolean = $state(false);
	private tokenParser = new UserTokenParser();

	async onLogin() {
		this.resetErrorState();

		try {
			await this.handleLoginRequest();
		} catch (error) {
			this.handleError(error);
		}
	}

	onErrorAlertClose(): void {
		this.showError = false;
		this.error = '';
	}

	private async handleLoginRequest() {
		let api = MessageBus.getLastMessage<ApiContext>(Messages.ApiContext);
		let { token } = await api.userApi.login(this.username, this.password);
		this.handleRegistrationResponse(token);
	}

	private handleRegistrationResponse(token: string) {
		MessageBus.sendMessage(Messages.UserToken, token);
		MessageBus.sendMessage(Messages.UserSession, this.tokenParser.parse(token));

		this.onLoginSuccessful();
	}

	private handleError(error: Error) {
		this.error = error.message;
		this.showError = true;
	}

	private resetErrorState() {
		this.showError = false;
		this.error = '';
	}
}
