using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/**
 *   Classe de registro de uma linha de caixa do sistema
 *   Autor JNunoFigueiredo
 *   2025/11/20
 *   Nesta classe vamos gerar um registro de caixa
 *   decimal SaldoAnterior
 *   decimal Entrada
 *   decimal Saida
 *   decimal Saldo
 *
 */

namespace DesafioFundamentos.Models
{
    public class Caixa {
        public DateTime DataLancamento {get; set;}
        public string Historico  {get; set;}
        public decimal SaldoAnterior {get; set;}
        public decimal Entrada {get; set;}
        public decimal Saida  {get; set;}
        public decimal Saldo  {get; set;}

        public Caixa( decimal SaldoAnterior) {
            this.DataLancamento = DateTime.Now;
            this.Historico = null;
            this.SaldoAnterior = SaldoAnterior;
            this.Entrada = 0M;
            this.Saida = 0M;
            this.Saldo = 0M;
        }

        // Retorna saldo
        public decimal LancarReceita( DateTime DataLancamento, string Historico, decimal Receita) {
            this.DataLancamento = DataLancamento;
            this.Historico = Historico;
            this.Entrada = Receita;
            this.Saldo = this.SaldoAnterior + Receita;
            return this.Saldo;
        }

        public decimal LancarDespesa( DateTime DataLancamento, string Historico, decimal Despesa) {
            this.DataLancamento = DataLancamento;
            this.Historico = Historico;
            this.Saida = Despesa;
            this.Saldo = this.SaldoAnterior - Despesa;
            return this.Saldo;
        }


    }
}