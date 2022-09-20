using System.Linq;

namespace Uno
{
    internal partial class UnoGame
    {
        public void Play()
        {
            Act IncrementOrDecrement = (ref int x) => x++;
            int currentPlayer = 0;
            bool validCard = true;
            CardColor WildCardColor = CardColor.Nothing;

            discard.Add(deck[0]);
            deck.RemoveAt(0);

            CardAction(ref IncrementOrDecrement, ref currentPlayer, 1, ref WildCardColor, true);

            while (true)
            {
                while (true)
                {
                    if (validCard)
                    {
                        Console.Clear();
                        Console.WriteLine($"{players[currentPlayer].Name} is up. Waiting 5 Seconds...");
                        Delay();
                    }
                    validCard = true;
                    Header();
                    PrintPlayers(currentPlayer);

                    Console.WriteLine($"\nCurrent Card: {((WildCardColor == CardColor.Nothing) ? discard[0].Color : WildCardColor)} {discard[0].Value}\nDeck has {deck.Count()} cards left\n");
                    Console.WriteLine("These are your cards. Enter the number of the card to make the selection");
                    PrintPlayerCards(currentPlayer);
                    Console.Write("Selection: ");
                    int selection = -1;
                    try
                    {
                        selection = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Please enter an integer between 1 and {players[currentPlayer].Hand.Count() + 1} inclusive");
                        Delay(2000);
                        validCard = false;
                        continue;
                    }
                    if (selection < 1 || selection > players[currentPlayer].Hand.Count() + 1)
                    {
                        Console.WriteLine($"Please enter an integer between 1 and {players[currentPlayer].Hand.Count() + 1} inclusive");
                        Delay(2000);
                        validCard = false;
                        continue;
                    }
                    else if (selection == players[currentPlayer].Hand.Count() + 1 && (deck.Count() >= 1 || discard.Count > 1))
                    {
                        Draw(1, currentPlayer, false);
                        while (true)
                        {
                            Header();
                            PrintPlayers(currentPlayer);
                            Console.WriteLine($"\nCurrent Card: {discard[0].Color} {discard[0].Value}\n");
                            Console.Write($"{players[currentPlayer].Name} drew {players[currentPlayer].Hand[selection - 1].Color} {players[currentPlayer].Hand[selection - 1].Value}, do you want to play it? (Yy/Nn): ");
                            string choice = Console.ReadLine() ?? "";
                            if (choice == "y" || choice == "Y")
                            {
                                if (ValidMove(players[currentPlayer].Hand[selection - 1], discard[0], ref WildCardColor))
                                {
                                    CardAction(ref IncrementOrDecrement, ref currentPlayer, selection, ref WildCardColor, false);
                                }
                                break;
                            }
                            else if (choice == "n" || choice == "N")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Please Enter a valid selection");
                                Delay(2000);
                                continue;
                            }
                        }
                        break;
                    }
                    else if(selection == players[currentPlayer].Hand.Count() + 1)
                    {
                        break;
                    }
                    if (ValidMove(players[currentPlayer].Hand[selection - 1], discard[0], ref WildCardColor))
                    {
                        CardAction(ref IncrementOrDecrement, ref currentPlayer, selection, ref WildCardColor, false);
                        ResetDeck();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("This is not a card that can be currently played");
                        validCard = false;
                        Delay(2000);
                        continue;
                    }
                }
                if (players[currentPlayer].Hand.Count() == 0)
                {
                    Console.WriteLine($"Congratulations {players[currentPlayer].Name}!!! You are the Winner!!!");
                    ResetDeck();
                    break;
                }
                else
                {
                    int i = 0;
                    IncrementOrDecrement(ref i);
                    if (i == 1)
                    {
                        if (currentPlayer < (players.Count() - 1)) IncrementOrDecrement(ref currentPlayer);
                        else currentPlayer = 0;
                    }
                    else if (i == -1)
                    {
                        if (currentPlayer > 0) IncrementOrDecrement(ref currentPlayer);
                        else currentPlayer = (players.Count() - 1);
                    }
                }            
            }
        }
        private void CardAction(ref Act IncrementOrDecrement, ref int currentPlayer, int selection, ref CardColor WildCardColor, bool firstIteration)
        {
            WildCardColor = CardColor.Nothing;
            if (!(firstIteration))
            { 
                discard.Insert(0, players[currentPlayer].Hand[selection - 1]);
                players[currentPlayer].Hand.Remove(players[currentPlayer].Hand[selection - 1]);
            }
            if (discard[0].Value == CardValue.Skip)
            {
                int i = 0;
                IncrementOrDecrement(ref i);
                if (i == 1)
                {
                    if (currentPlayer < (players.Count() - 1)) IncrementOrDecrement(ref currentPlayer);
                    else currentPlayer = 0;
                }
                else if (i == -1)
                {
                    if (currentPlayer > 0) IncrementOrDecrement(ref currentPlayer);
                    else currentPlayer = (players.Count() - 1);
                }
            }
            else if (discard[0].Value == CardValue.Reverse)
            {
                int i = 1;
                IncrementOrDecrement(ref i);
                if (i == 2) IncrementOrDecrement = (ref int x) => x--;
                else IncrementOrDecrement = (ref int x) => x++;
            }
            else if (discard[0].Value == CardValue.DrawTwo)
            {
                int i = 0;
                IncrementOrDecrement(ref i);
                if (i == 1)
                {
                    if (currentPlayer < (players.Count() - 1)) IncrementOrDecrement(ref currentPlayer);
                    else currentPlayer = 0;
                }
                else if (i == -1)
                {
                    if (currentPlayer > 0) IncrementOrDecrement(ref currentPlayer);
                    else currentPlayer = (players.Count() - 1);
                }
                Draw(2, currentPlayer, firstIteration);

            }
            else if (discard[0].Color == CardColor.Wildcard)
            {
                while (true)
                {
                    Header();
                    PrintPlayers(currentPlayer);
                    Console.WriteLine($"{((!firstIteration) ? players[currentPlayer].Name : "the deck")} has played a {discard[0].Value} WildCard. What color would you do you want");
                    for (int i = 0; i < 4; i++)
                    {
                        Console.WriteLine($"\t{i + 1}) {(CardColor)i}");
                    }
                    Console.Write("Selection: ");
                    int colorSelection = -1;
                    try
                    {
                        colorSelection = Convert.ToInt32(Console.ReadLine()) - 1;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Please enter an integer between 1 and 4 inclusive");
                        Delay(2000);
                        continue;
                    }
                    if (colorSelection < 0 || colorSelection > 3)
                    {
                        Console.WriteLine("This is not a valid selection");
                        Delay(2000);
                        continue;
                    }
                    WildCardColor = (CardColor)colorSelection;

                    if (discard[0].Value == CardValue.DrawFour)
                    {
                        int i = 0;
                        IncrementOrDecrement(ref i);
                        if (i == 1)
                        {
                            if (currentPlayer < (players.Count() - 1)) IncrementOrDecrement(ref currentPlayer);
                            else currentPlayer = 0;
                        }
                        else if (i == -1)
                        {
                            if (currentPlayer > 0) IncrementOrDecrement(ref currentPlayer);
                            else currentPlayer = (players.Count() - 1);
                        }
                        Draw(4, currentPlayer, firstIteration);
                        break;

                    }
                }
            }
        }
        private bool ValidMove(Card played, Card toCheck, ref CardColor WildCardColor)
        {
            if (WildCardColor == CardColor.Nothing)
            {
                if (toCheck.Color == CardColor.Wildcard || played.Color == CardColor.Wildcard || played.Color == toCheck.Color) return true;
                else if (played.Value == toCheck.Value) return true;
                else return false;
            }
            else
            {
                if (played.Color == WildCardColor || played.Color == CardColor.Wildcard) return true;
                else return false;
            }
        }
        private bool NoValidMoves(Player p, Card check, ref CardColor WildCardColor)
        {
            foreach(Card c in p.Hand)
            {
                if (ValidMove(c, check, ref WildCardColor))
                {
                    return false;
                }
            }
            return true;
        }
        private void ResetDeck()
        {
            if (deck.Count() == 0 && discard.Count() > 1)
            {
                Card top = discard[0];
                discard.RemoveAt(0);
                var temp1 = deck;
                deck = discard;
                discard = temp1;
                for (int i = 0; i < deck.Count(); i++)
                {
                    var rd = new Random();
                    int switcher = rd.Next(0, deck.Count());
                    var temp = deck[i];
                    deck[i] = deck[switcher];
                    deck[switcher] = temp;
                }
                discard.Add(top);
            }
        }
        private void Draw(int numOfDraws, int currentPlayer, bool firstIteration)
        {
            for (int i = 0; i < numOfDraws; i++)
            {
                if (deck.Count() > 0)
                {
                    players[((firstIteration) ? 0 : currentPlayer)].Add(deck[0]);
                    deck.RemoveAt(0);
                    ResetDeck();
                }
            }
        }
        private delegate void Act(ref int x);
    }
}
