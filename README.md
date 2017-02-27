![Glyphfriend](https://raw.githubusercontent.com/rionmonster/Glyphfriend/develop/art/glyphfriend-full-logo.png)
===========

[![Build status](https://ci.appveyor.com/api/projects/status/r8wjl6ukwlnpvwid?svg=true)](https://ci.appveyor.com/project/rionmonster/glyphfriend)

Glyphfriend is a Visual Studio extension to enhance the existing Intellisense to display preview glyphs for many of the common glyph-based font libraries like Font Awesome, Bootstrap, and much more.

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

Glyphfriend has a separate extension for both Visual Studio 2015 and Visual Studio 2017, so to get started, you'll simply need to download the version(s) that apply to you from the Visual Studio Marketplace:

* [Download Glyphfriend 2015](https://marketplace.visualstudio.com/items?itemName=RionWilliams.Glyphfriend)
* [Download Glyphfriend 2017](https://marketplace.visualstudio.com/items?itemName=RionWilliams.Glyphfriend2017)

You can also just search for it within the **Tools > Extensions and Updates** area of Visual Studio.

After installing the extension, that's it. Glyphfriend will automatically detect when a valid HTML flavored file is opened and it will add all of the supported icons to the autocompletion within `class` attributes as seen below:

![Glyphfriend in Action](https://raw.githubusercontent.com/rionmonster/Glyphfriend/develop/art/glyphfriend-in-action.gif)

## Setting Preferences

By default, Glyphfriend has support for Bootstrap Glyphicons enabled by default, however you can easily select the libraries that you prefer to use by simply **right-clicking and selecting the preferred library from the Glyphfriend context menu in any HTML-supported file** as seen below:

![Glyphfriend Library Toggling](https://raw.githubusercontent.com/rionmonster/Glyphfriend/develop/art/glyphfriend-toggling-support.gif)

It's important to note that **library preferences are persistent across Visual Studio sessions.** Basically, you will only need to define which libraries that you want to use and they will be available the next time you open Visual Studio.

## What's In Here?

Within this repository you'll find all of the necessary projects that make Glyphfriend work, which can be described below:

* **[Glyphfriend.Core](https://github.com/rionmonster/Glyphfriend/tree/develop/src/Glyphfriend.Core)** - This project is a Shared Library that contains all of the necessary code for the extension to work. Namely the autocompletion providers and the necessary code to handle deserializing the glyphs at run-time.
* **[Glyphfriend.Packager](https://github.com/rionmonster/Glyphfriend/tree/develop/src/Glyphfriend.Packager)** - This project functions as a build-time dependency for the two extension projects and handles generating a binary file containing all of the glyph mappings (via Protobuf), which is consumed and deserialized within the Glyphfriend.Core package.
* **[Glyphfriend.VS2015](https://github.com/rionmonster/Glyphfriend/tree/develop/src/Glyphfriend.VS2015)** - This project stores all of the necessary manifest information for the Visual Studio 2015 extension, and defines all of the metadata that populates the marketplace.
* **[Glyphfriend.VS2017](https://github.com/rionmonster/Glyphfriend/tree/develop/src/Glyphfriend.VS2017)** - This project stores all of the necessary manifest information for the Visual Studio 2015 extension, and defines all of the metadata that populates the marketplace.

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