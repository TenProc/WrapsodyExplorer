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
using System.Data.SQLite;
using System.Data.Linq;
using fWrapsodyExplorer.Assets;

namespace fWrapsodyExplorer
{

 
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
	{
        ResourceDictionary controlDictionary = new ResourceDictionary() { Source = new Uri("ScrollViewer.xaml", UriKind.RelativeOrAbsolute) };
        ResourceDictionary resourceDictionary = new ResourceDictionary() { Source = SetLanguageDictionary() };
    

        static WrapDocumentInfo wrapDocumentInfo = new WrapDocumentInfo();
        
        public MainWindow()
		{
            DataContext = new MainWindowViewModel();
			InitializeComponent();
            
			InitializeControls();
		}

		static private Uri SetLanguageDictionary()
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

            return dict.Source;
		}

		private void InitializeControls()
		{
            SyncDocListBox.Visibility = Visibility.Collapsed;

            var gridView = new GridView();
            this.SyncDocListView.View = gridView;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = resourceDictionary["file name"].ToString(),
                DisplayMemberBinding = new Binding("fName"),
            });
            gridView.Columns[0].Width = 200;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = resourceDictionary["version info"].ToString(),
                DisplayMemberBinding = new Binding("verInfo")
            });
            gridView.Columns[1].Width = 100;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = resourceDictionary["revision status"].ToString(),
                DisplayMemberBinding = new Binding("revStatus")
            });
            gridView.Columns[2].Width = 100;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = resourceDictionary["file path"].ToString(),
                DisplayMemberBinding = new Binding("fPath")
            });
            gridView.Columns[3].Width = 300;

        }

		private void SampleData()
		{


            //wrapDocumentInfo.ClearAllSyncInfos();
            SyncDocListView.Items.Clear();
            SyncDocListBox.Items.Clear();


            var connection = new SQLiteConnection(@"Data Source=" + Environment.GetEnvironmentVariable("LocalAppData") + @"\Fasoo\f_wsdData.dll");
            var context = new DataContext(connection);

            var syncSubItems = context.GetTable<DBTableSyncSub>();
            foreach (DBTableSyncSub syncSubItem in syncSubItems)
            {
                SyncDocListBox.Items.Add(new WrapsodyListViewItem
                {
                    fName = System.IO.Path.GetFileName(syncSubItem.filePath),
                    fPath = syncSubItem.filePath,
                    verInfo = String.Format("{0} / {1}", 1, 10),
                    fImage = "excel.JPG",
                    revStatus = @"리비전 가능"
                 });

               
                SyncDocListView.Items.Add(new WrapsodyListViewItem
                {
                    fName = System.IO.Path.GetFileName(syncSubItem.filePath),
                    fPath = syncSubItem.filePath,srg
                    verInfo = String.Format("{0} / {1}", 1, 10),
                    fImage = "excel.jpg",
                    revStatus = @"리비전 가능"
                });
            }

            return;
            List<String> synclist = new List<String>();
			//wrapDocumentInfo.GetSyncList(ref synclist);

			foreach(var syncPath in synclist)
			{
				//wrapDocumentInfo.Init(syncPath.ToString());
			}

			int ret = 1;
			while (ret == 1)
			{
				//ret = wrapDocumentInfo.UpdateAllSyncInfos(50);
			}

			int CurrentRevision = new int();
			int LatestRevision = new int();
			SYNC_USER_INFO userInfo = new SYNC_USER_INFO();

			foreach (var filepath in synclist)
			{
				//ret = wrapDocumentInfo.GetSyncInfo(filepath.ToString(), ref CurrentRevision, ref LatestRevision, ref userInfo);
				if (ret == 0)
				{
					string _filepath = filepath.ToString();
					string _revision = String.Format("{0}/{1}", CurrentRevision, LatestRevision);
					string _statues = String.IsNullOrEmpty(userInfo.userId) ? "" : String.Format("{0}({1})", userInfo.userName, userInfo.userId);
					this.SyncDocListView.Items.Add(new WrapsodyListViewItem
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
            this.UpdateSelectedView(e.NewValue as WrapsodyTreeViewItem);
        }

        private void UpdateSelectedView( WrapsodyTreeViewItem treeViewItem)
        {
            SampleData();
        }

        private void OnClickedChangeMode(object sender, RoutedEventArgs e)
        {
            object o = _treeView.SelectedItem;
            WrapsodyTreeViewItem _wrapsodytreeitem = o as WrapsodyTreeViewItem;
            SyncDocListView.Visibility = Visibility.Collapsed;
            SyncDocListBox.Visibility = Visibility.Visible;
        }
    }

    class MainWindowViewModel
	{
        private ResourceDictionary ControlDictionary = new ResourceDictionary();
        private ResourceDictionary ResourceDictionary = new ResourceDictionary();

        public ObservableCollection<WrapsodyTreeViewItem> Tree { get; set; }

        public MainWindowViewModel()
		{

            ControlDictionary.Source = new Uri("ScrollViewer.xaml", UriKind.RelativeOrAbsolute);
            ResourceDictionary.Source = new Uri("..\\Resources\\StringResources.ko.xaml", UriKind.Relative);


            Tree = new ObservableCollection<WrapsodyTreeViewItem>();
			GetLoadedTreeRoot();
        }


        /// <summary>
        /// 왼쪽 트리아이템 뷰 초기화
        /// </summary>
        private void GetLoadedTreeRoot()
		{
		
			
			var _treeViewHeaderStyle = ControlDictionary["TreeViewHeaderStyle"] as Style;
			var _treeViewItemStyle = ControlDictionary["TreeViewItemStyle"] as Style;
			var _treeViewSubHeaderStyle = ControlDictionary["TreeViewSubHeaderStyle"] as Style;

			WrapsodyTreeViewItem parent = new WrapsodyTreeViewItem()
            { Header = ResourceDictionary["docList"].ToString(), Style = _treeViewHeaderStyle, ItemContainerStyle = _treeViewItemStyle};
			WrapsodyTreeViewItem parent1 = new WrapsodyTreeViewItem() { Header = ResourceDictionary["Manage group"].ToString(), Style = _treeViewHeaderStyle, ItemContainerStyle = _treeViewItemStyle };
			WrapsodyTreeViewItem parent2 = new WrapsodyTreeViewItem() { Header = ResourceDictionary["Option"].ToString(), Style = _treeViewHeaderStyle, ItemContainerStyle = _treeViewItemStyle };
			List<WrapsodyTreeViewItem> listChildItem = new List<WrapsodyTreeViewItem>();
            var temp = new ListView();
			listChildItem.Add(new WrapsodyTreeViewItem() { Header = ResourceDictionary["All docs"].ToString()});
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
