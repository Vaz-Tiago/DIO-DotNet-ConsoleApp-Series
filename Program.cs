using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {

            string opcaoUsuario = ObterOpcaoUsuario();
            
            while(opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }
            Console.WriteLine("Obrigado por utilizar nosso serviço");
            Console.ReadLine();
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if(lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido() ? " - *Excluído*" : "";
                Console.WriteLine($"#ID: {serie.retornaId()}: {serie.retornaTitulo()}{excluido}");
            }
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            Serie novaSerie = ObterSerie();

            repositorio.Insere(novaSerie);
        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("Atualizar série");

            Serie serie = ObterSerie(false);
            repositorio.Atualiza(serie.Id, serie);
        }
        
        private static void ExcluirSerie()
        {
            Console.WriteLine("Excluir série");
            Console.WriteLine();

            Console.Write("Digite o id da série que será excluida: ");
            int id = int.Parse(Console.ReadLine());

            repositorio.Exclui(id);
        }
        
        private static void VisualizarSerie()
        {
            Console.WriteLine("Visualizar série");
            Console.WriteLine();

            Console.Write("Digite o id da série que deseja visualizar: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine(repositorio.RetornaPorId(id).ToString());
        }
        private static Serie ObterSerie(bool novaSerie = true)
        {
            int id = repositorio.ProximoId();
            if(!novaSerie)
            {
                Console.Write("Digite o id da série: ");
                id = int.Parse(Console.ReadLine());
            }

            foreach(int i in Enum.GetValues(typeof(Genero)))
                Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero), i)}");

            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o nome da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano de início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            return new Serie(
                id: id,
                genero: (Genero)entradaGenero,
                titulo: entradaTitulo,
                ano: entradaAno,
                descricao: entradaDescricao
            );
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Séries ao seu dispor!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Inserir nova série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar série");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
