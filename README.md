# Ride the bus precall probabilities

This program simulates the probabilities of success for pre calling each hand in the card game 'Ride the Bus'.

## The rules of the game
A player is attempting to correctly make 4 guesses in a row. 

<ol>
<li> The first guess is whether the next card flipped will be Red or Black </li>
<li> The second guess is whether the next card flipped will be Higher or Lower than the previous card.</li>
<li> The third guess is whether the next card flipped will be numerically Inside (inbetween) or Outside the previous two cards.</li>
<li> The last guess is whether the next card flipped will be the Same or Different suit to the previous three cards.</li>
</ol>

If the player successfully guesses all four in a row, they have won. If not they have lost and will restart.

- If the player is guessing Higher, Lower, Inside or Outside, and gets the same value in the next flip, they lose. E.g. If you call lower on a 10 and get a 10 you lose.
- Ace counts as 1.
- If you are playing with multiple decks, if you are at the last card of a deck and you haven't won, you lose and you have to start from the beginning on the next run through.- E.g. if you are o nyour second guess but there are no more cards, you lose and restart with a fresh deck.

## Precalling
The game is usually played one guess at a time which gives the player a better chance of guessing correctly e.g. If the first card was an Ace,
then it is most likely that the next card will be Higher.

However there is a variant where the player pre calls their chosen combination before the games starts e.g. Red, Higher, Outside, Same (RHOS).

It is this variant that this code simulates.

## How to use program
Simply create a `Game` object with a sample size you would like to run against (10000 works well), and a max shuffle number for the multi deck version (100 usually works well).
After this is done, call one of the methods listed below and these will print results to the console.

## Methods
The first method is `GetSuccessPercentagesOneDeck` which will run all possible precall hands against a shuffled deck of cards and print the success rates as percentages. Note that this only runs through the deck once and if you get to the end of the deck without winning, this is a loss.

The second method is `GetFailurePercentagesMultiDeck` which will run all possible precall hands against a shuffled deck of cards, reshuffling and restarting upon the deck running out. This reshuffling will occur `maxShuffles` 
amount of times and this method will return how many failures it took before the hand passed. 

The results for these method calls should be similar i.e. a high percentage win should correlate to a low failure count. 
There will slight variation each time because of the pure random nature of some of the options e.g. RHOS is equally likely as BHOS so their positions relative to each other may fluctuate.