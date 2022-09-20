using System;
using System.Collections.Generic;

namespace Uno
{
    internal partial class UnoGame
    {
        private struct Player
        {
            private List<Card> hand;
            public List<Card> Hand { get { return hand; } }
            public string Name { get; init; }

            public Player(string name)
            {
                this.Name = name;
                this.hand = new List<Card>();
            }
            public void Add(Card c)
            {
                hand.Add(c);
                hand.Sort((x, y) =>
                {
                    int ret = x.Color.CompareTo(y.Color);
                    return ret != 0 ? ret : x.Value.CompareTo(y.Value);
                });
            }
        }
    }
}
