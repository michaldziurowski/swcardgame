import React from 'react';
import { Grid, Typography } from '@material-ui/core';

const FetchingComponent: React.FC<{
    isLoading: boolean;
    isError: boolean;
    render: () => any;
}> = ({ isLoading, isError, render }) => {
    return isLoading || isError ? (
        <Grid
            container
            direction="column"
            alignItems="center"
            justify="center"
            style={{ minHeight: '100vh' }}
        >
            <Grid item>
                <Typography>
                    {isLoading
                        ? 'Loading...'
                        : 'Error occured while loading data. Are you sure server is running?'}
                </Typography>
            </Grid>
        </Grid>
    ) : (
        render()
    );
};

export default FetchingComponent;
