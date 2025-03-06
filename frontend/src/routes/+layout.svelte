<script lang="ts">
	import { onMount } from 'svelte';
	import MessageBus from '$lib/bus/MessageBus';
	import getRealStorageProvider from '$lib/bus/realStorageProvider';
	import Header from '$lib/components/navigation/Header.svelte';
	import UrlPathProvider, { RealUrlProvider } from '$lib/providers/urlPathProvider';
	import { ColorTheme } from '$lib/services/Theme/ThemeService';
	import { Messages } from '$lib/bus/Messages';
	import ConfigService, { type ApplicationConfig } from '$lib/services/Config/ConfigService';
	import ToastWrapper from '$lib/ui/containers/toast/ToastWrapper.svelte';
	import FeatureFlagService from '$lib/services/FeatureFlag/FeatureFlagService';
	import ConfigFeatureFlagProvider from '$lib/services/FeatureFlag/ConfigFeatureFlagProvider';
	import ApiContext from '$lib/api/apiContext';
	import UserService from '$lib/services/User/UserService.svelte';

	let currentTheme: ColorTheme = $state(ColorTheme.Light);
	let userService: UserService = $state();
	let initialized = $state(false);

	onMount(() => {
		MessageBus.initialize(getRealStorageProvider());
		UrlPathProvider.initialize(new RealUrlProvider());
		ConfigService.initialize();
		FeatureFlagService.initialize(new ConfigFeatureFlagProvider());
		ApiContext.initialize();

		userService = new UserService();

		MessageBus.subscribe<ColorTheme>(
			Messages.CurrentTheme,
			(value) => (currentTheme = value ?? ColorTheme.Light)
		);

		initialized = true;
	});
</script>

{#if initialized}
	<div
		class:light-theme={currentTheme === ColorTheme.Light}
		class:dark-theme={currentTheme === ColorTheme.Dark}
	>
		<ToastWrapper />
		<Header {userService} />
		<main id="content" class="main-content">
			<slot />
		</main>
	</div>
{/if}

<style global>
	@import '../style/reset.css';
	@import '../style/globals.css';

	.main-content {
		min-height: calc(100vh - var(--gutters-y) * 2 - var(--header-height));
		padding: var(--gutters-y) var(--gutters-x);
		scroll-behavior: auto;
		display: flex;
		flex-direction: column;
	}
</style>
