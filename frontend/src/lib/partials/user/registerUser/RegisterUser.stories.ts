import type { Meta } from '@storybook/svelte';
import RegisterUser from '$lib/partials/user/registerUser/RegisterUser.svelte';
import type { IUserRegistrationService } from '$lib/partials/user/registerUser/RegisterUser.types';

const meta: Meta = {
	title: 'Pages/User/Register',
	component: RegisterUser
};

export default meta;

function makeProps(overrides: Partial<IUserRegistrationService> = {}): IUserRegistrationService {
	return {
		error: '',
		onRegister: () => {
			return Promise.resolve();
		},
		password: '',
		showError: false,
		username: '',
		...overrides
	};
}

export const Default = {
	args: { service: makeProps() }
};
