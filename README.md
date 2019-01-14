# Lab03-SystemI.O.
Repository for the System Input/Output lab for CodeFellows dn-401d6 course - word guessing game/hangman

## Day 4 lab: Word Guessing Game

#### Requirements
- When playing a game, randomly select one of the words to output to the console for the user to guess (Use the Random class)
- When playing a game, there should be a record of the letters they have attempted so far
- If a correct letter is guessed, display that letter in the console for them to refer back to when making guesses (i.e. C _ T S )
- Guessed letters should not be case sensitive.
- Errors should be handled through Exception handling
- External classes should not be used to accomplish this task. All code should live in the Program.cs file
- Once the game is completed, the user should be presented with the option to “Play again” (a new random word is generated), or “Exit” (the program terminates)
- The user should only be allowed to guess only 1 letter at a time. Do not make it so that they can input the whole alphabet and get the answer.

#### Setup/Running instructions
- Clone or download this repository into a folder of your choice.
- Open up the solution file in an IDE of your choice, preferably Visual Studio
- Within your IDE, start the solution with or without debugging. Use the console window that opens to type in input.
- Choose option 1 to play the game.
- Guess letters one at a time until either all letters are revealed or you run out of guesses.
- Once returned to the menu, select option 3 to exit.

#### Sample inputs:
- Assume the word selected is "ocean".
- Select the letter o.
- Select the letter b.
- Select the letters a, c, e, and n in any order.
- Select the letter e.

#### Sample outputs:
- You have 26 guesses remaining
- Current state of word being guessed
- _____
- Previously guessed letters:

- You have 25 guesses remaining
- Current state of word being guessed
- o____
- Previously guessed letters: o,

- You have 24 guesses remaining
- Current state of word being guessed
- o____
- Previously guessed letters: b, o

- You have 21 guesses remaining
- Current state of word being guessed
- ocea_
- Previously guessed letters: a, b, c, e, o

- Congratulations you win

#### Screen captures:
- ![Initial state of game](https://github.com/Dervival/Lab03-SystemI.O/blob/master/InitialGame.PNG);
- ![Final state of won game](https://github.com/Dervival/Lab03-SystemI.O/blob/master/FinishedGame.PNG);

