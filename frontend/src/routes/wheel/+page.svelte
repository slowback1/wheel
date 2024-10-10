<script lang="ts">
	import { onMount } from 'svelte';
	import type { WheelSetting } from '$lib/api/wheelApi';
	import MessageBus from '$lib/bus/MessageBus';
	import { Messages } from '$lib/bus/Messages';
	import type { Api } from '$lib/services/Api/ApiService';
	import WheelList from '$lib/partials/wheel/WheelList.svelte';

	let isLoading = true;
	let wheels: WheelSetting[] = [];

	onMount(async () => {
		let api = MessageBus.getLastMessage<Api>(Messages.Api);

		wheels = await api.Wheel.GetWheelSettings();

		isLoading = false;
	});
</script>

{#if isLoading}
	<div>Loading...</div>
{:else}
	<WheelList {wheels} />
{/if}
