using FluentNHibernate.Mapping;

namespace Agenda.Models.Mapping
{
    public class ContatoMap : ClassMap<ContatoModel>
    {
        public ContatoMap()
        {
            Id(i => i.Identificador).GeneratedBy.Identity();

            Map(m => m.Nome);
            Map(m => m.Empresa);
            Map(m => m.Cep);
            Map(m => m.Logradouro);
            Map(m => m.Numero);
            Map(m => m.Complemento);
            Map(m => m.Bairro);
            Map(m => m.Cidade);
            Map(m => m.Uf);

            Table("Contato");
        }
    }
}
