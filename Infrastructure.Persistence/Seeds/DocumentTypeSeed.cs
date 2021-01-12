using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Seeds
{
    public static class DocumentTypeSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await context.DocumentTypes.AddAsync(new DocumentType { Name = "Facture", HasAmount = true, HasDate = true });
            await context.DocumentTypes.AddAsync(new DocumentType { Name = "Bulletin de salaire", HasAmount = true, HasDate = true });
            await context.DocumentTypes.AddAsync(new DocumentType { Name = "Garantie", HasAmount = true, HasDate = true, HasDuration = true });
        }
    }
}
