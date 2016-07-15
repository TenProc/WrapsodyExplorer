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

		public static readonly DependencyProperty SampleTypeProperty = DependencyProperty.Register("SampleType", typeof(Type), typeof(WrapsodyTreeViewItem), new UIPropertyMetadata(null));
		public Type SampleType
		{
			get
			{
				return (Type)GetValue(SampleTypeProperty);
			}
			set
			{
				SetValue(SampleTypeProperty, value);
			}
		}

		#endregion //Sample

		#endregion //Properties

		#region Methods

		protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
		{
			if (this.SampleType == null)
			{
				this.IsExpanded = !this.IsExpanded;
			}
			else
			{
				this.IsSelected = true;
			}
			e.Handled = true;
		}

		#endregion 

	}
}
