using System.Collections.Generic;

namespace Agenda.Regra
{
    public class Contato
    {
        public virtual int Identificador { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Empresa { get; set; }
        public virtual string Cep { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Cidade { get; set; }
        public virtual string Uf { get; set; }

        public virtual List<DadoAdicional> DadosAdicionais { get; set; }
    }
}
