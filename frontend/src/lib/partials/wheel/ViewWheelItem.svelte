<script lang="ts">
	import type { WheelSetting } from '$lib/api/wheelApi';
	export let wheel: WheelSetting;
</script>

<div class="wheel-view">
	<div class="wheel-view__name">{wheel.name}</div>
	<div class="wheel-view__slices">
		{#each wheel.slices as slice}
			<div class="wheel-view__slices__slice">{slice.label}</div>
		{/each}
	</div>
</div>

<style>
	.wheel-view {
		display: flex;
		flex-direction: column;
	}

	.wheel-view__slices {
		display: flex;
		flex-wrap: wrap;
	}

	.wheel-view__slices__slice {
		padding: 1rem;
		border: 1px solid #ccc;
		margin: 0.5rem;
		background-color: #3a43b2;
		color: white;
	}
</style>
