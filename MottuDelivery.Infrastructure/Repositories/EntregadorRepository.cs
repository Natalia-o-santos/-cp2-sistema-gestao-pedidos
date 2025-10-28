using Microsoft.EntityFrameworkCore;
using MottuDelivery.Domain.Entities;
using MottuDelivery.Domain.Enums;
using MottuDelivery.Domain.Interfaces;

namespace MottuDelivery.Infrastructure.Repositories;

public class EntregadorRepository : IEntregadorRepository
{
    private readonly Data.MottuDeliveryDbContext _context;

    public EntregadorRepository(Data.MottuDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<Entregador?> GetByIdAsync(Guid id)
    {
        return await _context.Entregadores
            .Include(e => e.Entregas)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Entregador>> GetAllAsync()
    {
        return await _context.Entregadores
            .Include(e => e.Entregas)
            .ToListAsync();
    }

    public async Task<Entregador> AddAsync(Entregador entity)
    {
        await _context.Entregadores.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Entregador entity)
    {
        _context.Entregadores.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Entregadores.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Entregadores.AnyAsync(e => e.Id == id);
    }

    public async Task<Entregador?> GetByCpfAsync(string cpf)
    {
        return await _context.Entregadores
            .Include(e => e.Entregas)
            .FirstOrDefaultAsync(e => e.Cpf == cpf);
    }

    public async Task<Entregador?> GetByEmailAsync(string email)
    {
        return await _context.Entregadores
            .Include(e => e.Entregas)
            .FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task<IEnumerable<Entregador>> GetByStatusAsync(StatusEntregador status)
    {
        return await _context.Entregadores
            .Include(e => e.Entregas)
            .Where(e => e.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Entregador>> GetDisponiveisAsync()
    {
        return await _context.Entregadores
            .Include(e => e.Entregas)
            .Where(e => e.Status == StatusEntregador.Ativo)
            .ToListAsync();
    }
}
