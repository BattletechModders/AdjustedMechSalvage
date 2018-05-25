# AdjustedMechSalvage
AdjustedMechSalvage is a BattleTech mod (using ModTek) that changes the rules that mech parts get generated.

## Requirements
** Warning: Uses the experimental BTML mod loader and ModTek that might change, come here again to check for updates **

* install [BattleTechModLoader](https://github.com/Mpstark/BattleTechModLoader/releases) using the [instructions here](https://github.com/Mpstark/BattleTechModLoader)
* install [ModTek](https://github.com/Mpstark/ModTek/releases) using the [instructions here](https://github.com/Mpstark/ModTek)

## Features
- Your own mechs now get destroyed and leave behind salvage.
- Number of salvage is now based the number of parts you destroyed.
- You get DefaultMechPartMax(number needed to assemble a mech) - DefaultMechPartMax / 5 parts for each arm or leg the target lost.
- CT lost is 0 parts

## Download
Downloads can be found on [github](https://github.com/Morphyum/AdjustedMechSalvage/releases).
    
## Settings
ownMechsForFree - bool - default false - Places the parts of the mechs you loose yourself directly into your inventory instead of making you salvage them.
    
## Install and use
- After installing BTML and ModTek, put the AdjustedMechSalvage folder into the \BATTLETECH\Mods\ folder.
- Change any settings in mods\AdjustedMechSalvage\StreamingAssets\data\simGameConstants\simGameConstants.json and settings.json if you would like to.
- Start the game
