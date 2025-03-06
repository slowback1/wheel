import MessageBus from '$lib/bus/MessageBus';
import type ApiContext from '$lib/api/apiContext';
import { Messages } from '$lib/bus/Messages';
import UserTokenParser from '$lib/partials/user/token/userTokenParser';
import type { IUserRegistrationService } from '$lib/partials/user/registerUser/RegisterUser.types';

export default class UserRegistrationService implements IUserRegistrationService {
	constructor(private onRegisterSuccessful: () => void) {}

	username: string = $state('');
	password: string = $state('');
	error: string = $state('');
	showError: boolean = $state(false);
	private tokenParser = new UserTokenParser();

	async onRegister() {
		this.resetErrorState();

		try {
			await this.handleRegistrationRequest();
		} catch (error) {
			this.handleError(error);
		}
	}

	onErrorAlertClose(): void {
		this.showError = false;
		this.error = '';
	}

	private async handleRegistrationRequest() {
		let api = MessageBus.getLastMessage<ApiContext>(Messages.ApiContext);
		let { token } = await api.userApi.createUser(this.username, this.password);
		this.handleRegistrationResponse(token);
	}

	private handleRegistrationResponse(token: string) {
		MessageBus.sendMessage(Messages.UserToken, token);
		MessageBus.sendMessage(Messages.UserSession, this.tokenParser.parse(token));

		this.onRegisterSuccessful();
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
