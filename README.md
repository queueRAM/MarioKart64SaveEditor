# Mario Kart 64 EEPROM Save Editor
Tool for editing the Mario Kart 64 EEPROM save file

![ScreenShot](http://i.imgur.com/hxDp9rn.png)

## Current Features
* Modify course and lap record times
   * Automatically sorts times and updates 1st and Lap copies
* Set trophies awarded for all cups and types
* Configure audio option
* Updates all checksums on saving

## Usage
Open the .eep or .sav file that your emulate created for you. Storage directory varies by emulator:
 * Project 64 1.6: %LOCALAPPDATA%\VirtualStore\Program Files (x86)\Project64 1.6\Save\
 * mupen64plus 2.5: %APPDATA%\Mupen64Plus\save\
 * Nemu64 0.8: same directory as nemu64.exe
 * cen64: the file specified with "-eep4k" on the command line

## Changelog
0.1: Initial release
* supports editing track records, cup trophies, audio settings
* updates all 3 types of checksums used

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
