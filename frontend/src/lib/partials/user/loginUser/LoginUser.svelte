<script lang="ts">
	import TextBox from '$lib/ui/inputs/TextBox/TextBox.svelte';
	import Button from '$lib/ui/buttons/Button/Button.svelte';
	import Alert from '$lib/ui/containers/alert/Alert.svelte';
	import { AlertType } from '$lib/ui/containers/alert/alertTypes';
	import type { LoginUserProps } from '$lib/partials/user/loginUser/LoginUser.types';

	const { service }: LoginUserProps = $props();
</script>

<form class="login-user" onsubmit={() => service.onLogin()}>
	{#if service.showError}
		<Alert
			type={AlertType.Error}
			message={service.error}
			testId="login-user-error"
			onClose={service.onErrorAlertClose}
		/>
	{/if}

	<TextBox id="login-user-username" label="Username" bind:value={service.username} />
	<TextBox
		id="login-user-password"
		label="Password"
		type="password"
		bind:value={service.password}
	/>
	<Button size="small" testId="login-user-button" variant="primary">Login</Button>
</form>

<style>
	.login-user {
		display: flex;
		flex-direction: column;
		align-items: center;
		gap: 6px;
	}
</style>
