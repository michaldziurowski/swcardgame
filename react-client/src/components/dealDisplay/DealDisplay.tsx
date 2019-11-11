import React from 'react';
import Grid from '@material-ui/core/Grid';

import { GameCard } from '../../types';
import GameCardItem from './GameCardItem';
import { Button, Typography } from '@material-ui/core';

const DealDisplay: React.FC<{
    leftCard: GameCard;
    rightCard: GameCard;
    leftScore: number;
    rightScore: number;
    onNext: () => void;
}> = ({ leftCard, rightCard, leftScore, rightScore, onNext }) => (
    <Grid
        container
        direction="column"
        alignItems="center"
        justify="center"
        style={{ minHeight: '100vh' }}
    >
        <Grid item>
            <Grid container justify="center" spacing={5}>
                <Grid item data-testid="leftSide">
                    <Typography>Score: {leftScore}</Typography>
                    <GameCardItem card={leftCard} />
                </Grid>
                <Grid item data-testid="rightSide">
                    <Typography>Score: {rightScore}</Typography>
                    <GameCardItem card={rightCard} />
                </Grid>
            </Grid>
        </Grid>
        <Grid item style={{ marginTop: '10px' }}>
            <Button variant="contained" onClick={() => onNext()}>
                Next
            </Button>
        </Grid>
    </Grid>
);

export default DealDisplay;
