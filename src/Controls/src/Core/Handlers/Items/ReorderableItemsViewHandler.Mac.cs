using System;

namespace Microsoft.Maui.Controls.Handlers.Items
{
	public partial class ReorderableItemsViewHandler<TItemsView> : GroupableItemsViewHandler<TItemsView> where TItemsView : ReorderableItemsView
	{
		protected override AppKit.NSView CreatePlatformView()
		{
			throw new NotImplementedException();
		}

		public static void MapCanReorderItems(ReorderableItemsViewHandler<TItemsView> handler, ReorderableItemsView itemsView)
		{
		}
	}
}
