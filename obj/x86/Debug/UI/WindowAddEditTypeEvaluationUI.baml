   M S B A M L   `   `   `  ����  School Brain,    &Ecole.UI.WindowAddEditTypeEvaluationUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$;  5School brain : Creer / Editer des Types d'évaluation��5   	   $	��636��6F   $	��584��6S    � WindowStartupLocation$ CenterScreen=�6_   ���	#FFD0DAE5 � 
ResizeMode$ CanMinimize=�6�   .��5	      � $	��558��6   ���	#FFC8DDE2$	��651��6.   .+�5
   
   ��  �� Orientation$ 
Horizontal=�6   $��
-2,3,0,495q�6.   .+�5      �� -   5        $��	groupBox1��5   G   $��Informations obligatoires��6   $	��122��6:   $	��380��6X   $��15,15q�6d   .��5      � -   5        $��grid1��5   #   $��68��6   $	��342��60   ���	#FF86B5E8 � RowDefinitions 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      $
��124* �6+   �� 5      $
��218* �6+   .+�5      �� -   5        $��txtCode��5   V   $��26��6   $
��Left=�6*   $��0,6,0,0q�6E   $	��Top=�6e   $	��196��6}   $��1��6�   �� 5      -   5        $��lblCode��5   J   $��Code (*)��6   $
��Left=�6/   $��28��6Y   $	��Top=�6e   $	��108��6}   $��0,4,0,0q�6�   �� 5      -   5        $��lblNom��5   U   $��Nom (*)��6   $��28��6.   $
��Left=�6:   $��Bottom=�6c   $��1��6~   $	��108��6�   �� 5      -   5        $��txtNom��5   <   $��24��6   $��0,6,22,0q�6*   $	��Top=�6J   $��1��6b   $��1��6r   5      5      � 5      -   5        $��grid2��5       $	��101��6   $	��128��6-   $��10q�69   ���	#FF86B5E8 5      �� 5      �� 5       5       �� 5!      .+�5#      �� -   5        $��cmdEnregistrer��5#   d   $��Valider��6   $��35��6+   $
��Left=�67   $��11,6,0,0q�6R   $	��Top=�6z   $	��111��6�   6�   �� 5$      -	   5        $��
cmdAnnuler��5$   e   $��Annuler��6   $��34��6+   $
��Left=�67   $��	11,10,0,0q�6R   $	��Top=�6w   $	��111��6�   $��1��6�   5%      5&      &   System.Windows.Controls.DataGrid  5'   
   -
   5        $��grdListeTypeEvaluation��5'   }      AutoGenerateColumns .     
IsReadOnly . $	��354��6A   $
��Left=�6N   $��
12,170,0,0q�6i   $	��Top=�6�   $	��534��6�   ��6�   �� *ListeTypeEvaluations+   Columns 5(      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 code��6H   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 150 6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *codeevaluation+6V     5*      $		 Nom��6A   $	
 370 6N    6%   �� *	nomeval+6N   5,      � 5-   
   -   5        $��
gridAction��5-      $��40��6   $��-2,556,0,55q�6-   ���	#FF86B5E8 5.      �� 5/       51      �� 52      �� 53      .+�55      �� -   5        $��cmdImprimer��55   j   $��Imprimer la liste��6   $��28��61   $
��Left=�6=   $��14,6,0,0q�6X   $	��Top=�6}   $	��160��6�   6�   �� 56      -   5        $��	cmdFermer��56   `   $��Fermer��6   $��28��6&   $
��Left=�62   $��	108,6,0,0q�6M   $	��Top=�6q   $	��160��6�   $��1��6�   6�   57      58      59      