namespace Agenda.Models
{
    public class DadoAdicionalModel
    {
        public virtual int Id { get; set; }
        public virtual ContatoModel Contato { get; set; }
        public virtual TipoDadoAdicionalModel TipoDado { get; set; }
        public virtual ClassificacaoDadoAdicionalModel ClassificacaoDado { get; set; }
        public virtual string Valor { get; set; }
    }
}
