# HSCSReader

HSCSReader is a HearthStone Card Statistics Reader application.

This is part of a bigger package which will eventually:
  - Consume Hearthstone Log Files and HSReplay Files
  - Derive statistics about the contained games and game entities
  - Commit those statistics to a community-driven database
  - Allow those statistics to be viewable via the web

Still not sure what we're dealing with? Consider the following card:

**Acolyte of Pain** [EX1_007][Classic][Neutral Minion][3 mana, 1/3]: Whenever this minion takes damage, draw a card. *He trained when he was younger to be an acolyte of joy, but things didn’t work out like he thought they would.*

Imagine if we could analyze some Hearthstone data and produce an output like this (sample):

```
=== Acolyte of Pain ===
Seen 67,252 times in 43,713 games over 5 months.
Phase of Game:
     Average Turn Drawn: 4.12
		[Histogram of Turns Drawn]
     Average Turn Played: 4.34
		[Histogram of Turns Played]
     Average Turn Died: 6.12
		[Histogram of Turns Died]
Effectiveness:
     Average Turns In Hand: 1.31
		[Histogram of Turns in Hand]
     Average Turns Alive: 2.15
		[Histogram of Turns Alive]
     Average Draws: 1.72
		[Histogram of Draws]
     Average Damage Taken: 3.62
		[Histogram of Damage Absorbed]
     Average Damage Dealt: 1.84
		[Histogram of Damage Given]
     Winrate when Drawn: 44.82%
     Winrate when Played: 51.16%
High Scores:
     Most Health: 24 (by PlayerX in Oct 2015)
     Most Damage Received in one Game: 26 (by PlayerX in Oct 2015)
     Most Attack: 6 (by PlayerY in Oct 2015)
     Most Damage Dealt in one Game: 8 (by PlayerY in Oct 2015)
     Most Cards Drawn: 8 (by PlayerZ in Oct 2015)
... and so on
```

A Hearthstone Fan could then upload their own statistics and compile their stats for Acolyte of Pain over their 30 games. They would be able to compare their stats side-by-side with the community average and realize:

- Wow, I need to work on more draws with my Acolyte because My average is 1.30 and the community average is 1.72.
- Wow, I need to mulligan for this card harder since my average drawn is 6.15 and the community average is 4.12.

Want more possibilities?

- How many Dream Cards are generally drawn off of Ysera?
- How often does Alexstrasza target the owning player vs the enemy player? What's the average life difference?
- What percentage of the time is Sludge Belcher silenced when played?
- What is the win rate with Card X across all the classes?
- What percentage of the time does Card X win Jousts?
- How much Armor does a Warrior gain over the course of a game?
- How much Armor does an Armorsmith give you throughout a game?
- How many times per game does Class X use their hero power?
- How many Grim Patrons are spawned off of one Patron?
- What is the most damage anyone has done with Pyroblast?
- ... and more!