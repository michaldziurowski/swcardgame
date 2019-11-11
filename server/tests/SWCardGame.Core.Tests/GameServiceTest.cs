using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using SWCardGame.Core.Domain;
using SWCardGame.Core.Interfaces;
using SWCardGame.Core.Services;

namespace SWCardGame.Core.Tests
{
    public class GameServiceTest
    {
        private const string CARD_PROPERTY_NAME = "speed";
        private readonly CardDefinition cardDefinition = new CardDefinition() { Key = "starships", Name = "Starships", Properties = new List<string> { CARD_PROPERTY_NAME } };

        [Test]
        public void NewDeal_NotExistingCardDefinition_ThrowsArgumentException()
        {
            var notExistingCardDefinitionKey = "notexisting";
            var cardRepositoryStub = Substitute.For<ICardsRepository>();
            cardRepositoryStub.GetRandomCard(Arg.Is(notExistingCardDefinitionKey)).ReturnsNull();

            var gameService = new GameService(cardRepositoryStub);

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.NewDeal(notExistingCardDefinitionKey, CARD_PROPERTY_NAME));
        }

        [Test]
        public void NewDeal_NotExistingProperty_ThrowsArgumentException()
        {
            var notExistingPropertyName = "notexisting";
            var card = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = 1 } }
            };
            var cardRepositoryStub = Substitute.For<ICardsRepository>();
            cardRepositoryStub.GetRandomCard(Arg.Is(cardDefinition.Key)).Returns(card, card);

            var gameService = new GameService(cardRepositoryStub);

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.NewDeal(cardDefinition.Key, notExistingPropertyName));
        }

        [Test]
        public async Task NewDeal_ReturnsTwoCardsAndVerdict()
        {
            var leftCard = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = 1 } }
            };

            var rightCard = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = 0 } }
            };

            var cardRepositoryStub = Substitute.For<ICardsRepository>();
            cardRepositoryStub.GetRandomCard(Arg.Is(cardDefinition.Key)).Returns(leftCard, rightCard);

            var gameService = new GameService(cardRepositoryStub);

            var dealResult = await gameService.NewDeal(cardDefinition.Key, CARD_PROPERTY_NAME);

            Assert.AreEqual(leftCard, dealResult.LeftCard);
            Assert.AreEqual(rightCard, dealResult.RightCard);
            Assert.AreEqual(Verdict.Left, dealResult.Verdict);
        }

        [Test]
        public async Task NewDeal_LeftCardHasBiggerPropertyValue_ReturnsLeftWinVerdict()
        {
            var leftCard = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = 1 } }
            };

            var rightCard = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = 0 } }
            };

            var cardRepositoryStub = Substitute.For<ICardsRepository>();
            cardRepositoryStub.GetRandomCard(Arg.Is(cardDefinition.Key)).Returns(leftCard, rightCard);

            var gameService = new GameService(cardRepositoryStub);

            var dealResult = await gameService.NewDeal(cardDefinition.Key, CARD_PROPERTY_NAME);

            Assert.AreEqual(Verdict.Left, dealResult.Verdict);
        }

        [Test]
        public async Task NewDeal_RightCardHasBiggerPropertyValue_ReturnsRightWinVerdict()
        {
            var leftCard = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = 0 } }
            };

            var rightCard = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = 1 } }
            };

            var cardRepositoryStub = Substitute.For<ICardsRepository>();
            cardRepositoryStub.GetRandomCard(Arg.Is(cardDefinition.Key)).Returns(leftCard, rightCard);

            var gameService = new GameService(cardRepositoryStub);

            var dealResult = await gameService.NewDeal(cardDefinition.Key, CARD_PROPERTY_NAME);

            Assert.AreEqual(Verdict.Right, dealResult.Verdict);
        }

        [Test]
        public async Task NewDeal_BothCardsHaveTheSamePropertyValue_ReturnsTieVerdict()
        {
            const int propertyValue = 1;
            var leftCard = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = propertyValue } }
            };

            var rightCard = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = propertyValue } }
            };

            var cardRepositoryStub = Substitute.For<ICardsRepository>();
            cardRepositoryStub.GetRandomCard(Arg.Is(cardDefinition.Key)).Returns(leftCard, rightCard);

            var gameService = new GameService(cardRepositoryStub);

            var dealResult = await gameService.NewDeal(cardDefinition.Key, CARD_PROPERTY_NAME);

            Assert.AreEqual(Verdict.Tie, dealResult.Verdict);
        }
    }
}