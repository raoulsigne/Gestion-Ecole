   M S B A M L   `   `   `  ����  School Brain%    Ecole.UI.WindowDefinirMentionUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$-  'School brain : Définition des mentions��5   	   $	��628��68   $	��591��6E    � WindowStartupLocation$ CenterScreen=�6Q   ���	#FFD0DAE5 � 
ResizeMode$ CanMinimize=�6�   .��5      � $	��569��6   ���	#FFC8DDE2$	��585��6.   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��
-2,3,0,400q�6.   .+�5      �� -   5        $��	groupBox1��5   G   $��Informations obligatoires��6   $	��167��6:   $	��380��6X   $
��15,5q�6d   .��5      � -   5        $��grid1��5   $   $	��109��6   $	��342��61   ���	#FF86B5E8 � RowDefinitions 5	      �� 5
      �� 5      �� 5       � ColumnDefinitions 5      �� 5      $	��87* �6+   �� 5      $
��255* �6+   .+�5      �� -   5        $��
lblNoteMax��5   Z   $��Note Max (*)��6   $��28��63   $
��Left=�6?   $	��Top=�6l   $��87��6�   $��-1,6,0,0q�6�   $��1��6�   �� 5      -   5        $��
lblNoteMin��5   m   $��Note Min (*)��6   $��28��63   $
��Left=�6?   $��	-1,0,0,11q�6Z   $��Bottom=�6   $��88��6�   �� 5      -   5        $��
lblMention��5   M   $��Mention (*)��6   $
��Left=�62   $��28��6_   $	��Top=�6k   $��2��6�   $��87��6�   $��0,4,0,0q�6�   �� 5      -   5        $��
txtNoteMin��5   *   $��23��6   $��1��6<   $��28,6,107,11q�6L   6a   �� 5      -   5        $��
txtNoteMax��5   t   $��1��6   $��1��6.   $��23��6;   $
��Left=�6G   $��28,7,0,0q�6b   $	��Top=�6�   $	��120��6�   6�   �� 5      -   5        $��
txtMention��5   d   $��2��6   $��23��6+   $
��Left=�67   $��28,6,0,0q�6R   $	��Top=�6v   $	��171��6�   $��1��6�   5      5      � 5      -	   5        $
��grid��5       $	��157��6   ���	#FF86B5E8$	��116��6C    5      �� 5       5      �� 5      $	��74* �6'   .+�5!      �� -
   5        $��
cmdAjouter��5!   R   $��Valider��6   $��28��6+   $
��Left=�67   $	��Top=�6d   $	��104��6|   $��6,24,0,0q�6�   �� 5"      -   5        $��
cmdAnnuler��5"   R   $��Annuler��6   $��28��6+   $
��Left=�67   $	��Top=�6d   $	��104��6|   $��	6,105,0,0q�6�   5#      5$      &   System.Windows.Controls.DataGrid  5%   
   -   5        $��grdListeMention��5%   }      AutoGenerateColumns .     
IsReadOnly . $	��295��6A   $
��Left=�6N   $��
12,207,0,0q�6i   $	��Top=�6�   $	��548��6�   ��6�   �� *ListeMentions+   Columns 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Note Min��6A   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 175 6S   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *	noteMin+6S     5(      $	 Note Max��6A   $	
 175 6S    6%   �� *	noteMax+6S     5)      $	 Mention��6A   $	
 185 6R    6%   �� *	mention+6R   5+      � 5,   
   -   5        $��
gridAction��5,      $��40��6   $��	0,545,0,0q�6-   ���	#FF86B5E8 5-      �� 5.       50      �� 51      �� 52      .+�54      �� -   5        $��cmdImprimer��54   b   $��Imprimer��6   $��22��6(   $
��Left=�64   $��	13,10,0,0q�6O   $	��Top=�6u   $	��118��6�   6�   �� 55      -   5        $��	cmdFermer��55   a   $��Fermer��6   $��27��6&   $
��Left=�62   $��
171,10,0,0q�6M   $	��Top=�6r   $	��104��6�   $��1��6�   6�   56      57      58      