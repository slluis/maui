#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using AppKit;

namespace Microsoft.Maui
{
	public static class ImageViewExtensions
	{
		public static void Clear(this NSImageView imageView)
		{
			imageView.Image = null;
		}

		public static void UpdateAspect(this NSImageView imageView, IImage image)
		{
//			imageView.ContentMode = image.Aspect.ToUIViewContentMode();
		}

		public static void UpdateIsAnimationPlaying(this NSImageView imageView, IImageSourcePart image)
		{
			if (image.IsAnimationPlaying)
			{
//				if (!imageView.IsAnimating)
//					imageView.StartAnimating();
			}
			else
			{
//				if (imageView.IsAnimating)
//					imageView.StopAnimating();
			}
		}

		public static void UpdateSource(this NSImageView imageView, NSImage? NSImage, IImageSourcePart image)
		{
			imageView.Image = NSImage;
			imageView.UpdateIsAnimationPlaying(image);
		}

		public static Task<IImageSourceServiceResult<NSImage>?> UpdateSourceAsync(
			this NSImageView imageView,
			IImageSourcePart image,
			IImageSourceServiceProvider services,
			CancellationToken cancellationToken = default)
		{
			imageView.Clear();
			return image.UpdateSourceAsync(imageView, services, (NSImage) =>
			{
				imageView.Image = NSImage;

			}, cancellationToken);
		}

		internal static Task<IImageSourceServiceResult<NSImage>?> UpdateSourceAsync(
			this IImageSourcePart image,
			NSView destinationContext,
			IImageSourceServiceProvider services,
			Action<NSImage?> setImage,
			CancellationToken cancellationToken = default)
		{
			throw new NotSupportedException();
/*			image.UpdateIsLoading(false);

			var imageSource = image.Source;
			if (imageSource == null)
				return null;

			var events = image as IImageSourcePartEvents;

			events?.LoadingStarted();
			image.UpdateIsLoading(true);

			try
			{
				var service = services.GetRequiredImageSourceService(imageSource);

				var scale = destinationContext.Window?.Screen?.Scale ?? 1;
				var result = await service.GetImageAsync(imageSource, (float)scale, cancellationToken);
				var NSImage = result?.Value;

				var applied = !cancellationToken.IsCancellationRequested && imageSource == image.Source;

				// only set the image if we are still on the same one
				if (applied)
				{
					setImage.Invoke(NSImage);
					if (destinationContext is NSImageView imageView)
						imageView.UpdateIsAnimationPlaying(image);

				}

				events?.LoadingCompleted(applied);

				return result;
			}
			catch (OperationCanceledException)
			{
				// no-op
				events?.LoadingCompleted(false);
			}
			catch (Exception ex)
			{
				events?.LoadingFailed(ex);
			}
			finally
			{
				// only mark as finished if we are still working on the same image
				if (imageSource == image.Source)
				{
					image.UpdateIsLoading(false);
				}
			}

			return null;*/
		}
	}
}