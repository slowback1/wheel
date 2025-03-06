export interface RegisterUserProps {
	service: IUserRegistrationService;
}
export interface IUserRegistrationService {
	username: string;
	password: string;
	error: string;
	showError: boolean;
	onRegister(): Promise<void>;
	onErrorAlertClose(): void;
}
