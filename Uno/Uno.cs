using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
namespace Uno
{
    internal partial class UnoGame
    {
        private List<Card> deck;
        private List<Card> discard;
        private List<Player> players;

        public UnoGame()
        {
            deck = new List<Card>();
            discard = new List<Card>();
            players = new List<Player>();

            deck.Add(new Card(CardColor.Red, CardValue.Zero));
            deck.Add(new Card(CardColor.Green, CardValue.Zero));
            deck.Add(new Card(CardColor.Blue, CardValue.Zero));
            deck.Add(new Card(CardColor.Yellow, CardValue.Zero));

            for (int value = 1; value <= 12; value++)
            {
                for (int color = 0; color <= 3; color++)
                {
                    deck.Add(new Card((CardColor) color, (CardValue) value));
                    deck.Add(new Card((CardColor) color, (CardValue) value));
                }
            }
            for (int value = 13; value <= 14; value++)
            {
                deck.Add(new Card(CardColor.Wildcard, (CardValue)value));
                deck.Add(new Card(CardColor.Wildcard, (CardValue)value));
                deck.Add(new Card(CardColor.Wildcard, (CardValue)value));
                deck.Add(new Card(CardColor.Wildcard, (CardValue)value));
            }
        }
        public void Run()
        {
            Menu();
            Setup();
            Play();
        }
        private void Menu()
        {
            int numOfPlayers = 0;
            while (true)
            {
                Header();
                Console.Write("Press <Enter> to Continue...");

                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    while (true)
                    {
                        Header();
                        Console.Write("Number of Players (2-10): ");
                        try
                        {
                            numOfPlayers = Convert.ToInt32(Console.ReadLine());
                            if (numOfPlayers < 2 || numOfPlayers > 10)
                            {
                                Console.WriteLine("Please enter an integer between 2 and 10");
                                Delay(2000);
                                continue;
                            }
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Please enter an integer between 2 and 10");
                            Delay(2000);
                            continue;
                        }
                    }

                    for (int i = 0; i < numOfPlayers; i++)
                    {
                        Console.Write($"\tName of Player {i + 1}: ");
                        string name = Console.ReadLine() ?? "";
                        name = (name == "") ? $"NoName No.{i + 1}" : name;
                        players.Add(new Player(name));
                    }
                    break;
                }
                Console.Clear();
            }
        }
        private void Setup()
        {
            for (int i = 0; i < deck.Count(); i++)
            {
                var rd = new Random();
                int switcher = rd.Next(0, deck.Count());
                var temp = deck[i];
                deck[i] = deck[switcher];
                deck[switcher] = temp;
            }
            for (int i = 0; i < players.Count(); i++)
            {
                var rd = new Random();
                int switcher = rd.Next(0, players.Count());
                var temp = players[i];
                players[i] = players[switcher];
                players[switcher] = temp;
            }
            foreach (var player in players)
            {
                for (int i = 0; i < 7; i++)
                {
                    player.Add(deck[0]);
                    deck.RemoveAt(0);
                }
            }
        }
        
        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
        private static void Header()
        {
            Console.Clear();
            Console.WriteLine("UNO!: The Command Line Card Game");
            Console.WriteLine(new String('-', 33));
        }
        private void PrintPlayers(int currentPlayer)
        {
            for (int i = 0; i < players.Count(); i++)
            {
                if (i == currentPlayer)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"{players[i].Name} ({players[i].Hand.Count()} Cards)");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine($"{players[i].Name} ({players[i].Hand.Count()} Cards)");
                }
            }
        }
        private void PrintPlayerCards(int currentPlayer)
        {
            for (int i = 0; i < players[currentPlayer].Hand.Count(); i++)
            {
                Console.WriteLine($"\t{i + 1}) {players[currentPlayer].Hand[i].Color} {players[currentPlayer].Hand[i].Value}");
            }
            Console.WriteLine($"\t{players[currentPlayer].Hand.Count() + 1}) {((deck.Count() >= 1 || (deck.Count() >= 1 || discard.Count > 1)) ? "Draw" : "Skip")}");
        }
        private static void Delay()
        {
            Task.Run(async delegate
            {
                await Task.Delay(5000);
                return 1;
            }).Wait();

        }
        private static void Delay(int ms)
        {
            Task.Run(async delegate
            {
                await Task.Delay(ms);
                return 1;
            }).Wait();

        }
    }
}
