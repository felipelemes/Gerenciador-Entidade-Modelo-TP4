using Dominio;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IEntidadeRepositorio _repositorio;

        public Worker(IEntidadeRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ///REFATORAR MÉTODO MAIN EM MÉTODOS MENORES

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            const string pressioneQualquerTecla = "Pressione qualquer tecla para exibir o menu principal ...";

            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("*** Gerenciador de [Entidades] *** ");
                Console.WriteLine("1 - Pesquisar [entidades]:");
                Console.WriteLine("2 - Adicionar [entidade]:");
                Console.WriteLine("3 - Sair:");
                Console.WriteLine("\nEscolha uma das opções acima: ");

                opcao = Console.ReadLine();
                if (opcao == "1")
                {
                    Console.WriteLine("Informe [o campo string ou parte do campo string] [da entidade] que deseja pesquisar:");
                    var termoDePesquisa = Console.ReadLine();
                    var entidadesEncontradas = _repositorio.Pesquisar(termoDePesquisa);

                    if (entidadesEncontradas.Count > 0)
                    {
                        Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados [das entidades] encontrados:");
                        for (var index = 0; index < entidadesEncontradas.Count; index++)
                            Console.WriteLine($"{index} - {entidadesEncontradas[index].GetInformacao2()}");

                        if (!ushort.TryParse(Console.ReadLine(), out var indexAExibir) || indexAExibir >= entidadesEncontradas.Count)
                        {
                            Console.WriteLine($"Opção inválida! {pressioneQualquerTecla}");
                            Console.ReadKey();
                            continue;
                        }

                        if (indexAExibir < entidadesEncontradas.Count)
                        {
                            var entidade = entidadesEncontradas[indexAExibir];

                            Console.WriteLine("Dados [da entidade]");
                            Console.WriteLine($"[campo string]: {entidade.GetInformacao2()}");
                            Console.WriteLine($"[campo DateTime]: {entidade.GetInformacao3():dd/MM/yyyy}");
                            ///IMPRIMIR NO CONSOLE DEMAIS CAMPOS
                            ///INVOCAR E EXIBIR RESULTADO DO MÉTODO DA ENTIDADE QUE CONTEM REGRA DE NEGÓCIO

                            Console.Write(pressioneQualquerTecla);
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Não foi encontrado nenhuma [entidade]! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                }
                else if (opcao == "2")
                {
                    ///SOLICITAR USUÁRIO QUE INFORME OS DADOS DA NOVA ENTIDADE (DO SEU TEMA) - PELO MENOS CINCO INFORMAÇÕES
                    Console.WriteLine("Informe [campo string] da [entidade] que deseja adicionar:"); //ex: uma informação do tipo string: nome medico, nome carro, nome prefeito
                    var campoDoTipoStringDaEntidade = Console.ReadLine();

                    Console.WriteLine("Informe [campo DateTime da entidade] (formato dd/MM/yyyy):"); //uma informação do tipo DateTime: data de aniversário do médico, data de compra do carro, data de nomeação do prefeito
                    if (!DateTime.TryParse(Console.ReadLine(), out var campoDoTipoDateTimeDaEntidade))
                    {
                        Console.WriteLine($"Data inválida! Dados descartados! {pressioneQualquerTecla}");
                        Console.ReadKey();
                        continue;
                    }

                    Console.WriteLine("Os dados estão corretos?");
                    Console.WriteLine($"[Campo string da entidade]: {campoDoTipoStringDaEntidade}");
                    Console.WriteLine($"[Campo DateTime da entidade]: {campoDoTipoDateTimeDaEntidade:dd/MM/yyyy}");
                    Console.WriteLine("1 - Sim \n2 - Não");

                    var opcaoParaAdicionar = Console.ReadLine();
                    if (opcaoParaAdicionar == "1")
                    {
                        ///ATRIBUIR INFORMAÇÕES OBTIDAS NO CONSOLE NA NOVA ENTIDADE
                        _repositorio.Adicionar(new Entidade());

                        Console.WriteLine($"Dados adicionados com sucesso! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                    else if (opcaoParaAdicionar == "2")
                    {
                        Console.WriteLine($"Dados descartados! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"Opção inválida! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                }
                else if (opcao == "3")
                {
                    Console.Write("Saindo do programa... ");
                }
                else if (opcao != "3")
                {
                    Console.Write($"Opcao inválida! Escolha uma opção válida. {pressioneQualquerTecla}");
                    Console.ReadKey();
                }

            } while (opcao != "3");
        }
    }
}
