using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using AppKit;
using Foundation;

namespace Microsoft.AspNetCore.Components.WebView.Maui
{
	/// <summary>
	/// A minimal implementation of an IFileProvider to be used by the BlazorWebView and WebViewManager types.
	/// </summary>
	internal sealed class MacOSMauiAssetFileProvider : IFileProvider
	{
		private readonly string _bundleRootDir;

		public MacOSMauiAssetFileProvider(string contentRootDir)
		{
			_bundleRootDir = Path.Combine(NSBundle.MainBundle.ResourcePath, contentRootDir);
		}

		public IDirectoryContents GetDirectoryContents(string subpath)
			=> new MacOSMauiAssetDirectoryContents(Path.Combine(_bundleRootDir, subpath));

		public IFileInfo GetFileInfo(string subpath)
			=> new MacOSMauiAssetFileInfo(Path.Combine(_bundleRootDir, subpath));

		public IChangeToken? Watch(string filter)
			=> null;

		private sealed class MacOSMauiAssetFileInfo : IFileInfo
		{
			private readonly string _filePath;

			public MacOSMauiAssetFileInfo(string filePath)
			{
				_filePath = filePath;

				Name = Path.GetFileName(_filePath);

				var fileInfo = new FileInfo(_filePath);
				Exists = fileInfo.Exists;
				Length = Exists ? fileInfo.Length : -1;
			}

			public bool Exists { get; }
			public long Length { get; }
			public string PhysicalPath { get; } = null!;
			public string Name { get; }
			public DateTimeOffset LastModified { get; } = DateTimeOffset.FromUnixTimeSeconds(0);
			public bool IsDirectory => false;

			public Stream CreateReadStream()
				=> File.OpenRead(_filePath);
		}

		// This is never used by BlazorWebView or WebViewManager
		private sealed class MacOSMauiAssetDirectoryContents : IDirectoryContents
		{
			public MacOSMauiAssetDirectoryContents(string filePath)
			{
			}

			public bool Exists => false;

			public IEnumerator<IFileInfo> GetEnumerator()
				=> throw new NotImplementedException();

			IEnumerator IEnumerable.GetEnumerator()
				=> throw new NotImplementedException();
		}
	}
}
