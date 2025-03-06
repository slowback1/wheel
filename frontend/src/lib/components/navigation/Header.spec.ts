import Header from '$lib/components/navigation/Header.svelte';
import { type RenderResult, waitFor } from '@testing-library/svelte';
import { render } from '@testing-library/svelte';
import { beforeEach } from 'vitest';
import type { HeaderProps } from '$lib/components/navigation/navigationTypes';
import TestUserService from '$lib/testHelpers/serviceMocks/testUserService.svelte';
import MessageBus from '$lib/bus/MessageBus';
import { Messages } from '$lib/bus/Messages';

describe('Header', () => {
	let result: RenderResult<Header>;

	function renderComponent(overrides: Partial<HeaderProps> = {}) {
		if (result) result.unmount();

		const props: HeaderProps = {
			userService: new TestUserService(),
			...overrides
		};

		result = render(Header, { props: props as any });

		MessageBus.sendMessage(Messages.UserSession, {});
	}

	beforeEach(() => {
		renderComponent();
	});

	it('renders the nav', () => {
		let nav = result.getByTestId('header');

		expect(nav).toBeInTheDocument();
	});

	it('contains a skip to content link', () => {
		expect(result.container.querySelector("[href='#content']")).toBeInTheDocument();
	});

	it('contains a link to the home page', () => {
		expect(result.container.querySelector("[href='/']")).toBeInTheDocument();
	});

	it('contains a link to register when the user is not logged in', () => {
		let notLoggedInUserService = new TestUserService();
		notLoggedInUserService.shouldBeLoggedIn = false;

		renderComponent({ userService: notLoggedInUserService });

		expect(result.container.querySelector("[href='/user/register']")).toBeInTheDocument();
	});

	it('does not contain a link to register when the user is logged in', async () => {
		let loggedInUserService = new TestUserService();
		loggedInUserService.shouldBeLoggedIn = true;

		renderComponent({ userService: loggedInUserService });

		await waitFor(() => {
			expect(result.container.querySelector("[href='/user/register']")).not.toBeInTheDocument();
		});
	});
});
