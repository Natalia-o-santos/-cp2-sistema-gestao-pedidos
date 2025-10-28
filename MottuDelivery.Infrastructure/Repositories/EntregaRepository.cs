using Microsoft.EntityFrameworkCore;
using MottuDelivery.Domain.Entities;
using MottuDelivery.Domain.Enums;
using MottuDelivery.Domain.Interfaces;

namespace MottuDelivery.Infrastructure.Repositories;

public class EntregaRepository : IEntregaRepository
{
    private readonly Data.MottuDeliveryDbContext _context;

    public EntregaRepository(Data.MottuDeliveryDbContext context)
    {
        _context = context;
    }

    public async Task<Entrega?> GetByIdAsync(Guid id)
    {
        return await _context.Entregas
            .Include(e => e.Entregador)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Entrega>> GetAllAsync()
    {
        return await _context.Entregas
            .Include(e => e.Entregador)
            .ToListAsync();
    }

    public async Task<Entrega> AddAsync(Entrega entity)
    {
        await _context.Entregas.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Entrega entity)
    {
        _context.Entregas.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Entregas.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Entregas.AnyAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Entrega>> GetByEntregadorIdAsync(Guid entregadorId)
    {
        return await _context.Entregas
            .Include(e => e.Entregador)
            .Where(e => e.EntregadorId == entregadorId)
            .OrderByDescending(e => e.DataCriacao)
            .ToListAsync();
    }

    public async Task<IEnumerable<Entrega>> GetByStatusAsync(StatusEntrega status)
    {
        return await _context.Entregas
            .Include(e => e.Entregador)
            .Where(e => e.Status == status)
            .OrderByDescending(e => e.DataCriacao)
            .ToListAsync();
    }

    public async Task<IEnumerable<Entrega>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        return await _context.Entregas
            .Include(e => e.Entregador)
            .Where(e => e.DataCriacao >= dataInicio && e.DataCriacao <= dataFim)
            .OrderByDescending(e => e.DataCriacao)
            .ToListAsync();
    }

    public async Task<IEnumerable<Entrega>> GetEntregasPendentesAsync()
    {
        return await _context.Entregas
            .Include(e => e.Entregador)
            .Where(e => e.Status == StatusEntrega.Pendente)
            .OrderBy(e => e.DataCriacao)
            .ToListAsync();
    }
}
