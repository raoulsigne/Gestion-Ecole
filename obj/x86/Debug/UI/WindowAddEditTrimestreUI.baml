   M S B A M L   `   `   `  ����  School Brain'    !Ecole.UI.WindowAddEditTrimestreUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$2  ,School brain : Creer / Editer des Trimestres��5   	   $	��638��6>   $	��633��6K    � WindowStartupLocation$ CenterScreen=�6W   ���	#FFD0DAE5 � 
ResizeMode$ CanMinimize=�6�   .��5	      � ���	#FFC8DDE2$��Center=�6"   $��Center=�6?   $	��607��6Z   $	��598��6f   .+�5
   
   ��  �� Orientation$ 
Horizontal=�6   $��-2,3,23,459q�6.   .+�5      �� -   5        $��	groupBox1��5   G   $��Informations obligatoires��6   $	��119��6:   $	��380��6X   $
��15,5q�6d   .��5      � -   5        $��grid1��5   #   $��72��6   $	��342��60   ���	#FF86B5E8 � RowDefinitions 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      $
��124* �6+   �� 5      $
��218* �6+   .+�5      �� -   5        $��txtCode��5   V   $��25��6   $
��Left=�6*   $��0,3,0,0q�6E   $	��Top=�6e   $	��184��6}   $��1��6�   �� 5      -   5        $��txtNom��5   E   $��22��6   $
��Left=�6*   $	��Top=�6S   $	��184��6k   $��1��6w   $��1��6�   $��0,6,0,0q�6�   �� 5      -   5        $��lblCode��5   h   $��Code (*)��6   $��28��6/   $
��Left=�6;   $��0,-1,0,0q�6V   $	��Top=�6w   $��97��6�   �� 5      -   5        $��lblNom��5   U   $��Nom (*)��6   $��28��6.   $
��Left=�6:   $	��Top=�6c   $��95��6{   $��1��6�   5      5      � 5      -   5        $��grid2��5       $	��106��6   $	��118��6-   $��10q�69   ���	#FF86B5E8 5      �� 5      �� 5       5       �� 5!      $	��36* �6'   �� 5"      $��8* �6'   �� 5#      $	��74* �6'   .+�5%      �� -   5        $��
cmdValider��5%   d   $��Valider��6   $��34��6+   $
��Left=�67   $��6,10,0,0q�6R   $	��Top=�6v   $	��106��6�   $��3��6�   �� 5&      -	   5        $��
cmdAnnuler��5&   +   $��Annuler��6   $	��106��6=   $��3��6b   $��6,14,6,6q�6v   $��1��6�   5'      5(      � 5)   
   -
   5        $��
gridAction��5)      $��40��6   $��15,157,24,401q�6-   ���	#FF86B5E8 5*      �� 5+       5-      �� 5.      $	��88* �6#   �� 5/      $
��200* �6#   �� 50      $
��144* �6#   �� 51      $
��144* �6#   .+�53      �� -   5        $��lblFilterCode��53   [   $
��Code��6   $��28��6#   $
��Left=�6/   $��0,6,0,0q�6J   $	��Top=�6p   $��80��6�   �� 54      -   5        $��cmbFilterCode��54   O   $��25��6   $
��Left=�6#   $��8,6,0,0q�6>   $	��Top=�6d   $	��153��6|   $��1��6�   �� 55      -   5        $��cmbFilterNom��55   Q   $��25��6   $
��Left=�6#   $��	128,6,0,0q�6>   $	��Top=�6e   $	��135��6}   $��2��6�   $��2��6�   �� 56      -   5        $��lblFilterNom��56   [   $	��Nom��6   $��28��6"   $
��Left=�6.   $��84,6,0,0q�6I   $	��Top=�6o   $��2��6�   57      &   System.Windows.Controls.DataGrid  58   
   -   5        $��grdListeTrimestre��58   }      AutoGenerateColumns .     
IsReadOnly . $	��335��6A   $
��Left=�6N   $��
15,197,0,0q�6i   $	��Top=�6�   $	��567��6�   ��6�   �� *ListeTrimestres+   Columns 59      0  *System.Windows.Controls.DataGridTextColumn  5:      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Code��6G   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 140 6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *codetrimestre+6U     5;      $		 Nom��6F   $	
 415 6S    6%   �� *nomtrimestre+6S   5N      � 5O   
   -   5        $
��grid��5O      $��40��6   $��
-2,554,1,0q�6'   ���	#FF86B5E8 5P      �� 5Q      $	��20* �6     5S      �� 5T      $
��182* �6#   �� 5U      $	��88* �6#   �� 5V      $	��95* �6#   �� 5W      $
��175* �6#   .+�5Y      �� -   5        $��	cmdFermer��5Y   _   $��Fermer��6   $��29��6&   $
��Left=�62   $��23,5,0,0q�6M   $	��Top=�6p   $	��152��6�   $��3��6�   �� 5Z      -   5        $��cmdImprimer��5Z   a   $��Imprimer��6   $��28��6(   $
��Left=�64   $��17,6,0,0q�6O   $	��Top=�6t   $	��142��6�   6�   5[      5\      5]      