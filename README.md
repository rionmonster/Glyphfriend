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

Glyphfriend has a separate extension for both Visual Studio 2015 and Visual Studio 2017, so to get started, you'll simply need to download the version(s) that apply to you from the Visual Studio Marketplace :

* [Download Glyphfriend 2015](https://marketplace.visualstudio.com/items?itemName=RionWilliams.Glyphfriend)
* [Download Glyphfriend 2017](https://marketplace.visualstudio.com/items?itemName=RionWilliams.Glyphfriend2017)

You can also just search for it within the **Tools > Extensions and Updates** area of Visual Studio.

After installing the extension, that's it. Glyphfriend will automatically detect when a valid HTML flavored file is opened and it will add all of the supported icons to the autocompletion within `class` attributes as seen below :

![Glyphfriend in Action](https://raw.githubusercontent.com/rionmonster/Glyphfriend/develop/art/glyphfriend-in-action.gif)

## What's In Here?

Within this repository you'll find all of the necessary projects that make Glyphfriend work, which can be described below :

* **[Glyphfriend.Core](https://github.com/rionmonster/Glyphfriend/tree/develop/src/Glyphfriend.Core)** - This project is a Shared Library that contains all of the necessary code for the extension to work. Namely the autocompletion providers and the necessary code to handle deserializing the glyphs at run-time.
* **[Glyphfriend.Packager](https://github.com/rionmonster/Glyphfriend/tree/develop/src/Glyphfriend.Packager)** - This project functions as a build-time dependency for the two extension projects and handles generating a binary file containing all of the glyph mappings (via Protobuf), which is consumed and deserialized within the Glyphfriend.Core package.
* **[Glyphfriend.VS2015](https://github.com/rionmonster/Glyphfriend/tree/develop/src/Glyphfriend.VS2015)** - This project stores all of the necessary manifest information for the Visual Studio 2015 extension, and defines all of the metadata that populates the marketplace.
* **[Glyphfriend.VS2017](https://github.com/rionmonster/Glyphfriend/tree/develop/src/Glyphfriend.VS2017)** - This project stores all of the necessary manifest information for the Visual Studio 2015 extension, and defines all of the metadata that populates the marketplace.

## Missing Something?

Pull Requests are openly accepted and encouraged. 

The libraries that were chosen were just some of the more common ones that I had come across, but I am sure that I left quite a few out. If you find that one of your favorites is missing, you can either report it within the [Issues](https://github.com/Rionmonster/Glyphfriend/issues) area or 
just clone this repository, make a fork and add your changes (and then just make a pull request when you are all done).

## Thanks

There are a few folks that I would like to give a special shout-out to several members of the Visual Studio Tooling team for the help and guidance in developing this project, and espeically during the migration over to Visual Studio 2017 :

* **[Mads Kristensen](https://github.com/madskristensen)**
* **[Alex Eyler](https://github.com/AlexEyler)**
* **[Justin Clarebert](https://github.com/justcla)**

Additionally, I want to thank everyone for their feedback on the extension, new feature ideas, and all of the folks that don't hesitate to shoot an e-mail my way regarding the project.
