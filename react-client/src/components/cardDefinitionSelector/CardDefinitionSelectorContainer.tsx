import React from 'react';
import { CardDefinition } from '../../types';
import CardDefinitionSelector from './CardDefinitionSelector';
import { useApi, fetchResources } from '../../api';
import FetchingComponent from '../FetchingComponent';

const CardDefinitionSelectorContainer: React.FC<{
    onCardDefinitionSelected: (selectedCardDefinition: CardDefinition) => void;
}> = ({ onCardDefinitionSelected }) => {
    const [resources, isLoading, isError] = useApi<CardDefinition[]>(
        fetchResources
    );
    return (
        <FetchingComponent
            isLoading={isLoading}
            isError={isError}
            render={() =>
                (resources && (
                    <CardDefinitionSelector
                        onCardDefinitionSelected={onCardDefinitionSelected}
                        cardDefinitions={resources}
                    />
                )) ||
                null
            }
        />
    );
};

export default CardDefinitionSelectorContainer;
