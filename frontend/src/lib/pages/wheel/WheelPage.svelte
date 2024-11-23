<script lang="ts">
	import WheelPageService from '$lib/pages/wheel/WheelPageService.svelte';
	import { onMount } from 'svelte';
	import MessageBus from '$lib/bus/MessageBus';
	import { Messages } from '$lib/bus/Messages';
	import SpinnerWheel from '$lib/partials/wheel/SpinnerWheel.svelte';
	import WheelForm from '$lib/partials/wheelForm/WheelForm.svelte';

	let service: WheelPageService;

	onMount(() => {
		let unsubscribe = MessageBus.subscribe(Messages.ApiContext, (api) => {
			service = new WheelPageService(api);
		});

		return () => {
			unsubscribe();
		};
	});
</script>

{#if service}
	<div class="wheel-page">
		<div class="wheel-page__wheel">
			<SpinnerWheel
				slices={service.wheelSlices}
				selectedSlice={service.landedSlice}
				onSpin={() => service.spin()}
				isSpinning={service.isSpinning}
			/>
		</div>
		<div class="wheel-page__form">
			<WheelForm
				onWheelSliceChange={(slices) => service.onWheelSliceChange(slices)}
				slices={service.wheelSlices}
			/>
		</div>
	</div>
{/if}

<style>
	.wheel-page {
		display: flex;
		flex-wrap: wrap;
		align-items: center;
		justify-content: space-between;
	}

	.wheel-page__form {
		align-self: flex-start;
	}

	.wheel-page__wheel {
		margin-left: 120px;
	}

	@media screen and (max-width: 1000px) {
		.wheel-page__wheel {
			margin: 0 auto;
		}
	}
</style>
