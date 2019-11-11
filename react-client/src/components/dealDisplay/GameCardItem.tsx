import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import CardHeader from '@material-ui/core/CardHeader';
import Typography from '@material-ui/core/Typography';

import { GameCard, CardResult } from '../../types';

const useStyles = makeStyles({
    winner: {
        backgroundColor: '#99e699'
    },
    loser: {
        backgroundColor: '#ff8080'
    },
    tie: {
        backgroundColor: '#ffeb99'
    }
});

const GameCardItem: React.FC<{
    card: GameCard;
}> = ({ card }) => {
    const classes = useStyles();

    const mapCardResultToCssClass = (cardResult: CardResult) => {
        switch (cardResult) {
            case CardResult.Tie:
                return classes.tie;
            case CardResult.Winner:
                return classes.winner;
            case CardResult.Loser:
                return classes.loser;
            default:
                throw new Error(
                    `Cannot map verdict [${cardResult}] to css class. Unknown verdict.`
                );
        }
    };

    const mapCardResultToSubheader = (cardResult: CardResult) => {
        switch (cardResult) {
            case CardResult.Tie:
                return 'tie';
            case CardResult.Winner:
                return 'winner';
            case CardResult.Loser:
                return 'loser';
            default:
                throw new Error(
                    `Cannot map verdict [${cardResult}] to subheader. Unknown verdict.`
                );
        }
    };

    return (
        <Card>
            <CardHeader
                title={card.name}
                subheader={mapCardResultToSubheader(card.result)}
            />
            <CardContent>
                {card.properties.map(property => (
                    <Typography
                        key={property.name}
                        className={
                            property.selected
                                ? mapCardResultToCssClass(card.result)
                                : undefined
                        }
                    >
                        {property.name}: {property.value}
                    </Typography>
                ))}
            </CardContent>
        </Card>
    );
};

export default GameCardItem;
