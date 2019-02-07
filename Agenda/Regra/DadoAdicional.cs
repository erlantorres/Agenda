namespace Agenda.Regra
{
    public class DadoAdicional
    {
        public virtual TipoDadoAdicional TipoDado { get; set; }
        public virtual ClassificacaoDadoAdicional ClassificacaoDado { get; set; }
        public virtual string Valor { get; set; }
    }
}
