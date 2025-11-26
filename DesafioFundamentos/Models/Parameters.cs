using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

/**
 *   Classe de parametrização do sistema
 *   Author JNunoFigueiredo
 *   2025/11/20
 *   Nesta classe vamos parametrizar os valores iniciais do sistema e seus parâmetros
 *   precoIninal = 12.50 (default) - É o custo para entrar no estacionamento. Em tese, deveria ser o preço 
 *                                   da primeira hora, mas vou respeitar a definição dos instrutores.
 *   precoPorHora = 4.99 (default) - É o custo da hora. Aqui, será o custo por minuto.
 *   numeroDeVagas = 100 (default) - É o total de vagas do estacionamento. Não se poderá comocar mais de 100 carros.
 *   saldoNoCaixa = 231.90 (default) - É o valor em caixa com que se abriu o dia
 *   Vagas  int[100] - Pilha de nodos disponíveis, topoPND=99 - Pinlha de nodos disponíveis
 *
 *   ATENÇÃO: Valores default, o usuário pode alterar no menu do sistema.
 *
 */

namespace DesafioFundamentos.Models
{
    public class Parameters
    {
        public decimal PrecoInicial {get; set;}
        public decimal PrecoPorHora  {get; set;}
        public int NumeroDeVagas  {get; set;}
        public decimal SaldoNoCaixa  {get; set;}

        
        // Construtor da classe assumindo o valor inicial
        public Parameters() {
            // setar valoress default
            this.PrecoInicial = 12.50M;
            this.PrecoPorHora = 4.99M;
            this.NumeroDeVagas = 100;
            this.SaldoNoCaixa = 231.90M;
        }


    }
}