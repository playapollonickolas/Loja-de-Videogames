using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Game
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public decimal Price { get; set; }
}

public class GameStore
{
    private List<Game> _games = new List<Game>();

    public void AddGame(Game game)
    {
        _games.Add(game);
    }

    public void RemoveGame(Game game)
    {
        _games.Remove(game);
    }

    public List<Game> GetGames()
    {
        return _games;
    }
}

class Program
{
    static void Main(string[] args)
    {

        //Login
        var username = "usuario";
        var password = "senha";

        bool logIn = false;

        while (!logIn)
        {

            Console.WriteLine("Digite o nome de usuário:");
            var inputUsername = Console.ReadLine();

            Console.WriteLine("Digite a senha:");
            var inputPassword = Console.ReadLine();

            if (inputUsername == username && inputPassword == password)
            {
                Console.WriteLine("Login bem-sucedido!");
                Console.WriteLine("Pressione ENTER para continuar.");
                Console.ReadLine();
                logIn = true;
            }
            else
            {
                Console.WriteLine("Nome de usuário ou senha incorretos. Tente novamente.");
                Console.WriteLine("Pressione ENTER para voltar.");
                Console.ReadLine();              
            }
        }

        Console.Clear();


        //Início do Programa

        var persistence = new Persistence("games.csv");

        var store = new GameStore();

        // Carregar jogos
        var games = persistence.LoadGames();
        foreach (var game in games)
        {
            store.AddGame(game);
        }

        while (true)
        {
            Console.WriteLine(" ");
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1. Adicionar um jogo");
            Console.WriteLine("2. Listar todos os jogos");
            Console.WriteLine("3. Remover um jogo");
            Console.WriteLine("4. Sair");
            Console.WriteLine(" ");

            var choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    AddGame(store);
                    break;
                case "2":
                    ListGames(store);
                    break;
                case "3":
                    RemoveGame(store);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }

            // Salvar jogos
            persistence.SaveGames(store.GetGames());

            Console.WriteLine();        
        }

    }

    static void AddGame(GameStore store)
    {
        Console.WriteLine("Digite o título do jogo:");
        var title = Console.ReadLine();

        Console.WriteLine("Digite o gênero do jogo:");
        var genre = Console.ReadLine();

        Console.WriteLine("Digite o preço do jogo:");
        var priceString = Console.ReadLine();
        decimal.TryParse(priceString, out decimal price);

        var game = new Game
        {
            Id = store.GetGames().Count,
            Title = title,
            Genre = genre,
            Price = price
        };

        store.AddGame(game);

        Console.WriteLine("Jogo adicionado!");
    }

    static void ListGames(GameStore store)
    {
        var games = store.GetGames();

        if (games.Count == 0)
        {
            Console.WriteLine("Nenhum jogo encontrado");
            return;
        }

        foreach (var game in games)
        {
            Console.WriteLine("{0} - Título: {1}, Gênero: {2}, Preço: {3:C}", game.Id, game.Title, game.Genre, game.Price);
        }
    }

    static void RemoveGame(GameStore store)
    {
        Console.WriteLine("Digite o título do jogo para remover:");
        var title = Console.ReadLine();

        var gameToRemove = store.GetGames().Find(game => game.Title == title);

        if (gameToRemove == null)
        {
            Console.WriteLine("Jogo não encontrado");
            return;
        }

        store.RemoveGame(gameToRemove);

        Console.WriteLine("Jogo removido!");
    }

}