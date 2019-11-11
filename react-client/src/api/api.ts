import { CardDefinition, DealResponse } from '../types';

const serverUrl = 'http://localhost:5000/api/v1';

export const fetchResources: () => Promise<CardDefinition[]> = () =>
    fetch(`${serverUrl}/cards/definitions`).then(response => response.json());

export const fetchNewDeal: (
    cardType: string,
    propertyName: string
) => Promise<DealResponse> = (cardType, propertyName) =>
    fetch(
        `${serverUrl}/game/deal?cardType=${cardType}&propertyName=${propertyName}`
    ).then(response => response.json());
