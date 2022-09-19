﻿#nullable enable
#if __IOS__ || MACCATALYST
using PlatformView = Microsoft.Maui.Platform.ContentView;
#elif MONOANDROID
using PlatformView = Microsoft.Maui.Platform.ContentViewGroup;
#elif WINDOWS
using PlatformView = Microsoft.Maui.Platform.ContentPanel;
#elif TIZEN
using PlatformView = Microsoft.Maui.Platform.ContentViewGroup;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID && !TIZEN)
using PlatformView = System.Object;
#endif

namespace Microsoft.Maui.Handlers
{
	public partial class ContentViewHandler : IContentViewHandler
	{
		public static IPropertyMapper<IContentView, IContentViewHandler> Mapper =
			new PropertyMapper<IContentView, IContentViewHandler>(ViewMapper)
			{
				[nameof(IContentView.Content)] = MapContent,
#if TIZEN
				[nameof(IContentView.Background)] = MapBackground,
#endif
			};

		public static CommandMapper<IContentView, IContentViewHandler> CommandMapper =
			new(ViewCommandMapper);

		public ContentViewHandler() : base(Mapper, CommandMapper)
		{

		}

		protected ContentViewHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null)
			: base(mapper, commandMapper ?? ViewCommandMapper)
		{
		}

		public ContentViewHandler(IPropertyMapper? mapper = null) : base(mapper ?? Mapper)
		{
		}

		IContentView IContentViewHandler.VirtualView => VirtualView;

		PlatformView IContentViewHandler.PlatformView => PlatformView;
	}
}
