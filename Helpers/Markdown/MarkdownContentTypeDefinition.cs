using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;

namespace Glyphfriend.Helpers.Markdown
{
    public class MarkdownContentTypeDefinition
    {
        public const string MarkdownContentType = "markdown";

        [Export(typeof(ContentTypeDefinition))]
        [Name(MarkdownContentType)]
        [BaseDefinition("htmlx")]
        public ContentTypeDefinition IMarkdownContentType { get; set; }

        // Removed due to conflict with MarkdownEditor extension (https://github.com/madskristensen/MarkdownEditor)

        //[Export(typeof(FileExtensionToContentTypeDefinition))]
        //[ContentType(MarkdownContentType)]
        //[FileExtension(".md")]
        //public FileExtensionToContentTypeDefinition MdFileExtension { get; set; }

        [Export(typeof(FileExtensionToContentTypeDefinition))]
        [ContentType(MarkdownContentType)]
        [FileExtension(".mdown")]
        public FileExtensionToContentTypeDefinition MDownFileExtension { get; set; }

        [Export(typeof(FileExtensionToContentTypeDefinition))]
        [ContentType(MarkdownContentType)]
        [FileExtension(".markdown")]
        public FileExtensionToContentTypeDefinition MarkdownFileExtension { get; set; }

        [Export(typeof(FileExtensionToContentTypeDefinition))]
        [ContentType(MarkdownContentType)]
        [FileExtension(".mkd")]
        public FileExtensionToContentTypeDefinition MkdFileExtension { get; set; }

        [Export(typeof(FileExtensionToContentTypeDefinition))]
        [ContentType(MarkdownContentType)]
        [FileExtension(".mkdn")]
        public FileExtensionToContentTypeDefinition MkdnFileExtension { get; set; }

        [Export(typeof(FileExtensionToContentTypeDefinition))]
        [ContentType(MarkdownContentType)]
        [FileExtension(".mdwn")]
        public FileExtensionToContentTypeDefinition MdwnFileExtension { get; set; }

        [Export(typeof(FileExtensionToContentTypeDefinition))]
        [ContentType(MarkdownContentType)]
        [FileExtension(".mmd")]
        public FileExtensionToContentTypeDefinition MmdFileExtension { get; set; }
    }
}
