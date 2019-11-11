using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using SWCardGame.Core.Domain;
using SWCardGame.Core.Services;
using SWCardGame.WebApi.Controllers;
using SWCardGame.WebApi.Contracts;
using SWCardGame.WebApi.Contracts.Game;

namespace SWCardGame.WebApi.Tests
{
    public class GameControllerTest
    {
        [Test]
        public async Task Deal_GameServiceThrowsArgumentException_ReturnsBadRequest()
        {
            var notExistingCardType = "notexisting";
            var loggerStub = Substitute.For<ILogger<GameController>>();
            var gameServiceStub = Substitute.For<IGameService>();
            gameServiceStub.NewDeal(Arg.Is(notExistingCardType), Arg.Any<string>()).Returns<DealResult>(x => throw new ArgumentException());

            var gameController = new GameController(gameServiceStub, loggerStub);

            var result = await gameController.Deal(notExistingCardType, string.Empty);

            Assert.IsInstanceOf(typeof(BadRequestResult), result);
        }

        [Test]
        public async Task Deal_WhenTie_ReturnsProperOkResponse()
        {
            var cardType = "type";
            var cardProperty = "property";
            var cardDefinition = new Core.Domain.CardDefinition
            {
                Key = cardType,
                Name = "name",
                Properties = new[] { cardProperty }
            };
            var leftCard = new Core.Domain.Card { Definition = cardDefinition, Name = "name1", Properties = new List<Property> { new Property { Name = cardProperty, Value = 1 } } };
            var rightCard = new Core.Domain.Card { Definition = cardDefinition, Name = "name2", Properties = new List<Property> { new Property { Name = cardProperty, Value = 1 } } };
            var dealResult = new DealResult
            {
                LeftCard = leftCard,
                RightCard = rightCard,
                Verdict = Verdict.Tie
            };

            var loggerStub = Substitute.For<ILogger<GameController>>();
            var gameServiceStub = Substitute.For<IGameService>();
            gameServiceStub.NewDeal(Arg.Is(cardType), Arg.Is(cardProperty)).Returns(dealResult);

            var gameController = new GameController(gameServiceStub, loggerStub);

            var result = await gameController.Deal(cardType, cardProperty);

            Assert.IsInstanceOf(typeof(OkObjectResult), result);

            var dealResponse = (result as OkObjectResult).Value as DealResponse;

            Assert.AreEqual((int)Verdict.Tie, dealResponse.Verdict);
            Assert.AreEqual(leftCard.Name, dealResponse.LeftCard.Name);
            Assert.AreEqual(CardResult.Tie, dealResponse.LeftCard.Result);
            Assert.AreEqual(rightCard.Name, dealResponse.RightCard.Name);
            Assert.AreEqual(CardResult.Tie, dealResponse.RightCard.Result);
        }

        [Test]
        public async Task Deal_WhenLeftWins_ReturnsProperOkResponse()
        {
            var cardType = "type";
            var cardProperty = "property";
            var cardDefinition = new Core.Domain.CardDefinition
            {
                Key = cardType,
                Name = "name",
                Properties = new[] { cardProperty }
            };
            var leftCard = new Core.Domain.Card { Definition = cardDefinition, Name = "name1", Properties = new List<Property> { new Property { Name = cardProperty, Value = 2 } } };
            var rightCard = new Core.Domain.Card { Definition = cardDefinition, Name = "name2", Properties = new List<Property> { new Property { Name = cardProperty, Value = 1 } } };
            var dealResult = new DealResult
            {
                LeftCard = leftCard,
                RightCard = rightCard,
                Verdict = Verdict.Left
            };

            var loggerStub = Substitute.For<ILogger<GameController>>();
            var gameServiceStub = Substitute.For<IGameService>();
            gameServiceStub.NewDeal(Arg.Is(cardType), Arg.Is(cardProperty)).Returns(dealResult);

            var gameController = new GameController(gameServiceStub, loggerStub);

            var result = await gameController.Deal(cardType, cardProperty);

            Assert.IsInstanceOf(typeof(OkObjectResult), result);

            var dealResponse = (result as OkObjectResult).Value as DealResponse;

            Assert.AreEqual((int)Verdict.Left, dealResponse.Verdict);
            Assert.AreEqual(leftCard.Name, dealResponse.LeftCard.Name);
            Assert.AreEqual(CardResult.Winner, dealResponse.LeftCard.Result);
            Assert.AreEqual(rightCard.Name, dealResponse.RightCard.Name);
            Assert.AreEqual(CardResult.Loser, dealResponse.RightCard.Result);
        }

        [Test]
        public async Task Deal_WhenRightWins_ReturnsProperOkResponse()
        {
            var cardType = "type";
            var cardProperty = "property";
            var cardDefinition = new Core.Domain.CardDefinition
            {
                Key = cardType,
                Name = "name",
                Properties = new[] { cardProperty }
            };
            var leftCard = new Core.Domain.Card { Definition = cardDefinition, Name = "name1", Properties = new List<Property> { new Property { Name = cardProperty, Value = 1 } } };
            var rightCard = new Core.Domain.Card { Definition = cardDefinition, Name = "name2", Properties = new List<Property> { new Property { Name = cardProperty, Value = 2 } } };
            var dealResult = new DealResult
            {
                LeftCard = leftCard,
                RightCard = rightCard,
                Verdict = Verdict.Right
            };

            var loggerStub = Substitute.For<ILogger<GameController>>();
            var gameServiceStub = Substitute.For<IGameService>();
            gameServiceStub.NewDeal(Arg.Is(cardType), Arg.Is(cardProperty)).Returns(dealResult);

            var gameController = new GameController(gameServiceStub, loggerStub);

            var result = await gameController.Deal(cardType, cardProperty);

            Assert.IsInstanceOf(typeof(OkObjectResult), result);

            var dealResponse = (result as OkObjectResult).Value as DealResponse;

            Assert.AreEqual((int)Verdict.Right, dealResponse.Verdict);
            Assert.AreEqual(leftCard.Name, dealResponse.LeftCard.Name);
            Assert.AreEqual(CardResult.Loser, dealResponse.LeftCard.Result);
            Assert.AreEqual(rightCard.Name, dealResponse.RightCard.Name);
            Assert.AreEqual(CardResult.Winner, dealResponse.RightCard.Result);
        }
    }
}