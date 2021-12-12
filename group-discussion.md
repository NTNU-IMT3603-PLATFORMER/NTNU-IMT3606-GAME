



# Platformer Group Discussion

December, 2021

Mikkel Aas

Ruben Christoffer Hegland-Antonsen

Michael Angelo Karpowicz

Mikael Falkenberg Krog

Erlend Johan Ringen Vannebo






# Strengths and weaknesses of Unity


## Strengths


### Good tutorials, documentation, and a big community

Unity is one of the most used game engines and has a huge community with over a million active users each month. This means there is a high demand for tutorials and guides on how to do many different things in Unity. Therefore, there are plenty of good resources such as video tutorials and forum threads on the internet to help you learn and understand Unity. Unity is also very thoroughly documented, with great descriptions and examples of everything Unity offers. We found these aspects of having Unity as our game engine very useful because we could look up anything we were unsure of.


### C# as main scripting language

C# is a very high-level, flexible programming language which makes it a lot easier to learn than lower level languages such as C or C++. C# is similar to Java in many ways and we have used Java before so a lot of the syntax and how C# work was familiar to us, and that helped us a lot during the process of learning the language. C# is also well documented which was a huge positive. We also didn’t encounter any C# specific problems, so we were very happy with the process of learning and using C# in our Unity project. 


### Built-in physics engine

Unity uses PhysX for 3D and the Box2D engine for 2D, which simplifies physics calculation for your game. It’s also very fast and relatively easy to use. We used the physics engine a lot for creating character movement in our game. In the beginning, we had some issues where we did not use the physics engine properly, by for instance moving the Transform of an object directly instead of using the Rigidbody, but we figured out how to properly do it in the end. 


### Mature Game Engine

Unity has been actively developing the engine since 2005 and has matured over the years. This means that they have seen a lot of improvements and provide good support. It is also well known and could be seen as an industry standard along with the Unreal Engine. 


### Build once, reach billions

Write once, build to any platform. Unity supports a wide range of platforms to deploy to desktop, mobile, and consoles. We decided to build for the web as it is the easiest to share and requires the least UI settings in-game as the web provides it. 


### Big asset store

Unity's asset store contains every asset an indie developer needs. It has visual scripting tools, so you do not even have to know how to code to make scripts for your game. But most importantly, it has a wide variety of art for every style, which means that we did not have to spend too much time making our own sprites and animations. However, we did create the sprites for our levels and collectibles.


### Prefabs 

Prefabs are easy to make and use and allow you to create templates of your game objects. This allows you to easily place multiple enemies for instance, and can also be used for instantiation. They do not always work as well with version control though if multiple people change a given prefab.


### Serialized fields

Serialized fields make fields accessible from the Unity editor. This is very helpful when you, for example, want to test the effect of different field values and get instant feedback from the game. Furthermore, it helps analyze the change of field values as you play the game. In our project, we used it, for example, to find the perfect speed for our main character and their enemies. We also used it to see that the player's health field was accurately updated when the player took damage.


## Weaknesses


### Poor built-in team collaboration tool (Unity Collaborate)

Unity Collaborate is a tool within Unity that you can use to collaborate in a team. Although Unity Collaborate tends to make it easier to share changes in the same scenes, it is severely limited by lack of features compared to git. 


### Difficult to use Unity with git

Version controlling became quite difficult when it came to merging prefabs and scenes. The latter was the most difficult, as it was practically impossible to merge two scenes without breaking the game. The files were also practically unreadable during code review, so it was difficult to depict what specific changes were made to the scene. Instead of dealing with the merge conflicts within git, we created copies of prefabs and scenes and worked on the copies instead. These copies were later combined manually within Unity in a meeting.


### Closed source

Most of the source code for Unity is not disclosed unless you pay for the commercial source code license. They have released the C# portion of the engine and editor as reference only which can help gain an understanding of how you can use some of the functionality, but it’s not possible to modify or build on the existing code without the commercial license. 


### 2D Tilemap

The Tilemap system in Unity is used for easily placing sprites in a tile-based map, where each tile is a fixed size, which was 16x16 pixel size we primarily utilized. Although this can be convenient for placing a lot of sprites at the same time, the Unity Tilemap is known for having problems. One of the problems we encountered was aliasing. By default, Unity will not render the tile map pixel perfect and it can cause some render issues. In order to fix this issue we had to disable or nullify any compression, set filter mode on Point, disable any anti-aliasing on our project, and envelop our sprites into a sprite atlas to make it even clearer to Unity that it should not try and alter the rendering of our pixelated images. 


# Process and communication systems during development


## Work Process

During the development process we had two regular meetings each week. These were mainly for showing each other what we had done and assigning new tasks to each team member. In one of the meetings we normally merged new features and had code peer reviews, and in the other meeting we planned and assigned new tasks and started working on them. During the rest of the week we would sometimes do smaller impromptu meetings if we felt the need to collaborate on something. Otherwise the group members would work independently or in smaller groups of two or three for most of the week.

This work process worked for us, and allowed each team member to decide themselves when they wanted to work. This was smart because most of our team members have different schedules and arranging more meetings would be quite challenging.

During the development process we utilized the mvp playtesting to improve our game. Although, all of the feedback from the MVP playtesting were problems we already knew, so this did not help us a lot. However, we did get some positive feedback as well. This way we knew what we had done well.


## Roles

We did not have any designated roles, but people often ended up working within certain fields. For instance, Michael made all the map sprites, map layouts, and maps in unity. While the rest of the group did work where it was needed. Since we wanted all the group members to have as much learning outcome as possible, we did not assign specific roles to people because this would reduce each person’s overall learning outcome within the field of game programming.


## Communication systems and file organizing

For arranging and holding the meetings we used Discord. This was the easiest considering we already had a discord server for the course and our own text and voice channels.

We used Google Drive for file sharing of all our documents, because it made organizing easy. We would use Google Drive for meeting notes, the game design document and game level sketches.


## Coding style

A general mark of code quality is consistency, which leads to predictability. Code consistency includes standard naming conventions, consistent indenting and spacing, and standardized architecture.

In our code, we agreed that all names should follow camel casing, classes and functions should begin with a capital letter, and private fields would start with an underscore. We also decided that the opening curly brackets should be on the same line as a method or conditional statement. Having opening curly brackets start on the same line made it possible to see more code at once and save vertical space.

We agreed on a standard architecture where constant fields were declared at the very top of the class. Right after came field declarations, then methods. Within these groups, the order of access ranged from least accessible to most. However, we had no public fields to ensure efficient encapsulation. We used the style cop GitHub repository as a reference when deciding on architecture.[^1]


# Ideas that were not implemented

During the game development process there were many ideas floating around that we did not implement. Most of these were not implemented due to time constraints.


## Multiplayer gamemode

At the start we were discussing having a multiplayer mode where you could fight other players. We also discussed having the entire game playable in multiplayer mode. We decided to not do any of these and focus on the single player aspect.


## Speedrun-Parkour gamemode

We discussed making an optional parkour/speedrun map where there were no enemies and you tried to get through a very difficult map where you had to use all the movement abilities in the game. This was not prioritized to focus on more important matters.


## More Hidden secret rooms

The hidden rooms were originally intended to not be visible before you had entered them, which would have made it a bit harder to find them. We ended up not doing this because we did not think this was a high priority issue, and instead chose to work on more important issues.


## Player transformation complexity

The combat system was supposed to be more complex. We wished to develop a combat system where each transformation had at least one special attack that you could use in sequence with the other transformation-dependent special attacks to make for a more unique and dynamic playstyle. Without prior experience in game development, we quickly realized that this was out of reach and had to be down-prioritized to make sure that we reached an MVP.

The transformation states were also supposed to have at least one set of unique movement abilities. The owl transformation, for example, was supposed to have a gliding ability instead of being able to jump more than the other transformations. Unique movement abilities were not prioritized to ensure that we reached an MVP, which resulted in the fact that we never had time to develop it.


## Puzzle elements

There were supposed to be puzzle elements in the final product, but this was also not prioritized due to time constraints. The original idea was to have puzzles where the player had to use specific animal transformations to solve them. Because of the animal transformations not being fully implemented as planned we decided to not make any puzzle elements.


## Sprites and animations

Unity's asset store contains a lot of art, but unfortunately, not everything is free. We could not find any free sprites for our 2D pixelated polar bear boss nor our 2D pixelated desert cat boss. Neither could we find animations and sprites for each player transformation and the transforming animations between each transformation. This meant that we had to improvise.

It turns out that creating a sprite for every animation state is a very time-consuming job that would halter the progress of more prioritized aspects of our game. Therefore these sprites and animations look very underdeveloped in our final game. The different player transformations, for example, just change the color of the player sprite. And the bosses do not have any animations.


## Transformation unlocking after each boss

Player transformations were supposed to be unlockables that you would be granted after killing a boss. We never finished all the levels, so keeping the transformations locked would serve no purpose as you would never be able to unlock all of them. Therefore the player can use all transformations from the beginning. Though utilizing the owl transformation with seemingly unlimited jumps is a bit overpowered.


# Use of version control systems, ticket tracking, branching, version control


## Git

We used git for version control. In the beginning, we tried out the Unity Collaboration tool, but decided that it was easier and more reliable to use Git (and only Git) for version control. In hindsight, we believe this was the right choice because we knew Git from before, and Unity Collaborate would not be able to offer us something other than easier scene merging. Although this would have been useful, it does not add up for all the extra features you get by using Git. 


### Issues

In the beginning we used a planning document on Google Docs instead of Git issues, but later changed this to track all our progress using github issues because it was nice to have this in our repository. It is appropriately more organized in github and allows us to easily assign or label issues to be concluded. It also helped us to easily receive feedback from students who have played our game and notice problems or issues that we could have overseen.


### Workflow and Branching

We tried to utilize Git Flow as our workflow[^2], meaning we have a _master_ branch that is supposed to be stable, a _dev_ branch for development (unstable), and feature branches for each feature. When finished with a feature, we opened a pull request on Github and had to get it reviewed by at least one other member before being allowed to merge it into the development branch. We used rebasing instead of merging when combining two branches. 

In order to merge from the development branch to the master branch, four other group members would have to approve it and should only be done for major changes (releases). The MVP and finished game are examples of major releases. 

We utilized hotfix branches which fork directly off the master branch and are merged with both the master and dev branch. This allows fixes to be made to the master branch without merging from dev which is reserved for releases.


# Final thoughts

While the final product may not be exactly as envisioned, and some ideas were not implemented, we feel that the final product is of a quality that is expected considering the time frame. The group has had good teamwork throughout the entire project, and we’ve had no problems in the group. We have learned a lot about game development. From game design concepts about what psychologically makes a game fun, to the technical side of coding and using Unity, it has been a very educational process.


<!-- Footnotes themselves at the bottom. -->
## Notes

[^1]:
     [https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1201.md](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1201.md) 

[^2]:
     [https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workfl
