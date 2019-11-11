import React from 'react';
import { render, fireEvent, waitForElement } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import { DealResponse, CardResult, DealVerdict } from '../../types';
import DealDisplayContainer from './DealDisplayContainer';

describe('Deal display container', () => {
    test('correctly dislpays score if left won', async () => {
        const fakeDealResponse: DealResponse = {
            leftCard: {
                name: 'Left card',
                result: CardResult.Winner,
                properties: [{ name: 'prop', value: 100, selected: true }]
            },
            rightCard: {
                name: 'Right card',
                result: CardResult.Loser,
                properties: [{ name: 'prop', value: 50, selected: true }]
            },
            verdict: DealVerdict.LeftWin
        };

        jest.spyOn(window, 'fetch').mockImplementation(() => {
            return Promise.resolve({
                json: () => Promise.resolve(fakeDealResponse)
            }) as Promise<Response>;
        });

        const { getByTestId } = render(
            <DealDisplayContainer
                cardType=""
                propertyName=""
                initialScore={{ leftScore: 0, rightScore: 0 }}
                onNext={() => {}}
            />
        );

        const leftCard = await waitForElement(() => getByTestId('leftSide'));
        const leftScore = leftCard.firstElementChild!.textContent;
        const rightCard = await waitForElement(() => getByTestId('rightSide'));
        const rightScore = rightCard.firstElementChild!.textContent;

        expect(leftScore).toBe('Score: 1');
        expect(rightScore).toBe('Score: 0');
    });

    test('correctly dislpays score if right won', async () => {
        const fakeDealResponse: DealResponse = {
            leftCard: {
                name: 'Left card',
                result: CardResult.Loser,
                properties: [{ name: 'prop', value: 50, selected: true }]
            },
            rightCard: {
                name: 'Right card',
                result: CardResult.Winner,
                properties: [{ name: 'prop', value: 100, selected: true }]
            },
            verdict: DealVerdict.RightWin
        };

        jest.spyOn(window, 'fetch').mockImplementation(() => {
            return Promise.resolve({
                json: () => Promise.resolve(fakeDealResponse)
            }) as Promise<Response>;
        });

        const { getByTestId } = render(
            <DealDisplayContainer
                cardType=""
                propertyName=""
                initialScore={{ leftScore: 0, rightScore: 0 }}
                onNext={() => {}}
            />
        );

        const leftCard = await waitForElement(() => getByTestId('leftSide'));
        const leftScore = leftCard.firstElementChild!.textContent;
        const rightCard = await waitForElement(() => getByTestId('rightSide'));
        const rightScore = rightCard.firstElementChild!.textContent;

        expect(leftScore).toBe('Score: 0');
        expect(rightScore).toBe('Score: 1');
    });

    test('correctly dislpays score if is tie', async () => {
        const fakeDealResponse: DealResponse = {
            leftCard: {
                name: 'Left card',
                result: CardResult.Tie,
                properties: [{ name: 'prop', value: 50, selected: true }]
            },
            rightCard: {
                name: 'Right card',
                result: CardResult.Tie,
                properties: [{ name: 'prop', value: 50, selected: true }]
            },
            verdict: DealVerdict.Tie
        };

        jest.spyOn(window, 'fetch').mockImplementation(() => {
            return Promise.resolve({
                json: () => Promise.resolve(fakeDealResponse)
            }) as Promise<Response>;
        });

        const { getByTestId } = render(
            <DealDisplayContainer
                cardType=""
                propertyName=""
                initialScore={{ leftScore: 0, rightScore: 0 }}
                onNext={() => {}}
            />
        );

        const leftCard = await waitForElement(() => getByTestId('leftSide'));
        const leftScore = leftCard.firstElementChild!.textContent;
        const rightCard = await waitForElement(() => getByTestId('rightSide'));
        const rightScore = rightCard.firstElementChild!.textContent;

        expect(leftScore).toBe('Score: 0');
        expect(rightScore).toBe('Score: 0');
    });
});
