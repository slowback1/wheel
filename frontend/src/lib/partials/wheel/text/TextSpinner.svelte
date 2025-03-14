<script lang="ts">
	import type { TextSpinnerProps } from '$lib/partials/wheel/text/textSpinner.types';
	import Button from '$lib/ui/buttons/Button/Button.svelte';
	import { onMount } from 'svelte';

	let { isSpinning, landedSlice, onSpin, slices }: TextSpinnerProps = $props();
	let currentlyHighlightedSlice = $state(0);

	onMount(() => {
		const interval = setInterval(() => {
			currentlyHighlightedSlice = (currentlyHighlightedSlice + 1) % slices.length;
		}, 100);

		return () => clearInterval(interval);
	});

	const hasLanded = $derived(() => landedSlice !== null && landedSlice !== undefined);
</script>

<div class="text-spinner">
	<Button size="small" disabled={isSpinning} onClick={onSpin}>Spin</Button>

	<ul class="text-spinner__list">
		{#each slices as slice, i}
			<li
				class="text-spinner__list-item"
				class:wheel-slice__landed={isSpinning ? currentlyHighlightedSlice === i : landedSlice === i}
				data-testid={`wheel-slice-${i}`}
			>
				{slice.label}
			</li>
		{/each}
	</ul>
</div>

<style>
	.text-spinner {
		--text-spinner-base-font-size: 1rem;

		display: flex;
		flex-direction: column;
		gap: 12px;
		align-items: flex-start;
	}

	.text-spinner__list {
		display: flex;
		flex-direction: column;
		flex-wrap: wrap;
		gap: 12px;
		list-style: disc;
		font-size: var(--text-spinner-base-font-size);
	}

	.wheel-slice__landed {
		font-weight: bold;
		font-size: calc(var(--text-spinner-base-font-size) * 1.2);
	}
</style>
