   M S B A M L   `   `   `  ����  School Brain,    &Ecole.UI.WindowAddEditCategorieEleveUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$?  9School brain : Creer / Modifier des catégories d'élève��5   	   $	��637��6H   $	��598��6U    � WindowStartupLocation$ CenterScreen=�6a   ���	#FFD0DAE5 � 
ResizeMode$ CanMinimize=�6�   .��5      � $	��578��6   ���	#FFC8DDE2$	��597��6.   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��-2,3,13,441q�6.   .+�5      �� -   5        $��	groupBox1��5   G   $��Informations obligatoires��6   $	��124��6:   $	��380��6X   $��15,15q�6d   .��5      � -   5        $��grid1��5   #   $��69��6   $	��342��60   ���	#FF86B5E8 � RowDefinitions 5	      �� 5
      �� 5       � ColumnDefinitions 5      �� 5      $
��124* �6+   �� 5      $
��218* �6+   .+�5      �� -   5        $��txtCode��5   V   $��25��6   $
��Left=�6*   $��0,3,0,0q�6E   $	��Top=�6e   $	��184��6}   $��1��6�   �� 5      -   5        $��txtNom��5   E   $��22��6   $
��Left=�6*   $	��Top=�6S   $	��184��6k   $��1��6w   $��1��6�   $��0,6,0,0q�6�   �� 5      -   5        $��lblCode��5   h   $��Code (*)��6   $��28��6/   $
��Left=�6;   $��0,-1,0,0q�6V   $	��Top=�6w   $��97��6�   �� 5      -   5        $��lblNom��5   U   $��Nom (*)��6   $��28��6.   $
��Left=�6:   $	��Top=�6c   $��95��6{   $��1��6�   5      5      � 5      -   5        $��grid2��5       $	��106��6   $	��118��6-   $��10q�69   ���	#FF86B5E8 5      �� 5      �� 5       5      �� 5      $	��36* �6'   �� 5      $��8* �6'   �� 5      $	��74* �6'   .+�5!      �� -   5        $��cmdEnregistrer��5!   d   $��Valider��6   $��32��6+   $
��Left=�67   $��6,13,0,0q�6R   $	��Top=�6z   $	��106��6�   $��3��6�   �� 5"      -	   5        $��
cmdAnnuler��5"   +   $��Annuler��6   $	��106��6=   $��3��6b   $��8,12,4,7q�6v   $��1��6�   5#      5$      &   System.Windows.Controls.DataGrid  5%   
   -
   5        $��grdListeCategorieEleve��5%   }      AutoGenerateColumns .     
IsReadOnly . $	��357��6A   $
��Left=�6N   $��
13,171,0,0q�6i   $	��Top=�6�   $	��553��6�    $� IsVirtualizing .  ��6�   �� *ListeCategorieEleves+	   Columns	 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn
   Header$

 Code��6F      Width5  /System.Windows.Controls.DataGridLengthConverter$	 230 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *codeCatEleve+6T     5(      $	
 Nom��6E   $	 310 6R    6%   �� *nomCatEleve+6R   5*      � 5+   
   -   5        $
��grid��5+      $��40��6   $��	3,554,1,0q�6'   ���	#FF86B5E8 5,      �� 5-      $	��20* �6     5/      �� 50      $
��182* �6#   �� 51      $	��88* �6#   �� 52      $	��95* �6#   �� 53      $
��175* �6#   .+�55      �� -   5        $��	cmdFermer��55   _   $��Fermer��6   $��29��6&   $
��Left=�62   $��17,5,0,0q�6M   $	��Top=�6p   $	��152��6�   $��3��6�   $��2��6�   �� 56      -   5        $��cmdImprimer��56   `   $��Imprimer��6   $��28��6(   $
��Left=�64   $��6,6,0,0q�6O   $	��Top=�6s   $	��142��6�   $��2��6�   6�   57      58      59      