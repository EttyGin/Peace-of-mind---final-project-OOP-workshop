   M S B A M L   `   `   `  ����B  >loginDb, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:loginDb.ViewloginDb.View  5      7 clr-namespace:loginDb.ConvertersloginDb.Converters  5      7 clr-namespace:loginDb.ViewModelsloginDb.ViewModels  5          loginDb.View.HomeView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:loginDb.View     5      7
converters clr-namespace:loginDb.Converters     5      X TFontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  6B   1
viewmodels clr-namespace:loginDb.ViewModels  5      -   5          � 	Resources  5      *   $loginDb.Converters.IdToNameConverter   idToNameConverter��%   &	          5   
   6'   5      .��5      �� $
��Auto=�6   $��Disabled=�66   .��5   
   ��  �� CornerRadius$ 12��6   ��5      �� 5       �� ImageSource$ /Images/screen.jpg��6    j� Stretch$ UniformToFill=�6>   ._�5      �� $ 10��6   $	��0.1q�5   	    J� Opacity$	 0.9Q�5      ��5      �� 5       �� 
StartPoint$	 0,0&�6*    �� EndPoint$	 1,1&�6;   .��5      �   winBorderColor1 � Color# [ 6'    � Offset$ 0Q�6P   � 5        winBorderColor2# [ 6'   $
 0.75Q�6P   � 5        winBorderColor3# [ 6'   $ 1Q�6P   5      ��5!      �� 5"      $	 0,1&�6*   $	 1,0&�6;   .��5#      �   secondaryBackColor1# [ 6'   $ 1Q�6T   � 5$        secondaryBackColor2# [ 6'   $ 0Q�6T   5%      ._�5'      �� -   5        	 ��Name$	 exStackPanel��5'      $
��Auto��63   $	��400��6@   $
��Auto��6O   $��Center=�6]   $��Center=�6x   $��	0,20,0,20q�6�   .+�5(      �� $��Calibri)�6<    
 color1#���  6Q   $��36(�6w   $
��Bold"�6�   $
��Auto��6�   $��50��6�   $
��Left=�6�   ��6   �� *DisplayName+6�   �� 5)      -   5        $	 inStackPanel��5)   !   
 �� Orientation$
 
Horizontal=�67   .+�5*      �� $��	You have ��6    $��Calibri)�64   #���  6I   $��30(�6o   $��Regular"�6}   $
��Auto��6�   $��50��6�   $
��Left=�6�   �� 5+      $��Calibri)�68   #���  6M   $��30(�6s   $��Regular"�6�   $
��Auto��6�   $��50��6�   $
��Left=�6�   ��6    �� *Num+6�   �� 5,      $�� meetings for today.��6    $��Calibri)�6?   #���  6T   $��30(�6z   $��Regular"�6�   $
��Auto��6�   $��50��6�   $
��Left=�6�   5.      �� 5/      $��Have a great day!��6   $��Calibri)�68   #���  6M   $��30(�6s   $��Regular"�6�   $
��Auto��6�   $
��Left=�6�   &   System.Windows.Controls.DataGrid  50      -   5          Name$ datagrid��50      $��
30,30,0,30q�51      $
��Auto��6,   $��0q�69      GridLinesVisibility$
 None=�52         
IsReadOnly .  J� IsHitTestVisible .    AutoGenerateColumns .  
������    AlternatingRowBackground �#0A003A �� AlternationCount$ 2��54   :      RowBackground
 ����    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 55         MinColumnWidth$ 70Q�6*   $
��Left=�6>   ��50   1   �� *LstMeetings+ �� Mode$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=�  56       �� Color��%�   (��       57   .   * SystemColors.HighlightBrushKey+)(�� %     58   .   *SystemColors.ControlBrushKey+)�� 57      $ Transparent��6`   �� 58      $ Transparent��6^   59         ColumnHeaderStyle 5:      �� 5;       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6$   .�5<   "   ��  �� Property � �� �� Value# [ 6?   �� 5=   "    � ��  
panelColor# �  6?   �� 5>   "    � ��$ 	10,5,10,5q�6<   �� 5?   "    � ��$ 
Montserrat)�6?   �� 5@   "    � ��$ DemiBold"�6?   �� 5A   "    � ��$ 23(�6=   5B         	CellStyle 5D      �� 5E      *  $System.Windows.Controls.DataGridCell"  6$   .�5F   "   ��  � ��  titleColor2# �  6?   �� 5G   "    � ��$ 19(�6=   �� 5H   "    � ��$ Stretch=�6O   �� 5I   "    � ��$ 
10,10,10,0q�6<   �� 5J   "    � ��$ 10,10,10,10q�6;   5K         RowStyle 5M      �� 5N      )  #System.Windows.Controls.DataGridRow"  6$    �� Triggers 5O   "   P� 5P   &    P� Property �@J�IsMouseOver  P� Value  . . �5Q   *   ��  � ��# [ 6G   �� 5R   *    � ��$
 Bold"�6G   �� 5S   *    � ��$
 Bold"�6G   5T   '   5V      !   Columns! 5Y      0  *System.Windows.Controls.DataGridTextColumn  5Z      ,  &System.Windows.Controls.DataGridColumn"   Header$	" No.��61   1	  +System.Windows.Controls.DataGridBoundColumn# 	  Binding# 6>   �� *Number+  5[      $
" Name��61   # 6?   �� *
ClientId+$ �� ConverterParameter$$ Client��% �� 	Converter#% [    5\      $
" Hour��61   # 6?   �� *Date+& �� StringFormat$& 	{0:HH:mm}��5^      5_      5a      5b      5c      5d      