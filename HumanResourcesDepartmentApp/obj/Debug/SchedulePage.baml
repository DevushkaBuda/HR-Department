   M S B A M L   `   `   `  ����V  RHumanResourcesDepartmentApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:HumanResourcesDepartmentAppHumanResourcesDepartmentApp  5      .    (HumanResourcesDepartmentApp.SchedulePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:HumanResourcesDepartmentApp     5        N� Title$  SchedulePage��5	       N� 
Background
 �����.��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��* �6   �� 5      $��40 �6   .+�5   
   �� $��График работы��6   
���   �$��0��6<   $
��Bold"�6I   $��30(�6[   $��Center=�6i   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
DGSchedule��5      $��2��6'      AutoGenerateColumns .     
IsReadOnly . 
�������   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ День недели��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6`   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6:   �� *DayOfTheWeek.Name+6l     5      $ Статус��6%   $ * 6P   	 65   �� *Status+6\     5      $ Время работы��6%   $ * 6X   	 6;   �� *
Duration+6d     5      $ Цех��6%   $	 100 6N   	 62   �� *	Cabinet+6\   5      �� 5   
   -   5        
 ��Name$	
 Add��5      $��Добавить��6   $
��Left=�61   $	��Top=�6L   $	��150��6w   $��3��6�   $��20(�6�   ���#bb2032���#FFF J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5!      �� 5"   
   -   5        $	
 Del��5"      $��Удалить��6   $��Right=�60   $	��Top=�6L   $	��150��6w   $��3��6�   $��20(�6�   ���#bb2032���#FFF 5#        5$      5&      �� 5'   
   -   5        $
 BtnSchedule��5'      $ ��Редактировать��6&   $	��150��6Y   $��3��6e   $��20(�6r   ���#bb2032$
��Left=�6�   $	��Top=�6�   ���#FFF$��	189,1,0,0q�6�    5(        5)      5+      �� 5,   
   -   5        $
 	BtnOtchet��5,      $��
Отчёт��6$   $	��150��6M   $��3��6Y   $��20(�6f   ���#bb2032$
��Left=�6�   $	��Top=�6�   ���#FFF$��	426,1,0,0q�6�    5-        5.      50      �� 51   
   -   5        $
 Search��51      $��Сформировать��6!   $��Right=�68   $��Center=�6T   $	��150��6�   $��1��6�   $��20(�6�   ���#bb2032���#FFF$��
0,-1,10,10q�6�    52        53      55      �� 57   
   -   5         ��Name$ CBSearch��57      $��20(�6%   $��1��63   ���#bb2032 �� DisplayMemberPath$ 	Full_Name��6v    �� SelectedValuePath$ 
Id_Profile��6�   $��
10,0,250,0q�6�    �� IsTextSearchEnabled .  �� 
IsReadOnly .  ���	#FFFB7200 �� SelectedItem 6U   �� *	Profile+6�   58      59      