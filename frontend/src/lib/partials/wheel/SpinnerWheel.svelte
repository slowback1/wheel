<script lang="ts">
	import WheelService from '$lib/partials/wheel/WheelService';
	import type { SpinnerWheelProps } from '$lib/partials/wheel/SpinnerWheel.types';

	const { slices, isSpinning, onSpin }: SpinnerWheelProps = $props();

	const service = new WheelService();

	const wheel = $derived.by(() => service.getWheel(slices));
	const hasNoSlices = $derived(wheel == null);
</script>

{#if hasNoSlices}
	<p data-testid="no-slices-message">Add a slice to spin the wheel</p>
{:else}
	<button onclick={onSpin} class="wheel__button">
		<svg
			width={wheel.width}
			height={wheel.height}
			viewBox="{-wheel.width / 2} {-wheel.height / 2} {wheel.width} {wheel.height}"
			data-testid="wheel"
			class="wheel-base wheel"
			class:wheel-spinning={isSpinning}
			data-spinning={isSpinning}
		>
			{#each wheel.wedges as wedge, i}
				<path
					fill={wheel.colors[i]}
					d={wheel.arcPath(wedge)}
					stroke={wheel.stroke}
					stroke-width={wheel.strokeWidth}
					data-testid="wheel-slice"
				/>
				<g text-anchor="middle" transform="translate({wheel.arcLabel.centroid(wedge)})">
					<text font-size={wheel.fontSize}>
						<tspan font-weight="bold">{wheel.xVals[i]}</tspan>
						<tspan x="0" dy="1.1em"
							>{wheel.percent
								? `${(wheel.yVals[i] * 100).toFixed(2)}%`
								: wheel.yVals[i].toLocaleString('en-US')}</tspan
						>
					</text>
				</g>
			{/each}
		</svg>
	</button>
{/if}

<style>
	@keyframes spin {
		from {
			transform: rotate(0deg);
		}

		to {
			transform: rotate(360deg);
		}
	}

	.wheel-base {
		--rotations-per-minute: 4;
	}

	.wheel-spinning {
		--rotations-per-minute: 120;
	}

	.wheel {
		--animation-time: calc(60 / var(--rotations-per-minute) * 1s);

		animation: spin var(--animation-time) linear infinite;
	}

	.wheel__button {
		padding: 0;
		border: none;
		background-color: transparent;
	}
</style>
