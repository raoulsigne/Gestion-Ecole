   M S B A M L   `   `   `  ����  School Brain$    Ecole.UI.BulletinTrimestrielUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$;  5School Brain : Impréssion des bulletins Trimestriels��5   	   $	��661��6F   $	��500��6S    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ CanMinimize=�6.   .��5      � ���	#FFC8DDE2$	��627��6"   .+�5	   
   �� -   5        $��gboxPrivilege��5	   h   $��Liste des élèves��6   $	��476��6-   $
��Left=�6:   $��	8,144,0,0q�6U   $	��Top=�6}   $	��463��6�   $
��Bold"�6�   
����ح�.��5
      � ���	#FF86B5E8.+�5      �� -   5        $��listBoxEleve��5   8   $��Stretch=�6   $��2,6,6,6q�6L    �� SelectionMode$ Multiple=�6]    $� IsVirtualizing .  ��5      �� 5      .R�5      � $	��0,2q�6    � ColumnDefinitions 5      �� 5      $��* �6/   �� 5      $	��200 �6/   .+�5   "   �� $��	chBxEleve��5   *   -   5        $��Center=�5   N   ��6+   �� *chaineAffichee+ d� 	IsChecked 5   %   ��  �� Path$ 
IsSelected� �� RelativeSource �� *TemplatedParent+	 �� Mode$	 TwoWay=�5   W   5      5      5   
   5    	   5!      �� 5"   
   -   5        $��
gboxGroupe��5"   k   $��Informations requises��6   $	��138��62   $
��Left=�6?   $��8,0,0,0q�6Z   $	��Top=�6}   $	��463��6�   $
��Bold"�6�   .��5#      � $	��122��6   ���	#FF86B5E8.+�5$      �� -   5        $��label1��5$   I   $��Classe ��6   $��23��6*   $��	6,2,389,0q�66   $	��Top=�6W   �� 5%      -   5        $��label2��5%   e   $��	Trimestre��6   $��24��6,   $
��Left=�68   $��5,29,0,0q�6S   $	��Top=�6s   $��73��6�   �� 5&      -   5        $��txtAnneeScolaire��5&   U   $��24��6   $
��Left=�6&   $��
154,58,0,0q�6A   $	��Top=�6m   $��91��6�   {�.  6�   �� 5'      -   5        $��	cmbClasse��5'   T   $��24��6   $
��Left=�6'   $��84,3,0,0q�6B   $	��Top=�6e   $	��161��6}   6�   �� 5(      -	   5        $��chBxTousCocher��5(   |   $%��Sélectionner tous les élèves��6   $��20��6B   $
��Left=�6N   $��	12,89,0,0q�6i   $	��Top=�6�   $	��182��6�   6�   �� 5)      -
   5        $��cmbTrimestre��5)   U   $��24��6   $
��Left=�6'   $��	83,29,0,0q�6B   $	��Top=�6i   $	��162��6�   �� 5*      -   5        $��txtAnnee��5*   e   $��24��6   $
��Left=�6&   {�. $��	83,58,0,0q�6R   $	��Top=�6u   $��65��6�   6�   �� 5+      -   5        $��label3��5+   a   $��Année��6   $��24��6(   $
��Left=�64   $��7,59,0,0q�6O   $	��Top=�6o   $��73��6�   �� 5,      -   5        $��	btnFermer��5,   *   $��Fermer��6   $��30��6;   $��Right=�6G   $��0,39,7,0q�6c   $	��Top=�6u   $	��125��6�   6�   � 5-      -   5        $��ProgressBar1��5-   @   $��22��6   $��216,87,29,13q�6*   6@   5.      5/      �� 50   
   -   5        $��btnImprimer��50   .   $��Imprimer Bulletins��6   $��30��6A   $
��Left=�6M   $��
333,20,0,0q�6h   $	��Top=�6|   $	��125��6�   $
��Bold"�6�   6�   51      52      