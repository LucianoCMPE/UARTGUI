   M S B A M L   `   `   `  ����>  :USA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:USAUSA  5          USA.AddItem   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      #localclr-namespace:USA     5      $	��400��5      $	��600��6     � WindowState$  Normal=�5	       � WindowStartupLocation$ CenterOwner=�5
       � SizeToContent$ Manual=�5      
������� � Title$ AddItem��5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      $	��150 �6   �� 5      $	��150 �6   �� 5      $	��150 �6   �� 5      $	��200 �6    � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      $��25 �6   �� 5      $��25 �6   �� 5      $��25 �6   �� 5      $��25 �6   �� 5      $��25 �6   �� 5       $��25 �6   �� 5!      $��25 �6   �� 5"      $��25 �6   �� 5#      $��25 �6   �� 5$      $��25 �6   �� 5%      $��25 �6   �� 5&      $��25 �6   .+�5*   
   � $��0��6   .+�5+      �� $��	Test Name��6   $��Center=�5,      5.      � 50   
   $��1��6   .+�51      �� -   5         ��Name$ testNameInput��51      52      � 53   
   $��0��6   $��1��6   .+�54      �� $��Include Header (Hex)��6   $��Center=�55      57      � 59   
   $��1��6   $��1��6   $��Center=�6,   $��Center=�6I   .+�5:      �� -   5         ��Name$ isHeader��5:      6B   5;      � 5>   
   $��2��6   $��1��6   .+�5?      �� -   5        $ headerValue��5?      $y�Hidden=�6+   5@      � 5B   
   $��0��6   $��2��6   .+�5C      �� $��Command # (Decimal)��6   $��Center=�5D      5F      � 5H   
   $��1��6   $��2��6   .+�5I      �� -   5        $ commandNumber��5I      5J      � 5K   
   $��0��6   $��3��6   .+�5L      �� $��Payload Length (Decimal)��6   $��Center=�5M      5O      � 5Q   
   $��1��6   $��3��6   .+�5R      �� -   5        $ payloadLength��5R      5S      �� 5U   
   -   5        $��1��5U      $��20��5V      $��70��5W      
�������
���   �$��5��5Z      $��Submit��5[      $��0,0,0,0q�5]      5d      5e      