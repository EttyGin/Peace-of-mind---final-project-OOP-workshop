   M S B A M L   `   `   `  ����B  >loginDb, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:loginDb.ViewloginDb.View  5   	   7 clr-namespace:loginDb.ConvertersloginDb.Converters  62   7 clr-namespace:loginDb.ViewModelsloginDb.ViewModels  5   	   '    !loginDb.View.AddOrEditMeetingView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:loginDb.View     5   	   7
converters clr-namespace:loginDb.Converters     62   7
viewmodels clr-namespace:loginDb.ViewModels     5   	   -   5        $	��350��5	   
     � WindowStartupLocation$  CenterScreen=�5
   	    � WindowStyle$
 None=�5   	    � Topmost .  � 
ResizeMode$ 	CanResize=�5   	   
������  � AllowsTransparency .  � 	Resources 5         BooleanToVisibility&    loginDb.Converters.ModeConverter  ModeConverter2   ,loginDb.Converters.ModeToVisibilityConverter  ModeToVisibilityConverter/   )loginDb.Converters.EnumToBooleanConverter  EnumToBooleanConverter*   $loginDb.Converters.IdToNameConverter  idToNameConverter��%�   &	        &	      &	 &     &	 9     &	 L     �� 5   
   6'     5   
   6#     5   
   6/     5   
   6,     5   
   6'   5      y�5      �� 5   
    �� Path$ IsViewVisible�6    �� Mode$ TwoWay=�6'    �� 	Converter# [  65   .��5      �� 	 �� CornerRadius$	 12��6   $��Stretch=�6   $��Center=�6=   ��5   
   �� 5       
 �� ImageSource$
 /Images/screen.jpg��6    j� Stretch$
 None=�6:   ._�5#   
   �� $	 10��6   $��1q�5$       J� Opacity$	 0.9Q�5%      ��5&      �� 5'       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67   .��5(      �   winBorderColor1 � Color# [ 6#    � Offset$ 0Q�6L   � 5)        winBorderColor2# [ 6#   $
 0.75Q�6L   � 5*        winBorderColor3# [ 6#   $ 1Q�6L   5,      ��5/      �� 50      $	 0,1&�6&   $	 1,0&�67   .��51      �   primaryBackColor1# [ 6#   $ 0Q�6N   � 52       	 primaryBackColor2# [	 6#   $
 0.75Q�6N   53      ._�55      �  � RowDefinitions 56      �� 57      $��30 �6$   �� 58      .+�5:      �  � ColumnDefinitions 5;      �� 5<      �� 5=      $��25 �6+   �� 5>      $��25 �6+   �� 5?      $��5 �6+   .+�5A      ��  
 titleColor3#��[
 5B       $��10(�6K   $��
Montserrat)�6Y   $��0��5C       $��Center=�60   $��10,0,0,0q�6K   ��5A       �� $ CurrentMode�# [  �� ConverterParameter$ MEETING��5C   K   �� 5D      -   5         ��Name$ btnClose��5D      $��0q�5E      $��X��6-   
�������$��12(�5F   ,   $��
Montserrat)�6:    � Cursor$
 Hand��5G      $��2��5H      ��5J      �� 5K       �� 
TargetType" ��6$   .�5L   "   ��  �� Property � �� 
 color3 �� Value# [ 6?    �� Triggers 5M   "   P� 5N   &    P� Property �@J�IsMouseOver P� Value . . �5O   *   ��  � �� �	#FFAF2B9A6G   5P   '   5R      ��5T      �� 5U       �� 
TargetType" ��6.   .R�5V   "   �� $��18��6)   $��18��64   $	 9��6@   #��z��6Q   ._�5W   &   �� $��Center=�67   $��Center=�6R   5Y   #   5Z      5\      5]      �� 5^      $��2��6   .+�5_      �� $��Number��6   $	��Top=�6.   $��Center=�6F   #��[
 6c   y�5`      �� *CurrentMode+# [ $
 Edit���� 5a      $��25��6   $ No��6R    �� 
IsReadOnly . $	��200��5c      $	��Top=�6*   $��Center=�6B   #��[
 6_   ��5a   *   �� *SelectedMeeting.Number+y�5b      �� *CurrentMode+# [ $
 Edit��5c   _   �� 5d      $��Client��6   $	��Top=�6.   $��Center=�6F   #��[
 6c   �� 5e       �� SelectedValuePath$ Id��5f       �� DisplayMemberPath$ Cname��62   $��25��5h      $	��200��6%   $��Center=�61   $��Center=�6L     panelActiveColor#��[ 5i        �� 
IsReadOnly  . ��5e      �� *	Clients+! �� SelectedValue! 5f   L   �� *
ClientId+y�5g      �� *CurrentMode+# [ $	 Add��5i   H   �� 5k      $ Id��5l      $ Cname��62   $��25��5n      $	��200��6%   $��Center=�61   $��Center=�6L   {�.  #��[ 5o      ��5k      �� *	Clients+! 5l   L   �� *SpeClientId+$ Default=�y�5m      �� *CurrentMode+# [ $
 Edit��5o      �� 5p      $
��Date��6   $	��Top=�6,   $��Center=�6D   #��[
 6a   (  "System.Windows.Controls.DatePicker  5q      $��25��6!   $	��200��6�   $	��Top=�6�   $��Center=�6�   #��[
 6�   "   SelectedDate" 6-   �� *Date+y�6K   �� *CurrentMode+# [ $	 Add��6�     5r      $��25��6!   $	��200��6�   $	��Top=�6�   $��Center=�6�   #��[
 6  " 6-   �� *SelectedMeeting.Date+y�6[   �� *CurrentMode+# [ $
 Edit��6  �� 5s      $
��Hour��6   $	��Top=�6,   $��Center=�6D   #��[
 6a   �� 5t      $��25��6   $	��200��5u      $	��Top=�6%   $��Center=�6=   #��[
 6Z   ��5t   *   �� *Hour+# �� StringFormat$# 	{0:HH:mm}��$ �� UpdateSourceTrigger$$ PropertyChanged=�y�5v      �� *CurrentMode+# [ $	 Add���� 5w      $��25��6   $	��200��5x      $	��Top=�6&   $��Center=�6>   #��[
 6[   ��5w   *   �� *SelectedMeeting.Date+$# 	{0:HH:mm}��$$ PropertyChanged=�y�5y      �� *CurrentMode+# [ $
 Edit���� 5z      $��Summary��6   $	��Top=�6/   $��Center=�6G   #��[
 6d   �� 5{      $
��Auto��6   % �� TextWrapping$
% Wrap=�6E   $	��200��6�   $	��Top=�6�   $��Center=�6�   #��[
 6  ��6,   �� *	Summary+y�6Y   �� *CurrentMode+# [ $	 Add��6  �� 5|      $
��Auto��6   $
% Wrap=�6V   $	��200��6�   $	��Top=�6�   $��Center=�6   #��[
 6  ��6,   �� *SelectedMeeting.Summary+y�6j   �� *CurrentMode+# [ $
 Edit��6  �� 5~      $��Status��6   $	��Top=�6.   $��Center=�6F   #��[
 6c   � 5      $	��250��6    $��Center=�5�   �   & � 
ItemHeight$& 30��6�   y�6   �� *CurrentMode+# [ $	 Add��.+�5�      	� $��Planned��6&   {�.  ' � ToolTip$' Fills automatically��5�   &   $��0,0,10,0q�6D   #��[
 6V   #��[ 6�   #��[
 6�   $��14(�6�   $��Center=�6�   ( d� 	IsChecked( 5�   8   �� *Status+# [ $ planned��5�   �   	� 5�      $��Unpaid��6&   {�.  $' Fills automatically��5�   +   $��0,0,10,0q�6I   #��[
 6[   #��[ 6�   #��[
 6�   $��14(�6�   $��Center=�6�   ( 5�   7   �� *Status+# [ $ unpaid��5�   �   	� 5�      $��Client Paid��6&   $��0,0,10,0q�5�      {�.  $' Fills automatically��6A   #��[
 6_   #��[ 6�   #��[
 6�   $��14(�6�   $��Center=�6�   ( 5�   <   �� *Status+# [ $ 
clientPaid��5�   �   	� 5�      $��
Payer Paid��6&   $��	40,0,20,0q�5�      {�.  $' Fills automatically��6B   #��[
 6`   #��[ 6�   #��[
 6�   $��14(�6�   $��Center=�6�   ( 5�   ;   �� *Status+# [ $ 	payerPaid��5�   �   	� 5�      $
��Paid��6&   {�.  $' Fills automatically��5�   *   #��[
 6H   #��[ 6r   #��[
 6�   $��14(�6�   $��Center=�6�   ( 5�   5   �� *Status+# [ $
 paid��5�   �   5�      � 5�      $	��250��6    $��Center=�6�   $& 30��6�   y�6,   �� *CurrentMode+# [ $
 Edit��.+�5�      	� $��Planned��6&   {�.  $' Fills automatically��5�      $��0,0,10,0q�61   #��[
 6C   #��[ 6m   #��[
 6�   $��14(�6�   $��Center=�6�   ( 5�   8   �� *SelectedMeeting.Status+# [ $ planned��5�   �   	� 5�      $��Unpaid��6&   {�.  $' Fills automatically��5�      $��0,0,10,0q�66   #��[
 6H   #��[ 6r   #��[
 6�   $��14(�6�   $��Center=�6�   ( 5�   7   �� *SelectedMeeting.Status+# [ $ unpaid��5�   �   	� 5�      $��Client Paid��6&   $��0,0,10,0q�5�   
   {�.  $' Fills automatically��6.   #��[
 6L   #��[ 6v   #��[
 6�   $��14(�6�   $��Center=�6�   ( 5�   <   �� *SelectedMeeting.Status+# [ $ 
clientPaid��5�   �   	� 5�      $��
Payer Paid��6&   $��	40,0,20,0q�5�   
   {�.  $' Fills automatically��6/   #��[
 6M   #��[ 6w   #��[
 6�   $��14(�6�   $��Center=�6�   ( 5�   ;   �� *SelectedMeeting.Status+# [ $ 	payerPaid��5�   �   	� 5�      $
��Paid��6&   {�.  $' Fills automatically��5�      #��[
 65   #��[ 6_   #��[
 6�   $��14(�6�   $��Center=�6�   ( 5�   5   �� *SelectedMeeting.Status+# [ $
 paid��5�   �   5�      �� 5�      ���#D7596D$��13(�5�      $��Medium"�5�      $��
Montserrat)�5�      $��30,10,30,10q�5�      $
��Wrap=�5�      ��5�       �� *ErrorMessage+$$ PropertyChanged=�5�      �� 5�      $��13(�5�      $��
Montserrat)�6'   $��0,0,0,20q�6?   $
 Hand��6Q   $��0q�6_   $	��110��6s   ��5�      �� *AorECommand+��6=   �� $ CurrentMode�# [ $ Meeting����5�      �� 5�      " ��6$   .�5�   "   ��  � ��# [ 6?   �� 5�   "    � ��# �  6?   �� 5�   "    � �� 5�   &   �� 5�   *   " ��6:   .R�5�   .   �� $��30��65   $	 15��5�   )   #��z��5�   )   ._�5�   2   �� $��Center=�6C   $��Center=�5�   ;   5�   /   5�   +   5�   #    5�   "   P� 5�   &    �@J�IsMouseOver . . �5�   *   ��  � �� �	#FF7FEFFD6G   5�   '   5�      5�      5�      5�      5�      5�      5�      