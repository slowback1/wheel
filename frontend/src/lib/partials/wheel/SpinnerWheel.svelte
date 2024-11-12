<script lang="ts">
	import type { SpinnerWheelProps } from '$lib/partials/wheel/SpinnerWheel.types';

	const { isSpinning, onSpin, slices }: SpinnerWheelProps = $props();

	let hasNoSlices = $derived(slices.length === 0);

	function getColorClassName(index: number) {
		const remainder = index % 9;

		return `wheel-slice__${remainder}`;
	}
</script>

{#if hasNoSlices}
	<p data-testid="no-slices-message">Add a slice to spin the wheel</p>
{:else}
	<button
		class="wheel"
		class:wheel__spinning={isSpinning}
		data-testid="wheel"
		data-spinning={isSpinning}
		onclick={onSpin}
	>
		{#each slices as slice, index}
			<div class={`wheel-slice ${getColorClassName(index)}`} data-testid="wheel-slice">
				{slice.label}
			</div>
		{/each}
	</button>
{/if}
