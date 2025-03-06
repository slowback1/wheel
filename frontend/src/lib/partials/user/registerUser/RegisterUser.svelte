<script lang="ts">
	import type { RegisterUserProps } from '$lib/partials/user/registerUser/RegisterUser.types';
	import TextBox from '$lib/ui/inputs/TextBox/TextBox.svelte';
	import Button from '$lib/ui/buttons/Button/Button.svelte';
	import Alert from '$lib/ui/containers/alert/Alert.svelte';
	import { AlertType } from '$lib/ui/containers/alert/alertTypes';

	const { service }: RegisterUserProps = $props();
</script>

<form class="register-user" onsubmit={() => service.onRegister()}>
	{#if service.showError}
		<Alert
			type={AlertType.Error}
			message={service.error}
			testId="register-user-error"
			onClose={service.onErrorAlertClose}
		/>
	{/if}

	<TextBox id="register-user-username" label="Username" bind:value={service.username} />
	<TextBox
		id="register-user-password"
		label="Password"
		type="password"
		bind:value={service.password}
	/>
	<Button size="small" testId="register-user-button" variant="primary">Register</Button>
</form>

<style>
	.register-user {
		display: flex;
		flex-direction: column;
		align-items: center;
		gap: 6px;
	}
</style>
