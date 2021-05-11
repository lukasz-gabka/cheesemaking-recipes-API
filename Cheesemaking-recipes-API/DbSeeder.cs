using Cheesemaking_recipes_API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Cheesemaking_recipes_API
{
    public class DbSeeder
    {
        private readonly ApiDbContext _dbContext;

        public DbSeeder(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }

                if (!_dbContext.Templates.Any())
                {
                    var template = GetDefaultTemplate();
                    _dbContext.Templates.AddRange(template);
                    _dbContext.SaveChanges();
                }
            }
        }
        private Template GetDefaultTemplate()
        {
            return new Template()
            {
                Name = "Domyślny szablon",
                Categories = new List<Category>
                {
                    new Category()
                    {
                        Name = "Składniki",
                        Order = 1,
                        Labels =  new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Rodzaj  mleka",
                                Order = 1
                            },
                            new Label()
                            {
                                Name = "Ilość  mleka",
                                Order = 2
                            },
                            new Label()
                            {
                                Name = "Chlorek wapnia",
                                Order = 3
                            },
                            new Label()
                            {
                                Name = "Rodzaj  kultury bakteryjnej",
                                Order = 4
                            },
                            new Label()
                            {
                                Name = "Ilość kultury bakteryjnej",
                                Order = 5
                            },
                            new Label()
                            {
                                Name = "Rodzaj  podpuszczki",
                                Order = 6
                            },
                            new Label()
                            {
                                Name = "Ilość podpuszczki",
                                Order = 7
                            },
                            new Label()
                            {
                                Name = "Dodatki",
                                Order = 8
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Warzenie",
                        Order = 2,
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Temperatura dodania kultury",
                                Order = 1
                            },
                            new Label()
                            {
                                Name = "Czas zakwaszania",
                                Order = 2
                            },
                            new Label()
                            {
                                Name = "Temperatura dodania podpuszczki",
                                Order = 3
                            },
                            new Label()
                            {
                                Name = "Czas krzepnięcia",
                                Order = 4
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Krojenie skrzepu",
                        Order = 3,
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Rozmiar kostek skrzepu",
                                Order = 1
                            },
                            new Label()
                            {
                                Name = "Czas obkurczania skrzepu",
                                Order = 2
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Formowanie sera",
                        Order = 4,
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Czas formowania",
                                Order = 1
                            },
                            new Label()
                            {
                                Name = "Temperatura formowania",
                                Order = 2
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Solenie sera",
                        Order = 5,
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Ilość soli w solance",
                                Order = 1
                            },
                            new Label()
                            {
                                Name = "Ilość wody w solance",
                                Order = 2
                            },
                            new Label()
                            {
                                Name = "Czas solenia",
                                Order = 3
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Obserwacje",
                        Order = 6,
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Degustacja",
                                Order = 1
                            },
                            new Label()
                            {
                                Name = "Komentarz",
                                Order = 2
                            }
                        }
                    }
                }
            };
        }
    }
}
