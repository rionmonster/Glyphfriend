![Glyphfriend](https://raw.githubusercontent.com/rionmonster/Glyphfriend/develop/art/glyphfriend-full-logo.png)
===========

[![Build status](https://ci.appveyor.com/api/projects/status/r8wjl6ukwlnpvwid?svg=true)](https://ci.appveyor.com/project/rionmonster/glyphfriend)

Glyphfriend is a Visual Studio 2017 extension to enhance the existing Intellisense to display preview glyphs for many of the common glyph-based font libraries like Font Awesome, Bootstrap, and much more.

## Supported Glyphs and Libraries

* [Bootstrap Glyphicons](http://getbootstrap.com/components/#glyphicons)
* [Entypo](http://www.entypo.com)
* [Font Awesome](http://fortawesome.github.io/Font-Awesome/)
* [Foundation](http://foundation.zurb.com/)
* [Ionicons](http://ionicons.com/) 
* [Material Design](https://materialdesignicons.com/)
* [MetroUI](https://metroui.org.ua/)
* [Octicons](https://octicons.github.com/)

## Getting Started

You can [download and install Glyphfriend from the Visual Studio Gallery here](https://marketplace.visualstudio.com/items?itemName=RionWilliams.Glyphfriend2017) or simply search for it within the **Tools > Extensions and Updates** area of Visual Studio.

After installing the extension, that's it. Glyphfriend will automatically detect when a valid HTML flavored file is opened and it will add all of the supported icons to the autocompletion within `class` attributes as seen below:

![Glyphfriend in Action](https://raw.githubusercontent.com/rionmonster/Glyphfriend/develop/art/glyphfriend-in-action.gif)

If you are looking for the Visual Studio 2015 version, you can [download it here](https://marketplace.visualstudio.com/items?itemName=RionWilliams.Glyphfriend).

## Setting Preferences

By default, Glyphfriend has support for Bootstrap Glyphicons enabled by default, however you can easily select the libraries that you prefer to use by simply **right-clicking and selecting the preferred library from the Glyphfriend context menu in any HTML-supported file** as seen below:

![Glyphfriend Library Toggling](https://raw.githubusercontent.com/rionmonster/Glyphfriend/develop/art/glyphfriend-toggling-support.gif)

It's important to note that **library preferences are persistent across Visual Studio sessions.** Basically, you will only need to define which libraries that you want to use and they will be available the next time you open Visual Studio.

## Find an Issue?

If you find an issue or encounter a problem with the extension, please don't hesitate to contact me or file an issue with the [Issues](https://github.com/Rionmonster/Glyphfriend/issues) are of this repository and I'll try to address it as soon as possible.

> **NOTE: If you use Resharper, then Glyphfriend may not work properly (or at all), so read this**. 

Issues with Resharper and Glyphfriend have been widely reported (enough to warrant a spot here) and are outside of my control. Many of the APIs that Glyphfriend relies on are overridden by Resharper and thus don't allow the extension to ever be accessed by Visual Studio directly.

The following two options have been successful with other Resharper users, so I would highly recommend trying either of them:

* **Try disabling any HTML features within Resharper** - By disabling the HTML autocompletion and other features, this should hopefully allow for Glyphfriend to hook into the necessary APIs and work as expected.
* **Consider downloading the [ResharperGlyphfriend Plug-in](https://github.com/Huntk23/ResharperGlyphfriend)** - A member of the community, [Kelby Hunt (Huntk23)](https://github.com/Huntk23) wrote a Resharper plug-in port of Glyphfriend, which attempts to implement much of the functionality of this extension. Consider downloading it if the previous approach didn't work as expected.

## Missing Something?

Pull Requests are openly accepted and encouraged. 

The libraries that were chosen were just some of the more common ones that I had come across, but I am sure that I left quite a few out. If you find that one of your favorites is missing, you can either report it within the [Issues](https://github.com/Rionmonster/Glyphfriend/issues) area or 
just clone this repository, make a fork and add your changes (and then just make a pull request when you are all done).

## Thanks

I would like to give a special shout-out to several members of the community, including a few from the Visual Studio Tooling team, for their assistance in evolving, developing, and testing Glyphfriend:

* **[Justin Clarebert](https://github.com/justcla)**
* **[Alex Eyler](https://github.com/AlexEyler)**
* **[Mads Kristensen](https://github.com/madskristensen)**
* **[Allen Underwood](http://www.codingblocks.net/about#joezack)**
* **[Joe Zack](http://www.codingblocks.net/about#joezack)**

Additionally, I want to thank everyone for their feedback on the extension, new feature ideas, and all of the folks that don't hesitate to shoot an e-mail my way regarding the project.
