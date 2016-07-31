using System;
using System.Collections.Generic;
using Foundation;
using NYTPhotoViewer;
using UIKit;

namespace Example
{
	public partial class ViewController : UIViewController
	{

		#region Declaration

		/// <summary>
		/// The photos.
		/// </summary>
		List<NYTPhoto> photos;

		/// <summary>
		/// The photos list.
		/// </summary>
		List<string> captionSummary;

		/// <summary>
		/// The index of the caption summary.
		/// </summary>
		int captionSummaryIndex;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Example.ViewController"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
		#endregion

		#region View Controller Override Methods
		/// <summary>
		/// Views the did load.
		/// </summary>
		/// <returns>The did load.</returns>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			///caption summary list
			captionSummary = new List<string>();
			captionSummary.Add("photo with custom everything");
			captionSummary.Add("photo with long caption. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum maximus laoreet vehicula. Maecenas elit quam, pellentesque at tempor vel, tempus non sem. ");

			//default value 0
			captionSummaryIndex = 0;
				
			//UIButton
			UIButton btn = new UIButton()
			{
				Frame = new CoreGraphics.CGRect(100, 100, 200, 200)
			};
			View.Add(btn);
			btn.Center = View.Center;
			btn.SetBackgroundImage(UIImage.FromFile("cartoon1.jpg"), UIControlState.Normal);
			btn.TouchUpInside += btnTouchUpInside;

		}
		#endregion

		#region UIButton Handler

		/// <summary>
		/// Buttons the touch up inside.
		/// </summary>
		/// <returns>The touch up inside.</returns>
		void btnTouchUpInside(object sender, EventArgs e)
		{
			//NYTPhotos list
			photos = new List<NYTPhoto>();

			var attrString = new NSAttributedString("The text",
				strikethroughStyle: NSUnderlineStyle.Single);
			
			for (int i = 1; i < 31; i++)
			{
				if (i == 2)
					captionSummaryIndex = 1;
				else
					captionSummaryIndex = 0;

				var CaptionTitleAttrString = new NSAttributedString(i.ToString(),
				                                                    foregroundColor: UIColor.White, font: UIFont.BoldSystemFontOfSize(17));
				var CaptionSummaryAttrString = new NSAttributedString(captionSummary[captionSummaryIndex].ToString(),
				                                                      foregroundColor: UIColor.LightGray, font:UIFont.PreferredBody);
				var CaptionCreditAttrString = new NSAttributedString("NYT Xamarin iOS Binding Photo Credit: Jigar Maheshwari",
				                                                     foregroundColor: UIColor.Gray, font: UIFont.PreferredCaption1);
				
				string imageName = "cartoon" + i + ".jpg";
				photos.Add(new NYTPhoto()
				{
					Image = UIImage.FromFile(imageName),
					AttributedCaptionTitle = CaptionTitleAttrString,
					AttributedCaptionSummary = CaptionSummaryAttrString,
					AttributedCaptionCredit = CaptionCreditAttrString
				});
			}

			NYTPhoto initial = new NYTPhoto()
			{
				Image = UIImage.FromFile("cartoon1.jpg"),
				AttributedCaptionTitle = attrString,
				AttributedCaptionSummary = attrString,
				AttributedCaptionCredit = attrString
			};

			NYTPhotosViewController controller = new NYTPhotosViewController(photos.ToArray(), initial);
			controller.Delegate = new NYTPhotosControllerDelegate();
			this.PresentViewController(controller, true, null);
		}
		#endregion

		#region NYTPhotosViewControllerDelegate

		/// <summary>
		/// NYTP hotos controller delegate.
		/// </summary>
		public class NYTPhotosControllerDelegate : NYTPhotosViewControllerDelegate
		{
			#region Constructor

			/// <summary>
			/// Initializes a new instance of the <see cref="T:Example.ViewController.NYTPhotosControllerDelegate"/> class.
			/// </summary>
			public NYTPhotosControllerDelegate()
			{
			}

			#endregion

			#region Override Methods

			/// <summary>
			/// Photoses the view controller maximum zoom scale for photo.
			/// </summary>
			/// <returns>The view controller maximum zoom scale for photo.</returns>
			/// <param name="photosViewController">Photos view controller.</param>
			/// <param name="photo">Photo.</param>
			public override nfloat PhotosViewControllerMaximumZoomScaleForPhoto(NYTPhotosViewController photosViewController, NYTPhoto photo)
			{
				return 5.0f;
			}
			#endregion
		}

		#endregion

		#region DidReceiveMemoryWarning

		/// <summary>
		/// Dids the receive memory warning.
		/// </summary>
		/// <returns>The receive memory warning.</returns>
		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
		#endregion
	}
}

