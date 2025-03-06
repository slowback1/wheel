import { fireEvent, type RenderResult } from '@testing-library/svelte';

const DEFAULT_ALERT_TEST_ID = 'alert';

export default class AlertUiTestHelpers {
	public static closeAlert(
		renderResult: RenderResult<any>,
		parentTestId: string = DEFAULT_ALERT_TEST_ID
	) {
		let alert = renderResult.getByTestId(parentTestId);

		let closeButton = alert.querySelector('[data-testid="alert-close"]');

		fireEvent.click(closeButton);
	}
}
