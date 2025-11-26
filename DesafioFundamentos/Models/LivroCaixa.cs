using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFundamentos.Models
{
    public class LivroCaixa
    {
        private decimal saldoInicial;
        private decimal saldoAtual = 0M;
        private List<Caixa> meuLivroCaixa = new List<Caixa>();
       
        public LivroCaixa(decimal saldoInicial) {
            this.saldoInicial = saldoInicial;
            this.Init();
        }

        public void SetSaldoInicial(decimal saldoInicial) { this.saldoInicial = saldoInicial;}

        public void Init() {
            meuLivroCaixa.Clear();
            Caixa meuItemCaixa = new Caixa( 0);
            this.saldoAtual = meuItemCaixa.LancarReceita(DateTime.Now, "Saldo inicial do exercício", this.saldoInicial);
            meuLivroCaixa.Add( meuItemCaixa);
        }

        public decimal LancarReceita(DateTime data, string historico, decimal Receita) {
            Caixa meuItemCaixa = new Caixa( this.saldoAtual);
            this.saldoAtual = meuItemCaixa.LancarReceita(DateTime.Now, historico, Receita);
            meuLivroCaixa.Add( meuItemCaixa);
            return this.saldoAtual;  
        }

        public decimal LancarDespesa(DateTime data, string historico, decimal Despesa) {
            Caixa meuItemCaixa = new Caixa( this.saldoAtual);
            this.saldoAtual = meuItemCaixa.LancarDespesa(DateTime.Now, historico, Despesa);
            meuLivroCaixa.Add( meuItemCaixa);
            return this.saldoAtual;  
        }

    public void ListarCaixa() {
            Console.WriteLine( "Estacionamento do Nuno Figueiredo - Livro Caixa");
            Console.WriteLine();
            Console.WriteLine(new string('-', 122));
            Console.WriteLine( "{0, -20}|{1, -40}|{2, 14}|{3, 14}|{4, 14}|{5, 14}","Data","Histórico","Saldo Anterior","Entrada","Saida","Saldo");
            Decimal receitaTotal = 0M;
            foreach( Caixa meuItemCaixa in this.meuLivroCaixa) {
                receitaTotal += meuItemCaixa.Entrada - meuItemCaixa.Saida;
                Console.WriteLine( $"{meuItemCaixa.DataLancamento, -20}|{meuItemCaixa.Historico,-40}|{meuItemCaixa.SaldoAnterior,14}|{meuItemCaixa.Entrada,14}|{meuItemCaixa.Saida,14}|{meuItemCaixa.Saldo,14}");
                Console.WriteLine(new string('-', 122));
            }
            Console.WriteLine();
            Console.WriteLine($"Receita total do dia: {receitaTotal-this.saldoInicial:C}");
        }

    }
}