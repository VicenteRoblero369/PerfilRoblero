using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerfilRoblero.Modelos;

namespace PerfilRoblero.AccesoDatos.Configuracion
{
    public class ChatConfiguracion : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.UsuarioAplicacionId).IsRequired();
            builder.Property(x => x.Textos).IsRequired().HasMaxLength(100);


            builder.HasOne(x => x.UsuarioAplicacion).WithMany()
                   .HasForeignKey(x => x.UsuarioAplicacionId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
