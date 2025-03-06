<script lang="ts">
	import WheelService from '$lib/partials/wheel/traditional/WheelService';
	import type { SpinnerWheelProps } from '$lib/partials/wheel/traditional/SpinnerWheel.types';

	const { slices, isSpinning, onSpin, selectedSlice }: SpinnerWheelProps = $props();

	const service = new WheelService();

	const wheel = $derived.by(() => service.getWheel(slices));

	const hasNoSlices = $derived(wheel == null);
	const shouldStopSpinning = $derived(selectedSlice != null && !isSpinning);
	const landedAngle = $derived.by(() => {
		if (!selectedSlice && !Number.isNaN(selectedSlice)) return null;
		return service.getLandedAngle(slices, selectedSlice);
	});
</script>

{#if hasNoSlices}
	<p data-testid="no-slices-message">Add a slice to spin the wheel</p>
{:else}
	<button onclick={onSpin} class="wheel__button">
		<span class="pointer"></span>
		<svg
			width={wheel.width}
			height={wheel.height}
			viewBox="{-wheel.width / 2} {-wheel.height / 2} {wheel.width} {wheel.height}"
			data-testid="wheel"
			class="wheel-base wheel"
			class:wheel-spinning={isSpinning}
			class:wheel-stopping={shouldStopSpinning}
			style="--landed-angle: {landedAngle}deg"
			data-spinning={isSpinning}
			data-stopping={shouldStopSpinning}
		>
			{#each wheel.wedges as wedge, i}
				<path
					fill={wheel.colors[i].background}
					d={wheel.arcPath(wedge)}
					stroke={wheel.stroke}
					stroke-width={wheel.strokeWidth}
					data-testid="wheel-slice"
				/>
				<g text-anchor="middle" transform="translate({wheel.arcLabel.centroid(wedge)})">
					<text
						fill={wheel.colors[i].text}
						font-size={wheel.fontSize}
						transform={`rotate(${service.getTextRotationAngle(slices, i)})`}
					>
						<tspan font-weight="bold">{wheel.xVals[i]}</tspan>
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

	@keyframes stop {
		from {
			transform: rotate(0deg);
		}

		to {
			transform: rotate(var(--landed-angle));
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
		position: relative;
		padding: 0;
		border: none;
		background-color: transparent;
	}

	.wheel-stopping {
		--animation-time: 5s;

		animation: stop var(--animation-time) linear forwards;
		animation-timing-function: cubic-bezier(0.1, 0.89, 0.885, 0.77);
	}

	.pointer {
		position: absolute;
		top: 60px;
		left: 50%;
		transform: translate(-50%, -50%);
		width: 50px;
		height: 50px;
		background-color: var(--color-font);
		border-radius: 50%;
		z-index: 1;
		clip-path: polygon(50% 100%, 0 0, 100% 0);
	}
</style>
