   M S B A M L   `   `   `  ����  School Brain$    Ecole.UI.CertificatScolariteUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$>  8School Brain : Impréssion des Certificats de scolarité��5   	   $	��661��6H   $	��500��6U    � WindowStartupLocation$ CenterScreen=�5   	    � 
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
��Bold"�6�   .��5#      � $	��122��6   ���	#FF86B5E8.+�5$      �� -   5        $��label1��5$   I   $��Classe ��6   $��23��6*   $��	6,2,389,0q�66   $	��Top=�6W   �� 5%      -   5        $��txtAnneeScolaire��5%   U   $��24��6   $
��Left=�6&   $��
154,36,0,0q�6A   $	��Top=�6m   $��91��6�   {�.  6�   �� 5&      -   5        $��	cmbClasse��5&   T   $��24��6   $
��Left=�6'   $��84,3,0,0q�6B   $	��Top=�6e   $	��161��6}   6�   �� 5'      -   5        $��chBxTousCocher��5'   |   $%��Sélectionner tous les élèves��6   $��20��6B   $
��Left=�6N   $��	12,77,0,0q�6i   $	��Top=�6�   $	��182��6�   6�   �� 5(      -	   5        $��txtAnnee��5(   e   $��24��6   $
��Left=�6&   {�. $��	83,36,0,0q�6R   $	��Top=�6u   $��65��6�   6�   �� 5)      -
   5        $��label3��5)   a   $��Année��6   $��24��6(   $
��Left=�64   $��7,37,0,0q�6O   $	��Top=�6o   $��73��6�   �� 5*      -   5        $��	btnFermer��5*   *   $��Fermer��6   $��30��6;   $��Right=�6G   $��0,39,7,0q�6c   $	��Top=�6u   $	��125��6�   6�   � 5+      -   5        $��ProgressBar1��5+   @   $��22��6   $��221,85,24,15q�6*   6@   5,      5-      �� 5.   
   -   5        $��btnImprimer��5.   0   $��Imprimer Certificats��6   $��30��6C   $
��Left=�6O   $��
330,20,0,0q�6j   $	��Top=�6~   $	��128��6�   $
��Bold"�6�   6�   5/      50      