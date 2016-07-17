using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace fWrapsodyExplorer.Controls
{
	public class WrapsodyTreeViewItem : TreeViewItem
	{
		#region Properties

		#region IsNewFeature

		public static readonly DependencyProperty IsNewFeatureProperty = DependencyProperty.Register("IsNewFeature", typeof(bool), typeof(WrapsodyTreeViewItem), new UIPropertyMetadata(false));
		public bool IsNewFeature
		{
			get
			{
				return (bool)GetValue(IsNewFeatureProperty);
			}
			set
			{
				SetValue(IsNewFeatureProperty, value);
			}
		}

		#endregion //IsNewFeature

		#region IsPlusOnlyFeature

		public static readonly DependencyProperty IsPlusOnlyFeatureProperty = DependencyProperty.Register("IsPlusOnlyFeature", typeof(bool), typeof(WrapsodyTreeViewItem), new UIPropertyMetadata(false));
		public bool IsPlusOnlyFeature
		{
			get
			{
				return (bool)GetValue(IsPlusOnlyFeatureProperty);
			}
			set
			{
				SetValue(IsPlusOnlyFeatureProperty, value);
			}
		}

		#endregion //IsPlusOnlyFeature

		#region SampleType


		#endregion //Sample

		#endregion //Properties

		#region Methods

		//protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
		//{
		//    this.IsSelected = true;
		//	e.Handled = true;
		//}

		#endregion 

	}
}
