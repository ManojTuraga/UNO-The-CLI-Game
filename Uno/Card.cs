using System.Collections.Generic;

namespace Uno
{
    internal partial class UnoGame
    {
        private enum CardColor
        {
            Red, Green, Blue, Yellow, Wildcard, Nothing

        }
        private enum CardValue
        {
            Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Skip,
            DrawTwo, Reverse, Normal, DrawFour
        }
        private struct Card
        {
            public CardColor Color { get; set; }
            public CardValue Value { get; init; }

            public Card(CardColor color, CardValue value)
            {
                this.Color = color;
                this.Value = value;
            }
        }
    }
}
