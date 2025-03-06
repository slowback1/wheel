<script lang="ts">
	import { goto } from '$app/navigation';
	import UserLoginService from '$lib/partials/user/loginUser/userLoginService.svelte';
	import LoginUser from '$lib/partials/user/loginUser/LoginUser.svelte';

	function onLoginSuccess() {
		goto('/');
	}

	let service = new UserLoginService(onLoginSuccess);
</script>

<LoginUser {service} />
