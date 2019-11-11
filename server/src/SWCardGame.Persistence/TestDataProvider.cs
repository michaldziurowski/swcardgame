using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SWCardGame.Core.Domain;

namespace SWCardGame.Persistence
{
    public class TestDataProvider
    {
        private readonly SWCardGameContext context;

        public TestDataProvider(SWCardGameContext context)
        {
            this.context = context;
        }

        public async Task Seed()
        {
            if (!context.Database.EnsureCreated())
            {
                context.Database.Migrate();
            }

            if (await context.CardDefinitions.AnyAsync())
            {
                return;
            }

            var starshipsDefinition = new Entities.CardDefinition
            {
                Key = "starships",
                Name = "Starships",
                Properties = "speed,noOfPassengers"
            };
            var peopleDefinition = new Entities.CardDefinition
            {
                Key = "people",
                Name = "People",
                Properties = "noOfLegs,noOfHeads"
            };
            context.CardDefinitions.Add(starshipsDefinition);
            context.CardDefinitions.Add(peopleDefinition);

            var cards = new List<Entities.Card>{
                new Entities.Card
                {
                    Name = "enterprise",
                    Definition=starshipsDefinition,
                    Properties = JsonConvert.SerializeObject(new List<Property>
                    {
                        new Property{
                            Name = "speed",
                            Value = 100
                        },
                        new Property{
                            Name = "noOfPassengers",
                            Value = 1000
                        }
                    })
                },
                new Entities.Card
                {
                    Name = "executor",
                    Definition=starshipsDefinition,
                    Properties = JsonConvert.SerializeObject(new List<Property>
                    {
                        new Property{
                            Name = "speed",
                            Value = 200
                        },
                        new Property{
                            Name = "noOfPassengers",
                            Value = 500
                        }
                    })
                },
                new Entities.Card
                {
                    Name = "spock",
                    Definition=peopleDefinition,
                    Properties = JsonConvert.SerializeObject(new List<Property>
                    {
                        new Property{
                            Name = "noOfHeads",
                            Value = 1
                        },
                        new Property{
                            Name = "noOfLegs",
                            Value = 2
                        }
                    })
                },
                new Entities.Card
                {
                    Name = "octopusAlien",
                    Definition=peopleDefinition,
                    Properties = JsonConvert.SerializeObject(new List<Property>
                    {
                        new Property{
                            Name = "noOfHeads",
                            Value = 1
                        },
                        new Property{
                            Name = "noOfLegs",
                            Value = 8
                        }
                    })
                },
                new Entities.Card
                {
                    Name = "twoHeadedAlien",
                    Definition=peopleDefinition ,
                    Properties = JsonConvert.SerializeObject(new List<Property>
                    {
                        new Property{
                            Name = "noOfHeads",
                            Value = 2
                        },
                        new Property{
                            Name = "noOfLegs",
                            Value = 1
                        }
                    })
                },
            };

            context.Cards.AddRange(cards);

            context.SaveChanges();
        }
    }
}