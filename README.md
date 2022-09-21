# UNO!® The CLI Game
## About
This program brings the classic game of UNO!® to the Command Line. Take turns playing a round of UNO® with a group of 2 to 10 people and try to be the first to clear your hand. 
## Rules
The rules for this version of UNO® follow closely to the original rules, but with some deviations. Check out below to see the setup, gameplay, and rules for special cards
### Setup
The game setup starts with finding a group of 2 to 10 people to play with. There will be no dealer since the program will handle all of that. Each player will be dealt 7 cards from the Deck and the first card from the remaining cards is the first card in play (see Special conditions in the event the first card is a special one). The order of play is decided at random.
### Gameplay
Each turn starts with the player who's turn is incoming and the program generates a 5 second delay. This is to account for the fact that the game is played on one screen and it takes time to shift the device. The party will know when each member when the program generates the following message
```
Player 1 is up. Waiting 5 seconds...
```
Once the 5 seconds are done, the current player can begin their turn. The player is presented with a menu showing all the players and the number of cards they have, the current card in play, and each of the cards they posses. During their turn, a player may:<br />
1. Play a valid card (A wildcard or any card of the same color/value)<br />
2. Draw/Skip on their turn (Depending on the number cards remaining in the deck/discard) <br />
</br>
If a player chooses to draw on their turn, the may play the card the drawed or choose to pass the turn to the next player. The game ends when the a player is the first to play all of the cards in their hand. The following shows a sample menu


```
UNO!: The Command Line Card Game
_________________________________

Player 1 (3 Cards)
Player 2 (2 Cards) <-
Player 3 (5 Cards)

Current Card: Blue Five
Deck has 10 cards

These are your cards. Enter the number of the card to make the selection
     1. Blue Three
     2. Wildcard DrawFour
     3. Draw
Selection: 
```

## The Cards
The version of UNO® this program is based off has 108 cards with the main colors being Red, Green, Blue, and Yellow. These include both numbered cards and special cards and each serve a different purpose
### Numbered Cards
These Cards make up the bulk of the deck (76 Cards). For each Color in the deck, there will be<br />
1. One 0 Card<br />
2. Cards Numbered 1-9 (2) = 18 Cards<br />
<br />
This holds true for the rest of the colors. These cards can only be played if the card currently in play was a Wildcard or the card has either the same number or color
### Special Cards
These cards are types of cards that can affect the actions of the game or the player. These Cards include<br />

1. Two Skip Cards per color = 8 Cards<br />
2. Two Reverse Cards per color = 8 Cards<br />
3. Two DrawTwo Cards per color = 8 Cards<br />
4. Four Normal Wildcards per color = 4 Cards<br />
5. Four DrawFour Wildcards per color = 4 Cards<br />
<br />
NOTE: Unlike certain versions of UNO®, the rseult of these Cards CANNOT be avoided. That means that these cards cannot be stacked

#### Skip Card
This card can be used to skip the next player in the order of play. Can be played on cards of the same color, other skip cards, or on a Wildcard. If the first card in play is a skip card, the first player is skipped and the play starts with the second player in the order
#### Reverse Card
This card can be used to reverse the order of play. Can be played on cards of the same color, other reverse cards, or on a Wildcard. If the first card in play is a reverse card, play starts with the original first player, but the order of play moves backwards
#### DrawTwo Card
This card can be used to force the next player in the order of play to Draw two cards and have their turn skipped. Can be played on cards of the same color, other DrawTwo cards, or on a Wildcard. If the first card in play is the DrawTwo card, then the first player is forced to Draw Two cards and their turn is skipped, with play resuming on the second player in the order of play
#### WildCard Normal and WildCard DrawFour
These cards can be used to change the color and value of the current card in play. Can be played anytime. The Normal Card does not change the order of play but the DrawFour card forces the next player in the order of play to draw Four cards and has their turn skipped. If the first card in the deck is a Wildcard Normal, then the group deliberates on the color of the first card and play starts as normal. If the first card is a DrawFour, then the group deliberates as usual but the first player is forced to Draw 4 cards and their turn is skipped
## Happy Playing!
