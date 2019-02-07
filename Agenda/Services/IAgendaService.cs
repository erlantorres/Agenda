using Agenda.Models.DTO;
using Agenda.Regra;
using System.Collections.Generic;

namespace Agenda.Services
{
    public interface IAgendaService
    {
        List<Contato> ListarContatos(string p_Filter);
        Contato ObterContatoPorId(int p_IdContato);
        RetornoTO IncluirContato(Contato p_Contato);
        RetornoTO AlterarContato(Contato p_Contato);
        RetornoTO ExcluirContato(int p_IdContato);
    }
}
