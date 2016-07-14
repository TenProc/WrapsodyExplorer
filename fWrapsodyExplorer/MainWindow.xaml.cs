using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;
using System.Threading;
using fWrapsodyExplorer.Controls;
using System.Collections.ObjectModel;

namespace fWrapsodyExplorer
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window
	{
		//public WrapFhm wrapFhm = new WrapFhm();
		//public WrapPol wrapPol = new WrapPol();
		//public WrapSynclib wrapSynclib = new WrapSynclib();
		public WrapDocumentInfo wrapDocumentInfo = new WrapDocumentInfo();
		
		public class SyncDocListItem
		{
			public string fName { get; set; }
			public string verInfo { get; set; }
			public string revStatus { get; set; }
			public string fPath { get; set; }
		}
		public MainWindow()
		{
			//_treeView.DataContext = new MainWindowViewModel();
			DataContext = new MainWindowViewModel();
			InitializeComponent();

			if(true != wrapDocumentInfo.Initialize())
			{
				MessageBox.Show("f_documentinfo initialization failed");
			}

			SetLanguageDictionary();
			InitializeControls();
		}

		private void SetLanguageDictionary()
		{
			ResourceDictionary dict = new ResourceDictionary();
			switch (Thread.CurrentThread.CurrentCulture.ToString())
			{
				case "ko-KR":
					dict.Source = new Uri("..\\Resources\\StringResources.ko.xaml", UriKind.Relative);
					break;
				case "en-US":
				default:
					dict.Source = new Uri("..\\Resources\\StringResources.en.xaml", UriKind.Relative);
					break;
			}
			this.Resources.MergedDictionaries.Add(dict);
		}

		private void InitializeControls()
		{
			var gridView = new GridView();
			this.SyncDocList.View = gridView;

			gridView.Columns.Add(new GridViewColumn
			{
				Header = FindResource("file name").ToString(),
				DisplayMemberBinding = new Binding("fName")
			});
			gridView.Columns[0].Width = 200;

			gridView.Columns.Add(new GridViewColumn
			{
				Header = FindResource("version info").ToString(),
				DisplayMemberBinding = new Binding("verInfo")
			});
			gridView.Columns[1].Width = 100;

			gridView.Columns.Add(new GridViewColumn
			{
				Header = FindResource("revision status").ToString(),
				DisplayMemberBinding = new Binding("revStatus")
			});
			gridView.Columns[2].Width = 100;

			gridView.Columns.Add(new GridViewColumn
			{
				Header = FindResource("file path").ToString(),
				DisplayMemberBinding = new Binding("fPath")
			});
			gridView.Columns[3].Width = 300;

		}

		private void SampleData()
		{
			wrapDocumentInfo.ClearAllSyncInfos();
			SyncDocList.Items.Clear();

			List<string> filepathlist = new List<string>();

			RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Wrapsody\\Sync");
			foreach (var v in key.GetSubKeyNames())
			{
				RegistryKey productKey = key.OpenSubKey(v); //Sync값
				if (productKey != null)
				{
					foreach (var value in productKey.GetSubKeyNames()) //Sync값 Date
					{
						RegistryKey SyncSubKey = productKey.OpenSubKey(value);
						string filepath = SyncSubKey.GetValue(null).ToString();
						if (File.Exists(filepath))
						{
							wrapDocumentInfo.Init(filepath);
							filepathlist.Add(filepath);
						}
					}
				}
			}

			int ret = 1;

			while (ret == 1)
			{
				ret = wrapDocumentInfo.UpdateAllSyncInfos(50);
			}

			int CurrentRevision = new int();
			int LatestRevision = new int();
			SYNC_USER_INFO userInfo = new SYNC_USER_INFO();

			foreach (var filepath in filepathlist)
			{
				ret = wrapDocumentInfo.GetSyncInfo(filepath, ref CurrentRevision, ref LatestRevision, ref userInfo);
				if (ret == 0)
				{
					string _filepath = filepath;
					string _revision = String.Format("{0}/{1}", CurrentRevision, LatestRevision);
					string _statues = String.IsNullOrEmpty(userInfo.userId) ? "" : String.Format("{0}({1})", userInfo.userName, userInfo.userId);
					this.SyncDocList.Items.Add(new SyncDocListItem
					{
						fName = _filepath,
						verInfo = _revision,
						revStatus = _statues,
						fPath = _filepath
					});
				}
			}
			
		}

		private void ButtonClickedUpdateAllSyncInfos(object sender, RoutedEventArgs e)
		{
			SampleData();
		}

		private void OnTreeViewSelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{

		}
	}

	class MainWindowViewModel
	{
		public ObservableCollection<WrapsodyTreeViewItem> Tree { get; set; }

		public MainWindowViewModel()
		{
			Tree = new ObservableCollection<WrapsodyTreeViewItem>();
			GetLoadedTreeRoot();
		}

		private void GetLoadedTreeRoot()
		{
			var ControlDictionary = new ResourceDictionary();
			var ResourceDictionary = new ResourceDictionary();
			ControlDictionary.Source = new Uri("ScrollViewer.xaml", UriKind.RelativeOrAbsolute);
			ResourceDictionary.Source = new Uri("..\\Resources\\StringResources.ko.xaml", UriKind.Relative);
			
			var _treeViewHeaderStyle = ControlDictionary["TreeViewHeaderStyle"] as Style;
			var _treeViewItemStyle = ControlDictionary["TreeViewItemStyle"] as Style;
			var _treeViewSubHeaderStyle = ControlDictionary["TreeViewSubHeaderStyle"] as Style;

			WrapsodyTreeViewItem parent = new WrapsodyTreeViewItem() { Header = ResourceDictionary["docList"].ToString(), Style = _treeViewHeaderStyle, ItemContainerStyle = _treeViewItemStyle };
			WrapsodyTreeViewItem parent1 = new WrapsodyTreeViewItem() { Header = ResourceDictionary["Manage group"].ToString(), Style = _treeViewHeaderStyle, ItemContainerStyle = _treeViewItemStyle };
			WrapsodyTreeViewItem parent2 = new WrapsodyTreeViewItem() { Header = ResourceDictionary["Option"].ToString(), Style = _treeViewHeaderStyle, ItemContainerStyle = _treeViewItemStyle };
			
			List<WrapsodyTreeViewItem> listChildItem = new List<WrapsodyTreeViewItem>();
			listChildItem.Add(new WrapsodyTreeViewItem() { Header = ResourceDictionary["All docs"].ToString() });
			listChildItem.Add(new WrapsodyTreeViewItem() { Header = ResourceDictionary["Revision availble docs"].ToString() });
			listChildItem.Add(new WrapsodyTreeViewItem() { Header = ResourceDictionary["View available docs"].ToString() });
			listChildItem.Add(new WrapsodyTreeViewItem() { Header = ResourceDictionary["Revisioning docs"].ToString() });
			listChildItem.Add(new WrapsodyTreeViewItem() { Header = ResourceDictionary["Coediting docs"].ToString() });
			listChildItem.Add(new WrapsodyTreeViewItem() { Header = ResourceDictionary["Recent docs"].ToString() });
			foreach (var childItem in listChildItem)
			{
				parent.Items.Add(childItem);
			}

			List<WrapsodyTreeViewItem> listChildItem1 = new List<WrapsodyTreeViewItem>();
			listChildItem1.Add(new WrapsodyTreeViewItem() { Header = ResourceDictionary["Favorite group"].ToString() });
			listChildItem1.Add(new WrapsodyTreeViewItem() { Header = ResourceDictionary["Tag group"].ToString() });
			listChildItem1.Add(new WrapsodyTreeViewItem() { Header = ResourceDictionary["Owner group"].ToString() });
			listChildItem1.Add(new WrapsodyTreeViewItem() { Header = ResourceDictionary["File id group"].ToString() });
			foreach (var childItem1 in listChildItem1)
			{
				parent1.Items.Add(childItem1);
			}

			Tree.Add(parent);
			Tree.Add(parent1);
			Tree.Add(parent2);
		}
	}
}
