   M S B A M L   `   `   `  ����B  >loginDb, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:loginDb.ViewloginDb.View  5      7 clr-namespace:loginDb.ConvertersloginDb.Converters  5      7 clr-namespace:loginDb.ViewModelsloginDb.ViewModels  5          loginDb.View.MeetingsView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:loginDb.View     5      3local1 clr-namespace:loginDb.Converters     5      X TFontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  6>   1
viewmodels clr-namespace:loginDb.ViewModels  5      -   5          � 	Resources  5      +   %loginDb.Converters.DateToAgeConverter   dateToAgeConverter*   $loginDb.Converters.IdToNameConverter  idToNameConverter2   ,loginDb.Converters.DateTimeToStringConverter  DateTimeToStringConverter��%W   &	        &	      &	 &       5   
   6$     5   
   6#     5   
   6+   5      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $	��120 �6#   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��Top=�5      $��0��5       .+�5!      !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ Search=�6   $��20��6-   $��20��69     plainTextColor3   
Foreground# [ 6D   �� 5#      -   5         ��Name$ ToSearch��5#      $��13(�5$      $��24��6#   $	��200��6/   
������ $��Medium"�5%      $��
Montserrat)�5&      #��[ 5'       �� 
CaretBrush# [ 5(      #��[ 5)      $��0,0,0,1q�5*      $��Center=�5+      $��5,5,0,0q�5,      ��5#   ,   �� *
SearchText+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�.��5,   /   5.      �� 5/      $��Search��51      $��13(�52      $��
Montserrat)�53      
 � Cursor$

 Hand��54      $��0q�55      $��90��61   ��50      �� *SearchCommand+��57      �� 58       �� 
TargetType" ��6    .�59      ��  �� Property � �� 
 color3 �� Value# [ 6;   �� 5:       � ��  panelActiveColor# �  6;   �� 5;       � �� 5<   "   �� 5=   &    �� 
TargetType" ��66   .R�5>   *   �� $��30��61    �� CornerRadius$ 15��5?   5   #��z��5@   5   ._�5A   .   �� $��Center=�6?   $��Center=�5B   G   5C   +   5D   '   5F       �� Triggers 5H      P� 5I   "    P� Property �@J�IsMouseOver P� Value . . �5J   &   ��  � �� �	#FF7FEFFD6C   5K   #   5M      5O      5R      �� 5S      $��1��6   $��+    Add a Meeting��5U      $��13(�5V      $��
Montserrat)�5W      $

 Hand��5X      $��0q�5Y        
panelColor#���  5Z      $	��Top=�5[      $
��Left=�65   $	��120��6P   ��5T      �� *ShowAddCommand+��5]      �� 5^      " ��6   .�5_      ��  � ��# [ 67   �� 5`       � �� 5a      �� 5b   "   " ��62   .R�5c   &   �� $��30��6-   $ 15��5d   5   #��z��5e   5   ._�5f   *   �� $��Center=�6;   $��Center=�5g   G   5h   '   5i   #   5k       5m      P� 5n       �@J�IsMouseOver . . �5o   "   ��  � �� �	#FF7FEFFD6?   5p      5r      5t      5u      �� 5v   
   $��1��6   $ 10��5w       �� Padding$ 10q�5x      ��5y      �� 5z       �� 
StartPoint$	 1,1&�6&    �� EndPoint$	 0,0&�67   .��5{      �   secondaryBackColor1 � Color# [ 6#    � Offset$ 0Q�6P   � 5|        secondaryBackColor2# [ 6#   $ 1Q�6P   5}      ._�5�      &   System.Windows.Controls.DataGrid  -   5          Name$ datagrid��5�      $��Hidden=�6�   $
��Auto=�5�      $��0,0,0,10q�6I      GridLinesVisibility$
 None=�5�         
IsReadOnly .  J� IsHitTestVisible .    AutoGenerateColumns .  
������    AlternatingRowBackground �#0A003A �� AlternationCount$ 2��5�   :   $��0q�5�         RowBackground
 ����     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
  Auto 5�      $��Stretch=�5�      $��Stretch=�65   ��5�   )   �� *FilteredMeetings+$ OneWay=�$	 PropertyChanged=�!   ColumnHeaderStyle! 5�      �� 5�      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5�      ��  � ��# [ 67   �� 5�       � ��# �  67   �� 5�       � ��$ 	10,5,10,5q�64   �� 5�       � ��$ 
Montserrat)�67   �� 5�       � ��$ DemiBold"�67   �� 5�       � ��$ 20(�65   5�      "   	CellStyle" 5�      �� 5�      *	  $System.Windows.Controls.DataGridCell" 	 6   .�5�      ��  � �� 	 titleColor2# � 	 67   �� 5�       � ��$ 17(�65   �� 5�       � ��$ Stretch=�6G   �� 5�       � ��$ 
10,10,20,0q�64   �� 5�       � ��$ 10,10,10,10q�63   5�        5�      # �� Color��%�   (��       5�   &   * SystemColors.HighlightBrushKey+)(�� %     5�   &   *SystemColors.ControlBrushKey+)�� 5�      $# Transparent��6X   �� 5�      $# Transparent��6V   5�      $   RowStyle$ 5�      �� 5�      )
  #System.Windows.Controls.DataGridRow" 
 6    5�      P� 5�       �@J�IsMouseOver . . �5�   "   ��  � ��# [ 6?   �� 5�   "    � ��$
 Bold"�6?   �� 5�   "    � ��$
 Bold"�6?   5�      5�      %   Columns% 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn&   Header$	& No.��6)   1  +System.Windows.Controls.DataGridBoundColumn'   Binding' 66   �� *Number+  5�      $& ID��6)   ' 65   �� *
ClientId+  5�      $
& Name��6)   ' 67   �� *
ClientId+( �� 	Converter#( [ ) �� ConverterParameter$) Client��  5�      $
& Time��6)   ' 67   �� *Date+#( [   5�      $& Status��6)   ' 69   �� *Status+  5�      $& Summary��6)   ' 6:   �� *	Summary+*   ElementStyle* 5�      �� 5�      " ��6$   .�5�   "   ��  �@�ToolTip 6<   �� *	Summary+�� 5�   "    � ��$ CharacterEllipsis=�6A   5�      5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�      $& Actions��6-   +   CellTemplate+ 5�      �� 5�      .R�5�   "   �� $ 
Horizontal=�6-   
������ $��0,0,5,0q�6_   $
��Auto��6q   .+�5�   &   �� , ��Name$, editBtn��6-    
 ActionsButton#��[
 6>   $��35��6e   - � ToolTip$
- Edit��6q   ��6�   �� *DataContext.ShowEditCommand+. �� RelativeSource. �� / �� AncestorType$/ DataGridL���6�   �� .��5�   *     $	 Pen=�67     controlButtonIcon#��[ 6B   5�   '   �� 5�   &   $, 	deleteBtn��6-   #��[
 6@   $��35��6g   $- Delete��6s   ��6�   �� *DataContext.DeleteCommand+. �� $/ DataGridL���6�   �� .��5�   *     $ TrashCan=�67   #��[ 6G   5�   '   5�   #   5�      5�      5�      5�      5�      5�      