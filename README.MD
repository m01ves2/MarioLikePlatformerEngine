# Mario (MonoGame)

A 2D Mario-inspired platformer built with C# and MonoGame. The project features a custom physics and collision system, tile-based level design, enemy behaviors, sprite animations, and a camera that follows the player.

The game was developed as a learning project to explore real-time game architecture and the implementation of platformer mechanics without relying on an existing physics engine.

## Features

- **Player movement:** Horizontal acceleration, friction, gravity, variable-height jumps, jump buffering, and coyote time.
- **Custom physics:** Axis-separated collision resolution for tile-based environments.
- **Enemy AI:** Ground patrol and flying behaviors with different movement strategies.
- **Gameplay interactions:** Collectible coins, enemy damage, stomping, scoring, and limited lives.
- **Tile-based world:** A level constructed from a character-based map definition.
- **Animations:** Sprite-sheet animations for the player, enemies, and collectibles.
- **Camera:** Player-following 2D camera with world-boundary clamping and background parallax.
- **Audio:** Sound effects and background music for gameplay and different scenes.
- **Scene management:** Main menu, gameplay, victory, and game-over screens.

## Technology Stack

- C#
- .NET 10
- MonoGame (DesktopGL)
- MonoGame Content Pipeline

## Architecture

The project separates game state, entity behavior, movement calculations, collision detection, gameplay rules, and rendering.

### Game Loop and Scene Management

`Game1` serves as the entry point and coordinates resource initialization, input updates, scene updates, and rendering.

`SceneManager` manages scene transitions using commands returned by individual scenes.

The main game scenes include:

- `MenuScene` - Main menu and game startup.
- `GameScene` - Gameplay simulation, rendering, and game-flow management.
- `GameOverScene` - Game-over screen.
- `GameWinScene` - Victory screen.

Scene transitions are represented by `SceneUpdateResult`, which contains a `GameCommand` and the relevant `GameResult`.

### Entities and Movement

All gameplay objects derive from the abstract `Entity` class, which stores common data such as position, velocity, collision bounds, contact state, and animation state.

Different responsibilities are handled by separate components:

- `IBehavior` - Determines the movement intention of an entity.
- `MovementIntent` - Represents the desired movement direction and jump input.
- `IMovement` - Converts movement intentions into velocity changes.
- `PhysicsMovement` - Implements acceleration, friction, and speed limits.
- `SimpleMovement` - Assigns velocity directly.
- `VerticalMovement` - Handles gravity, jumping, jump buffering, and coyote time.

Player input and enemy AI use different mechanisms to produce movement intentions, while the physics system is responsible for applying movement to the world.

### Physics and Collision Handling

`PhysicsSystem` updates entity positions and handles collisions with the tile map and other entities.

Tile collisions are resolved separately along the horizontal and vertical axes.

The main processing stages are:

1. Calculate entity movement and resolve tile collisions.
2. Detect collisions between entities.
3. Update contact information.
4. Commit the calculated physical state to the entity.
5. Apply gameplay-specific collision rules.

Entity collisions generate `CollisionEvent` objects containing the involved entities and collision side.

`CollisionRulesSystem` processes these events through implementations of `ICollisionRule`.

Examples include:

- `PlayerCoinCollectRule` - Collect coins and increase the score.
- `PlayerEnemyDamageRule` - Apply damage when the player collides with an enemy.
- `PlayerEnemyStompRule` - Defeat an enemy and make the player bounce.

This separation keeps gameplay-specific reactions outside the main physics system.

### Level Construction

`LevelBuilder` constructs the game world from a character-based level definition.

Each character represents a tile or gameplay object, such as ground, walls, coins, enemies, or the player spawn position.

`TileMap` stores the level geometry, while `EntityFactory` creates gameplay entities and assigns their animations.

The current version contains one hardcoded level.

### Camera and Rendering

`Camera2D` uses a transformation matrix to render the world relative to the player's position.

The camera follows the player and is constrained by the level boundaries.

The rendering system separates world rendering from the user interface. Backgrounds, tiles, entities, and the goal are rendered in world coordinates, while score and lives are displayed in screen coordinates.

## Project Structure

```text
Application/
├── LevelBuilders/
├── Scenes/
├── SceneManager.cs
├── GameCommand.cs
├── GameResult.cs
└── GameSession.cs

Core/
├── Components/
│   ├── Behavior/
│   └── Movement/
└── Entities/

Inputs/
Resources/
Systems/
├── GameplayRules/
└── Physics/

World/
Game1.cs
Program.cs
```

## Known Limitations

The project intentionally uses a simplified game engine architecture.

- Only one hardcoded level is currently available.
- Entity-to-entity collisions do not provide complete symmetric physical resolution.
- Physics calculations may depend on entity processing order.
- Collision detection is discrete and does not implement continuous collision detection.
- Tile rendering does not perform camera-based culling.
- The engine is designed for a small platformer rather than general-purpose game development.

## Future Improvements (Original Plans)

The original project scope included several features that were deliberately postponed to keep development within the planned deadline.

- **External Level Loading:** Replace the hardcoded level definition with levels loaded from files.
- **Multiple Levels:** Support sequential level progression while preserving the player's score and remaining lives.
- **Built-in Level Editor:** Allow players to create and edit their own levels directly from the game's main menu.
- **Custom Level Sharing:** Save user-created levels to files that can be shared and played by others.
- **Level Testing:** Allow players to immediately test their custom levels in the game.

These features were intentionally left out of the final version to prioritize learning platformer physics, collision detection, entity behavior, camera movement, and the MonoGame framework.

## Learning Outcomes

This project provided practical experience with:

- Real-time game loops and frame-based updates.
- Delta-time-based movement calculations.
- Designing and implementing a custom 2D physics system.
- Separating entity behavior from movement implementation.
- Tile-based collision detection and resolution.
- Event-driven gameplay collision rules.
- Sprite animations and camera transformations.
- Scene management and resource loading.
- Managing project scope and prioritizing learning objectives.

## Notes

This is an educational project rather than a production-ready game engine.

The primary goal was to understand how the fundamental systems of a 2D platformer work internally, including the interaction between player input, entity behavior, physics, collisions, gameplay rules, and rendering.


## Build and Run

### Requirements

- Windows
- .NET 10 SDK
- Visual Studio with C#/.NET development support
- MonoGame development tools and Content Pipeline support

### Running the Game

1. Clone the repository.
2. Open the solution (`.sln`) in Visual Studio.
3. Restore NuGet packages if they are not restored automatically.
4. Select the game project as the startup project.
5. Press **F5** to build and run the game.

MonoGame uses the Content Pipeline to process textures, sounds, fonts, and other game assets. Make sure the required MonoGame tools are installed and the content project builds successfully.

### Controls

- **Left / Right Arrow** - Move Mario.
- **Space** - Jump (hold for a higher jump).
- **Esc** - Return to the main menu.
- **Up / Down Arrow** - Navigate the main menu.
- **Enter** - Confirm a menu selection.