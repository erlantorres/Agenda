using FluentNHibernate.Mapping;

namespace Agenda.Models.Mapping
{
    public class ClassificacaoDadoAdicionalMap : ClassMap<ClassificacaoDadoAdicionalModel>
    {
        public ClassificacaoDadoAdicionalMap()
        {
            Id(i => i.SiglaClassificacao);
            Map(m => m.Nome);

            Table("Classificacao_Dado_Adicional");
        }
    }
}
