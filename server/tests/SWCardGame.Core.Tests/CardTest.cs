using System;
using System.Collections.Generic;
using NUnit.Framework;
using SWCardGame.Core.Domain;

namespace SWCardGame.Core.Tests
{
    public class CardTest
    {
        private const string CARD_PROPERTY_NAME = "prop";
        private const int CARD_VALUE = 1;
        private readonly CardDefinition cardDefinition = new CardDefinition() { Key = "starships", Name = "Starships", Properties = new List<string> { CARD_PROPERTY_NAME } };

        [Test]
        public void Compare_WithDifferentType_ThrowsArgumentException()
        {
            var card = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = CARD_VALUE } }
            };

            var cardToCompare = new Card
            {
                Definition = new CardDefinition() { Key = "different" },
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = CARD_VALUE } }
            };

            Assert.Throws<ArgumentException>(() => card.CompareByProperty(cardToCompare, CARD_PROPERTY_NAME));
        }

        [Test]
        public void Compare_NotExistingProperty_ThrowsArgumentException()
        {
            var card = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = CARD_VALUE } }
            };

            var cardToCompare = new Card();

            Assert.Throws<ArgumentException>(() => card.CompareByProperty(cardToCompare, "notExistingProperty"));
        }

        [Test]
        public void Compare_PropertiesHaveTheSameValues_Returns0()
        {
            var card = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = CARD_VALUE } }
            };

            var cardToCompare = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = CARD_VALUE } }
            };

            var comparisonResult = card.CompareByProperty(cardToCompare, CARD_PROPERTY_NAME);

            Assert.AreEqual(0, comparisonResult);
        }

        [Test]
        public void Compare_CardToCompareHasPropertyWithSmallerValue_Returns1()
        {
            var card = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = CARD_VALUE } }
            };

            var cardToCompare = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = CARD_VALUE - 1 } }
            };

            var comparisonResult = card.CompareByProperty(cardToCompare, CARD_PROPERTY_NAME);

            Assert.AreEqual(1, comparisonResult);
        }

        [Test]
        public void Compare_CardToCompareHasPropertyWithBiggerValue_ReturnsNegative1()
        {
            var card = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = CARD_VALUE } }
            };

            var cardToCompare = new Card
            {
                Definition = cardDefinition,
                Properties = new List<Property> { new Property { Name = CARD_PROPERTY_NAME, Value = CARD_VALUE + 1 } }
            };

            var comparisonResult = card.CompareByProperty(cardToCompare, CARD_PROPERTY_NAME);

            Assert.AreEqual(-1, comparisonResult);
        }
    }
}