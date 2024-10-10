<script lang="ts">
	import { onMount } from 'svelte';
	import MessageBus from '$lib/bus/MessageBus';
	import { Messages } from '$lib/bus/Messages';
	import type { Api } from '$lib/services/Api/ApiService';
	import WheelList from '$lib/partials/wheel/WheelList.svelte';
	import type { WheelSetting } from '$lib/api/wheelApi';
	import type { PageData } from './$types';
	import ViewWheelItem from '$lib/partials/wheel/ViewWheelItem.svelte';

	let isLoading = true;
	let wheel: WheelSetting;
	export let data: PageData;

	onMount(async () => {
		let api = MessageBus.getLastMessage<Api>(Messages.Api);

		wheel = await api.Wheel.GetWheelSetting(data.name);

		isLoading = false;
	});
</script>

{#if isLoading}
	<p>Loading...</p>
{:else}
	<ViewWheelItem {wheel} />
{/if}
