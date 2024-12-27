# Goblin-King
 
# Pitch

Step into the shoes of the Goblin King, the ruler of a scrappy, underestimated goblin faction struggling to carve out its place in a hostile world. 

In this survival-meets-management game, you command your goblins to gather resources, construct structures, craft items, and fend off enemies. Balance survival and expansion as you strive to create a thriving goblin kingdom.

## Technical Overview

This project was developed over 7 weeks as part of a school exam, with a focus on advancing my programming skills through the implementation of complex systems in Unity. The game leverages advanced coding techniques to deliver a dynamic and engaging experience. Below are the key technical aspects of the project:

## Project Goals

Improve my coding abilities by creating systems for:
- Procedurally generated endless worlds
- AI-driven goblin behaviors for resource gathering, combat, and construction.
- Real-time management mechanics for resource balancing and decision-making.
- System Integration: Combine survival mechanics with strategic management gameplay.

## Core Features

1. Procedural World Generation

The game generates an endless world consisting of biomes, resources, and enemy encounters.
Implemented using noise-based algorithms for terrain generation and object placement.

2. Goblin AI and Behavior

Goblins are designed as autonomous units with task-based AI.
Assign a tool to each Goblin that determines their cabalities.
Keep your Goblins alive by fending of threats like human enemies.

AI Tasks include:
- Gathering resources like wood, stone, and food.
- Building structures based on player commands.
- Defending the kingdom and attacking enemies in proximity.
- Crafting items when provided with required resources.

3. Building and Crafting Systems

A modular building system allows players to construct essential structures like crafting stations and item storage.
The crafting system enables the creation of items that improve goblin capabilities or provide survival benefits.

## Development Highlights

Unity Version: [Unity 6.0.]

Programming Language: C#

Art Assets: [My own 2D assets]


## Challenges and Solutions

1. Random World Generation

Challenge: Creating an endless world

Solution: Used 

2. Goblin AI Task Management

Challenge: Designing AI that can switch tasks dynamically based on player commands and environmental factors.

Solution: Implemented a Task system that queues tasks and finds goblins that are approriate for the task, like checking if they have the correct tool