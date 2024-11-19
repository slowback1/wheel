<script lang="ts">
	import type { WheelFormProps } from '$lib/partials/wheelForm/WheelForm.types';
	import SimpleWheelForm from '$lib/partials/wheelForm/simple/SimpleWheelForm.svelte';

	const props: WheelFormProps = $props();
</script>

<SimpleWheelForm {...props} />
