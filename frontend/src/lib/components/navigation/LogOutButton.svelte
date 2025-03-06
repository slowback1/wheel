<script lang="ts">
	import type { IUserService } from '$lib/services/User/UserService.svelte';
	import Button from '$lib/ui/buttons/Button/Button.svelte';

	let { userService }: { userService: IUserService } = $props();
</script>

<Button onClick={() => userService.logOut()} testId="log-out-button" variant="text" size="small">
	Log Out
</Button>
