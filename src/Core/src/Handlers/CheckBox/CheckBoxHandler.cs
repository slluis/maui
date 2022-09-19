﻿#nullable enable
#if __IOS__ || MACCATALYST
using PlatformView = Microsoft.Maui.Platform.MauiCheckBox;
#elif __ANDROID__
using PlatformView = AndroidX.AppCompat.Widget.AppCompatCheckBox;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.Controls.CheckBox;
#elif TIZEN
using PlatformView = Tizen.UIExtensions.NUI.GraphicsView.CheckBox;
#elif (NETSTANDARD || !PLATFORM)
using PlatformView = System.Object;
#endif

namespace Microsoft.Maui.Handlers
{
	public partial class CheckBoxHandler : ICheckBoxHandler
	{
		public static IPropertyMapper<ICheckBox, ICheckBoxHandler> Mapper = new PropertyMapper<ICheckBox, ICheckBoxHandler>(ViewHandler.ViewMapper)
		{
#if MONOANDROID
			[nameof(ICheckBox.Background)] = MapBackground,
#endif
			[nameof(ICheckBox.IsChecked)] = MapIsChecked,
			[nameof(ICheckBox.Foreground)] = MapForeground,
		};

		public static CommandMapper<ICheckBox, CheckBoxHandler> CommandMapper = new(ViewCommandMapper)
		{
		};

		public CheckBoxHandler() : base(Mapper)
		{

		}

		public CheckBoxHandler(IPropertyMapper mapper) : base(mapper ?? Mapper)
		{

		}

		ICheckBox ICheckBoxHandler.VirtualView => VirtualView;

		PlatformView ICheckBoxHandler.PlatformView => PlatformView;
	}
}