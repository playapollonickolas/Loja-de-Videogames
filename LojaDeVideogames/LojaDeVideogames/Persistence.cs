using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


class Persistence
{
    private string filename;

    public Persistence(string filename)
    {
        this.filename = filename;
    }

    public void SaveGames(IEnumerable<Game> games)
    {
        using (var writer = new StreamWriter(filename))
        {
            writer.WriteLine("Title,Genre,Price");

            foreach (var game in games)
            {
                writer.WriteLine("{0},{1},{2}", game.Title, game.Genre, game.Price);
            }
        }
    }

    public List<Game> LoadGames()
    {
        var games = new List<Game>();

        if (!File.Exists(filename))
        {
            return games;
        }

        using (var reader = new StreamReader(filename))
        {
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                var game = new Game
                {
                    Title = values[0],
                    Genre = values[1],
                    Price = decimal.Parse(values[2])
                };

                games.Add(game);
            }
        }

        return games;
    }
}

