==========================================================================================
Yahtzee by Daan Juttmann
Created: 2017-11-16
License: GNU General Public License 3.0 (https://www.gnu.org/licenses/gpl-3.0.en.html).
==========================================================================================

-- DESCRIPTION --
A two player Yahtzee game.


-- GAMEPLAY --
On each turn a player can roll the dice up to 3 times using the 'Roll' button. For the
second and third roll, the player must select the dice which they want to reroll (selected
dice are highlighted red). When the player is satisfied with the result, they may select a
category for which they want to score the dice. Each available category will show what
the current set of dice will score. The player with the highest score after 13 rounds
wins the game. The categories are:

Upper Section     - Score
- Aces            : Sum of all ones rolled
- Twos            : Sum of all twos rolled
- Threes          : Sum of all threes rolled
- Fours           : Sum of all fours rolled
- Fives           : Sum of all fives rolled
- Sixes           : Sum of all sixes rolled

Lower Section     - Score
- Three of a Kind : For three equal dice, sum of all dice
- Four of a Kind  : For four equal dice, sum of all dice
- Full House      : For three equal dice and two different equal dice, 25 points
- Small Straight  : For 4 consecutive dice, 30 points
- Large Straight  : For 5 consecutive dice, 40 points
- Yahtzee         : For all dice equal, 50 points
- Chance          : Sum of all dice

Note that this program implements the free choice joker rule. If the player rolls a
yahtzee when they have already scored 50 points in the yahtzee category, they get a 100
point bonus, and may then score the yahtzee roll for any other category. Furthermore, if
the upper section category for the rolled yahtzee is already used (e.g. 'Threes' when you
rolled 3,3,3,3,3), you may use the yahtzee as a joker for either full house, or the small
or large straight categories.

==========================================================================================