   M S B A M L   `   `   `  ����B  >loginDb, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:loginDb.ViewloginDb.View  5   	   7 clr-namespace:loginDb.ConvertersloginDb.Converters  62   7 clr-namespace:loginDb.ViewModelsloginDb.ViewModels  5   	   &     loginDb.View.AddOrEditClientView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:loginDb.View     5   	   3local1 clr-namespace:loginDb.Converters     62   7
viewmodels clr-namespace:loginDb.ViewModels     5   	   -   5        $	��350��5	   
     � WindowStartupLocation$  CenterScreen=�5
   	    � WindowStyle$
 None=�5   	    � Topmost .  � 
ResizeMode$ 	CanResize=�5   	   
������  � AllowsTransparency .  � 	Resources 5         BooleanToVisibility&    loginDb.Converters.ModeConverter  ModeConverter2   ,loginDb.Converters.ModeToVisibilityConverter  ModeToVisibilityConverter��%W   &	        &	      &	 &     �� 5   
   6'     5   
   6     5   
   6+   5      y�5      �� 5   
    �� Path$ IsViewVisible�6    �� Mode$ TwoWay=�6'    �� 	Converter# [  65   .��5      �� 	 �� CornerRadius$	 12��6   $��Stretch=�6   $��Center=�6=   ��5   
   �� 5      
 �� ImageSource$
 /Images/screen.jpg��6    j� Stretch$
 None=�6:   ._�5"   
   �� $	 10��6   $��1q�5#       J� Opacity$	 0.9Q�5$      ��5%      �� 5&       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67   .��5'      �   winBorderColor1 � Color# [ 6#    � Offset$ 0Q�6L   � 5(        winBorderColor2# [ 6#   $
 0.75Q�6L   � 5)        winBorderColor3# [ 6#   $ 1Q�6L   5+      ��5.      �� 5/      $	 0,1&�6&   $	 1,0&�67   .��50      �   primaryBackColor1# [ 6#   $ 0Q�6N   � 51        primaryBackColor2# [ 6#   $
 0.75Q�6N   52      ._�54      �  � RowDefinitions 55      �� 56      $��30 �6$   �� 57      .+�59      �  � ColumnDefinitions 5:      �� 5;      �� 5<      $��25 �6+   �� 5=      $��25 �6+   �� 5>      $��5 �6+   .+�5@      ��   titleColor3#��[ 5A       $��10(�6K   $��
Montserrat)�6Y   $��0��5B       $��Center=�60   $��10,0,0,0q�6K   ��5@   $   �� $ CurrentMode�# [  �� ConverterParameter$ CLIENT��5B   K   �� 5C      -   5         ��Name$ btnClose��5C      $��0q�5D      $��X��6-   
�������$��12(�5E   ,   $��
Montserrat)�6:    � Cursor$
 Hand��5F      $��2��5G      ��5I      �� 5J       �� 
TargetType" ��6$   .�5K   "   ��  �� Property � �� 
	 color2 �� Value# [	 6?    �� Triggers 5L   "   P� 5M   &    P� Property �@J�IsMouseOver P� Value . . �5N   *   ��  � �� �	#FFAF2B9A6G   5O   '   5Q      ��5S      �� 5T       �� 
TargetType" ��6.   .R�5U   "   �� $��18��6)   $��18��64   $	 9��6@   #��z��6Q   ._�5V   &   �� $��Center=�67   $��Center=�6R   5X   #   5Y      5[      5\      �� 5]      $��2��6   .+�5^      �� $��Id��6   $	��Top=�6*   $��Center=�6B   #��[ 6_   �� 5_      $��25��6   $	��200��6>   $	��Top=�6�   $��Center=�6�   #��[ 6�   ��6*   �� *Id+y�6J   �� *CurrentMode+# [ $	 Add��6�   �� 5`      $��25��6   $ No��5a       �� 
IsReadOnly . $	��200��6<   $	��Top=�6H   $��Center=�6`   #��[ 6}   ��5`   *   �� *SelectedClient.Id+y�6M   �� *CurrentMode+# [ $
 Edit��5a   }   �� 5b      $
��Name��6   $	��Top=�6,   $��Center=�6D   #��[ 6a   �� 5c      $��25��6   $	��200��6�   $	��Top=�6�   $��Center=�6�   #��[ 6�   ��6*   �� *Name+y�6@   �� *CurrentMode+# [ $	 Add��6�   �� 5d      $��25��6   $	��200��6�   $	��Top=�6�   $��Center=�6�   #��[ 6  ��6*   �� *SelectedClient.Cname+y�6P   �� *CurrentMode+# [ $
 Edit��6  �� 5e      $��
Birth Date��6   $	��Top=�62   $��Center=�6J   #��[ 6g   (  "System.Windows.Controls.DatePicker  5f      $��25��6!   $	��200��6�   $	��Top=�6�   $��Center=�6�   #��[ 6     SelectedDate 6-   �� *	BirthDate+y�6P   �� *CurrentMode+# [ $	 Add��6    5g      $��25��6!   $	��200��6�   $	��Top=�6�   $��Center=�6�   #��[ 6   6-   �� *SelectedClient.BirthDate+y�6_   �� *CurrentMode+# [ $
 Edit��6  �� 5h      $��Phone��6   $	��Top=�6-   $��Center=�6E   #��[ 6b   �� 5i      $��25��6   $	��200��6�   $	��Top=�6�   $��Center=�6�   #��[ 6�   ��6*   �� *Phone+y�6A   �� *CurrentMode+# [ $	 Add��6�   �� 5j      $��25��6   $	��200��6�   $	��Top=�6�   $��Center=�6�   #��[ 6  ��6*   �� *SelectedClient.Phone+y�6P   �� *CurrentMode+# [ $
 Edit��6  �� 5k      $��Email��6   $	��Top=�6-   $��Center=�6E   #��[ 6b   �� 5l      $��25��6   $	��200��6�   $	��Top=�6�   $��Center=�6�   #��[ 6�   ��6*   �� *Email+y�6A   �� *CurrentMode+# [ $	 Add��6�   �� 5m      $��25��6   $	��200��6�   $	��Top=�6�   $��Center=�6�   #��[ 6  ��6*   �� *SelectedClient.Email+y�6P   �� *CurrentMode+# [ $
 Edit��6  �� 5n      $��Payer��6   $	��Top=�6-   $��Center=�6E   #��[ 6b   �� 5o       �� SelectedValuePath$ Id��5p        �� DisplayMemberPath$  Pname��5q      $��25��5t      $	��200��6%   $��Center=�61   $��Center=�6L    
 panelActiveColor#��[
 5u      ! �� 
IsReadOnly! . ��5o      �� *Payers+" �� SelectedValue" 5r      �� *
SpePayerId+y�5s      �� *CurrentMode+# [ $	 Add��5u   H   �� 5v      $ Id��6   $��25��5x      $	��200��6%   $��Center=�61   $��Center=�6L   #��[
 5y      ! . $  Pname��6Z   ��5v   6   �� *Payers+" 6U   �� *
SpePayerId+$ Default=�y�5w      �� *CurrentMode+# [ $
 Edit��5y   Z   �� 5{      ���#D7596D$��13(�5}      $��Medium"�5~      $��
Montserrat)�5      $��30,10,30,10q�5�      $
��Wrap=�5�      ��5{       �� *ErrorMessage+# �� UpdateSourceTrigger$# PropertyChanged=�5�      �� 5�      $��13(�5�      $��
Montserrat)�6'   $��0,0,0,20q�6?   $
 Hand��6Q   $��0q�6_   
�������$	��110��6�   ��5�      �� *AorECommand+��6=   �� $ CurrentMode�# [ $ Client����5�      �� 5�      " ��6$   .�5�   "   ��  � ��# [	 6?   �� 5�   "    � �� 5�   &   �� 5�   *   " ��6:   .R�5�   .   �� $��30��65   $	 15��5�   )   #��z��5�   )   ._�5�   2   �� $��Center=�6C   $��Center=�5�   ;   5�   /   5�   +   5�   #    5�   "   P� 5�   &    �@J�IsMouseOver . . �5�   *   ��  � �� �#836EFB6G   5�   '   5�      5�      5�      5�      5�      5�      5�      