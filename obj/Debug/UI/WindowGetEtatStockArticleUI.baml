   M S B A M L   `   `   `  ����  School Brain*    $Ecole.UI.WindowGetEtatStockArticleUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$8  2School brain : Obtenir l'état du stock d'articles��5   	   $	��677��6C   $	��997��6P    � WindowStartupLocation$ CenterScreen=�6\   ���	#FFD0DAE5.��5	      � ���	#FFC8DDE2$��Center=�6"   $��Center=�6?   $	��989��6Z   $	��638��6f   .+�5
   
   �� -   5        $��
rectangle1��5
   N   $	��124��6   $
��Left=�6!   $��10,7,0,0q�6<   
���   �$	��Top=�6o   $	��311��6�   ���	#FF86B5E86�   �� 5   
   -   5        $��lblCategorieArticle��5   j   $��Catégorie Article (*)��6   $��28��60   $
��Left=�6<   $��	27,14,0,0q�6W   $	��Top=�6�   $	��114��6�   �� 5   
   -   5        $��cmbCategorieArticle��5   N   $��28��6   $
��Left=�6   $��
144,14,0,0q�6:   $	��Top=�6i   $	��157��6�   6�   �� 5   
   -   5        $��
cmdValider��5   ]   $��Valider��6   $��25��6#   $
��Left=�6/   $��	27,98,0,0q�6J   $	��Top=�6o   $	��114��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��GrdListEtatStock��5   }      AutoGenerateColumns .     
IsReadOnly . $	��407��6A   $
��Left=�6N   $��
10,148,0,0q�6i   $	��Top=�6�   $	��964��6�    $� IsVirtualizing .  ��6�   �� *ListeEtatStocks+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Date Opération��6M      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6e   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *dateOperationString+6e     5      $ Magasin��6E   $	 100 6V    6%   �� *codeMagasin+6V     5      $ Article��6E   $	 100 6V    6%   �� *codeArticle+6V     5      $ Désignation��6L   $	 200 6a    6%   �� *designationArticle+6a     5      $ Stock Debut exercice��6D   $	 150 6b    6%   �� *
stockDebut+6b     5      $ Quantité achetée��6I   $	 100 6c    6%   �� *quantiteAchetee+6c     5      $ Quantité Vendue��6H   $	 100 6a    6%   �� *quantiteVendue+6a     5      $ Stock Restant��6F   $	 100 6]    6%   �� *stockRestant+6]   5      � 5   
   -   5        $��grid1��5   I   $��50��6   $
��Left=�6   $��	0,586,0,0q�66   $	��Top=�6V   $	��985��6n   ���	#FF86B5E8.+�5      �� -   5        $��	cmdFermer��5   M   $��Fermer��6   $��32��6&   $
��Left=�62   $	��Top=�6^   $	��112��6v   $��	571,9,0,0q�6�   6�   �� 5      -   5        $��cmdImprimer��5   a   $��Imprimer��6   $��32��6(   $
��Left=�64   $��10,9,0,0q�6O   $	��Top=�6t   $	��122��6�   6�   5      �� 5   
   -	   5        $��label1��5   `   $��Article (*)��6   $��28��6&   $
��Left=�62   $��	81,48,0,0q�6M   $	��Top=�6n   �� 5    
   -
   5        $��
cmbArticle��5    N   $��28��6   $
��Left=�6   $��
144,48,0,0q�6:   $	��Top=�6`   $	��157��6x   6�   �� 5!   
   -   5        $��
cmdAnnuler��5!   ^   $��Annuler��6   $��24��6#   $
��Left=�6/   $��
180,98,0,0q�6J   $	��Top=�6p   $	��121��6�   6�   �� 5"   
   -   5        $��lblFilterCodeArticle��5"   c   $��code article��6   $��28��6'   $
��Left=�63   $��362,114,0,0q�6N   $	��Top=�6   $��90��6�   �� 5#   
   -   5        $��txtFilterCodeArticle��5#   N   $��23��6   $
��Left=�6   $��450,114,0,0q�69   $	��Top=�6j   $	��120��6�   6�   �� 5$   
   -   5        $��lblFilterDateAchat��5$   e   $��Date Opération��6   $��28��6)   $
��Left=�65   $��615,114,0,0q�6P   $	��Top=�6   $��92��6�   (  "System.Windows.Controls.DatePicker  5%   
   -   5        $��datePickerFilterDateAchat��5%   Q   $��25��6   $
��Left=�6!   $��713,115,0,0q�6<   $	��Top=�6r   $	��137��6�   6�   5&      5'      