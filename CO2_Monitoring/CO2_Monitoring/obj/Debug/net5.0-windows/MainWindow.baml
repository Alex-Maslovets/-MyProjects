   M S B A M L   `   `   `  ����I  ECO2_Monitoring, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CO2_MonitoringCO2_Monitoring  5   	       CO2_Monitoring.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:CO2_Monitoring     5   	     � Topmost$
  True��5   	    � WindowStartupLocation$ Manual=�5	   	    � Left$ 0��5
   	   
 � Top$ 0��6    � Title$ 
MainWindow��5   	   $
��1080��6   $
��1920��6*   .��5      � .+�5   
   �� -   5         ��Name$ MainTabControl��5      $��	0,0,0,100q�6-   ��6@   Z� .@�5      �� $��Times New Roman)�6   $��
ЗАВОД��63   $��Normal"�6B   ��5      �� 5      .��5      � $	��850��6   $	��Top=�6$   ��5      �� 5       �� ImageSource$ /Plant_CO2.png��6%   .+�5      �� -   5        $��Button��5      $
��Left=�6.   $��51��6I   $��	60,91,0,0q�6U   $	��Top=�6h   $	��105��6�   6�   5      5      �� 5      $��ЭНЕРГОБЛОК��6   ���	#FFD60EEC
���   �$��Times New Roman)�6T   $��Normal"�6q   .��5      � $	��860��6   $	��Top=�6$   ��5      �� 5      $ /Utilities.png��6%    � ColumnDefinitions 5      �� 5      5!      5"      �� 5#      $
��ЦР��6   ���	#FF0EEC4E
���   �$��Times New Roman)�6L   $��Normal"�6i   .��5$      � $	��860��6   $	��Top=�6$   ��5%      �� 5&      $ /Packaging.png��6%    5(      �� 5)      5+      5,      �� 5-      $��ЦПП��6   ���	#FF1D4BDA
���   �$��Times New Roman)�6M   .��5.      � $	��860��6   $	��Top=�6$   ��5/      �� 50      $ /Brewing.png��6%    52      �� 53      55      56      57      &   System.Windows.Controls.DataGrid  58   
   -   5          Name$ Grid_MainMessages��58   u   $��Center=�6   $	��140��60   $��	0,880,0,0q�6=   $	��Top=�6P   $
��1920��6h   	   AutoGenerateColumns$
	 True��6�   
   MinColumnWidth$
 30Q�6�      RowStyle 5:      �� 5;       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    �� Triggers 5<      �� 5=       �� Binding 6&   �� *State+ �� Value Авария6@   .a�5>      ��  �� Property � �� �� Value
 �  ��6I   5?      �� 5@       6&   �� *State+  Предупреждение6@   .a�5A      ��  � ��
 � � �6K   5B      5D      5F      5G      5L      