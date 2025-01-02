using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace net_rogue
{
    internal class Game
    {
        public bool valid;
        long number1;
        Map level;
        int CurrentMap;
        public void Run()
        {
            Console.WindowWidth = 50;
            Console.WindowHeight = 20;

            PlayerCharacter player = new PlayerCharacter();

            // Kysy pelaajan nimi uudelleen, kunnes se on kelvollinen
            while (true)
            {
                Console.Write("Anna pelaajan nimi: ");
                player.Name = Console.ReadLine();

                // Tarkistetaan, että nimi ei ole tyhjä ja että se sisältää vain kirjaimia ja mahdollisesti välejä
                bool isValidName = true;
                foreach (char c in player.Name)
                {
                    if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    {
                        isValidName = false;
                        break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(player.Name) && isValidName)
                {
                    break; // Hyväksytään nimi, jos se on kelvollinen
                }
                else
                {
                    Console.WriteLine("Nimi ei ole kelvollinen. Varmista, että nimi ei ole tyhjä ja että se sisältää vain kirjaimia.");
                }
            }

            // Kysy pelaajan Rotu
            while (true)
            {
                Console.WriteLine("Select Species");
                foreach (int i in Enum.GetValues(typeof(Species)))
                {
                    Console.Write($"{Enum.GetName(typeof(Species), i)}");
                    Console.WriteLine($" {i}, ");
                }
                string SpeciesAnswer = Console.ReadLine();
                if (Enum.TryParse<Species>(SpeciesAnswer, true, out player.species))
                {
                    if (long.TryParse(SpeciesAnswer, out number1))
                    {
                        if (Convert.ToInt64(SpeciesAnswer) <= Enum.GetNames(typeof(Species)).Length - 1)
                        {
                            valid = true;
                        }
                    }
                    else
                    {
                        valid = true;
                    }
                }
                if (valid)
                {
                    player.species = Enum.Parse<Species>(SpeciesAnswer, true);
                    break;
                }
                Console.WriteLine("Invalid selection. Please input the name of a Species, or its order in the list");
            }

            //Kysy pelaajan rooli
            while (true)
            {
                Console.WriteLine("Select Class");
                foreach (int i in Enum.GetValues(typeof(Role)))
                {
                    Console.Write($"{Enum.GetName(typeof(Role), i)}");
                    Console.WriteLine($" {i}, ");
                }
                string classAnswer = Console.ReadLine();
                if (Enum.TryParse<Role>(classAnswer, true, out player.role))
                {
                    if (long.TryParse(classAnswer, out number1))
                    {
                        if (Convert.ToInt64(classAnswer) <= Enum.GetNames(typeof(Role)).Length - 1)
                        {
                            valid = true;
                        }
                    }
                    else
                    {
                        valid = true;
                    }
                }
                if (valid)
                {
                            player.role = Enum.Parse<Role>(classAnswer, true);
                            break;
                }
                Console.WriteLine("Invalid selection. Please input the name of a class, or its order in the list");
            }

                // Tallenna ja näytä tiedot
                Console.WriteLine("Pelaaja luotu!");
            Console.WriteLine($"Nimi: {player.Name}");
            Console.WriteLine($"Laji: {player.species}");
            Console.WriteLine($"Rooli: {player.role}");

            // Set player starting position
            player.position = new Vector2(1, 1);

            // Clear screen
            Console.Clear();
            // Draw the player
            Console.SetCursorPosition((int)player.position.X, (int)player.position.Y);

            MapLoader loader = new MapLoader();

            loader.TestFileReading("Maps/mapfile.json");

            level = loader.LoadMapFromFile("Maps/mapfile.JSON");
            CurrentMap = 1;
            Console.ForegroundColor = ConsoleColor.White;
            level.Draw();
            

            player.image = '@';
            player.drawColor = ConsoleColor.Green;
            
            Console.Write("@");

            // Start the game loop:
            while (true)
            {
                // ------------Update:

                
                // Prepare to read movement input
                int moveX = 0;
                int moveY = 0;
                // Wait for keypress and compare value to ConsoleKey enum
                ConsoleKeyInfo key = Console.ReadKey(true);

                Console.ForegroundColor = ConsoleColor.White;
                level.Draw();
                Console.ForegroundColor = player.drawColor;

                if (key.Key == ConsoleKey.UpArrow)
                {
                    player.move(0, -1, level);
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    player.move(0, 1, level);
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    player.move(-1, 0, level);
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    player.move(1, 0, level);
                }

               

            }
        }

    }
}
