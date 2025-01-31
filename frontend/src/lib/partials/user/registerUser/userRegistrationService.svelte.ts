import MessageBus from '$lib/bus/MessageBus';
import type ApiContext from '$lib/api/apiContext';
import { Messages } from '$lib/bus/Messages';
import UserTokenParser from '$lib/partials/user/token/userTokenParser';

export default class UserRegistrationService {
	constructor(private onRegisterSuccessful: () => void) {}

	public username: string = $state('');
	public password: string = $state('');
	public error: string = $state('');
	public showError: boolean = $state(false);
	private tokenParser = new UserTokenParser();

	public async onRegister() {
		this.resetErrorState();

		try {
			await this.handleRegistrationRequest();
		} catch (error) {
			this.handleError(error);
		}
	}

	private async handleRegistrationRequest() {
		let api = MessageBus.getLastMessage<ApiContext>(Messages.ApiContext);
		let token = await api.userApi.createUser(this.username, this.password);
		this.handleRegistrationResponse(token);
	}

	private handleRegistrationResponse(token: string) {
		MessageBus.sendMessage(Messages.UserToken, token);
		MessageBus.sendMessage(Messages.UserSession, this.tokenParser.parse(token));

		this.onRegisterSuccessful();
	}

	private handleError(error: string) {
		this.error = error;
		this.showError = true;
	}

	private resetErrorState() {
		this.showError = false;
		this.error = '';
	}
}
