# Glyphfriend.Packager
======

This project is console application which is responsible for generating a binary file containing all of the supported glyphs for the project. Both of the extension projects, [Glyphfriend.VS2015](https://github.com/rionmonster/Glyphfriend/tree/develop/src/Glyphfriend.VS2015) and [Glyphfriend.VS2017](https://github.com/rionmonster/Glyphfriend/tree/develop/src/Glyphfriend.VS2017) have a built dependency on it, which ensures that the binary file will be available prior to those projects being built.

## What this project does?

Basically, the following steps occur when this project is run :

1. The [Glyphs](https://github.com/rionmonster/Glyphfriend/tree/develop/src/Glyphfriend.Packager/Glyphs) directory is traversed, and each image that is encountered is stored in a Dictionary, which maps the name of each of the files to it's associated contents.
2. This dictionary is then serialized via the [protobuf-net](https://github.com/mgravell/protobuf-net) library and written out to a binary file.

## How does it fit in the overall extension?

Since both of the extension projects depend on this, it is ensured to be built when those projects are. This allows us to take advantage of some of the build-related workflow for the extensions, so that the entire process looks like this :

1. One (or both) of the extensions are built, as a result this project is built prior to those as a dependency.
2. As a post-build event, the Packager executable is executed, which generates the `glyphs.bin` binary file containing all of the glyph mappings.
3. As a pre-build event for the extension projects, this binary file is copied into the appropriate location within those projects.
4. At run-time, the binary file is deserialized into memory and the glyphs are available for use.

