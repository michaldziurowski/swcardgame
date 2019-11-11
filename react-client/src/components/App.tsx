import React, { useState } from 'react';

import {
    CHOOSE_RESOURCE_STEP_NAME,
    CHOOSE_PROPERTY_STEP_NAME,
    DISPLAY_RESULTS_STEP_NAME
} from '../consts';
import CardDefinitionSelector from './cardDefinitionSelector';
import DealDisplay from './dealDisplay';
import PropertySelector from './propertySelector';
import { CardDefinition, Score } from '../types';
import { Container, Button } from '@material-ui/core';

const App: React.FC = () => {
    const [step, setStep] = useState(CHOOSE_RESOURCE_STEP_NAME);
    const [resource, setSelectedResource] = useState<CardDefinition>();
    const [property, setSelectedProperty] = useState('');
    const [currentScore, setCurrentScore] = useState<Score>({
        leftScore: 0,
        rightScore: 0
    });

    const onReset = () => {
        setSelectedResource(undefined);
        setSelectedProperty('');
        setCurrentScore({ leftScore: 0, rightScore: 0 });
        setStep(CHOOSE_RESOURCE_STEP_NAME);
    };

    const onResourceSelected = (selectedResource: CardDefinition) => {
        setSelectedResource(selectedResource);
        setStep(CHOOSE_PROPERTY_STEP_NAME);
    };

    const onPropertySelected = (selectedPropertyName: string) => {
        setSelectedProperty(selectedPropertyName);
        setStep(DISPLAY_RESULTS_STEP_NAME);
    };

    const onNextRound = (scoreFromCurrentRound: Score) => {
        setSelectedProperty('');
        setCurrentScore(scoreFromCurrentRound);
        setStep(CHOOSE_PROPERTY_STEP_NAME);
    };

    const renderStep = () => {
        switch (step) {
            case CHOOSE_RESOURCE_STEP_NAME:
                return (
                    <CardDefinitionSelector
                        onCardDefinitionSelected={onResourceSelected}
                    />
                );
            case CHOOSE_PROPERTY_STEP_NAME:
                return (
                    (resource && (
                        <PropertySelector
                            resource={resource}
                            onPropertySelected={onPropertySelected}
                        />
                    )) ||
                    null
                );
            case DISPLAY_RESULTS_STEP_NAME:
                return (
                    (resource && property && (
                        <DealDisplay
                            cardType={resource.key}
                            propertyName={property}
                            initialScore={currentScore}
                            onNext={onNextRound}
                        />
                    )) ||
                    null
                );
            default:
                return <div></div>;
        }
    };

    return (
        <Container maxWidth="md">
            <Button variant="contained" onClick={onReset}>
                Reset
            </Button>
            {renderStep()}
        </Container>
    );
};

export default App;
