   M S B A M L   `   `   `  ����  School Brain    Ecole.UI.DepartementUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$.  (School Brain : Gestion des départements��5   	   $	��474��69   $	��559��6F    � WindowStartupLocation$ CenterScreen=�6R   .��5      �� -   5        $��stackPanel1��5      $	��431��6   $	��526��61   ���	#FFC8DDE2.+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5        $��	groupBox1��5   G   $��Informations obligatoires��6   $	��130��6:   $	��380��6X   $
��Left=�6d   $��15,15q�6   .��5      � -   5        $��grid1��5   #   $��89��6   $	��342��60   ���	#FF86B5E8 � RowDefinitions 5	      �� 5
      $	��32* �6(   �� 5      $	��60* �6(    � ColumnDefinitions 5      �� 5      $
��134* �6+   �� 5      $
��231* �6+   .+�5      �� -   5        $��lblCodeDepartement��5   o   $��Code département��6   $��28��67   $
��Left=�6C   $��6,3,0,0q�6^   $	��Top=�6�   $	��120��6�   �� 5      -   5        $��lblNom��5   o   $	��Nom��6   $��1��6*   $��28��67   $
��Left=�6C   $��6,3,0,0q�6^   $	��Top=�6}   $	��120��6�   �� 5      -   5        $��txtCodeDepartement��5   f   $��1��6   $��23��6.   $
��Left=�6:   $��0,3,0,0q�6U   $	��Top=�6�   $	��179��6�   �� 5      -   5        $��txtNom��5   s   $��1��6   $��1��6.   $��23��6;   $
��Left=�6G   $��0,7,0,0q�6b   $	��Top=�6�   $	��179��6�   5      5      � 5      -   5        $��grid2��5       $	��126��6   $��85��6-   $��10,0,0,0q�68    5      �� 5      �� 5      �� 5       5      �� 5      .+�5       �� -	   5        $��
cmdValider��5    d   $��Valider��6   $��23��6+   $
��Left=�67   $��6,13,0,0q�6R   $	��Top=�6v   $��75��6�   6�   �� 5!      -
   5        $��	cmdAnuler��5!   p   $��Anuler��6   $��1��6*   $��23��67   $
��Left=�6C   $��6,15,0,0q�6^   $	��Top=�6�   $��75��6�   6�   �� 5"      -   5        $��	cmdFermer��5"   p   $��Fermer��6   $��2��6*   $��23��67   $
��Left=�6C   $��6,17,0,0q�6^   $	��Top=�6�   $��75��6�   6�   5#      5$      &   System.Windows.Controls.DataGrid  5%   
   -   5        $��grdListDepartements��5%   <      AutoGenerateColumns .  $	��200��6/   $	��505��6W   $��10q�6c      CanUserAddRows .     
IsReadOnly .  $� IsVirtualizing .  "	   VerticalScrollBarVisibility$	 Visible=�5&   m   
   Columns
 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn   Header$ Code du département��6B      
IsReadOnly . -   5        grdCode��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 220 5)   :   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5(   %   �� *
codeDept+5*        5+      $ Nom du département��6A    . -   5        
grdNom��$	 255 5,   9    5+   %   �� *	nomDept+5-      5/      � 50   
   -   5        $
��grid��50      $��40��6   $	��524��6'   $��0,10,0,0q�63   ���	#FF86B5E8 51      �� 52       54      �� 55      �� 56      �� 57      .+�59      �� -   5        $��cmdImprimer��59   k   $��Imprimer la liste��6   $��23��61   $
��Left=�6=   $��	183,9,0,0q�6X   $	��Top=�6~   $	��194��6�   $��3��6�   6�   5:      5;      5<      