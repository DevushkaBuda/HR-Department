   M S B A M L   `   `   `  ����V  RHumanResourcesDepartmentApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:HumanResourcesDepartmentAppHumanResourcesDepartmentApp  5      .    (HumanResourcesDepartmentApp.StaffingPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:HumanResourcesDepartmentApp     5      -   5          N� Title$  StaffingPage��5	       N� 
Background
 �����.��5      � $��0,10,0,0q�6    � ColumnDefinitions 5   
   �� 5      $
��681* �6   �� 5      $
��119* �6    � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��* �6   �� 5      $��40 �6   �� 5      $��30 �6   .+�5   
   �� $)��#Штатное расписание��6   
���   �$��0��6A   $
��Bold"�6N   $��30(�6a   $
��Left=�6o   $��
248,-5,0,5q�6�   �� 5   
   $��Сортировка:��6   $	��250��6'   $��0,-10,431,10q�63    �� TextAlignment$ Center=�6I   �� 5   
   -   5        $��SortComboBox��5      $��28��5      $��
10,7,448,5q�5      5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
DGStaffing��5      $��1��6'      AutoGenerateColumns .     
IsReadOnly . 
�������$��2��6u      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn	   Header$ 	 Специальность��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *Position.Name+6j     5!      $1	 +Количество сотрудников��6%   $
 * 6o    6E   �� *Number_Of_Staff_Units+6{     5"      $	 Оклад (руб.)��6%   $
 * 6V    6;   �� *Salary+6b     5#      $=	 7Надбавка за ночные смены (руб.)��6%   $
 * 6x    6N   �� *Night_Shift_Allowance+6�     5$      $7	 1Премиальная надбавка (руб.)��6%   $
 * 6f    6J   �� *	Premium+6r     5%      $-	 'Районный коэффициент��6%   $
 * 6l    6C   �� *District_Coefficient+6x     5&      $	 Итого (руб.)��6%   $
 * 6V    6;   �� *In_All+6b   5(      �� 5)   
   -   5         ��Name$	 Add��5)      $��Добавить��6   $
��Left=�61   $	��Top=�6L   $	��150��6w   $��2��6�   $��20(�6�   ���#bb2032���#FFF J� Effect 5*      3  -System.Windows.Media.Effects.DropShadowEffect  5+      5-      �� 5.   
   -   5        $	 Del��5.      $��Удалить��6   $��Right=�60   $	��Top=�6L   $	��150��6w   $��2��6�   $��20(�6�   ���#bb2032���#FFF$��2��6�    5/        50      52      �� 53   
   -   5        $ BtnStaffing��53      $ ��Редактировать��6&   $	��150��6Y   $��2��6e   $��20(�6r   ���#bb2032���#FFF$
��Left=�6�   $	��Top=�6�   $��	325,0,0,0q�6�    54        55      57      �� 58   
   -   5         ��Name$ TBSearch��58       $��20(�6   $��3��62   $	��540��6?   $
��Left=�6L   ���#bb2032$��	130,0,0,0q�6|   �� 59   
   -   5        $ 	BtnSearch��59      $��
Найти��6$   $��3��64   $	��100��6A   ���#bb2032���#FFF$��Center=�6u   $��20(�6�   $��1��6�   �� 5:   
   -	   5        $ 	BtnOtchet��5:      $��
Отчёт��6$   $��3��64   $	��100��6A   ���#bb2032���#FFF$
��Left=�6u   $��10,0,0,0q�6�   $��20(�6�   5;      5<      