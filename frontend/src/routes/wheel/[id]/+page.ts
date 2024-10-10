import type { PageLoad } from './$types';

export const load: PageLoad = (data) => {
	return {
		name: data.params.id
	};
};
