   M S B A M L   `   `   `  ����B  >loginDb, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:loginDb.ViewloginDb.View  5      7 clr-namespace:loginDb.ConvertersloginDb.Converters  5      7 clr-namespace:loginDb.ViewModelsloginDb.ViewModels  5          loginDb.View.PayersView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:loginDb.View     5      3local1 clr-namespace:loginDb.Converters     5      X TFontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  6>   1
viewmodels clr-namespace:loginDb.ViewModels  5      -   5          � 	Resources  5      +   %loginDb.Converters.DateToAgeConverter   dateToAgeConverter*   $loginDb.Converters.IdToNameConverter  idToNameConverter��%:   &	        &	        5   
   6$     5   
   6#   5      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $	��120 �6#   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��Top=�5      $��0��5      .+�5       !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ Search=�6   $��20��6-   $��20��69     plainTextColor3   
Foreground# [ 6D   �� 5"      -   5         ��Name$ ToSearch��5"      $��13(�5#      $��24��6#   $	��200��6/   
������ $��Medium"�5$      $��
Montserrat)�5%      #��[ 5&       �� 
CaretBrush# [ 5'      #��[ 5(      $��0,0,0,1q�5)      $��Center=�5*      $��5,5,0,0q�5+      ��5"   ,   �� *
SearchText+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�.��5+   /   5-      �� 5.      -   5        $��Search��50      $��13(�51      $��
Montserrat)�52      
 � Cursor$

 Hand��53      $��0q�54        
panelColor#���  55      $��90��6G   ��5/      �� *SearchCommand+��57      �� 58       �� 
TargetType" ��6    .�59      ��  �� Property � �� 
 color4 �� Value# [ 6;   �� 5:       � �� 5;   "   �� 5<   &    �� 
TargetType" ��66   .R�5=   *   �� $��30��61    �� CornerRadius$ 15��5>   5   #��z��5?   5   ._�5@   .   �� $��Center=�6?   $��Center=�5A   G   5B   +   5C   '   5E       �� Triggers 5G      P� 5H   "    P� Property �@J�IsMouseOver P� Value . . �5I   &   ��  � �� �	#FFFF91C06C   5J   #   5L      5N      5Q      �� 5R      $��1��6   $��+    Add a Payer��5T      $��13(�5U      $��
Montserrat)�5V      $

 Hand��5W      $��0q�5X      #���  5Y      $	��Top=�5Z      $
��Left=�65   $	��120��6P   ��5S      �� *ShowAddCommand+��5\      �� 5]      " ��6   .�5^      ��  � ��# [ 67   �� 5_       � �� 5`      �� 5a   "   " ��62   .R�5b   &   �� $��30��6-   $ 15��5c   5   #��z��5d   5   ._�5e   *   �� $��Center=�6;   $��Center=�5f   G   5g   '   5h   #   5j       5l      P� 5m       �@J�IsMouseOver . . �5n   "   ��  � �� �	#FFFF91C06?   5o      5q      5s      5t      �� 5u   
   $��1��6   $ 10��5v       �� Padding$ 10q�5w      ��5x      �� 5y       �� 
StartPoint$	 1,1&�6&    �� EndPoint$	 0,0&�67   .��5z      �   secondaryBackColor1 � Color# [ 6#    � Offset$ 0Q�6P   � 5{        secondaryBackColor2# [ 6#   $ 1Q�6P   5|      ._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ datagrid��5      $��Hidden=�6�   $
��Auto=�5�      $��0,0,0,10q�6I      GridLinesVisibility$
 None=�5�         
IsReadOnly .  J� IsHitTestVisible .    AutoGenerateColumns .  
������    AlternatingRowBackground �#0A003A �� AlternationCount$ 2��5�   :   $��0q�5�         RowBackground
 ����     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
  Auto 5�      $��Stretch=�5�      $��Stretch=�65   ��5   )   �� *FilteredPayers+$ OneWay=�$	 PropertyChanged=�!   ColumnHeaderStyle! 5�      �� 5�      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5�      ��  � ��# [ 67   �� 5�       � ��# �  67   �� 5�       � ��$ 	10,5,10,5q�64   �� 5�       � ��$ 
Montserrat)�67   �� 5�       � ��$ DemiBold"�67   �� 5�       � ��$ 18(�65   5�      "   	CellStyle" 5�      �� 5�      *  $System.Windows.Controls.DataGridCell"  6   .�5�      ��  � ��  titleColor2# �  67   �� 5�       � ��$ 17(�65   �� 5�       � ��$ Stretch=�6G   �� 5�       � ��$ 
10,10,20,0q�64   �� 5�       � ��$ 10,10,10,10q�63   5�        5�      # �� Color��%�   (��       5�   &   * SystemColors.HighlightBrushKey+)(�� %     5�   &   *SystemColors.ControlBrushKey+)�� 5�      $# Transparent��6X   �� 5�      $# Transparent��6V   5�      $   RowStyle$ 5�      �� 5�      )	  #System.Windows.Controls.DataGridRow" 	 6    5�      P� 5�       �@J�IsMouseOver . . �5�   "   ��  � ��# [ 6?   �� 5�   "    � ��$
 Bold"�6?   5�      5�      %   Columns% 5�      0
  *System.Windows.Controls.DataGridTextColumn
  5�      ,  &System.Windows.Controls.DataGridColumn&   Header$& ID��6)   1  +System.Windows.Controls.DataGridBoundColumn'   Binding' 65   �� *Id+
  5�      $
& Name��6)   ' 67   �� *Pname+
  5�      $& Contact-Name��6)   ' 6?   �� *ContactName+
  5�      $& Contact-Email��6)   ' 6@   �� *ContactEmail+
  5�      $& Payment��6)   ' 6:   �� *TotalPayment+4  .System.Windows.Controls.DataGridTemplateColumn  5�      $& Actions��6-   (   CellTemplate( 5�      �� 5�      .R�5�   "   �� $ 
Horizontal=�6-   
������ $��0,0,5,0q�6_   $
��Auto��6q   .+�5�   &   �� ) ��Name$) editBtn��6-     ActionsButton#��[ 6>   $��35��6e   * � ToolTip$
* Edit��6q   ��6�   �� *DataContext.ShowEditCommand++ �� RelativeSource+ �� , �� AncestorType$, DataGridL���6�   �� .��5�   *     $	 Pen=�67    	 controlButtonIcon#��[	 6B   5�   '   �� 5�   &   $) 	deleteBtn��6-   #��[ 6@   $��35��6g   $* Delete��6s   ��6�   �� *DataContext.DeleteCommand++ �� $, DataGridL���6�   �� .��5�   *     $ TrashCan=�67   #��[	 6G   5�   '   5�   #   5�      5�      5�      5�      5�      5�      