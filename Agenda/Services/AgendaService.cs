using Agenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agenda.Services
{
    public class AgendaService
    {
        private List<Contato> _contatos;
        public AgendaService()
        {
            _contatos = new List<Contato>()
            {
                new Contato { Identificador = 1, Nome = "Erlan Torres de Aguiar", Empresa = "Erlan Ltda", Cep = "06020194",
                    Logradouro = "Av Manoel Pedro Pimental", Numero = "200", Complemento = "Bloco 12 Apto 54",
                    Bairro = "Continental", Cidade = "Osasco", Uf = "SP",
                    DadosAdicionais = new List<DadoAdicional>
                    {
                        new DadoAdicional
                        {
                            TipoDado = new TipoDadoAdicional { Sigla = "TELEFONE", Nome = "Telefone" },
                            ClassificacaoDado = new ClassificacaoDadoAdicional { Sigla = "CASA", Nome = "Casa" },
                            Valor = "11954203111"
                        },
                        new DadoAdicional
                        {
                            TipoDado = new TipoDadoAdicional { Sigla = "TELEFONE", Nome = "Telefone" },
                            ClassificacaoDado = new ClassificacaoDadoAdicional { Sigla = "TRABALHO", Nome = "Trabalho" },
                            Valor = "11954203111"
                        },
                        new DadoAdicional
                        {
                            TipoDado = new TipoDadoAdicional { Sigla = "EMAIL", Nome = "E-mail" },
                            ClassificacaoDado = new ClassificacaoDadoAdicional { Sigla = "CASA", Nome = "Casa" },
                            Valor = "erlantorres@hotmail.com"
                        }
                    }
                },
                new Contato { Identificador = 20, Nome = "Júlia Peres Pereira", Empresa = "Magical Clock Ltda", Cep = "06020194",
                    Logradouro = "Av Manoel Pedro Pimental", Numero = "200", Complemento = "Bloco 12 Apto 54",
                    Bairro = "Continental", Cidade = "Osasco", Uf = "SP",
                    DadosAdicionais = new List<DadoAdicional>
                    {
                        new DadoAdicional
                        {
                            TipoDado = new TipoDadoAdicional { Sigla = "TELEFONE", Nome = "Telefone" },
                            ClassificacaoDado = new ClassificacaoDadoAdicional { Sigla = "TRABALHO", Nome = "Trabalho" },
                            Valor = "11954203111"
                        },
                        new DadoAdicional
                        {
                            TipoDado = new TipoDadoAdicional { Sigla = "EMAIL", Nome = "E-mail" },
                            ClassificacaoDado = new ClassificacaoDadoAdicional { Sigla = "CASA", Nome = "Casa" },
                            Valor = "julia-peres@hotmail.com"
                        }
                    }
                }
            };
        }

        public List<Contato> ListarContatos(string p_Filter)
        {
            return AplicarFiltro(p_Filter, _contatos).OrderBy(c => c.Nome).ToList();
        }

        private List<Contato> AplicarFiltro(string p_Filter, List<Contato> contatos)
        {
            if (!string.IsNullOrWhiteSpace(p_Filter) && contatos != null && contatos.Count > 0)
            {
                List<Contato> aux = new List<Contato>();
                aux = contatos.Where(c => c.Nome.Contains(p_Filter, System.StringComparison.OrdinalIgnoreCase)).ToList();

                foreach (var contato in contatos)
                {
                    if (contato.DadosAdicionais != null)
                    {
                        foreach (var dado in contato.DadosAdicionais)
                        {
                            if (dado.Valor.Contains(p_Filter, System.StringComparison.OrdinalIgnoreCase))
                            {
                                if (!aux.Contains(contato))
                                {
                                    aux.Add(contato);
                                }
                            }
                        }
                    }
                }

                contatos = aux;
            }

            return contatos;
        }

        public Contato ObterContatoPorId(int p_IdContato)
        {
            return _contatos.Where(c => c.Identificador == p_IdContato).FirstOrDefault();
        }

        public bool IncluirContato(Contato p_Contato)
        {
            throw new NotImplementedException();
        }

        public bool AlterarContato(Contato p_Contato)
        {
            throw new NotImplementedException();
        }

        public bool ExcluirContato(int p_IdContato)
        {
            throw new NotImplementedException();
        }
    }
}
