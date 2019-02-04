using System.Collections.Generic;

namespace Agenda.Models
{
    public class Contato
    {
        public int Identificador { get; set; }
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }

        public List<DadoAdicional> DadosAdicionais { get; set; }
    }
}
