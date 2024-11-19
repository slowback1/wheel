<script lang="ts">
	import type { WheelFormProps } from '$lib/partials/wheelForm/WheelForm.types';
	import type { WheelSlice } from '$lib/api/types';

	const props: WheelFormProps = $props();

	let value = $state(props.slices.map((slice) => slice.label).join('\n'));

	function handleChange(event: Event) {
		const target = event.target as HTMLTextAreaElement;
		value = target.value;

		const slices: WheelSlice[] = value.split('\n').map((v) => ({
			label: v,
			size: 1
		}));

		props.onWheelSliceChange(slices);
	}
</script>

<div class="simple-wheel-form">
	<label class="simple-wheel-form__label" for="simple-wheel-input">Wheel Slices</label>
	<textarea
		id="simple-wheel-input"
		class="simple-wheel-form__input"
		onchange={handleChange}
		{value}
		placeholder="Enter each slice on a new line"
	></textarea>
</div>

<style>
	.simple-wheel-form {
		display: flex;
		flex-direction: column;
	}

	.simple-wheel-form__label {
		margin-bottom: 0.5rem;
	}

	.simple-wheel-form__input {
		padding: 0.5rem;
		border: 1px solid #ccc;
		border-radius: 0.25rem;
		width: clamp(200px, 100%, 320px);
		height: 400px;
		resize: none;
		background-color: color-mix(in lab, var(--color-background) 80%, var(--color-font));
		color: color-mix(in lab, var(--color-background) 10%, var(--color-font));
	}

	.simple-wheel-form__input::placeholder {
		color: color-mix(in lab, var(--color-background) 15%, var(--color-font));
	}
</style>
