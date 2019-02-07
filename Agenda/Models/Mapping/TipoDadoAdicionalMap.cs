using FluentNHibernate.Mapping;

namespace Agenda.Models.Mapping
{
    public class TipoDadoAdicionalMap : ClassMap<TipoDadoAdicionalModel>
    {
        public TipoDadoAdicionalMap()
        {
            Id(i => i.SiglaTipo);
            Map(m => m.Nome);

            Table("Tipo_Dado_Adicional");
        }
    }
}
