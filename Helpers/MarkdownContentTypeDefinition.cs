using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glyphfriend.Helpers
{
    /// <summary>
    /// Exports the Markdown content type and file extension
    /// </summary>
    public class MarkdownContentTypeDefinition
    {
        public const string MarkdownContentType = "markdown";

        /// <summary>
        /// Exports the Markdown HTML content type
        /// </summary>
        [Export(typeof(ContentTypeDefinition))]
        [Name(MarkdownContentType)]
        [BaseDefinition("htmlx")]
        public ContentTypeDefinition IMarkdownContentType { get; set; }

        [Export(typeof(FileExtensionToContentTypeDefinition))]
        [ContentType(MarkdownContentType)]
        [FileExtension(".md")]
        public FileExtensionToContentTypeDefinition MDFileExtension { get; set; }

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
