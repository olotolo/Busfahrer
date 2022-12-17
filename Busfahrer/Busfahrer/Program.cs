// Playing cards 2,3,.. , Ace
string cardIdentifier = "234567890jqka";
// 0 = karo, 1 = herz, 2 = pik, 3 = kreuz
string cardIdentifier2 = "0123";
// Holds all the cards in a deck.
List<string> cards = new List<string>();

Random random = new Random();
Run runStupid = new Run();

List<string> currentCards = new List<string>();


// Add all 52 cards to cards...
for(int i = 0; i < cardIdentifier.Length; i++)
{
    for(int j = 0; j < cardIdentifier2.Length; j++)
    {
        cards.Add(cardIdentifier[i].ToString() + cardIdentifier2[j]);

    }
}
currentCards.AddRange(cards);

Run runSmart = new Run();
Run runAverage = new Run();

for(int i = 0; i < 10000; i++)
{
    runStupid.PlayStupid(currentCards, random, cards);
    
}
/*
Console.WriteLine(runStupid.FirstRoundWon.ToString());
Console.WriteLine(runStupid.FirstRoundLosses.ToString());
Console.WriteLine(runStupid.SecondRoundWon.ToString());
Console.WriteLine(runStupid.SecondRoundLosses.ToString());
Console.WriteLine(runStupid.ThirdRoundWon.ToString());
Console.WriteLine(runStupid.ThirdRoundLosses.ToString());
Console.WriteLine(runStupid.FourthRoundWon.ToString());
Console.WriteLine(runStupid.FourthRoundLosses.ToString());
*/
Console.WriteLine("Total amount of wins with random decisions: " + runStupid.FourthRoundWon.ToString());

currentCards.AddRange(cards);
for (int i = 0; i < 10000; i++)
{
    runSmart.PlaySmart(currentCards, random, cards);
}
/*
Console.WriteLine("Smart Wins  " + runSmart.FirstRoundWon.ToString());
Console.WriteLine("Smart Losses  " + runSmart.FirstRoundLosses.ToString());
Console.WriteLine("Smart Wins  " + runSmart.SecondRoundWon.ToString());
Console.WriteLine("Smart Losses  " + runSmart.SecondRoundLosses.ToString());
Console.WriteLine("Smart Wins  " + runSmart.ThirdRoundWon.ToString());
Console.WriteLine("Smart Losses  " + runSmart.ThirdRoundLosses.ToString());
Console.WriteLine("Smart Wins  " + runSmart.FourthRoundWon.ToString());
Console.WriteLine("Smart Losses  " + runSmart.FourthRoundLosses.ToString());
*/
Console.WriteLine("Total amount of wins with card counting: " + runSmart.FourthRoundWon.ToString());

currentCards.AddRange(cards);
for (int i = 0; i < 10000; i++)
{
    runAverage.PlayAverage(currentCards, random, cards);
}
/*
Console.WriteLine(runAverage.FirstRoundWon.ToString());
Console.WriteLine(runAverage.FirstRoundLosses.ToString());
Console.WriteLine(runAverage.SecondRoundWon.ToString());
Console.WriteLine(runAverage.SecondRoundLosses.ToString());
Console.WriteLine(runAverage.ThirdRoundWon.ToString());
Console.WriteLine(runAverage.ThirdRoundLosses.ToString());
Console.WriteLine(runAverage.FourthRoundWon.ToString());
Console.WriteLine(runAverage.FourthRoundLosses.ToString());
*/
Console.WriteLine("Total amount of wins when only making decisions based on the current card: " + runAverage.FourthRoundWon.ToString());


public class Run {
    public bool Won = false;
    public int Tries = 0;

    public string firstCard = "";
    public string secondCard = "";

    public int currentIndex = 0;

    public int FirstRoundWon = 0;
    public int FirstRoundLosses = 0;
    public int SecondRoundWon = 0;
    public int SecondRoundLosses = 0;
    public int ThirdRoundWon = 0;
    public int ThirdRoundLosses = 0;
    public int FourthRoundWon = 0;
    public int FourthRoundLosses = 0;

    public List<string> PlayAverage(List<string> currentCards, Random random, List<string> cards)
    {
        if (currentCards.Count == 0) currentCards.AddRange(cards);
        int result = random.Next(4);
        Won = FirstGame(currentCards, random, result);
        currentCards.RemoveAt(currentIndex);
        if(Won)
        {
            FirstRoundWon++;
        } else
        {
            FirstRoundLosses++;
            return currentCards;
        }

        if (currentCards.Count == 0) currentCards.AddRange(cards);
        if (GetCardValue(firstCard[0]) >= 8)
        {
            result = 0;
        } else
        {
            result = 1;
        }
        Won = SecondGame(currentCards, random, result);
        currentCards.RemoveAt(currentIndex);
        if(Won)
        {
            SecondRoundWon++;
        } else
        {
            SecondRoundLosses++;
            return currentCards;
        }


        if (currentCards.Count == 0) currentCards.AddRange(cards);
        int firstCardValue = GetCardValue(firstCard[0]);
        int secondCardValue = GetCardValue(secondCard[0]);
        int difference = 0;
        if(firstCardValue >= secondCardValue)
        {
            difference = firstCardValue - secondCardValue;
        } else
        {
            difference = secondCardValue - firstCardValue;
        }

        if(difference >= 6)
        {
            result = 0;
        } else
        {
            result = 1;
        }
        Won = ThirdGame(currentCards, random, result);
        currentCards.RemoveAt(currentIndex);
        if(Won)
        {
            ThirdRoundWon++;
        } else
        {
            ThirdRoundLosses++;
            return currentCards;
        }

        if (currentCards.Count == 0) currentCards.AddRange(cards);
        result = random.Next(4);
        Won = FourthGame(currentCards, random, result);
        currentCards.RemoveAt(currentIndex);
        if(Won)
        {
            FourthRoundWon++;
        } else
        {
            FourthRoundLosses++;
            return currentCards;
        }

        return currentCards;
    }
    public List<string> PlaySmart(List<string> currentCards, Random random, List<string> cards)
    {
        if (currentCards.Count == 0) currentCards.AddRange(cards);
        int red = 0;
        int black = 0;
        // The Player knows exacly how many cards of each type are still in the deck.
        // in reality the Player knows what cards have been discarded so he can also know all that are left in.
        for(int i = 0; i < currentCards.Count; i++)
        {
            if (currentCards[i][1] - '0' < 2)
            {
                red++;
                
            } else
            {

                black++;
            }
        }
        int result = 0;
        if(red >= black)
        {
            result = 0;
        } else
        {
            result = 2;
        }

        Won = FirstGame(currentCards, random, result);
        currentCards.RemoveAt(currentIndex);
        if (Won)
        {
            FirstRoundWon++;
        }
        else
        {
            FirstRoundLosses++;
            return currentCards;
        }

        if (currentCards.Count == 0) currentCards.AddRange(cards);

        int above = 0;
        int below = 0;
        for(int i = 0; i < currentCards.Count; i++)
        {
            if(GetCardValue(firstCard[0]) > GetCardValue(currentCards[i][0]))
            {
                below++;
                continue;
            }
            if (GetCardValue(firstCard[0]) < GetCardValue(currentCards[i][0]))
            {
                above++;
                continue;
            }
        }

        if(above >= below)
        {
            result = 1;
        } else
        {
            result = 0;
        }
        Won = SecondGame(currentCards, random, result);
        currentCards.RemoveAt(currentIndex);
        if (Won)
        {
            SecondRoundWon++;
        }
        else
        {
            SecondRoundLosses++;
            return currentCards;
        }

        if (currentCards.Count == 0) currentCards.AddRange(cards);

        int firstCardValue = GetCardValue(firstCard[0]);
        int secondCardValue = GetCardValue(secondCard[0]);
        int outside = 0;
        int inside = 0;
        for(int i = 0; i < currentCards.Count; i++)
        {
            if (GetCardValue(currentCards[i][0]) > Math.Max(firstCardValue, secondCardValue) || GetCardValue(currentCards[i][0]) < Math.Min(firstCardValue, secondCardValue))
            {
                outside++;
            } else
            {
                inside++;
            }
        }
        if(outside >= inside)
        {
            result = 1;
        } else
        {
            result = 0;
        }

        Won = ThirdGame(currentCards, random, result);
        currentCards.RemoveAt(currentIndex);

        if(Won)
        {
            ThirdRoundWon++;
        } else
        {
            ThirdRoundLosses++;
            return currentCards;
        }

        if (currentCards.Count == 0) currentCards.AddRange(cards);
        int karo = 0;
        int herz = 0;
        int pik = 0;
        int kreuz = 0;
        for(int i = 0; i < currentCards.Count; i++)
        {
            if (currentCards[i][1] == 0)
            {
                karo++;
                continue;
            }
            if(currentCards[i][1] == 1)
            {
                herz++;
                continue;
            }
            if (currentCards[i][1] == 2)
            {
                pik++;
                continue;
            } else
            {
                kreuz++;
                continue;
            }
        }

        if (karo >= herz && karo >= pik && karo >= kreuz)
        {
            result = 0;
        } else if(herz>=pik && herz >= kreuz)
        {
            result = 1;
        } else if(pik >= kreuz)
        {
            result = 2;
        } else
        {
            result = 3;
        }

        Won = FourthGame(currentCards, random, result);
        currentCards.RemoveAt(currentIndex);

        if(Won)
        {
            FourthRoundWon++;
        } else
        {
            FourthRoundLosses++;
        }


        return currentCards;
    }

    public List<string> PlayStupid(List<string> currentCards, Random random, List<string> cards)
    {
        if(currentCards.Count == 0) currentCards.AddRange(cards);
        Won = FirstGame(currentCards, random, random.Next(4));
        currentCards.RemoveAt(currentIndex);

        if(Won)
        {
            FirstRoundWon++;
        } else
        {
            FirstRoundLosses++;
            return currentCards;
        }

        if (currentCards.Count == 0) currentCards.AddRange(cards);
        Won = SecondGame(currentCards, random, random.Next(2));
        currentCards.RemoveAt(currentIndex);

        if(Won)
        {
            SecondRoundWon++;
        } else
        {
            SecondRoundLosses++;
            return currentCards;
        }

        if (currentCards.Count == 0) currentCards.AddRange(cards);
        Won = ThirdGame(currentCards, random, random.Next(2));
        currentCards.RemoveAt(currentIndex);

        if(Won)
        {
            ThirdRoundWon++;
        } else
        {
            ThirdRoundLosses++;
            return currentCards;
        }

        if (currentCards.Count == 0) currentCards.AddRange(cards);
        Won = FourthGame(currentCards, random, random.Next(4));
        currentCards.RemoveAt(currentIndex);

        if(Won)
        {
            FourthRoundWon++;
        } else
        {
            FourthRoundLosses++;
            return currentCards;
        }
        return currentCards;
    }
    //First game where a random number is chosen (If the player is playing without any stragetgy)
    public bool FirstGame(List<string> currentCards, Random random, int guess)
    {
        bool result = false;
        // Sets it to a number between 0-3.   0-1 = red,   2-3 = black
        
        // Gets a random card
        int index = random.Next(currentCards.Count);
        firstCard = currentCards[index];
        currentIndex = index;
        //Console.WriteLine(currentCards[index][1] + "   " + guess);
        if (currentCards[index][1] - '0' < 2)
        {
            if (guess < 2)
            {
                result = true;
            } else
            {
                result =  false;
            }
        } else
        {
            if(guess < 2)
            {
                result = false;
            } else
            {
                result =  true;
            }
        }
        return result;
    }

    public bool SecondGame(List<string> currentCards, Random random, int guess)
    {
        bool result = false;
        // 0 = is lower, 1 = is higher
        
        // get random card
        int index = random.Next(currentCards.Count);
        


        if (guess == 0)
        {
            if (GetCardValue(currentCards[index][0]) >= GetCardValue(firstCard[0]))
            {
                result = false;
            } else
            {
                result = true;
            }
        } else
        {
            if (GetCardValue(currentCards[index][0]) > GetCardValue(firstCard[0]))
            {
                result = true;
            } else
            {
                result = false;
            }
        }

        secondCard = currentCards[index];
        currentIndex = index;
        return result;

    }
    
    public bool ThirdGame(List<string> currentCards, Random random, int guess)
    {
        bool result = false;
        int firstCardValue = GetCardValue(firstCard[0]);
        int secondCardValue = GetCardValue(secondCard[0]);
        int index = random.Next(currentCards.Count);
        int currentCardValue = GetCardValue(currentCards[index][0]);
        currentIndex = index;
        // 0 = inbetween, 1 = outside 
        if(guess == 0)
        {
            if (currentCardValue < Math.Max(firstCardValue, secondCardValue) && currentCardValue > Math.Min(firstCardValue, secondCardValue))
            {
                result = true;
            } else
            {
                result = false;
            }
        } else
        {
            if (currentCardValue > Math.Max(firstCardValue, secondCardValue) || currentCardValue < Math.Min(firstCardValue, secondCardValue))
            {
                result = true;
            } else
            {
                result = false;
            }
        }


        return result;
    }
    
    public bool FourthGame(List<string> currentCards, Random random, int guess)
    {
        bool result = false;
        int index = random.Next(currentCards.Count);
        currentIndex = index;
        if (guess.ToString() == currentCards[index][1].ToString())
        {
            result = true;
        } else
        {
            result = false;
        }


        return result;
    }
    
    public int GetCardValue(char a)
    {
        if (a == '2') return 2;
        if (a == '3') return 3;
        if (a == '4') return 4;
        if (a == '5') return 5;
        if (a == '6') return 6;
        if (a == '7') return 7;
        if (a == '8') return 8;
        if (a == '9') return 9;
        if (a == '0') return 10;
        if (a == 'j') return 11;
        if (a == 'q') return 12;
        if (a == 'k') return 13;
        if (a == 'a') return 14;
        return 0;

    }
}