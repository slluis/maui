using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppKit;
using Microsoft.Maui.Devices;
using MobileCoreServices;

namespace Microsoft.Maui.Storage
{
	partial class FilePickerImplementation : IFilePicker
	{
		Task<IEnumerable<FileResult>> PlatformPickAsync(PickOptions options, bool allowMultiple = false)
		{
			var openPanel = new NSOpenPanel
			{
				CanChooseFiles = true,
				AllowsMultipleSelection = allowMultiple,
				CanChooseDirectories = false
			};

			if (options.PickerTitle != null)
				openPanel.Title = options.PickerTitle;

			SetFileTypes(options, openPanel);

			var resultList = new List<FileResult>();
			var panelResult = openPanel.RunModal();
			if (panelResult == (nint)(long)NSModalResponse.OK)
			{
				foreach (var url in openPanel.Urls)
					resultList.Add(new FileResult(url.Path));
			}

			return Task.FromResult<IEnumerable<FileResult>>(resultList);
		}

		static void SetFileTypes(PickOptions options, NSOpenPanel panel)
		{
			var allowedFileTypes = new List<string>();

			if (options?.FileTypes?.Value != null)
			{
				foreach (var type in options.FileTypes.Value)
				{
					allowedFileTypes.Add(type.TrimStart('*', '.'));
				}
			}

			if(OperatingSystem.IsMacOSVersionAtLeast(11))
#pragma warning disable CA1416
				panel.AllowedContentTypes = allowedFileTypes.Select(type => UniformTypeIdentifiers.UTType.CreateFromExtension(type)).ToArray();
#pragma warning restore CA1416
			else
				panel.AllowedFileTypes = allowedFileTypes.ToArray();
		}
	}

	public partial class FilePickerFileType
	{
		static FilePickerFileType PlatformImageFileType() =>
			OperatingSystem.IsMacOSVersionAtLeast(11) ?
				new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.macOS, new string[] { UniformTypeIdentifiers.UTTypes.Png.Identifier, UniformTypeIdentifiers.UTTypes.Jpeg.Identifier, "jpeg" } }
				}):
				new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.macOS, new string[] { UTType.PNG, UTType.JPEG, "jpeg" } }
				});

		static FilePickerFileType PlatformPngFileType() =>
			OperatingSystem.IsMacOSVersionAtLeast(11) ?
				new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.macOS, new string[] { UniformTypeIdentifiers.UTTypes.Png.Identifier } }
				}):
				new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.macOS, new string[] { UTType.PNG } }
				});

		static FilePickerFileType PlatformJpegFileType() =>
			OperatingSystem.IsMacOSVersionAtLeast(11) ?
				new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.macOS, new string[] { UniformTypeIdentifiers.UTTypes.Jpeg.Identifier } }
				}) :
				new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.macOS, new string[] { UTType.JPEG } }
				});

		static FilePickerFileType PlatformVideoFileType() =>
			OperatingSystem.IsMacOSVersionAtLeast(11) ?
				new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.macOS, new string[] {
						UniformTypeIdentifiers.UTTypes.Mpeg4Movie.Identifier,
						UniformTypeIdentifiers.UTTypes.Video.Identifier,
						UniformTypeIdentifiers.UTTypes.Avi.Identifier,
						UniformTypeIdentifiers.UTTypes.AppleProtectedMpeg4Video.Identifier,
						"mp4", "m4v", "mpg", "mpeg", "mp2", "mov", "avi", "mkv", "flv", "gifv", "qt" } }
				}):
				new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.macOS, new string[] { UTType.MPEG4, UTType.Video, UTType.AVIMovie, UTType.AppleProtectedMPEG4Video, "mp4", "m4v", "mpg", "mpeg", "mp2", "mov", "avi", "mkv", "flv", "gifv", "qt" } }
				});

		static FilePickerFileType PlatformPdfFileType() =>
			OperatingSystem.IsMacOSVersionAtLeast(11) ?
				new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.macOS, new string[] { UniformTypeIdentifiers.UTTypes.Pdf.Identifier } }
				}):
				new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.macOS, new string[] { UTType.PDF } }
				});
	}
}
