
# VER Abaixo a minha solução ampliada.



# DIO - Trilha .NET - Fundamentos
www.dio.me

## Desafio de projeto
Para este desafio, você precisará usar seus conhecimentos adquiridos no módulo de fundamentos, da trilha .NET da DIO.

## Contexto
Você foi contratado para construir um sistema para um estacionamento, que será usado para gerenciar os veículos estacionados e realizar suas operações, como por exemplo adicionar um veículo, remover um veículo (e exibir o valor cobrado durante o período) e listar os veículos.

## Proposta
Você precisará construir uma classe chamada "Estacionamento", conforme o diagrama abaixo:
![Diagrama de classe estacionamento](diagrama_classe_estacionamento.png)

A classe contém três variáveis, sendo:

**precoInicial**: Tipo decimal. É o preço cobrado para deixar seu veículo estacionado.

**precoPorHora**: Tipo decimal. É o preço por hora que o veículo permanecer estacionado.

**veiculos**: É uma lista de string, representando uma coleção de veículos estacionados. Contém apenas a placa do veículo.

A classe contém três métodos, sendo:

**AdicionarVeiculo**: Método responsável por receber uma placa digitada pelo usuário e guardar na variável **veiculos**.

**RemoverVeiculo**: Método responsável por verificar se um determinado veículo está estacionado, e caso positivo, irá pedir a quantidade de horas que ele permaneceu no estacionamento. Após isso, realiza o seguinte cálculo: **precoInicial** * **precoPorHora**, exibindo para o usuário.

**ListarVeiculos**: Lista todos os veículos presentes atualmente no estacionamento. Caso não haja nenhum, exibir a mensagem "Não há veículos estacionados".

Por último, deverá ser feito um menu interativo com as seguintes ações implementadas:
1. Cadastrar veículo
2. Remover veículo
3. Listar veículos
4. Encerrar


## Solução
O código está pela metade, e você deverá dar continuidade obedecendo as regras descritas acima, para que no final, tenhamos um programa funcional. Procure pela palavra comentada "TODO" no código, em seguida, implemente conforme as regras acima.

# Minha solução
Na intenção de aplicar mais a fundo os meus conhecimentos, acrescentando o que já sei de outras linguagens, escolhi fazer um programa mais complexo
Ele é composto por dois menus:
Menu principal e menu de parametrização.
O menu de parametrização é o primeiro item do menu principal.
Ele abre as opções de configuração.
Além de calcular o tempo de estacionamento, o programa irá gerar um livro caixa, que usa uma colection de uma classe Caixa.
Outra característica deste programa consiste em controlar o número do vagas do estacionamento (parametrizavel). Para tal, usa uma lista de classes do tipo Veiculo (placa, box e hora de entrada), uma pilha de nodos disponiveis (array em forma de pilha) para controle de vagas disponíveis, e um arrai simples para o mapa de estacionamento, visível na tela quando aperta a tecla V.
Para fazer testes e experimentos, tem um pequeno (e muito simples) simulador.
O sistema computa cada minuto passado como uma hora.

Espero que ele agrade a todos e, se alguém encontrar falhas, agradeço por me avisar.

Menus:

Digite a sua opção:
P - Parametrizar/Informações
E - Entrada Manual de Veículo
B - Saida Manual de Veículo (box)
S - Saida Manual de Veículo (placa)
V - Situação Atual (das vagas)
G - Gerar uma simulação!
X - Encerrar

Vagas disponiveis: 100


Param:
Digite a sua opção:
R - Retornar menu Anterior
I - Valor Inicial: Atual: R$ 12,50
H - Valor da Hora: Atual: R$ 4,99
V - Total de Vagas: (será reiniciado) 100
A - Abertura de caixa: (será reiniciado) R$ 231,90
C - Listar Caixa


Caixa:
Estacionamento do Nuno Figueiredo - Livro Caixa

--------------------------------------------------------------------------------------------------------------------------
Data                |Histórico                               |Saldo Anterior|       Entrada|         Saida|         Saldo
26/11/2025 00:51:33 |Saldo inicial do exercício              |             0|        231,90|             0|        231,90
--------------------------------------------------------------------------------------------------------------------------

Receita total do dia: R$ 0,00


Aperte qualquer tecla

Mapa de vagas:
Vagas disponiveis: 100



b: 001     |b: 002     |b: 003     |b: 004     |b: 005     |b: 006     |b: 007     |b: 008     |b: 009     |b: 010     |
b: 011     |b: 012     |b: 013     |b: 014     |b: 015     |b: 016     |b: 017     |b: 018     |b: 019     |b: 020     |
b: 021     |b: 022     |b: 023     |b: 024     |b: 025     |b: 026     |b: 027     |b: 028     |b: 029     |b: 030     |
b: 031     |b: 032     |b: 033     |b: 034     |b: 035     |b: 036     |b: 037     |b: 038     |b: 039     |b: 040     |
b: 041     |b: 042     |b: 043     |b: 044     |b: 045     |b: 046     |b: 047     |b: 048     |b: 049     |b: 050     |
b: 051     |b: 052     |b: 053     |b: 054     |b: 055     |b: 056     |b: 057     |b: 058     |b: 059     |b: 060     |
b: 061     |b: 062     |b: 063     |b: 064     |b: 065     |b: 066     |b: 067     |b: 068     |b: 069     |b: 070     |
b: 071     |b: 072     |b: 073     |b: 074     |b: 075     |b: 076     |b: 077     |b: 078     |b: 079     |b: 080     |
b: 081     |b: 082     |b: 083     |b: 084     |b: 085     |b: 086     |b: 087     |b: 088     |b: 089     |b: 090     |
b: 091     |b: 092     |b: 093     |b: 094     |b: 095     |b: 096     |b: 097     |b: 098     |b: 099     |b: 100     |

pressione qualquer tecla para voltar!
