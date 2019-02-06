using Agenda.Models;
using Agenda.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Agenda.Services
{
    public class AgendaService : IAgendaService
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
                        },
                        new DadoAdicional
                        {
                            TipoDado = new TipoDadoAdicional { Sigla = "TELEFONE", Nome = "Telefone" },
                            ClassificacaoDado = new ClassificacaoDadoAdicional { Sigla = "TRABALHO", Nome = "Trabalho" },
                            Valor = "1197889888"
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
            return _contatos.Where(c => c.Identificador == p_IdContato).OrderBy(c => c.DadosAdicionais.OrderBy(d => d.TipoDado.Sigla)).FirstOrDefault();
        }

        public RetornoTO IncluirContato(Contato p_Contato)
        {
            try
            {
                RetornoTO ret = ValidarDadosContato(p_Contato);
                if (!ret.Sucesso)
                {
                    return ret;
                }

                int idMax = _contatos.Max(c => c.Identificador);
                p_Contato.Identificador = (idMax + 1);
                _contatos.Add(p_Contato);

                return new RetornoTO { Sucesso = true };
            }
            catch (Exception ex)
            {
                return new RetornoTO { Sucesso = false, Mensagem = ex.ToString() };
            }
        }

        private RetornoTO ValidarDadosContato(Contato p_Contato)
        {
            /*
            * Nome do contato é obrigatório
             Telefone do contato é obrigatório
             Um contato pode ter mais de um número de telefone, mas cada número deve possuir uma
            classificação (casa, trabalho, outro)
             E-mail do contato é opcional
             Um contato pode ter mais de um e-mail, mas cada endereço deve possuir uma
            classificação (casa, trabalho, outro)
             Empresa é um campo opcional
             Endereço é um campo opcional
            */

            if (p_Contato == null)
            {
                return new RetornoTO { Sucesso = false, Mensagem = "Dados do contato não informado!" };
            }

            List<string> erros = new List<string>();

            if (string.IsNullOrWhiteSpace(p_Contato.Nome))
            {
                erros.Add("Nome do contato é obrigatório!");
            }

            if (p_Contato.DadosAdicionais == null)
            {
                erros.Add("Telefone do contato é obrigatório!");
            }

            DadoAdicional dado = p_Contato.DadosAdicionais.Where(d => "TELEFONE".Equals(d.TipoDado.Sigla)).FirstOrDefault();

            if (dado == null || string.IsNullOrWhiteSpace(dado.Valor))
            {
                erros.Add("Telefone do contato é obrigatório!");
            }
            else
            {
                foreach (var item in p_Contato.DadosAdicionais)
                {
                    if ("TELEFONE".Equals(item.TipoDado.Sigla))
                    {
                        if (!ValidarTelefone(item.Valor))
                        {
                            erros.Add("Telefone inválido!");
                        }
                    }
                    else if ("EMAIL".Equals(item.TipoDado.Sigla))
                    {
                        if (!ValidarEmail(item.Valor))
                        {
                            erros.Add("E-mail inválido!");
                        }
                    }
                }
            }

            if (erros.Count > 0)
            {
                string msg = string.Join("<br />", erros);
                return new RetornoTO { Sucesso = false, Mensagem = msg };
            }

            return new RetornoTO { Sucesso = true };
        }

        private bool ValidarEmail(string p_Email)
        {
            string regex = @"/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/ig";
            return Regex.IsMatch(regex, p_Email);
        }

        private bool ValidarTelefone(string p_Telefone)
        {
            string regex = @"/^(?:(?:\+|00)?(55)\s?)?(?:\(?([1-9][0-9])\)?\s?)?(?:((?:9\d|[2-9])\d{3})\-?(\d{4}))$/";
            return Regex.IsMatch(regex, p_Telefone);
        }

        public RetornoTO AlterarContato(Contato p_Contato)
        {
            try
            {
                RetornoTO ret = ValidarDadosContato(p_Contato);
                if (!ret.Sucesso)
                {
                    return ret;
                }

                Contato contato = this.ObterContatoPorId(p_Contato.Identificador);
                if (contato == null)
                {
                    return new RetornoTO { Sucesso = false, Mensagem = "Contato não localizado!" };
                }

                _contatos.Remove(contato);
                _contatos.Add(p_Contato);

                return new RetornoTO { Sucesso = true };
            }
            catch (Exception ex)
            {
                return new RetornoTO { Sucesso = false, Mensagem = ex.ToString() };
            }
        }

        public RetornoTO ExcluirContato(int p_IdContato)
        {
            try
            {
                Contato contato = this.ObterContatoPorId(p_IdContato);
                if (contato == null)
                {
                    return new RetornoTO { Sucesso = false, Mensagem = "Contato não localizado!" };
                }

                _contatos.Remove(contato);

                return new RetornoTO { Sucesso = true };
            }
            catch (Exception ex)
            {
                return new RetornoTO { Sucesso = false, Mensagem = ex.ToString() };
            }

        }

        public TipoDadoAdicional ObterTipoDadoAdcional(string p_Sigla)
        {
            if (string.IsNullOrWhiteSpace(p_Sigla))
            {
                throw new Exception("Sigla do tipo de dado não informando!");
            }

            List<TipoDadoAdicional> tipos = new List<TipoDadoAdicional>();
            tipos.Add(new TipoDadoAdicional { Sigla = "TELEFONE", Nome = "Telefone" });
            tipos.Add(new TipoDadoAdicional { Sigla = "EMAIL", Nome = "E-mail" });

            return tipos.Where(t => p_Sigla.Equals(t.Sigla)).FirstOrDefault();
        }

        public ClassificacaoDadoAdicional ObterClassificacaoDado(string p_Sigla)
        {
            if (string.IsNullOrWhiteSpace(p_Sigla))
            {
                throw new Exception("Sigla da classificação de dado não informando!");
            }

            List<ClassificacaoDadoAdicional> classificacao = new List<ClassificacaoDadoAdicional>();
            classificacao.Add(new ClassificacaoDadoAdicional { Sigla = "CASA", Nome = "Casa" });
            classificacao.Add(new ClassificacaoDadoAdicional { Sigla = "TRABALHO", Nome = "Trabalho" });

            return classificacao.Where(c => p_Sigla.Equals(c.Sigla)).FirstOrDefault();
        }
    }
}
