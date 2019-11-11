import React from 'react';
import Grid from '@material-ui/core/Grid';
import { CardDefinition } from '../../types';
import { Typography, Button } from '@material-ui/core';

const PropertySelector: React.FC<{
    resource: CardDefinition;
    onPropertySelected: (selectedProperty: string) => void;
}> = ({ resource, onPropertySelected }) => {
    return (
        <Grid
            container
            justify="center"
            alignItems="center"
            direction="column"
            spacing={5}
            style={{ minHeight: '100vh' }}
        >
            <Grid item>
                <Typography variant="h4">
                    Select property from {resource.name}:
                </Typography>
            </Grid>
            <Grid item>
                {resource.properties.map(property => (
                    <Button
                        key={property}
                        variant="contained"
                        onClick={() => onPropertySelected(property)}
                    >
                        {property}
                    </Button>
                ))}
            </Grid>
        </Grid>
    );
};

export default PropertySelector;
