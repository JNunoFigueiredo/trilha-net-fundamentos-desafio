using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Swift;
using System.Threading.Tasks;
using DesafioFundamentos.Models;

namespace DesafioFundamentos {
    public class Business {
        private Parameters param = new();
        private LivroCaixa livroCaixa;
        private Estacionamento estacionamento;


        public Business() { 
            livroCaixa = new(param.SaldoNoCaixa);
            estacionamento = new(param.NumeroDeVagas);
            this.Init();
        }

        public void Init() {
            estacionamento.AjustarNumeroDeVagas( param.NumeroDeVagas);
            livroCaixa.Init();
        }



        public void OpcoesParametrizar() {
            // string opcao = string.Empty;
            bool parar = false;
            ConsoleKeyInfo keyPress;
            decimal newPreco = 0M;

            do {
                Console.Clear();
                Console.WriteLine("Digite a sua opção:");
                Console.WriteLine("R - Retornar menu Anterior");
                Console.WriteLine($"I - Valor Inicial: Atual: {param.PrecoInicial:C}");
                Console.WriteLine($"H - Valor da Hora: Atual: {param.PrecoPorHora:C}");
                Console.WriteLine($"V - Total de Vagas: (será reiniciado) {param.NumeroDeVagas}");
                Console.WriteLine($"A - Abertura de caixa: (será reiniciado) {param.SaldoNoCaixa:C}");
                Console.WriteLine($"C - Listar Caixa");
                
                //opcao = Console.ReadLine().ToLower();  mudado para readkey
                keyPress = Console.ReadKey(true); 
                // opcao = ;

                switch (keyPress.Key) {
                    case ConsoleKey.R:
                        parar = true;
                        break;

                    case ConsoleKey.I:
                        newPreco = 0M; 
                        while (true) {
                            Console.Write($" Informe valor Inicial: Atual = ({param.PrecoInicial.ToString("0.00")})->");
                            string valor = Console.ReadLine();
                            try {
                                newPreco = Convert.ToDecimal(valor);
                                break;
                            } catch(Exception e) {
                                Console.WriteLine($"Erro: {e.Message}");
                                Console.WriteLine("Entre um valor válido");
                            }
                        }
                        param.PrecoInicial =  newPreco;
                        break;
                    
                    case ConsoleKey.H:
                        newPreco = 0M;
                        while (true) {
                            Console.Write($" Informe valor p/Hora: Atual = ({param.PrecoPorHora.ToString("0.00")})->");
                            string valor = Console.ReadLine();
                            try {
                                newPreco = Convert.ToDecimal(valor);
                                break;
                            } catch(Exception e) {
                                Console.WriteLine($"Erro: {e.Message}");
                                Console.WriteLine("Entre um valor válido");
                            }
                        }
                        param.PrecoPorHora =  newPreco;
                        break;

                    case ConsoleKey.V:
                        int vagas = 0;
                        while (true) {
                            Console.Write($" Informe Total de Vagas: Atual = ({param.NumeroDeVagas})->");
                            string valor = Console.ReadLine();
                            try {
                                vagas = Convert.ToInt32(valor);
                                break;
                            } catch(Exception e) {
                                Console.WriteLine($"Erro: {e.Message}");
                                Console.WriteLine("Entre um valor válido");
                            }
                        }
                        param.NumeroDeVagas =  vagas;
                        this.Init();
                        break;
                    

                    case ConsoleKey.A:
                        decimal newStart = 0M;
                        while (true) {
                            Console.Write($" Informe valor de abertura de caixa: Atual = ({param.SaldoNoCaixa.ToString("0.00")})->");
                            string valor = Console.ReadLine();
                            try {
                                newStart = Convert.ToDecimal(valor);
                                break;
                            } catch(Exception e) {
                                Console.WriteLine($"Erro: {e.Message}");
                                Console.WriteLine("Entre um valor válido");
                            }
                        }
                        param.SaldoNoCaixa =  newStart;
                        livroCaixa.SetSaldoInicial( newStart);
                        this.Init();
                        break;

                    case ConsoleKey.C:
                        Console.Clear();
                        livroCaixa.ListarCaixa();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Aperte qualquer tecla");
                        Console.ReadKey(true);
                        break;
                    
                    
                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
            } while (! parar);    

        }

        public void geradorSim(int tempoMin) {
            Random gerador = new();
            TimeSpan tempo, tempoEst;
            DateTime inicio = DateTime.Now;
            int rnd = 0;
            int op = 0;
            string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string placa;
            
            while (true) {
                tempo = DateTime.Now - inicio;
                if (tempo.Minutes >= tempoMin)
                    break;

                // num aleatório para saber que operação
                // [0..3] sem op 
                // [4..8] op entrada
                // [9..13] op saida
                rnd = gerador.Next(0, 13);
                if (rnd <= 3)
                    continue;
                else if ( rnd >3 && rnd <=8)
                    if (estacionamento.VagasDisponiveis() == 0)
                        continue; // lotado
                    else
                        op = 1;
                else if ( rnd >=9 && rnd <=12)
                    if (estacionamento.VagasDisponiveis() == param.NumeroDeVagas)
                        continue; // vazio
                    else
                        op = 2;
                
                if (op == 1) {
                    // entrada
                    placa = "";
                    for (int i = 0; i < 3; i++) {
                        placa = placa + caracteres[gerador.Next(0,26)];
                    }
                    placa = placa + gerador.Next(0,10000).ToString();
                    Console.Write($"Sim: {DateTime.Now} Entrada de carro: {placa} ");
                    if ( ! estacionamento.EntrarVeiculo(placa))
                        Console.WriteLine("fracassou");
                    else 
                        Console.WriteLine("Ok;");
    
                } else {
                    // saida
                    // precisa de um box aleatório para retirar um carro para isso, vou carregar uma lista de boxes usados
                    List<int> boxes = estacionamento.boxesOcupados();
                    int indice = gerador.Next(0, boxes.Count);
                    int box = boxes[ indice];
                    Console.Write($"Sim: {DateTime.Now} Saida de carro: {box} ");
                    Veiculo meuCarro = estacionamento.SairVeiculo(box);
                    if ( meuCarro==null)
                        Console.WriteLine("fracassou");
                    else {
                        tempoEst = DateTime.Now - meuCarro.Entrada;
                        decimal preco = param.PrecoInicial + (tempoEst.Minutes + 1) * param.PrecoPorHora; // cada minuto conta uma hora, comtcando em zero~
                        string hist = $"Receita veículo {meuCarro.Placa} por {tempoEst.Minutes + 1} h Box {box}";
                        livroCaixa.LancarReceita(DateTime.Now, hist, preco);
                        Console.WriteLine("Ok;");
                    }
                }
                // sleep
                Thread.Sleep(500); // para 0,5 seg.



            }
            Console.WriteLine("Saindo da simulação...");
        }

        public void MenuPrincipal() {
            //string opcao = string.Empty;
            bool parar = false;
            ConsoleKeyInfo keyPress;
            string opt;
            TimeSpan tempo;
            decimal preco;
            string hist;
            do {
                Console.Clear();
                Console.WriteLine("Digite a sua opção:");
                Console.WriteLine("P - Parametrizar/Informações");
                Console.WriteLine("E - Entrada Manual de Veículo");
                Console.WriteLine("B - Saida Manual de Veículo (box)");
                Console.WriteLine("S - Saida Manual de Veículo (placa)");
                Console.WriteLine("V - Situação Atual (das vagas)");
                Console.WriteLine("G - Gerar uma simulação!");
                Console.WriteLine("X - Encerrar");
                Console.WriteLine();
                Console.WriteLine($"Vagas disponiveis: {estacionamento.VagasDisponiveis()}");
                //opcao = Console.ReadLine().ToLower();
                keyPress = Console.ReadKey(true); 
                // opcao = ;
                
                switch (keyPress.Key) {
                    case ConsoleKey.P:
                        this.OpcoesParametrizar();
                        break;

                    case ConsoleKey.V:
                        Console.Clear();
                        Console.WriteLine($"Vagas disponiveis: {estacionamento.VagasDisponiveis()}");
                        Console.WriteLine();
                        estacionamento.MostraMapa();
                        break;
                    
                    case ConsoleKey.E:
                        Console.Clear();
                        Console.WriteLine($"Entrada de Veículo");
                        Console.WriteLine($"Vagas disponiveis: {estacionamento.VagasDisponiveis()}");
                        Console.WriteLine();
                        Console.Write("Informe a placa do veículo: ");
                        opt = Console.ReadLine();
                        if ( ! estacionamento.EntrarVeiculo(opt))
                            Console.WriteLine("Não há vagas. Aperte qualquer tecla");
                        else 
                            Console.WriteLine("Veículo estacionado. Aperte qualquer tecla");
                        Console.ReadKey(false);
                        break;
                    
                    case ConsoleKey.B:
                        Console.Clear();
                        Console.WriteLine("Saída de Veículo");
                        int box = 0;
                        while (true) {
                            Console.Write("Informe o Box (0 para cancelar) -> ");
                            string valor = Console.ReadLine();
                            try {
                                box = Convert.ToInt32(valor);
                                break;
                            } catch(Exception e) {
                                Console.WriteLine($"Erro: {e.Message}");
                                Console.WriteLine("Entre um valor válido");
                            }
                        }
                        if (box == 0) 
                            break;
                        Veiculo meuCarro = estacionamento.SairVeiculo(box);
                        if (meuCarro == null) {
                            Console.WriteLine("Veículo não encontrado. Aperte qualquer tecla!");
                            Console.ReadKey(false);
                            break;
                        }
                        tempo = DateTime.Now - meuCarro.Entrada;
                        preco = param.PrecoInicial + (tempo.Minutes + 1) * param.PrecoPorHora; // cada minuto conta uma hora, comcando em zero~
                        hist = $"Receita veículo {meuCarro.Placa} por {tempo.Minutes + 1}";
                        livroCaixa.LancarReceita(DateTime.Now, hist, preco);

                        Console.WriteLine("Veículo liberado, vaga disponibilisada. Receita lançada.");
                        Console.WriteLine("Visualize o caixa no menu de informaçõe.");
                        Console.WriteLine("Visualize as vagas no menu item V.");
                        Console.WriteLine();
                        Console.WriteLine("Aperte qualquer tecla!");
                        Console.ReadKey(false);
                        break;

                    case ConsoleKey.S:
                        Console.Clear();
                        Console.WriteLine($"Entrada de Veículo");
                        Console.WriteLine($"Vagas disponiveis: {estacionamento.VagasDisponiveis()}");
                        Console.WriteLine();
                        Console.Write("Informe a placa do veículo (vazio para desistir): ");
                        opt = Console.ReadLine();
                        if (opt == "" || opt == null)
                            break;

                        Veiculo myCar = estacionamento.SairVeiculo(opt);
                        if (myCar == null) {
                            Console.WriteLine("Veículo não encontrado. Aperte qualquer tecla!");
                            Console.ReadKey(false);
                            break;
                        }
                        tempo = DateTime.Now - myCar.Entrada;
                        preco = param.PrecoInicial + (tempo.Minutes + 1) * param.PrecoPorHora; // cada minuto conta uma hora, comcando em zero~
                        hist = $"Receita veículo {myCar.Placa} por {tempo.Minutes + 1}";
                        livroCaixa.LancarReceita(DateTime.Now, hist, preco);

                        Console.WriteLine("Veículo liberado, vaga disponibilisada. Receita lançada.");
                        Console.WriteLine("Visualize o caixa no menu de informaçõe.");
                        Console.WriteLine("Visualize as vagas no menu item V.");
                        Console.WriteLine();
                        Console.Write("Aperte qualquer tecla!");
                        Console.ReadKey(false);
                        break;
                    
                    case ConsoleKey.G:
                        Console.Clear();
                        Console.WriteLine("Rodar por Simulação");
                        int tempoMin = 0;
                        while (true) {
                            Console.Write("Informe tempo em minutos (0 para cancelar) -> ");
                            string valor = Console.ReadLine();
                            try {
                                tempoMin = Convert.ToInt32(valor);
                                break;
                            } catch(Exception e) {
                                Console.WriteLine($"Erro: {e.Message}");
                                Console.WriteLine("Entre um valor válido");
                            }
                        }
                        if (tempoMin == 0) {
                            Console.WriteLine("Simulção Abortada! Continue normalmente...");
                            Console.Write("Aperte qualquer tecla...");
                            Console.ReadKey(false);
                            break;
                        }
                        this.geradorSim(tempoMin);
                        Console.WriteLine();
                        Console.WriteLine("Simulação executada. Pode continuar nuo manual ou consultar as situações.");
                        Console.WriteLine();
                        Console.Write("Aperte qualquer tecla!");
                        Console.ReadKey(false);
                        break;

                    case ConsoleKey.X:
                        parar = true;
                        break;

                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
            } while (! parar);

//     Console.WriteLine("Pressione uma tecla para continuar");
//     Console.ReadLine();
// }

// Console.WriteLine("O programa se encerrou");
    
        }
    }
}

/*





// Coloca o encoding para UTF8 para exibir acentuação
Console.OutputEncoding = System.Text.Encoding.UTF8;
 


param.precoPorHora = 5;
param.AjustarNumeroDeVagas(10);
Console.WriteLine(param.precoPorHora);
Console.WriteLine(param.VagasDisponiveis());
Console.WriteLine(param.RecolocarVagaDaLista(11));
Console.WriteLine(param.VagasDisponiveis());
Console.WriteLine(param.RetirarVagaDaLista());
Console.WriteLine(param.VagasDisponiveis());
Console.WriteLine(param.RetirarVagaDaLista());
Console.WriteLine(param.VagasDisponiveis());
Console.WriteLine(param.RetirarVagaDaLista());
Console.WriteLine(param.VagasDisponiveis());
*/
// decimal precoInicial = 0;
// decimal precoPorHora = 0;

// Console.WriteLine("Seja bem vindo ao sistema de estacionamento!\n" +
//                   "Digite o preço inicial:");
// precoInicial = Convert.ToDecimal(Console.ReadLine());

// Console.WriteLine("Agora digite o preço por hora:");
// precoPorHora = Convert.ToDecimal(Console.ReadLine());

// // Instancia a classe Estacionamento, já com os valores obtidos anteriormente
// Estacionamento es = new Estacionamento(precoInicial, precoPorHora);

