using FluentNHibernate.Mapping;

namespace Agenda.Models.Mapping
{
    public class DadoAdicionalMap : ClassMap<DadoAdicionalModel>
    {
        public DadoAdicionalMap()
        {
            //CompositeId().KeyReference(k => k.Contato, "Identificador");
            //CompositeId().KeyReference(k => k.TipoDado, "SiglaTipo");
            //CompositeId().KeyReference(k => k.ClassificacaoDado, "SiglaClassificacao");
            Id(i => i.Id).GeneratedBy.Identity();
            References(r => r.Contato).Column("Identificador");
            References(r => r.TipoDado).Column("SiglaTipo");
            References(r => r.ClassificacaoDado).Column("SiglaClassificacao");
            Map(m => m.Valor);

            Table("Dado_Adicional");
        }
    }
}
