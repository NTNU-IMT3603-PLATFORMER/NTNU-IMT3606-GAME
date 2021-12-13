# **Game Design Document**

_A game design document is a living document that describes the intent of the game design. It has two goals. First, it documents the decisions that have been made about the game and communicates those concepts to the entire team. Thus, it needs to be detailed enough for programmers to refer to when they need clarification about any game aspect. It must be able to be updated as the game is to be built. The need to have a game design document increases with the size of the team and the length of the project._

_For a student project, the intent is to capture as much as possible of your design. The game design will be larger than what you can achieve in a semester, but you must decide what you need to do first. This document should be in version control so that you can see it changing and growing. Given that we are using git, you could also use @name to assign design parts to individual team members._

## Overview

{some\_name}

Mikkel Aas, Ruben Christoffer Hegland-Antonsen, Mikael Falkenberg Krog, Erlend Johan Vannebo, Michael-Angelo Karpowicz

## Game Concept

You play as a druid that travels different biomes/areas to fight different factions of animals. Once you defeat a boss (an animal), you claim its soul granting you the ability to transform into that animal.

## Genre

2D Platformer

- Hollow knight, Ori and the Blind forest

## Target Audience

Teens/Young adults

## Game Flow Summary

Linearly, from left, right, up, down, on a platform. It will be a semi-open world—freedom to go where you please (to some extent).

## Look and Feel

[https://www.youtube.com/watch?v=wOJAErCvVhs](https://www.youtube.com/watch?v=wOJAErCvVhs)

![](img/1.png)

![](img/2.png)

The game will have a nature-based theme; this works well with the druid and animal transformations—different biomes like forests, snowy areas, desert areas, etc.

## Gameplay and Mechanics

The player traverses through a semi-open world where they are met with obstacles, puzzles, and enemies. Each scene or biome will contain one boss you must defeat to continue to the next level/scene. By defeating the boss, you gain its attributes that will increase the complexity of the combat and puzzles by adding new abilities to your character.

## Gameplay

The player will use a set of animal attributes and abilities to make their way through the game.

## Game progression

Very clear progression system with different bosses, unlocking new abilities along the way. Some sort of leveling system with different base stats.

## Mission/challenge Structure

In the beginning, the player will have a very limited set of abilities. The combat and the puzzles will therefore be straightforward to complete. When you gain more abilities by defeating the bosses, the challenges will become increasingly more complex.

## Puzzle Structure

There will be some puzzles, mostly small ones, that can be solved using different animal transformations.

## Objectives

The player&#39;s main objective is to defeat all the bosses.

## Mechanics

What are the rules to the game, both implicit and explicit?
 This is the model of the universe that the game works under.
 Think of it as a simulation of a world. How do all the pieces interact?

- Entities have health
- Break objects
- Combat
  - Real time
  - Not turn-based
  - Melee and ranged
  - Good positioning and good timing required
- Movement
  - Jump
  - Double jump
  - Sprint, Dash
  - Fast fall (enhanced gravity)
    - Not all animal shapes will be able to do every movement
    - Fall down quickly as a bear, take fall as a cat (puzzle?), glide as a bird (fly on the wings of love), etc.

- Transformations
  - Owl
    - Glide
  - Bear

## Physics

Physics will not be favored over fluent movement. We will have some physics, such as collision with other entities and gravity. Friction, gravity.

## Movement

The player will move / attack/interact by using a keyboard. Though, the mouse can be used to navigate the interface/UI. The player will also have the option to play with a controller.

## Objects

- Platforms! (some moving)
- Obstacles
  - Objects that needs to be destroyed or moved to progress
- Enemies
  - Smaller entities that needs to be dodged/kill to prevent yourself from doom
- Bosses
  - Big entities that when slain will allow you to enter the next scene/biome
- Soul
  - currency
- Blood
  - health

## Actions

What are the other interactions the player has with the game world?

- Runic checkpoints
- Hidden walls
- Picking herbs / flowers / mushrooms in small plastic bags

## Combat

If there is combat or conflict, how is this specifically modeled?

- Druid
  - Melee stick (kjepp)
- Bear
  - Higher defence
  - Heavy hitting, slow attacks
  - Heavy (falls faster, moves slower, jumps low)
- Cat
  - Agile
  - Low damage, quick attacks
  - Lightweight (absorbs fall damage, faster, jumps high)
  - Night vision (?)
- Owl
  - Can semi-fly (hover)
  - Ranged attack (?) (ohoooooooo)
  - Fear? ( **o**hooo**oo**ooooho**oo**ooooo)

## Economy

Killing enemies grants you...

Animal soul points.

Soul points can be used to upgrade animal specific stats. For each animal upgrade the druid receives an upgrade (health etc.)

## Scene Flow

Each scene represents the biome of the animal-boss. When the boss is defeated there is a small travel sequence before the next biome is loaded.

## Game Options

Difficulty modes. Adds multipliers to mob/player health, damage upgrades etc.

## Replay and Saving

- Borderlands-style replay, second time is harder, but you keep upgrades
  - In theory, endless upgrades
- Save after each checkpoint
  - Save inventory, levels, etc. anytime (automatically)
  - Respawn at checkpoint

## Cheats and Easter Eggs

This part will not be prioritized

- No cheats.
- Unlock rabbit as easter egg, all it does is lay easter eggs
  - 1hp, challenge to complete the game as the rabbit

## The Story, Setting, and Character

## Story and Narrative

The world has been corrupted, and you, as a druid, must save nature and the souls of the animals. The game will not have a big focus on the story, but some small story elements will be present. Some minor cut scenes can be included, for example when changing the scene or meeting the boss.

## Game World

The world will be in nature, with nature being corrupted and dark when entering a new area. When the boss of the area has been defeated, the area will become lighter.

## General look and feel of the World

Mostly dark and gloomy nature look.

## Areas

Each area will be based on a different biome that corresponds to the natural habitat of the given animal boss. Forests, deserts, icy area etc.

## Characters

The game will not include a lot of characters, except the main character.

## Levels

## Playing Levels

Each level should include a synopsis, the required introductory material (and how it is provided), the objectives, and the details of what happens in the level.
 Depending on the game, this may include the physical description of the map, the critical path that the player needs to take, and what encounters are important or incidental.

![](img/3.png)

![](img/4.png)

Forest:

- Owl boss
- Gains the owl transformation after defeating the boss

Desert:

- Cat boss(sphinx)
- Gains the cat transformation after defeating the boss

Icy:

- Polar bear boss
- Gains the polar bear transformation after defeating the boss

Final boss area, final boss tba. Maybe a god / demon?

## Training level

How is onboarding managed?

At first the player will learn how to move and attack. Upon acquiring a new animal spirit, the payer will receive a quick tip on how to utilize it.

## Interface

## Visual System

If you have a HUD, what is on it? What menus are you displaying? What is the camera model?

- Health bar
- Currency
  - Displays how many animal souls you have collected (they are corrupted so it&#39;s fine)
- Amount of herbs / shrooms
- Current animal
  - Display the current animal in an icon

## Control System

How does the game player control the game? What are the specific commands?

Possible option for binding your own keys

- Default keyboard

- WASD: move
- Space: jump
- j: Primary attack
- bnm: Transformation
- 1: Smoke herb
- e: Interact
- shift: sprint
- dd: dash right
- aa: dash left
  - This has to be tuned so that you don't accidentally dash when you just want to make several small movements.

## Audio, Music, Sound Effects

[https://www.youtube.com/watch?v=vdwYUbVtR6U](https://www.youtube.com/watch?v=vdwYUbVtR6U)

## Help System

## Artificial Intelligence

## Opponent and Enemy AI

The active opponent plays against the player and therefore requires strategic decision-making. The boss changes state during combat. Would be appropriate to use a finite state machine here. Each state is designed to be in favor of certain animals.

## Non-combat and Friendly Characters

## Support AI

- Garden gnome

## Player and Collision Detection, Path-finding.

- Pathfinding
  - Enemies walk back and forth on the platforms. When the player is nearby, they will walk towards the player.
  - Will collide with enemies

## Technical

## Target Hardware

- Anything
- It is not hardware cost heavy

## Development Hardware and Software (including the game engine)

- Unity 2020
- Linux, Windows

## Network requirements

None

## Game Art

## Key assets

How are they being developed? Intended style.

The art will be developed in photoshop. The style will be pixel art.

Something like this:

![](img/4.png)

Ideally something like this:

![](img/5.png)

![](img/6.png)

