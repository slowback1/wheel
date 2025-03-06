import type { Meta } from '@storybook/svelte';
import RegisterUser from '$lib/partials/user/registerUser/RegisterUser.svelte';
import type { IUserRegistrationService } from '$lib/partials/user/registerUser/RegisterUser.types';
import LoginUser from '$lib/partials/user/loginUser/LoginUser.svelte';
import type { IUserLoginService } from '$lib/partials/user/loginUser/LoginUser.types';

const meta: Meta = {
	title: 'Pages/User/Login',
	component: LoginUser
};

export default meta;

function makeProps(overrides: Partial<IUserLoginService> = {}): IUserLoginService {
	return {
		error: '',
		onLogin: () => {
			return Promise.resolve();
		},
		password: '',
		showError: false,
		username: '',
		onErrorAlertClose() {},
		...overrides
	};
}

export const Default = {
	args: { service: makeProps() }
};
