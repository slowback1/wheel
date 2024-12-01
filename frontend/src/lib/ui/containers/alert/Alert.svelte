<script lang="ts">
	import { type AlertProps, AlertType } from '$lib/ui/containers/alert/alertTypes';

	const { testId, message, type, onClose }: AlertProps = $props();

	const dataTestId = $derived(testId ?? 'alert');
	const alertType = $derived(type ?? AlertType.Info);

	const isInfo = $derived(alertType === AlertType.Info);
	const isWarning = $derived(alertType === AlertType.Warning);
	const isError = $derived(alertType === AlertType.Error);

	//TODO: add a storybook for this component, then style it!
</script>

<div
	class:alert__info={isInfo}
	class:alert__warning={isWarning}
	class:alert__error={isError}
	data-testid={dataTestId}
	role="alert"
>
	{message}

	<button onclick={onClose} data-testid="alert-close">X</button>
</div>
