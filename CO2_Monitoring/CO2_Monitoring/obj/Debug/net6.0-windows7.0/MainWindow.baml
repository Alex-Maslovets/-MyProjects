   M S B A M L   `   `   `  ����I  ECO2_Monitoring, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CO2_MonitoringCO2_Monitoring  5   	       CO2_Monitoring.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:CO2_Monitoring     5   	     � Topmost$
  True��5   	    � WindowStartupLocation$ Manual=�5	   	    � Left$ 0��5
   	   
 � Top$ 0��6    � Title$ 
MainWindow��5   	   $
��1080��6   $
��1920��6*   .��5      � .+�5   
   �� -   5         ��Name$ MainTabControl��5      $��	0,0,0,100q�6-   ��6@   Z� .@�5      �� $��Times New Roman)�6   $��
ЗАВОД��63   $��Normal"�6B   ��5      �� 5      .��5      � $	��850��6   $	��Top=�6$   ��5      �� 5       �� ImageSource$ /Plant_CO2.png��6%   .+�5      �� -   5        $��Button��5      $
��Left=�6.   $��51��6I   $��	60,91,0,0q�6U   $	��Top=�6h   $	��105��6�   6�   �� 5      -   5         ��Name$ ARHF_1066_Grp��5      $
��Left=�66   $��46��6Q   $��	ARHF 1066��6]   $��192,107,0,0q�6p   $	��Top=�6�   $��88��6�   ���	#FFC3C3C3 � BindingGroup 6�   �� *Value+	 �� ElementName$	 slider_Value��.��5      �� -   5        
 ��Name$
 	CO2_Value��5       $
��Left=�6g   $��Center=�6�   $��21,0,0,0q�6�   ��63   �� *Value+$	 slider_Value��6�   5      �� 5      -   5         ��Name$ slider_Value��5      $
��Left=�63   $��303,124,0,0q�6N   $	��Top=�6c   $	��120��6{   5      5      �� 5      $��ЭНЕРГОБЛОК��6   ���	#FFD60EEC
���   �$��Times New Roman)�6T   $��Normal"�6q   .��5      � $	��860��6   $	��Top=�6$   ��5      �� 5       $ /Utilities.png��6%    � ColumnDefinitions 5"      �� 5#      5%      5&      �� 5'      $
��ЦР��6   ���	#FF0EEC4E
���   �$��Times New Roman)�6L   $��Normal"�6i   .��5(      � $	��860��6   $	��Top=�6$   ��5)      �� 5*      $ /Packaging.png��6%    5,      �� 5-      5/      50      �� 51      $��ЦПП��6   ���	#FF1D4BDA
���   �$��Times New Roman)�6M   .��52      � $	��860��6   $	��Top=�6$   ��53      �� 54      $ /Brewing.png��6%    56      �� 57      59      5:      5;      &   System.Windows.Controls.DataGrid  5<   
   -   5          Name$ Grid_MainMessages��5<   u   $��Center=�6   $	��140��60   $��	0,880,0,0q�6=   $	��Top=�6P   $
��1920��6h      AutoGenerateColumns$
 True��6�      MinColumnWidth$ 30Q�6�      RowStyle 5>      �� 5?       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    �� Triggers 5@      �� 5A       �� Binding 6&   �� *Message_Type+ �� Value Авария6G   .a�5B      ��  �� Property � �� �� Value
 �  ��6I   5C      �� 5D       6&   �� *Message_Type+  Предупреждение6G   .a�5E      ��  � �� �	#FFE7F3076O   5F      5H      5J      5K      5P      