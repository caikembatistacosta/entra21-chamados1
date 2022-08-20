using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    internal class ChamadoMapConfig : IEntityTypeConfiguration<Chamado>
    {
        public void Configure(EntityTypeBuilder<Chamado> builder)
        {
            builder.ToTable("CHAMADOS");
            builder.Property(c => c.Nome).HasMaxLength(20).IsRequired().IsUnicode(false);
            builder.Property(c => c.DescricaoCurta).HasMaxLength(50).IsRequired().IsUnicode(false);
            builder.Property(c => c.DescricaoDetalhada).HasMaxLength(100).IsRequired().IsUnicode(false);
            builder.Property(c => c.ImagemUrl).HasMaxLength(2048).IsRequired().IsUnicode(false);
            builder.Property(c => c.DataInicio).IsRequired().HasColumnType("datetime2");
            builder.Property(c => c.DataFim).IsRequired().HasColumnType("datetime2");
        }
    }
}
