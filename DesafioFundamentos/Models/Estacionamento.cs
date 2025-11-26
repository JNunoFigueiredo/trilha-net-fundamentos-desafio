using System.ComponentModel;
using System.Globalization;

namespace DesafioFundamentos.Models
{
    public class Estacionamento  {
        private List<Veiculo> Estacionados = new List<Veiculo>();
        private int[] vagasDisponiveis;
        private int topoPND;
        private int numeroDeVagas = 0;
        private int[] vagasMapa;


        public Estacionamento(int numeroDeVagas) {
            Estacionados.Clear();
            this.numeroDeVagas = numeroDeVagas;
            this.vagasDisponiveis = new int[numeroDeVagas];
            this.vagasMapa = new int[numeroDeVagas];
            this.topoPND = numeroDeVagas - 1; // lembrar que inicia em zero
            for(int i=0; i < numeroDeVagas; i++) {
                this.vagasDisponiveis[i] = i + 1; // coloquei os numeros das vagas iniciando em 1. Não interfere
                this.vagasMapa[i] = 0;
            }

        }

        // reajusta o array que é a pilha de nodos disponíveis
        public void AjustarNumeroDeVagas( int novoNumeroDeVagas) {
            Estacionados.Clear();
            // precisa dar reset para simplificar
            Array.Resize(ref this.vagasDisponiveis, novoNumeroDeVagas);
            for(int i=0; i < novoNumeroDeVagas; i++) {
                this.vagasDisponiveis[i] = i + 1;
                this.vagasMapa[i] = 0;
            }
            this.topoPND = novoNumeroDeVagas - 1; // lembrar que inicia em zero
            this.numeroDeVagas = novoNumeroDeVagas;
        }

        // método especial para o simulador
        public List<int> boxesOcupados() {
            List<int> lista = new();
            for(int i=0; i < this.numeroDeVagas; i++)
                if ( this.vagasMapa[i] != 0)
                    lista.Add(i+1);
            return lista;
        }

        public int VagasDisponiveis() {
            // retorna o total de vagas disponiveis
            return this.topoPND + 1; // +1 PORQUE SEMPRE COMECA O INDEXADOR EM ZERO
        }
        
        private int RetirarVagaDaLista() { 
            // primeiro verifica se tem vaga
            if (this.topoPND == -1)
                return -1; // não tem vaga
            // retorna o numero da vaga (não é o indice do array)
            int vaga = this.vagasDisponiveis[ this.topoPND];
            this.topoPND--;
            return vaga;
        }

        private int RecolocarVagaDaLista( int vaga) { 
            // primeiro verifica se tem vai estourar o total
            if (this.topoPND == this.numeroDeVagas - 1)
                return -1; // não tem vaga
            // retorna 1 para OK e -1 para falha de vaga
            this.topoPND++;
            this.vagasDisponiveis[ this.topoPND] = vaga;
            return 1;
        }

        public void MostraMapa() {
            Console.WriteLine();
            Console.WriteLine();
            int j = 0;
            for( int i = 0; i < this.numeroDeVagas; i++) {
                j++;
                string status = (this.vagasMapa[i] == 0) ? "" : "Ocu";
                Console.Write($"b: {j.ToString("000"), 3} {status, -3} |");
                if ( j % 10 == 0) Console.WriteLine(); // quebra de linha
            }
            Console.WriteLine();
            Console.WriteLine("pressione qualquer tecla para voltar!"); 
            Console.ReadKey(false);
        }

        //Lanco novo veículo na vaga disponivel
        public bool EntrarVeiculo(string placa) {
            int box = RetirarVagaDaLista();
            if (box == -1) 
                return false;
            Veiculo carro = new() {
                Box = box,
                Placa = placa,
                Entrada = DateTime.Now
            };
            this.vagasMapa[box - 1] = 1;
            Estacionados.Add(carro);
            return true;
        }

        public Veiculo SairVeiculo(int box) {
            int res = Estacionados.FindIndex(x => x.Box == box);
            if (res == -1)
                return null;
            Veiculo meuCarro = Estacionados[res];
            this.RecolocarVagaDaLista(meuCarro.Box);
            Estacionados.RemoveAt(res);
            this.vagasMapa[meuCarro.Box - 1] = 0;
            return meuCarro; 
        }

        public Veiculo SairVeiculo(string placa) {
            int res = Estacionados.FindIndex(x => x.Placa.ToLower() == placa.ToLower());
            if (res == -1)
                return null;
            Veiculo meuCarro = Estacionados[res];
            this.RecolocarVagaDaLista(meuCarro.Box);
            Estacionados.RemoveAt(res);
            this.vagasMapa[meuCarro.Box - 1] = 0;
            return meuCarro; 
            
        }
    }
}


/**
* Abaixo é o codigo original. Esta classe dicou apenas para os lancamentos de entrada e saida
**/

 /*
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            // TODO: Pedir para o usuário digitar uma placa (ReadLine) e adicionar na lista "veiculos"
            // *IMPLEMENTE AQUI*
            Console.WriteLine("Digite a placa do veículo para estacionar:");
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            // Pedir para o usuário digitar a placa e armazenar na variável placa
            // *IMPLEMENTE AQUI*
            string placa = "";

            // Verifica se o veículo existe
            if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                // TODO: Pedir para o usuário digitar a quantidade de horas que o veículo permaneceu estacionado,
                // TODO: Realizar o seguinte cálculo: "precoInicial + precoPorHora * horas" para a variável valorTotal                
                // *IMPLEMENTE AQUI*
                int horas = 0;
                decimal valorTotal = 0; 

                // TODO: Remover a placa digitada da lista de veículos
                // *IMPLEMENTE AQUI*

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                // TODO: Realizar um laço de repetição, exibindo os veículos estacionados
                // *IMPLEMENTE AQUI*
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
        */