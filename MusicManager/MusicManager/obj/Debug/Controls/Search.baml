   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       MusicManager"   MusicManager.Controls.Search   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9System&clr-namespace:System;assembly=mscorlib     5      .��5	      � $	��700��6   $	��500��6   .+�5
   
   �� -   5          ��Name$  txtSearchBox��5
   A   $
��Left=�6   $��
167,30,0,0q�6-   $	��Top=�6W   $	��533��6o   6{   �� 5   
   -   5         ��Name$ 
btn_search��5      $
��Tìm��6%   $
��Left=�63   $��
591,57,0,0q�6N   $	��Top=�6b   $��99��6z   6�   �� 5   
   -   5         ��Name$	 cmb��5      $
��Left=�6    $��0,30,0,0q�6;   $	��Top=�6M   $	��162��6e   .@�5      ��Tên Bài Hát��6)   ��5      Tên Ca Sĩ��6'   ��5      
Tên Album��6'   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ datagrid��5      $
��Left=�6%   $��0,92,0,0q�6@   $	��Top=�6R   $	��408��6j   $	��700��6w      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 240 6b      Header$ Tên Bài Hát��6n   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *Title+   ClipboardContentBinding 6?   Z� 6n     5      $	 230 6g   $ Tên Ca Sĩ��6s    6%   �� *
ArtistName+ 6D   Z� 6s     5      $	 230 6f   $ 
Tên Album��6r    6%   �� *	AlbumName+ 6C   Z� 6r   5      5      5      