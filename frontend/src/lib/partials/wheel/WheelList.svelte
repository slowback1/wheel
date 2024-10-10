<script lang="ts">
	import type { WheelSetting } from '$lib/api/wheelApi';
	import Button from '$lib/ui/buttons/Button/Button.svelte';

	export let wheels: WheelSetting[];
</script>

<div class="wheel-list">
	{#each wheels as wheel}
		<div class="wheel-list__item">
			<div class="wheel-list__item__name">{wheel.name}</div>
			<div class="wheel-list__item__actions">
				<Button href={`/wheel/${wheel.name}`}>View</Button>
			</div>
		</div>
	{/each}
</div>

<style>
	.wheel-list {
		display: flex;
		flex-direction: column;
	}

	.wheel-list__item {
		display: flex;
		justify-content: space-between;
		padding: 1rem;
		border-bottom: 1px solid #ccc;
	}
</style>
