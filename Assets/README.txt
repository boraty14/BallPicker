You can set all game settings from "GameSettings" folder
via ScriptableObjects. All settings are gathered in one
place so designers can modify them easily and don't
have to deep dive into project folders each time
they want to change something.

For example some basic settings:

Creating new level:
RightClick->Create->Game Settings->Create Level Settings
After that you can enter the settings such as
how many object is needed for this level, level object 
collection duration and level prefab of course.

Adding new level to LevelList:
In the "GameSettings" folder you can drag and drop
the new level you created to level list in the
"LevelManagerSettings" and that is all.

Changing Level Order:
In "GameSettings" folder LevelManagerSettings holds
the level order list. You can change the order from here.

Adjusting Existing Level:
You can always change needed object count, level prefab 
and wait duration of level from relative scriptable object.
If you want to change level prefab itself you can modify the
level from "Prefabs/Levels/{RelativeLevel}" as you'd like.

Adjusting Player Movement:
You can find all relative settings inside "PlayerSettings"
scriptable object inside of "GameSettings" folder.

Those primary settings and the other relative settings 
can be modified from "GameSettings" folder.

Author: Bora Ataysen

