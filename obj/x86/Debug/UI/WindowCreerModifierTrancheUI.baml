   M S B A M L   `   `   `  ����  School Brain+    %Ecole.UI.WindowCreerModifierTrancheUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$*  $School brain : Définir des Tranches��5   	    � WindowStartupLocation$ CenterScreen=�65   ���	#FFD0DAE5$	��620��6q   $	��564��6~    � 
ResizeMode$ CanMinimize=�6�   .��5      � $	��539��6   ���	#FFC8DDE2$	��650��6.   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��
-2,3,0,467q�6.   .+�5      �� -   5        $��	groupBox1��5   G   $��Informations obligatoires��6   $	��141��6:   $	��380��6X   $��15,15q�6d   .��5	      � -   5        $��grid1��5	   #   $��95��6   $	��342��60   ���	#FF86B5E8 � RowDefinitions 5
      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      �� 5      .+�5      	� -   5        $��radioAutomatique��5   p   $��Automatique��6"   $��16��68   $
��Left=�6D   $��0,2,0,0q�6_   $	��Top=�6�   $	��119��6�   	� 5      -   5        $��radioManuelle��5   m   $��Manuelle��6"   $��16��65   $
��Left=�6A   $��0,2,0,0q�6\   $	��Top=�6�   $��92��6�   $��1��6�   �� 5      -   5        $��lblNbreTranche��5   w   $��Nombre de Tranches (*)��6   $��28��6=   $
��Left=�6I   $��	-2,20,0,0q�6d   $	��Top=�6�   $��2��6�   $	��150��6�   �� 5      -   5        $��txtNbreTranche��5   E   $��19��6   $
��Left=�6*   $	��Top=�6[   $	��120��6s   $��1��6   $��1��6�   6�   �� 5      -   5        $��lblCodeTrance��5   p   $��Code Tranche (*)��6   $��24��67   $
��Left=�6C   $��-2,0,0,0q�6^   $	��Top=�6�   $��2��6�   $��2��6�   $	��121��6�   �� 5      -   5        $��txtCodeTranche��5   E   $��23��6   $
��Left=�6*   $	��Top=�6[   $	��120��6s   $��1��6   $��2��6�   �� 5      -	   5        $��lblNom��5   U   $��Nom (*)��6   $��25��6.   $
��Left=�6:   $	��Top=�6c   $��2��6{   $	��119��6�   $��0,22,0,0q�6�   $��2��6�   �� 5      -
   5        $��txtNom��5   E   $��22��6   $
��Left=�6*   $	��Top=�6S   $	��120��6k   $��1��6w   $��3��6�   $��0,1,0,0q�6�   5      5      � 5      -   5        $��grid2��5       $	��103��6   $��97��6-   $��10,0,0,0q�68   ���	#FF86B5E8 5      �� 5       �� 5!       5#      �� 5$      .+�5&      �� -   5        $��
cmdValider��5&   R   $��Valider��6   $��30��6+   $
��Left=�67   $	��Top=�6d   $��85��6|   $��5,11,0,0q�6�   �� 5'      -   5        $��
cmdAnnuler��5'   R   $��Annuler��6   $��30��6+   $
��Left=�67   $	��Top=�6d   $��85��6|   $��1��6�   $��6,16,0,0q�6�   5(      5)      &   System.Windows.Controls.DataGrid  5*   
   -   5        $��grdListeTranche��5*   }      AutoGenerateColumns .     
IsReadOnly . $	��318��6A   $
��Left=�6N   $��
13,195,0,0q�6i   $	��Top=�6�   $	��512��6�   ��6�   �� *ListeTranches+   Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Code��6E   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 200 6S   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *codetranche+6S     5-      $		 Nom��6D   $	
 300 6Q    6%   �� *
nomtranche+6Q   5/      � 50   
   -   5        $��
gridAction��50      $��40��6   $��
0,539,0,69q�6-   ���	#FF86B5E8 51      �� 52       54      �� 55      �� 56      �� 57      .+�59      �� -   5        $��cmdImprimer��59   j   $��Imprimer la liste��6   $��31��61   $
��Left=�6=   $��36,5,0,0q�6X   $	��Top=�6}   $	��160��6�   $��2��6�   6�   �� 5:      -   5        $��	cmbFermer��5:   `   $��Fermer��6   $��31��6&   $
��Left=�62   $��	168,5,0,0q�6M   $	��Top=�6q   $	��160��6�   $��1��6�   $��2��6�   6�   5;      5<      5=      