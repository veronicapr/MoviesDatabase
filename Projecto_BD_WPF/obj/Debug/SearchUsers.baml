   M S B A M L   `   `   `  ����  Projecto_BD_WPF!    Projecto_BD_WPF.SearchUsers   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  SearchUsers��5      $	��500��6   $
��1000��6#    N� 
Background
 �����.��5
      � $	��500��6   $��	10,12,0,0q�6   $��Center=�6+   $	��Top=�6H   $
��1000��6`   $��7��6m   $��7��6~   
�������.+�5      �� $��Search Users:��6   $��40(�6$   $
��Bold"�62   $
��Left=�6D   $��
139,28,0,0q�6_   $	��Top=�6s   �� 5      $��
Username: ��6   $
��Left=�6!   $��
54,125,0,0q�6<   $	��Top=�6P   �� 5      $��Name: ��6   $
��Left=�6   $��
75,168,0,0q�68   $	��Top=�6L   �� 5      $��Birth Date: ��6   $
��Left=�6#   $��
54,211,0,0q�6>   $	��Top=�6R   �� 5      $��Email: ��6   $
��Left=�6   $��
79,252,0,0q�69   $	��Top=�6M   �� 5      $��	Country: ��6   $
��Left=�6    $��
65,297,0,0q�6;   $	��Top=�6O   �� 5   
   -   5        $��username��5      $
��Left=�6"   $��23��6=   $��142,128,0,0q�6I    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��204��6�   �� 5   
   -   5        $
��name��5      $
��Left=�6   $��23��69   $��142,171,0,0q�6E   $
 Wrap=�6Z   $	��Top=�6n   $	��204��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��
birth_date��5      $��139,212,654,263q�6'    J� RenderTransformOrigin$ 0.498,0.933&�6@   �� 5   
   -   5        $��email��5      $
��Left=�6   $��23��6:   $��142,255,0,0q�6F   $
 Wrap=�6[   $	��Top=�6o   $	��204��6�   �� 5   
   -   5        $��country��5      $
��Left=�6!   $��23��6<   $��142,300,0,0q�6H   $
 Wrap=�6]   $	��Top=�6q   $	��204��6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��search_result��5         
IsReadOnly . $
��Left=�6:   $��449,125,0,0q�6U   $	��Top=�6j   $	��343��6�   $	��517��6�   5      �� 5      -   5        $��search��5      $��Search��6   $
��Left=�6,   $��142,448,0,0q�6G   $	��Top=�6\   $��75��6t   6   �� 5      -   5        $��clear��5      $��Clear��6   $
��Left=�6*   $��271,448,0,0q�6E   $	��Top=�6Z   $��75��6r   6}   5       5!      