<script lang="ts">
	import SkipToContentLink from '$lib/components/navigation/SkipToContentLink.svelte';
	import HeaderLink from '$lib/components/navigation/HeaderLink.svelte';
	import ThemeToggle from '$lib/components/navigation/ThemeToggle.svelte';
	import type { HeaderProps } from '$lib/components/navigation/navigationTypes';
	import { onMount } from 'svelte';
	import MessageBus from '$lib/bus/MessageBus';
	import { Messages } from '$lib/bus/Messages';
	import LogOutButton from '$lib/components/navigation/LogOutButton.svelte';

	const { userService } = $props() as HeaderProps;

	let isLoggedIn = $state(false);

	onMount(() => {
		MessageBus.subscribe(Messages.UserSession, () => {
			isLoggedIn = userService.isLoggedIn();
		});
	});
</script>

<nav data-testid="header">
	<SkipToContentLink />

	<HeaderLink href="/" label="Home" />
	{#if !isLoggedIn}
		<HeaderLink href="/user/login" label="Login" />
		<HeaderLink href="/user/register" label="Register" />
	{:else}
		<LogOutButton {userService} />
	{/if}

	<ThemeToggle />
</nav>

<style>
	nav {
		display: flex;
		flex-direction: row;
		justify-content: flex-end;
		align-items: center;
		margin: 0;
		padding-inline: var(--gutters-x);
		background-color: var(--color-primary-background);
		color: var(--color-primary-font);
		height: var(--header-height);
		gap: 12px;
	}
</style>
