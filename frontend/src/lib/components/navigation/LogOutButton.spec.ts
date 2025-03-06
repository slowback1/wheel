import { render, type RenderResult } from '@testing-library/svelte';
import LogOutButton from '$lib/components/navigation/LogOutButton.svelte';
import type { IUserService } from '$lib/services/User/UserService.svelte';
import { beforeEach } from 'vitest';
import TestUserService from '$lib/testHelpers/serviceMocks/testUserService.svelte';

describe('Log Out Button', () => {
	let result: RenderResult<LogOutButton>;

	function renderComponent(service: IUserService) {
		if (result) result.unmount();

		result = render(LogOutButton, { props: { userService: service } as any });
	}

	beforeEach(() => {
		let userService = new TestUserService();

		renderComponent(userService);
	});

	it('renders a log out button', () => {
		let button = result.getByTestId('log-out-button');

		expect(button).toBeInTheDocument();
	});

	it('clicking the button will call the logOut method on the user service', () => {
		let userService = new TestUserService();
		userService.logOut = vi.fn();

		renderComponent(userService);

		let button = result.getByTestId('log-out-button');
		button.click();

		expect(userService.logOut).toHaveBeenCalled();
	});
});
