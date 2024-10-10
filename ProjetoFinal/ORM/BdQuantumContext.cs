using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjetoFinal.ORM;

public partial class BdQuantumContext : DbContext
{
    public BdQuantumContext()
    {
    }

    public BdQuantumContext(DbContextOptions<BdQuantumContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCliente> TbClientes { get; set; }

    public virtual DbSet<TbEndereco> TbEnderecos { get; set; }

    public virtual DbSet<TbProduto> TbProdutos { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    public virtual DbSet<TbVendum> TbVenda { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAB205_9\\SQLEXPRESS;Database=bd_quantum;User Id=quantum;Password=admin2;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCliente>(entity =>
        {
            entity.ToTable("TB_CLIENTE");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<TbEndereco>(entity =>
        {
            entity.ToTable("TB_ENDERECOS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cep).HasColumnName("cep");
            entity.Property(e => e.Cidade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cidade");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FkCliente).HasColumnName("FK_CLIENTE");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PontoReferencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pontoReferencia");

            entity.HasOne(d => d.FkClienteNavigation).WithMany(p => p.TbEnderecos)
                .HasForeignKey(d => d.FkCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_ENDERECOS_TB_CLIENTE");
        });

        modelBuilder.Entity<TbProduto>(entity =>
        {
            entity.ToTable("TB_PRODUTO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.NotaFiscal).HasColumnName("notaFiscal");
            entity.Property(e => e.Preco).HasColumnName("preco");
            entity.Property(e => e.Quant).HasColumnName("quant");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.ToTable("TB_USUARIO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Senha)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("senha");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<TbVendum>(entity =>
        {
            entity.ToTable("TB_VENDA");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FkCliente).HasColumnName("FK_Cliente");
            entity.Property(e => e.FkProduto).HasColumnName("FK_Produto");
            entity.Property(e => e.NotaF)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("notaF");
            entity.Property(e => e.Valor).HasColumnName("valor");

            entity.HasOne(d => d.FkClienteNavigation).WithMany(p => p.TbVenda)
                .HasForeignKey(d => d.FkCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_VENDA_TB_CLIENTE");

            entity.HasOne(d => d.FkProdutoNavigation).WithMany(p => p.TbVenda)
                .HasForeignKey(d => d.FkProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_VENDA_TB_PRODUTO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
