import React, { useCallback } from 'react';
import DealDisplay from './DealDisplay';
import { useApi, fetchNewDeal } from '../../api';
import { DealResponse, DealVerdict, Score } from '../../types';
import FetchingComponent from '../FetchingComponent';

const DealDisplayContainer: React.FC<{
    cardType: string;
    propertyName: string;
    initialScore: Score;
    onNext: (currentScore: Score) => void;
}> = ({ cardType, propertyName, initialScore, onNext }) => {
    const fetchNewDealCallback = useCallback(
        () => fetchNewDeal(cardType, propertyName),
        [cardType, propertyName]
    );
    const [dealResponse, isLoading, isError] = useApi<DealResponse>(
        fetchNewDealCallback
    );

    let leftScore = initialScore.leftScore;
    let rightScore = initialScore.rightScore;

    if (dealResponse) {
        if (dealResponse.verdict === DealVerdict.LeftWin) {
            leftScore += 1;
        } else if (dealResponse.verdict === DealVerdict.RightWin) {
            rightScore += 1;
        }
    }

    return (
        <FetchingComponent
            isLoading={isLoading}
            isError={isError}
            render={() =>
                (dealResponse && (
                    <DealDisplay
                        leftCard={dealResponse.leftCard}
                        rightCard={dealResponse.rightCard}
                        leftScore={leftScore}
                        rightScore={rightScore}
                        onNext={() => onNext({ leftScore, rightScore })}
                    />
                )) ||
                null
            }
        />
    );
};

export default DealDisplayContainer;
