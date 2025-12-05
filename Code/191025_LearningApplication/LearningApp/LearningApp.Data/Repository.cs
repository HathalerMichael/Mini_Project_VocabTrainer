using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LearningApp.Data;

public static class VocabRepository
{
    private static readonly string _connectionString;
    private static readonly ApplicationDataContext _ctx;

    static VocabRepository()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        _connectionString = configuration.GetConnectionString("LearningApp");

        _ctx = new ApplicationDataContext(
            new DbContextOptionsBuilder<ApplicationDataContext>()
                .UseSqlite(_connectionString)
                .Options);

        _ctx.Database.EnsureCreated();
    }
    public static Task<List<VocabSet>> GetAllVocabSetsAsync() =>
        _ctx.VocabSets
            .Include(vs => vs.VocabCards)
            .AsNoTracking()
            .ToListAsync();

    public static Task<VocabSet?> GetVocabSetByIdAsync(int id) =>
        _ctx.VocabSets
            .Include(vs => vs.VocabCards)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    public static async Task AddVocabSetAsync(VocabSet entity)
    {
        _ctx.VocabSets.Add(entity);
        await _ctx.SaveChangesAsync();
    }

    public static Task<List<VocabCard>> GetAllVocabCardsAsync() =>
        _ctx.VocabCards
            .Include(vc => vc.VocabSet)
            .AsNoTracking()
            .ToListAsync();

    public static Task<List<VocabCard>> GetVocabCardsBySetIdAsync(int setId) =>
        _ctx.VocabCards
            .Include(vc => vc.VocabSet)
            .Where(vc => vc.VocabSetId == setId)
            .AsNoTracking()
            .ToListAsync();

    public static Task<VocabCard?> GetVocabCardByIdAsync(int id) =>
        _ctx.VocabCards
            .Include(vc => vc.VocabSet)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    public static async Task AddVocabCardAsync(VocabCard entity)
    {
        _ctx.VocabCards.Add(entity);
        await _ctx.SaveChangesAsync();
    }
}