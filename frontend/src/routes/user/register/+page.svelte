<script lang="ts">
	import RegisterUser from '$lib/partials/user/registerUser/RegisterUser.svelte';
	import UserRegistrationService from '$lib/partials/user/registerUser/userRegistrationService.svelte';
	import { goto } from '$app/navigation';

	function onRegisterSuccess() {
		goto('/');
	}

	let service = new UserRegistrationService(onRegisterSuccess);
</script>

<RegisterUser {service} />
