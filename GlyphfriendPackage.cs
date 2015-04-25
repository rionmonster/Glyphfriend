using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glyphfriend
{
	public sealed class GlyphfriendPackage : Package
	{
		// Store the current solution path
		private static string _currentSolutionPath;

		internal static string CurrentSolutionPath
		{
			get
			{
				if (_currentSolutionPath == null)
				{
					var dte2 = (DTE)Package.GetGlobalService(typeof(DTE));
					if (dte2 != null)
					{
						var tempPath = dte2.Solution.FullName;
						_currentSolutionPath = Path.Combine(new Uri(tempPath).Segments.Take(new Uri(tempPath).Segments.Count() - 1).ToArray());
                        _currentSolutionPath = _currentSolutionPath.Replace("%20", " "); // Handles space characters
					}
				}
				return _currentSolutionPath;
			}
		}
	}
}
