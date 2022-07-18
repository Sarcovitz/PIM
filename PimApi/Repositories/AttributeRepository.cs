using Microsoft.EntityFrameworkCore;
using PimApi.Data;
using PimApi.Repositories.Interfaces;
using PimModels.Models;

namespace PimApi.Repositories;

public class AttributeRepository : IAttributeRepository
{
    private readonly PimDbContext _context;
    public AttributeRepository(PimDbContext context)
    {
        _context = context;
    }

    public Task<List<ProductAttributeProto>> GetAllProtosByCatalog(int catalogId) => _context.ProductAttributeProtos.Where(p => p.CatalogId == catalogId).ToListAsync();

    public Task<List<ProductAttributeProto>> GetAllProtosByCategory(int categoryId)
      //  => _context.ProductAttributeProtos.Include(p => p.Categories).Where(p => p.Categories.Any(c => c.CategoryId == categoryId)).ToListAsync();
    =>_context.categoryProductAttributeProtos.Where(x=>x.CategoryId == categoryId).Select(x => x.ProductAttributeProto).ToListAsync();

    public Task<List<ProductAttributeProto>> GetAllProtos() => _context.ProductAttributeProtos.ToListAsync();

    public async Task<int?> CreateProto(ProductAttributeProto proto)
    {
        var info = await _context.ProductAttributeProtos.AddAsync(proto);
        await _context.SaveChangesAsync();
        return info.Entity.Id;
    }

    public async Task<ProductAttributeProto?> GetProtoById(int id)
        => await _context.ProductAttributeProtos.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<int> UpdateAsync(ProductAttributeProto model)
    {
        var _proto = await  GetProtoById(model.Id);
        _proto.Name = model.Name;
        _proto.PossibleValues = model.PossibleValues;
        _proto.DefaultValue = model.DefaultValue;
        _proto.AttributeType = model.AttributeType;
        _context.ProductAttributeProtos.Update(_proto);

        return await _context.SaveChangesAsync();
    }
}
