using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    internal class Program
    {
        static string[,] produtos = new string[100, 4];
        static string[,] vendas = new string[100, 3];
        static int totalvendas = 0;
        static int totalProdutos = 0;

        static void Main(string[] args)
        {
            int opcao;

            do
            {
                Console.Clear();
                opcao = Menu();
                OpcaoMenu(opcao);

            } while (opcao != 0);
        }

        static int Menu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("==========================================");
            Console.WriteLine("1 - Cadastrar produtos");
            Console.WriteLine("2 - Realizar uma venda");
            Console.WriteLine("3 - Relatório de vendas");
            Console.WriteLine("4 - Relatório de vendas por funcionário");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("==========================================");
            Console.Write("Selecione uma opção: ");
            int opcao = int.Parse(Console.ReadLine());

            return opcao;
        }

        static void OpcaoMenu(int opcao)
        {
            switch (opcao)
            {
                case 1:
                    Console.Clear();
                    CadastrarProduto();
                    break;
                case 2:
                    Console.Clear();
                    CadastrarVenda();
                    break;
                case 3:
                    Console.Clear();
                    RelatorioVendas();
                    break;
                case 4:
                    Console.Clear();
                    RelatorioVendaFuncionario();
                    break;

            }
        }

        static void CadastrarProduto()
        {

            Console.WriteLine("Cadastrar produto: ");
            Console.WriteLine("==================");
            Console.WriteLine("Código do produto: ");
            string codigo = (Console.ReadLine());
            Console.WriteLine("Descrição do produto: ");
            string descricao = (Console.ReadLine());
            Console.WriteLine("Valor do produto:R$ ");
            string valor = (Console.ReadLine());
            Console.WriteLine("Quantidade do estoque produto: ");
            string quantidadeestoque = (Console.ReadLine());

            produtos[totalProdutos, 0] = codigo;
            produtos[totalProdutos, 1] = descricao;
            produtos[totalProdutos, 2] = valor;
            produtos[totalProdutos, 3] = quantidadeestoque;

            totalProdutos++;

            Console.WriteLine("Produto criado com sucesso!");


            Console.ReadKey();
        }

        static void CadastrarVenda()
        {

            Console.WriteLine("Cadastrar venda: ");
            Console.WriteLine("==================");
            Console.WriteLine("Código do produto: ");
            string codigoproduto = (Console.ReadLine());

            bool produtoexistente = false;

            for (int i = 0; i < totalProdutos; i++)
            {

                if (produtos[i, 0] == codigoproduto)
                {
                    produtoexistente = true;
                    break;
                }
            }
            if (!produtoexistente)
            {
                Console.WriteLine("Produto não encontrado! ");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Código do funcionário: ");
            string codigofuncionario = (Console.ReadLine());
            Console.WriteLine("quantidade do produto: ");
            string quantidadedoproduto = (Console.ReadLine());

            int quantidade;
            if (!int.TryParse(quantidadedoproduto, out quantidade) || quantidade <= 0)
            {
                Console.WriteLine("quantidade invalida!");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < totalProdutos; i++)
            {
                if (produtos[i, 0] == codigoproduto)
                {
                    int estoqueAtual = int.Parse(produtos[i, 3]);
                    if (quantidade > estoqueAtual)
                    {
                        Console.WriteLine("Quantidade insuficiente em estoque!");
                        Console.ReadKey();
                        return;

                    }
                    else
                    {
                        produtos[i, 3] = (estoqueAtual - quantidade).ToString();
                        break;
                    }

                }
            }

            vendas[totalvendas, 0] = codigoproduto;
            vendas[totalvendas, 1] = codigofuncionario;
            vendas[totalvendas, 2] = quantidade.ToString();

            totalvendas++;

            Console.WriteLine("Venda registrada!");

            Console.ReadKey();
        }
        //Um relatório completo que contenha as informações das vendas: código
        //do produto, código do funcionário e valor da venda.Além disso, ao final
        //do relatório deverá demonstrar o valor total de todas as vendas.
        static void RelatorioVendas()
        {

            Console.WriteLine("Relatorio de vendas: ");
            Console.WriteLine("========================================");

            double vendatotais = 0;

            for (int i = 0; i < totalvendas; i++)
            {

                string codigoProduto = vendas[i, 0];
                string codigoFuncionario = vendas[i, 1];
                int quantidade = int.Parse(vendas[i, 2]);


                double valorProduto = 0;
                for (int j = 0; j < totalProdutos; j++)
                {
                    if (produtos[j, 0] == codigoProduto)
                    {
                        valorProduto = double.Parse(produtos[j, 2]);
                        break;
                    }
                }

                double valorVenda = valorProduto * quantidade;
                vendatotais += valorVenda;

                Console.WriteLine($"Venda do produto: {produtos[i, 0]}, código do vendedor: {codigoFuncionario}, valor da venda:R$ {valorVenda},00");
            }

            Console.WriteLine($"Valor total das vendas:R$ {vendatotais},00 ");
            Console.WriteLine("========================================");
            Console.ReadKey();
        }


        static void RelatorioVendaFuncionario()
        {
            Console.WriteLine("==========================");
            Console.WriteLine("Relatório de venda por funcionário");
            Console.WriteLine("Código do funcionário: ");
            string codigofuncionario = Console.ReadLine();

            double vendastotais = 0;

            for (int i = 0; i < totalvendas; i++)
            {
                if (vendas[i, 1] == codigofuncionario)
                {
                    string codigoproduto = vendas[i, 0];
                    int quantidade = int.Parse(vendas[i, 2]);

                    double valorproduto = 0;
                    for (int j = 0; j < totalProdutos; j++)
                    {
                        if (produtos[j, 0] == codigoproduto)
                        {
                            valorproduto = double.Parse(produtos[j, 2]);
                            break;

                        }
                    }

                    vendastotais += valorproduto * quantidade;
                    Console.WriteLine("");
                }

            }
            double comissao = vendastotais * 0.10;

            Console.WriteLine("===========================================================");
            Console.WriteLine($"Valor total das vendas do funcionário: {vendastotais:C}");
            Console.WriteLine($"Comissão do funcionário (10%): {comissao:C}");
            Console.ReadKey();


            Console.ReadKey();
        }
    }
}

