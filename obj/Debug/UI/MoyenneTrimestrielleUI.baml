   M S B A M L   `   `   `  ����  School Brain%    Ecole.UI.MoyenneTrimestrielleUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$,  &School Brain : Moyennes Trimestrielles��5   	   $	��658��68   $
��1028��6E    � WindowStartupLocation$ CenterScreen=�6R    � 
ResizeMode$ CanMinimize=�6w   .��5      �� -   5        $��stackPanel1��5      $	��617��6   $
��1007��61   ���	#FFC8DDE2.+�5   
   � $	��173��6   .+�5      �� -   5        $��	groupBox1��5   G   $��Informations obligatoires��6   $	��161��6:   $	��514��6X   $
��Left=�6d   $��	15,10,0,0q�6   .��5      � -   5        $��grid1��5   $   $	��131��6   $	��495��61   ���	#FF86B5E8 � RowDefinitions 5	      �� 5
      �� 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      $
��165* �6+   �� 5      $
��182* �6+   �� 5      $
��148* �6+   .+�5      �� -   5        $��lblAnneeScolaire��5   o   $��Année scolaire��6   $��28��65   $
��Left=�6A   $��	-3,10,0,0q�6\   $	��Top=�6�   $	��114��6�   $��2��6�   $��2��6�   �� 5      -   5        $��	lblClasse��5   T   $��Classe��6   $��28��6-   $
��Left=�69   $	��Top=�6e   $	��114��6}   �� 5      -   5        $��	cmbClasse��5   g   $��1��6   $��23��6/   $
��Left=�6;   $��0,5,0,0q�6V   $	��Top=�6x   $	��169��6�   6�   	� 5      -   5        $��radioOrdreMerite��5   �   $��Ordre de mérite��6"    	� 	GroupName$
 type��6<   $��3��6M   $��16��6Z   $
��Left=�6f   $��6,12,0,0q�6�   $	��Top=�6�   $	��120��6�   6�   	� 5      -   5        $��radioOrdreAlphabetique��5   �   $��Ordre alphabétique��6"   $
 type��6?   $��1��6P   $��3��6`   $��16��6m   $
��Left=�6y   $��	14,11,0,0q�6�   $	��Top=�6�   $	��168��6�   6�   �� 5      -	   5        $��lblTrimestre��5   u   $��	Trimestre��6   $��1��60   $��28��6=   $
��Left=�6I   $��0,6,0,0q�6d   $	��Top=�6�   $	��126��6�   �� 5      -
   5        $��cmbTrimestre��5   t   $��1��6   $��1��6/   $��23��6<   $
��Left=�6H   $��0,6,0,0q�6c   $	��Top=�6�   $	��169��6�   6�   �� 5      -   5        $��txtAnnee��5   P   $��23��6   {�.  $��
74,10,13,0q�6<   $	��Top=�6`   $��1��6x   $��2��6�   �� 5      -   5        $��txtAnneeScolaire��5   W   $��23��6   $
��Left=�6*   $��0,10,0,0q�6E   $	��Top=�6o   $��68��6�   $��1��6�   $��2��6�   6�   5      5      �� 5      -   5        $��image2��5   +   $��840,11,22,100q�6   $<��6/School%20Brain;component/Images/Logo-Brain-Studio.png��69   $
��Fill=�6y   5       &   System.Windows.Controls.DataGrid  5!   
   -   5        $��grdListe��5!   <      AutoGenerateColumns .  $	��343��6/   $	��964��6L   "   VerticalScrollBarVisibility$ Visible=�6X      CanUserAddRows .  $��20q�5"   *   	 $� IsVirtualizing	 .  
 � 	Resources
 5#      )  #System.Windows.Controls.DataGridRow �� 
TargetType �� Property �� Value��%a   '        �� 5$      "  6   .�5%      ��  �@J�IsHitTestVisible .  69   5&      5'         Columns 5(      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Matricule��6C      
IsReadOnly . -   5        grdMatricule��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 5*   ?   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5)   %   �� *	matricule+5+      4  .System.Windows.Controls.DataGridTemplateColumn  5,      $	 240 6)      HeaderTemplate 5-      �� 5.      .R�5/      ��  �� Orientation$ 
Horizontal=�6)   .+�50   "   �� $	��Nom��6(   $��25��66   $
��20,0q�6B   6R   �� 51   "   $��NameSearchBox��6*   -   5        $	��100��51   ]   ��6?   �� *FilterString+6i   52      53         CellTemplate 55      �� 56      .R�57      �� ��6(   �� *nom+58      5:        5;      $ Moyenne��5<      -   5        grdNote�� . $ 80 5<   K    5;   %   �� *note+5=        5>      $
 Coef��5?      -   5        grdCoef�� . $ 50 5?   H    5>   %   �� *coef+5@        5A      $ Total��5B      -   5        grdTotal�� . $ 80 5B   J    5A   %   �� *total+5C        5D      $
 Rang��5E      -   5        grdRang�� . $ 50 5E   H    5D   %   �� *rang+5F        5G      $ Mention��5H      -   5        grdAppreciation�� . $	 150 5H   S    5G   %   �� *appreciation+5I        5J      $" Appreciation de l'Enseignant��5K      -   5        grdAppreciationEnseignant�� . $	 190 5K   r    5J   %   �� *appreciationEnseignant+5L      5N      � 5O   
   -   5        $��grid3��5O      $��41��6   $
��1003��6(   ���	#FF86B5E8$
��0,20q�6L    5P      �� 5Q       5S      �� 5T      �� 5U      .+�5W      �� -   5        $��cmdImprimer��5W   b   $��Imprimer��6   $��23��6(   $
��Left=�64   $��	20,12,0,0q�6O   $	��Top=�6u   $	��150��6�   6�   �� 5X      -   5        $��	cmdFermer��5X   q   $��Fermer��6   $��1��6&   $��23��66   $
��Left=�6B   $��
336,12,0,0q�6]   $	��Top=�6�   $	��146��6�   6�   5Y      5Z      5[      