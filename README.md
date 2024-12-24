# TileMap Game
 
This is an assignemnt in developing games course.
In this game I practiced with tile mapping.

[Itch.io](https://lizachep.itch.io/tilemap-game)

## What is in the game?

### Cool effects

Rideable objects: [Rideable controller](https://github.com/Liza-Gaming/TileMap-Game/blob/main/Assets/Scripts/RideableController.cs), [Rideable Mover](https://github.com/Liza-Gaming/TileMap-Game/blob/main/Assets/Scripts/RideableMovement.cs).

You can ride a boat to cross the sea and a sheep to cross the eleveations.

You can collect a pickaxe to [Pickaxe effect](https://github.com/Liza-Gaming/TileMap-Game/blob/main/Assets/Scripts/PickaxeEffect.cs)

### Eveding objects

You can see the code here: [Evading](https://github.com/Liza-Gaming/TileMap-Game/blob/main/Assets/Scripts/EvadingEnemy.cs)

**The algorithm**

Check Player Distance:

If the player is too far away (beyond the safe distance), stop moving.
Calculate Escape Directions:

Get three potential escape directions: 
- Directly away from the player
- Perpendicular to the right
- Perpendicular to the left

Find Best Escape Route:

For each potential direction:
Cast a ray to check for obstacles
Choose the direction with the most clear path (least obstacles)
Move in Chosen Direction:
Set the enemy's velocity to move in the chosen direction
If moving right and facing left (or vice versa), flip the enemy sprite

Continuous Update:

Repeat this process every frame to constantly adjust the enemy's movement


