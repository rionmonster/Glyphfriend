![Glyphfriend](https://rionscode.files.wordpress.com/2015/01/glyphfriend-logo-e1420492452632.png)
===========

[![Build status](https://ci.appveyor.com/api/projects/status/i5vgnjkxwjq8shh3?svg=true)](https://ci.appveyor.com/project/Rionmonster/glyphfriend)

Glyphfriend is a Visual Studio 2015 extension to enhance the existing Intellisense to display preview glyphs for many of the common glyph-based font libraries like Font Awesome, Foundation, IonIcons and more.

## Supported Glyphs and Libraries

* [Bootstrap Glyphicons](http://getbootstrap.com/components/#glyphicons)
* [Entypo](http://www.entypo.com)
* [Font Awesome](http://fortawesome.github.io/Font-Awesome/)
* [Foundation](http://foundation.zurb.com/)
* [Ionicons](http://ionicons.com/) 
* [IcoMoon](https://icomoon.io/)
* [Material Design](https://materialdesignicons.com/)
* [MetroUI](https://metroui.org.ua/)
* [Octicons](https://octicons.github.com/)

Glyphfriend now also supports [Emoji emoticons](http://www.emoji-cheat-sheet.com/) within Markdown files, for those of you that enjoy using Visual Studio as a Markdown 
editor.

## Getting Started

You can [download and install Glyphfriend from the Visual Studio Gallery here](https://visualstudiogallery.msdn.microsoft.com/5fd24afb-b3b2-4cec-9b03-1cfcec6123aa). After installing the extension,
simply add any of the applicable libraries above to your project. Glyphfriend should detect all of the appropriate classes within the CSS files and apply the appropriate glyphs within the Intellisense
drop-down list when you start typing :

![Glyphfriend in Action](https://rionscode.files.wordpress.com/2015/01/gif-friend.gif)

Additionally, if you are in a Markdown file, you can simply use the `:` character to trigger the available Emoji-Intellisense as seen below :

![Glyphfriend Emojis in Action](https://rionscode.files.wordpress.com/2015/12/glyphfriend_emoji_support.gif)

If you want to see Glyphfriend in action and don't feel like building your own project and referencing these libraries, you can check out the [Glyphfriend Testing repository](https://github.com/Rionmonster/Glyphfriend.Testing),
which should have the latest version of all of the currently supported libraries.

## Missing Something?

Pull Requests are openly accepted and encouraged. 

The libraries that were chosen were just some of the more common ones that I had come across, but I am sure that I left quite a few out. If you find that one of your favorites is missing, you can either report it within the [Issues](https://github.com/Rionmonster/Glyphfriend/issues) area or 
just clone this repository, make a fork and add your changes (and then just make a pull request when you are all done).

## Thanks

I just want to give a big thanks to [Mads Kristensen](https://github.com/madskristensen) for all of his assistance with this project (and getting me interested in Visual Studio Extension development).