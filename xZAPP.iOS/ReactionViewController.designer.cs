// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace xZAPP.iOS
{
	[Register ("ReactionViewController")]
	partial class ReactionViewController
	{
		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextView ReactionContent { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField ReactionEmailAddress { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField ReactionTitle { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UILabel ReactionToLabel { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UILabel RweactionContent { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UIButton Verzenden { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ReactionToLabel != null) {
				ReactionToLabel.Dispose ();
				ReactionToLabel = null;
			}

			if (Verzenden != null) {
				Verzenden.Dispose ();
				Verzenden = null;
			}

			if (ReactionTitle != null) {
				ReactionTitle.Dispose ();
				ReactionTitle = null;
			}

			if (ReactionEmailAddress != null) {
				ReactionEmailAddress.Dispose ();
				ReactionEmailAddress = null;
			}

			if (RweactionContent != null) {
				RweactionContent.Dispose ();
				RweactionContent = null;
			}

			if (ReactionContent != null) {
				ReactionContent.Dispose ();
				ReactionContent = null;
			}
		}
	}
}
