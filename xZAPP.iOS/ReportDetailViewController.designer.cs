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
	[Register ("ReportDetailViewController")]
	partial class ReportDetailViewController
	{
		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UILabel ReportBy { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField ReportContent { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UILabel ReportDate { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UILabel ReportSubject { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ReportBy != null) {
				ReportBy.Dispose ();
				ReportBy = null;
			}

			if (ReportDate != null) {
				ReportDate.Dispose ();
				ReportDate = null;
			}

			if (ReportSubject != null) {
				ReportSubject.Dispose ();
				ReportSubject = null;
			}

			if (ReportContent != null) {
				ReportContent.Dispose ();
				ReportContent = null;
			}
		}
	}
}
