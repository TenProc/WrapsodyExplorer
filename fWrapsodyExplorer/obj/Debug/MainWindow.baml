   M S B A M L   `   `   `  ����L  HfWrapsodyExplorer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:fWrapsodyExplorerfWrapsodyExplorer  5   	   G(clr-namespace:fWrapsodyExplorer.ControlsfWrapsodyExplorer.Controls  5   	   "    fWrapsodyExplorer.MainWindow   5   	   Y UXceed.Wpf.DataGrid, Version=2.9.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f45xcdg*http://schemas.xceed.com/wpf/xaml/datagrid  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   1localclr-namespace:fWrapsodyExplorer     5   	   =controls(clr-namespace:fWrapsodyExplorer.Controls     5   	   X TXceed.Wpf.Toolkit, Version=2.9.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � AllowsTransparency  .   � Title$ Wrapsody Explorer��5   	   $	��778��6#   $
��1300��60    � Icon$ wrapsody.PNG��5	   	    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ ScrollViewer.xaml?�6%   �� 5      $ Styles.UniformGrid.xaml?�6%   %    5      .��5      � $��	30,30,0,0q�6    � ColumnDefinitions 5   
   �� 5   
   $	��260 �6   �� 5      $��1 �6   �� 5      $��* �6   �� 5      $	��100 �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      .+�5"   
   R� -   5         R�Name$ 	_treeView��5"      $��0��5#      $��1��5$      $��0q�5%      ��5'      �� *Tree+ 5(      	 �� 
TargetType !  LiveExplorerScrollViewerStyle
 �� BasedOn��%4   '��       7   �� 5)      "	 ��6   8
   5*      5+      5,      �� 5-   
   $��1��6   $��1��5.      $��0,0,0,30q�5/      $��1,0,0,0q�50      
�������51      �� 52   
   -   5         ��Name$ SyncDocListView��55      $��2��52      $��1��53      $��	0,0,30,30q�54      55      �� 56   
   -   5         ��Name$ SyncDocListBox��59      $��2��56      $��1��57      $��	0,0,30,30q�58        LBX_UNIFORMGRID#���  5:      �� 5;   
   -   5         ��Name$ button��5;      $��Button��6!   $��2��62   $
��Left=�6B   $��846,637,-886,0q�6]   $��1��6u   $	��Top=�6�   $��75��6�   6�   5<      5=      