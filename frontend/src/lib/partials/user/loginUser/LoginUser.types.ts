export interface LoginUserProps {
	service: IUserLoginService;
}
export interface IUserLoginService {
	username: string;
	password: string;
	error: string;
	showError: boolean;
	onLogin(): Promise<void>;
	onErrorAlertClose(): void;
}
