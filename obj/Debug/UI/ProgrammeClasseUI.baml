   M S B A M L   `   `   `  ����  School Brain     Ecole.UI.ProgrammeClasseUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$7  1School Brain : Imprimer le programme d'une classe��5   	   $	��625��6C   $	��569��6P    � WindowStartupLocation$ CenterScreen=�6\    � 
ResizeMode$ CanMinimize=�6�   .��5      �� -   5        $��stackPanel1��5      $	��586��6   $	��543��61   ���	#FFC8DDE2.+�5   
   �� -   5        $��	groupBox1��5   C   $��Informations obligatoires��6   $	��130��66   $	��514��6T   $
��Left=�6`   $��	15,10,0,0q�6{   .��5      � -   5        $��grid1��5      $��89��6   $	��495��6,   ���	#FF86B5E8 � RowDefinitions 5      �� 5	      �� 5
      �� 5       � ColumnDefinitions 5      �� 5      $
��165* �6'   �� 5      $
��182* �6'   �� 5      $
��148* �6'   .+�5      �� -   5        $��lblAnneeScolaire��5   j   $��Année scolaire��6   $��28��61   $
��Left=�6=   $��0,28,0,0q�6X   $	��Top=�6�   $	��114��6�   $��2��6�   �� 5      -   5        $��	lblClasse��5   P   $��Classe��6   $��28��6)   $
��Left=�65   $	��Top=�6a   $	��114��6y   �� 5      -   5        $��	cmbClasse��5   c   $��1��6   $��23��6+   $
��Left=�67   $��0,5,0,0q�6R   $	��Top=�6t   $	��169��6�   6�   	� 5      -   5        $��radioSansProf��5   �   $��Sans enseignants��6    	� 	GroupName$
 type��69   $��2��6J   $��16��6W   $
��Left=�6c   $��6,10,0,0q�6~   $	��Top=�6�   $	��120��6�   6�   	� 5      -   5        $��radioAvecProf��5   �   $��Avec enseignants��6   $
 type��69   $��1��6J   $��2��6Z   $��16��6g   $
��Left=�6s   $��14,9,0,0q�6�   $	��Top=�6�   $	��168��6�   6�   �� 5      -	   5        $��txtAnnee��5   K   $��23��6   {�.  $��	74,7,13,0q�68   $	��Top=�6[   $��1��6s   $��1��6�   $��2��6�   �� 5      -
   5        $��txtAnneeScolaire��5   R   $��23��6   $
��Left=�6&   $��0,7,0,0q�6A   $	��Top=�6j   $��68��6�   $��1��6�   $��1��6�   $��2��6�   6�   5      5      &   System.Windows.Controls.DataGrid  5&   
   -   5        $��grdProgrammes��5&   <      AutoGenerateColumns .  $	��343��6/   $	��510��6Q   "   VerticalScrollBarVisibility$ Visible=�6]      CanUserAddRows .  $��20q�5'   *   	 $� IsVirtualizing	 .  
   Columns
 5(      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn   Header$ Matiere��6A      
IsReadOnly . -   5        
grdMatiere��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 200 5*   =   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5)   %   �� *	matiere+5+        5,      $ 
Coeficient��6D    . -   5        grdCoeficient��$	 100 5-   @    5,   %   �� *
coeficient+5.        5/      $ 
Enseignant��50      -   5        grdEnseignant�� . $	 200 50   T    5/   %   �� *
enseignant+51      53      � 54   
   -   5        $��grid3��54      $��41��6   $	��543��6(   ���	#FF86B5E8$
��0,20q�6K    55      �� 56       58      �� 59      �� 5:      .+�5<      �� -   5        $��cmdImprimer��5<   b   $��Imprimer��6   $��23��6(   $
��Left=�64   $��	15,12,0,0q�6O   $	��Top=�6u   $	��150��6�   6�   �� 5=      -   5        $��	cmdFermer��5=   q   $��Fermer��6   $��1��6&   $��23��66   $
��Left=�6B   $��
112,13,0,0q�6]   $	��Top=�6�   $	��146��6�   6�   5>      5?      5@      