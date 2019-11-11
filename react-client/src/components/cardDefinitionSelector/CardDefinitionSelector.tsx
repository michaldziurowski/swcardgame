import React from 'react';
import Grid from '@material-ui/core/Grid';
import { CardDefinition } from '../../types';
import { Button, Typography } from '@material-ui/core';

const CardDefinitionSelector: React.FC<{
    onCardDefinitionSelected: (selectedCardDefinition: CardDefinition) => void;
    cardDefinitions: CardDefinition[];
}> = ({ onCardDefinitionSelected, cardDefinitions }) => {
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
                <Typography variant="h4">Select card definition:</Typography>
            </Grid>
            <Grid item>
                {cardDefinitions.map(r => (
                    <Button
                        key={r.key}
                        variant="contained"
                        onClick={() => onCardDefinitionSelected(r)}
                    >
                        {r.name}
                    </Button>
                ))}
            </Grid>
        </Grid>
    );
};

export default CardDefinitionSelector;
