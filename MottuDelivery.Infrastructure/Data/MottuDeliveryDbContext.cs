using Microsoft.EntityFrameworkCore;
using MottuDelivery.Domain.Entities;

namespace MottuDelivery.Infrastructure.Data;

public class MottuDeliveryDbContext : DbContext
{
    public MottuDeliveryDbContext(DbContextOptions<MottuDeliveryDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da entidade Cliente
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Status)
                .IsRequired()
                .HasConversion<int>();
            
            entity.Property(e => e.DataCadastro)
                .IsRequired();
            
            entity.Property(e => e.DataUltimaAtualizacao);

            // Índice único
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Configuração da entidade Funcionario
        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Cargo)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Salario)
                .IsRequired()
                .HasPrecision(10, 2);
            
            entity.Property(e => e.Status)
                .IsRequired()
                .HasConversion<int>();
            
            entity.Property(e => e.DataContratacao)
                .IsRequired();
            
            entity.Property(e => e.DataUltimaAtualizacao);
        });

        // Configuração da entidade Pedido
        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(500);
            
            entity.Property(e => e.Status)
                .IsRequired()
                .HasConversion<int>();
            
            entity.Property(e => e.DataCriacao)
                .IsRequired();
            
            entity.Property(e => e.DataInicio);
            entity.Property(e => e.DataConclusao);
            entity.Property(e => e.Observacoes).HasMaxLength(1000);
            
            entity.Property(e => e.ValorTotal)
                .IsRequired()
                .HasPrecision(10, 2);
            
            entity.Property(e => e.ClienteId)
                .IsRequired();

            // Relacionamento com Cliente
            entity.HasOne(e => e.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Many-to-Many com Funcionario
            entity.HasMany(e => e.Funcionarios)
                .WithMany(f => f.Pedidos)
                .UsingEntity(j => j.ToTable("PedidoFuncionarios"));
        });

        // Seed Data
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Clientes
        var cliente1Id = Guid.NewGuid();
        var cliente2Id = Guid.NewGuid();

        modelBuilder.Entity<Cliente>().HasData(
            new Cliente("João Silva", "joao.silva@email.com")
            {
                Id = cliente1Id,
                Status = Domain.Enums.StatusCliente.Ativo,
                DataCadastro = DateTime.UtcNow.AddDays(-30)
            },
            new Cliente("Maria Santos", "maria.santos@email.com")
            {
                Id = cliente2Id,
                Status = Domain.Enums.StatusCliente.Ativo,
                DataCadastro = DateTime.UtcNow.AddDays(-25)
            }
        );

        // Seed Funcionarios
        var funcionario1Id = Guid.NewGuid();
        var funcionario2Id = Guid.NewGuid();

        modelBuilder.Entity<Funcionario>().HasData(
            new Funcionario("Carlos Oliveira", "Desenvolvedor", 5000.00m)
            {
                Id = funcionario1Id,
                Status = Domain.Enums.StatusFuncionario.Ativo,
                DataContratacao = DateTime.UtcNow.AddDays(-60)
            },
            new Funcionario("Ana Costa", "Analista", 4500.00m)
            {
                Id = funcionario2Id,
                Status = Domain.Enums.StatusFuncionario.Ativo,
                DataContratacao = DateTime.UtcNow.AddDays(-45)
            }
        );

        // Seed Pedidos
        modelBuilder.Entity<Pedido>().HasData(
            new Pedido("Desenvolvimento de sistema web", cliente1Id, 15000.00m, "Projeto urgente")
            {
                Id = Guid.NewGuid(),
                Status = Domain.Enums.StatusPedido.Concluido,
                DataCriacao = DateTime.UtcNow.AddDays(-10),
                DataInicio = DateTime.UtcNow.AddDays(-10).AddHours(1),
                DataConclusao = DateTime.UtcNow.AddDays(-5)
            }
        );
    }
}
