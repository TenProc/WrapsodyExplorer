   M S B A M L   `   `   `  ����L  HfWrapsodyExplorer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:fWrapsodyExplorerfWrapsodyExplorer  5   	   G(clr-namespace:fWrapsodyExplorer.ControlsfWrapsodyExplorer.Controls  5   	   "    fWrapsodyExplorer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   1localclr-namespace:fWrapsodyExplorer     5   	   =controls(clr-namespace:fWrapsodyExplorer.Controls     5   	   X TXceed.Wpf.Toolkit, Version=2.9.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � AllowsTransparency  .   � Title$ Wrapsody Explorer��5   	   $	��778��6#   $
��1300��60    � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ ScrollViewer.xaml?�6%   %    5      .��5      � $��	30,30,0,0q�6    � ColumnDefinitions 5   
   �� 5      $	��260 �6   �� 5      $
��auto �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      .+�5   
   R� -   5         R�Name$ 	_treeView��5      $��0��5       $��1��5!      $��0q�5"      ��5$      �� *Tree+ 5%       �� 
TargetType !  LiveExplorerScrollViewerStyle	 �� BasedOn��%4   '��       7   �� 5&      " ��6   8	   5'      5(      5)      �� 5+   
   -   5        $��0��5+      $��0��5,      $#��랩소디 정보 업데이트��5-      $
��Left=�5.      $	��Top=�5/      $	��168��50      51      �� 53   
   -   5        
 ��Name$
 SyncDocList��55      $��1��53      $��1��54      55      56      57      