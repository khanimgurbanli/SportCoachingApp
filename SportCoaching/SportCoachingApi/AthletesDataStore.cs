using SportCoachingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportCoachingApi
{
    public class AthletesDataStore
    {
        public static AthletesDataStore Current { get; } = new AthletesDataStore();
        public List<AthleteDTO> Athletes { get; set; }


        public AthletesDataStore()
        {
            Athletes = new List<AthleteDTO>()
            {
                new AthleteDTO()
                {
                    Id = 1,
                    Name = "Elnur",
                    Age = 20,
                    Goals =new List<GoalsDTO>()
                    {
                        new GoalsDTO()
                        {
                            Id=1,
                            Name="Weith loss",
                            Description="I want to lost th weight"
                        },
                        new GoalsDTO()
                        {
                            Id=2,
                            Name="Developer",
                            Description="I want to learn the coding"
                        }
                    }
                },
                new AthleteDTO()
                {
                    Id = 2,
                    Name = "Leyla",
                    Age = 73,
                    Goals =new List<GoalsDTO>()
                    {
                        new GoalsDTO()
                        {
                            Id=1,
                            Name="Play piano",
                            Description="I want to learn to playing piano"
                        },
                        new GoalsDTO()
                        {
                            Id=2,
                            Name="Buy the ILTS",
                            Description="I want to pass the IELT exam"
                        }
                    }
                },
                new AthleteDTO()
                {
                    Id = 3,
                    Name = "Samir",
                    Age = 24,
                    Goals =new List<GoalsDTO>()
                    {
                        new GoalsDTO()
                        {
                            Id=1,
                            Name="Buy the car",
                            Description="I want to buy the black car"
                        },
                        new GoalsDTO()
                        {
                            Id=2,
                            Name="Travel Spain",
                            Description="I want to travel to the Spain"
                        }
                    }
                }
            };
        }
    }
}

