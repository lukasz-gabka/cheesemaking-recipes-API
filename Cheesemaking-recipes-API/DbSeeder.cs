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
                        Labels =  new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Rodzaj  mleka"
                            },
                            new Label()
                            {
                                Name = "Ilość  mleka"
                            },
                            new Label()
                            {
                                Name = "Chlorek wapnia"
                            },
                            new Label()
                            {
                                Name = "Rodzaj  kultury bakteryjnej"
                            },
                            new Label()
                            {
                                Name = "Ilość kultury bakteryjnej"
                            },
                            new Label()
                            {
                                Name = "Rodzaj  podpuszczki"
                            },
                            new Label()
                            {
                                Name = "Ilość podpuszczki"
                            },
                            new Label()
                            {
                                Name = "Dodatki"
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Warzenie",
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Temperatura dodania kultury"
                            },
                            new Label()
                            {
                                Name = "Czas zakwaszania"
                            },
                            new Label()
                            {
                                Name = "Temperatura dodania podpuszczki"
                            },
                            new Label()
                            {
                                Name = "Czas krzepnięcia"
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Krojenie skrzepu",
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Rozmiar kostek skrzepu"
                            },
                            new Label()
                            {
                                Name = "Czas obkurczania skrzepu"
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Formowanie sera", 
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Czas formowania"
                            },
                            new Label()
                            {
                                Name = "Temperatura formowania"
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Solenie sera",
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Ilość soli w solance"
                            },
                            new Label()
                            {
                                Name = "Ilość wody w solance"
                            },
                            new Label()
                            {
                                Name = "Czas solenia"
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Obserwacje",
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Degustacja"
                            },
                            new Label()
                            {
                                Name = "Komentarz"
                            }
                        }
                    }
                }
            };
        }
    }
}
