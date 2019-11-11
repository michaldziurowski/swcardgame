import { useState, useEffect } from 'react';

function useApi<T>(
    fetchAction: () => Promise<T>
): [T | undefined, boolean, boolean] {
    const [data, setData] = useState<T>();
    const [isLoading, setIsLoading] = useState(false);
    const [isError, setIsError] = useState(false);

    useEffect(() => {
        async function getData() {
            try {
                setIsLoading(true);
                const data = await fetchAction();
                setIsLoading(false);
                setData(data);
            } catch {
                setIsLoading(false);
                setIsError(true);
            }
        }

        getData();
    }, [fetchAction]);

    return [data, isLoading, isError];
}

export default useApi;
