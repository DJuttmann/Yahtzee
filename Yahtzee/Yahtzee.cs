//========================================================================================
// Yahtzee by Daan Juttmann
// Created: 2017-11-16
// License: GNU General Public License 3.0 (https://www.gnu.org/licenses/gpl-3.0.en.html).
//========================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Yahtzee
{

﻿//========================================================================================
// Class Rules
﻿//========================================================================================

  class Rules
  {
    public enum Category
    {
      Chance = 0,
      Aces = 1,
      Twos = 2,
      Threes = 3,
      Fours = 4,
      Fives = 5,
      Sixes = 6,
      ThreeOfAKind = 7,
      FourOfAKind = 8,
      FullHouse = 9,
      SmallStraight = 10,
      LargeStraight = 11,
      Yahtzee = 12
    }

    public static string [] CategoryNames = new []
    {
      "Chance",
      "Aces",
      "Twos",
      "Threes",
      "Fours",
      "Fives",
      "Sixes",
      "Three of a Kind",
      "Four of a Kind",
      "Full House",
      "Small Straight",
      "Large Straight",
      "Yahtzee",
    };

    public const int ScoreFullHouse = 25;
    public const int ScoreSmallStraight = 30;
    public const int ScoreLargeStraight = 40;
    public const int ScoreYahtzee = 50;

    public const int CategoryCount = 13;
    public const int MaxRolls = 3;
    public const int TotalRounds = 13;
    public const int GameNotActive = 100;
  }

﻿//========================================================================================
// Class RandomGenerator
﻿//========================================================================================

  class RandomGenerator
  {
    private static Random Generator = new Random ();


    // Returns the roll of a six sided die.
    public static int D6 ()
    {
      return Generator.Next (6);
    }
  }

﻿//========================================================================================
// Class DiceSet
﻿//========================================================================================

  class DiceSet
  {
    const int DiceCount = 5;
    private int [] Dice;
    public int this [int i]
    {
      get {return Dice [i];}
    }


    // Constructor.
    public DiceSet ()
    {
      Dice = new int [5];
    }


    // Set all dice values to zero (unrolled)
    public void Reset ()
    {
      for (int i = 0; i < DiceCount; i++)
        Dice [i] = 0;
    }


    // Roll all dice.
    public void RollAll ()
    {
      for (int i = 0; i < DiceCount; i++)
        Dice [i] = RandomGenerator.D6 ();
    }


    // Roll one die.
    public void RollDie (int index)
    {
      if (index >= 0 && index < DiceCount)
        Dice [index] = RandomGenerator.D6 ();
    }


    // Score the dice according to category from Yahtzee
    public int ScoreDice (Rules.Category c)
    {
      switch (c)
      {
      case Rules.Category.Aces:
        return ScoreNumbers (1);
      case Rules.Category.Twos:
        return ScoreNumbers (2);
      case Rules.Category.Threes:
        return ScoreNumbers (3);
      case Rules.Category.Fours:
        return ScoreNumbers (4);
      case Rules.Category.Fives:
        return ScoreNumbers (5);
      case Rules.Category.Sixes:
        return ScoreNumbers (6);
      case Rules.Category.ThreeOfAKind:
        return ScoreThreeOfAKind ();
      case Rules.Category.FourOfAKind:
        return ScoreFourOfAKind ();
      case Rules.Category.FullHouse:
        return ScoreFullHouse ();
      case Rules.Category.SmallStraight:
        return ScoreSmallStraight ();
      case Rules.Category.LargeStraight:
        return ScoreLargeStraight ();
      case Rules.Category.Yahtzee:
        return ScoreYahtzee ();
      case Rules.Category.Chance:
        return ScoreChance ();
      default:
        return 0;
      }
    }


    // Compares two int tuples, first by Item1, if equal by Item2.
    private int CompareTuple (Tuple <int, int> x, Tuple <int, int> y)
    {
      if (x.Item1 < y.Item1)
        return -1;
      if (x.Item1 > y.Item1)
        return 1;
      return x.Item2 - y.Item2;
    }


    // Returns sorted list of tuples (die count, die value) for a dice set.
    private List <Tuple <int, int>> Counts ()
    {
      List <Tuple <int, int>> counts = new List <Tuple <int, int>> ();
      List <int> sorted = new List <int> (Dice);

      sorted.Sort ();
      int lastValue = Dice [0],
          valueCount = 1;
      for (int i = 1; i < DiceCount; i++)
      {
        if (Dice [i] == lastValue)
          valueCount++;
        else
        {
          counts.Add (new Tuple <int, int> (valueCount, lastValue));
          lastValue = Dice [i];
          valueCount = 1;
        }
      }
      counts.Sort (CompareTuple);
      return counts;
    }


    // Score dice set as Aces, Twos, ... , Sixes.
    private int ScoreNumbers (int number)
    {
      int score = 0;
      foreach (int n in Dice)
        if (n == number)
          score += number;
      return score;
    }


    // Score dice set as Three of a Kind.
    private int ScoreThreeOfAKind ()
    {
      List <Tuple <int, int>> diceCounts = Counts ();
      if (diceCounts [0].Item1 >= 3) // Check if most common die count is at least 3
        return 3 * diceCounts [0].Item2;
      return 0;
    }


    // Score dice set as Four of a Kind.
    private int ScoreFourOfAKind ()
    {
      List <Tuple <int, int>> diceCounts = Counts ();
      if (diceCounts [0].Item1 >= 4) // Check if most common die count >= 4
        return 4 * diceCounts [0].Item2;
      return 0;
    }


    // Score dice set as  Full House.
    private int ScoreFullHouse ()
    {
      List <Tuple <int, int>> diceCounts = Counts ();
      if (diceCounts.Count >= 2 && // check if at least two different dice
          diceCounts [0].Item1 >= 3 && // and most common die count >= 3
          diceCounts [1].Item1 >= 2) // and second most common die count >= 2
        return Rules.ScoreFullHouse;
      return 0;
    }


    // Score dice set as Small Straight.
    private int ScoreSmallStraight ()
    {
      bool success;
      List <int> sorted = new List <int> (Dice);
      sorted.Sort ();
      for (int startIndex = 1; startIndex <= 2; startIndex++)
      {
        success = true;
        for (int i = startIndex; i < DiceCount; i++)
          if (sorted [i] != sorted [i - 1] + 1)
            success = false;
        if (success)
          return Rules.ScoreLargeStraight;
      }
      return 0;
    }


    // Score dice set as Large Straight.
    private int ScoreLargeStraight ()
    {
      List <int> sorted = new List <int> (Dice);
      sorted.Sort ();
      for (int i = 1; i < DiceCount; i++)
        if (sorted [i] != sorted [i - 1] + 1)
          return 0;
      return Rules.ScoreLargeStraight;
    }


    // Score dice set as Yahtzee.
    private int ScoreYahtzee ()
    {
      for (int i = 1; i < DiceCount; i++)
        if (Dice [i] != Dice [0])
          return 0;
      return Rules.ScoreYahtzee;
    }


    // Score dice set as Chance.
    private int ScoreChance ()
    {
      int sum = 0;
      foreach (int n in Dice)
        sum += n;
      return sum;
    }
  }

﻿//========================================================================================
// Class YahtzeeGame
﻿//========================================================================================

  class YahtzeeGame
  {
    public enum Player {Player1, Player2}

    public DiceSet Dice {get; private set;}
    public uint Round {get; private set;} // game rounds are numbered 0-12
    public int ScorePlayer1 {get; private set;}
    public int ScorePlayer2 {get; private set;}
    public bool [] CategoriesPlayer1; // records used categories for player 1
    public bool [] CategoriesPlayer2; //                 "                  2
    public Player ActivePlayer {get; private set;}
    public uint RollsUsed {get; private set;} // number of rolls used by active player


    // Constructor
    public YahtzeeGame ()
    {
      Round = Rules.GameNotActive; // set to a value indicating game has not started;
      CategoriesPlayer1 = new bool [Rules.CategoryCount];
      CategoriesPlayer2 = new bool [Rules.CategoryCount];
      Dice = new DiceSet ();
    }


    // Start a new game.
    public void NewGame ()
    {
      Round = 0;
      ScorePlayer1 = 0;
      ScorePlayer2 = 0;
      ActivePlayer = Player.Player1;
      RollsUsed = 0;
      for (int i = 0; i < Rules.CategoryCount; i++)
      {
        CategoriesPlayer1 [i] = false;
        CategoriesPlayer2 [i] = false;
      }
    }


    // Roll all dice as the first roll.
    public bool FirstRoll ()
    {
      if (RollsUsed != 0 && Round < Rules.TotalRounds)
        return false;
      Dice.RollAll ();
      RollsUsed = 1;
      return true;
    }


    // Reroll selected dice listed int array.
    public bool Reroll (int [] selectedDice)
    {
      if (RollsUsed >= Rules.MaxRolls || Round < Rules.TotalRounds)
        return false;
      foreach (int index in selectedDice)
        Dice.RollDie (index);
      RollsUsed++;
      return true;
    }


    // Add the score of the dice in selected category to active player's total score.
    // Then end the turn.
    public bool SubmitScore (Rules.Category c)
    {
      if (RollsUsed > 0 && Round < Rules.TotalRounds)
      {
        if (ActivePlayer == Player.Player1)
        {
          if (!CategoriesPlayer1 [(int) c])
            return false; // cancel if player 1 has already used scoring category
          ScorePlayer1 += Dice.ScoreDice (c);
          ActivePlayer = Player.Player2;
        }
        else
        {
          if (!CategoriesPlayer2 [(int) c])
            return false; // cancel if player 2 has already used scoring category
          ScorePlayer2 += Dice.ScoreDice (c);
          ActivePlayer = Player.Player1;
        }
        RollsUsed = 0;
        Round++;
        return true;
      }
      return false;
    }
  }

﻿//========================================================================================
// Class Program
﻿//========================================================================================

  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main ()
    {
      Application.EnableVisualStyles ();
      Application.SetCompatibleTextRenderingDefault (false);
      Application.Run (new Form1 ());
    }
  }
}
