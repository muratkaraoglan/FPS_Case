DEFINITION 
We want you to make a first-person shooter game with additional mechanics. Please read 
the instructions carefully. 
## All Mechanics 
-  Talent System 
-  AI System 
-  Gun system 
-  Collecting health and ammo from the Environment 
-  UI Elements 
-  Optional Mechanics 
## Character 
-  Must have a gun. 
-  Must have a certain amount of current health 
-  Must have Maximum amount of health 
-  Must sprint. (Bind to L-Shift preferable) 
-  Must have a leveling system. This level Increases as the character gains experience. 
-  The character gains experience by killing the enemies. 
-  The character gains 1 talent point for each level up. 
## Gun 
-  Must shoot bullets with line tracing. 
-  Must have maximum amount of bullet 
-  Must have certain amount of bullet at the beginning of the game 
## Collecting Bullet from Environment 
-  In Scene, there can be at most of 3 (three) Collectible ammo items. 
-  Once you collect an ammo item, there must be a certain amount of time interval before spawning next ammo item. 
-  The Amount of bullets must be shown on top of the ammo item. 
-  If you collect an ammo item and the amount of collectible bullets exceeds the current maximum amount of ammo, the character must collect needed amount of bullets from the ground. The remaining amount of bullets will stay on the ground. If the all amount of collectible ammo is equal to 3, then no spawning ammo.
## Collecting Health from Environment
-  In level, there can be at most of 1 (one) Collectible health. 
-  Once you collect a health item, there must be a certain time interval before the next health item spawns. 
-  The Amount of collectible health must be shown on top of the health item. 
-  If you collect an health item and if it exceed the current amount of health. The character will heal needed amount of health. Then the collectible health will be destroyed, Contrary to the ammo. 
## Talent System
 The character has 2 categories of upgradable talents. One for the Character itself, the other one for the gun that the character uses.
 Character Talents 
-  Maximum walk and sprint speed (5 level, 1 talent point for each level) 
-  Maximum jump height (5 level, 1 talent point for each level) 
-  Maximum amount of health (5 level, 1 talent point for each level) 
## Gun Talents 
-  Damage amount (5 level, 1 talent point for each level) 
-  Ammo capacity (5 level, 1 talent point for each level) 
-  The pierce shot (1 level, 3 talent point) - Projectiles should pass through enemies instead of stopping at the first target. At the beginning of the game, shots cannot pierce the enemies until this talent is opened. 
## Enemy 
-  The maximum number of enemies in the level/scene is 5. There can be at most 5 enemies at the same time in the scene. 
-  Enemies must patrol at certain patrol points. 
-  Enemies must have a certain amount of health. 
-  Enemies must have a certain attack damage. 
-  Once any enemy see the character, It/they must pursue the character. 
-  If the character is missing for enemies, Enemies must return to the patrolling point. 
-  If the enemy is close enough to the character, it must attack the character. 
-  Idle, walk, run, attack animations must be added to the enemies.
-  The enemies are killed when they ran out of health. 
-  Once the enemy is killed, there must be certain time interval before the next enemy spawns.
## Leveling Up 
-  When the enemy is killed by the character(us), the character gains experience point. 
-  The character needs experience points to level up. 
-  Every Character level has its own experience point amount. (From Character Level 1 to Level 2 needs 100 EXP) 
-  As the character level increases, leveling up requires more experience points. (For example: Level 1 to Level 2 needs 100 EXP, Level 2 to level 3 needs 300 EXP). The needed experience points amount is all up to you. 
## UI 
-  When the player dies, there must be a simple UI page/indicator to restart the game. 
-  When in game, there must be an indicator that shows number of enemies that the character killed. 
-  When in game, there must be Health bar UI that shows the character’s health. 
-  When in game, there must be character Level bar UI that shows the current level and experience point. 
-  When in game, there must be current amount of ammo indicator in UI that shows the character’s ammo.
-  When in a game, there must be health bar above the enemies that shows the enemy’shealth amount. 
-  When in a game, the player can access talent system UI by pressing TAB. 
-  From talent system UI, the player can increase talent by spending talent point. Here is some tips that you can benefit from.
## Variables of the Enemy Object: 
-  Health Amount 
-  Attack Damage 
-  Experience point given to the character when the enemy killed 
## Variables of the Character Object: 
-  Maximum Run/Sprint speed 
-  Maximum height the character can jump 
-  Maximum health 
-  Current Health 
-  Kill score (number of killed enemies) 
-  Character level 
-  Current experience points
## Variables of the Gun Object: 
-  Attack damage 
-  Ammo capacity 
-  Current amount of ammo 
-  Pierce shot 
## Optional 
-  Highest Score In the UI 
-  The hit by the character and die animations of enemies. 
-  Rearrange the maximum number of enemies in the scene by leveling up the character. (For example: When reached 5 level, now the maximum number of enemies that can be exist in the scene is 6) 
-  When shooting, bullets can leave trails behind them. 
-  Enemies can patrol random patrol points. Every time it reaches the current patrol point, generate a random new patrol point. 
-  Don’t exit the atmosphere :)

You are free to add the optional features. If you do not add any of the optional features, it 
will not reflect negatively on your test case. However, if you do, that will evaluate extra for 
you. 
You can use freely any asset you like. However, for the purpose of this case, the esthetic of 
project is not included in the assessment

## EVALUATION 
In this project, we would like to see a clean, organized, and well-structured code and editor 
expertise. The visual quality of the game will not be considered during the evaluation 
process. You can use Unity or Unreal Engine game engine for a test case
