   M S B A M L   `   `   `  ����  School Brain!    Ecole.UI.ChangementClasseUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$)  #School Brain : Changement de classe��5   	   $	��632��65   $	��849��6B    � WindowStartupLocation$ CenterScreen=�6N    � 
ResizeMode$ CanMinimize=�6s   .��5      �� -   5        $��stackPanel1��5      $	��594��6   $	��823��61   ���	#FFC8DDE2.+�5   
   �� -   5        $��	groupBox1��5   ?   $��Critères de sélection��6   $	��130��62   $	��668��6P   $
��Left=�6\   $��	15,10,0,0q�6w   .��5      � -   5        $��grid1��5      $��89��6   $	��649��6,   ���	#FF86B5E8 � RowDefinitions 5      �� 5	      �� 5
      �� 5       � ColumnDefinitions 5      �� 5      $
��269* �6'   �� 5      $
��380* �6'   .+�5      �� -   5        $��	lblClasse��5   P   $��Classe��6   $��28��6)   $
��Left=�65   $	��Top=�6a   $	��145��6y   $��Center=�6�   $��39,0,0,0q�6�   �� 5      -   5        $��lblAnnee��5   S   $��Année��6   $��1��6(   $
��Left=�68   $	��141��6c   $��Center=�6o   $��0,0,0,2q�6�   �� 5      -   5        $��	cmbClasse��5   a   $��1��6   $��23��6(   $
��Left=�64   $��54,1,0,0q�6O   $	��Top=�6r   $	��120��6�   6�   �� 5      -   5        $��txtAnnee��5   �   {�.  $��1��6,   $��1��6<   $��23��6I   $��Right=�6U   $��	0,4,242,0q�6q   $	��Top=�6�   $��62��6�   6�   �� 5      -   5        $��
cmdAnnuler��5   �   $��Ré-initialiser��6   $��1��62   $��2��6B   $��23��6O   $
��Left=�6[   $��18,5,0,0q�6v   $	��Top=�6�   $	��120��6�   6�   �� 5      -	   5        $��cmdOK��5   l   $��OK��6   $��2��6&   $��23��63   $
��Left=�6?   $��54,6,0,0q�6Z   $	��Top=�6y   $	��120��6�   6�   �� 5      -
   5        $��txtAnneeScolaire��5   S   $��23��6   $
��Left=�6&   $��18,3,0,0q�6A   $	��Top=�6k   $��52��6�   $��1��6�   $��1��6�   5      5      �� 5   
   -   5        $��label1��5   C   $"��Saisie des nouvelles classes��6   $��28��67   $	��665��6Q   $��	20,20,0,0q�6]   $
��Left=�6p   ���	#FF86B5E8$��Center=�5   '   � 5   
   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��grdListe��5   @      AutoGenerateColumns .  $	��343��63   $	��665��6P   "   VerticalScrollBarVisibility$ Visible=�6\      SelectionMode$ Single=�6�      SelectionUnit$
 Cell=�6�   	   
IsReadOnly	 .  
   CanUserAddRows
 .  $
��Left=�5   *   $��20,0,0,0q�6E   $��Center=�6W    $� IsVirtualizing .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$	 N°��6D      
IsReadOnly . -   5        	grdNumero��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 60 5!   <   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5    )   �� *numero+5"        5#      $ Nom et Prenoms��6A    . -   5        
grdNom��$	 340 5$   9    5#   )   �� *nom+5%        5&      $ 	Matricule��5'      -   5        grdMatricule�� .  $	 120 5'   S    5&   )   �� *	matricule+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�5(      4  .System.Windows.Controls.DataGridComboBoxColumn  5,      $	 120 6-   $ Nouvelle classe��69   -   5        
classe��   SelectedValueBinding 5,   b   �� *
codeclasse+$ TwoWay=�   DisplayMemberPath 5-      �� *
codeclasse+5/      �� 50      -   5        $��image1��50   6   $��67��6   $��688,-1,0,277q�6    $<��6/School%20Brain;component/Images/Logo-Brain-Studio.png��6D   $
��Fill=�6�   �� 51      -   5        $��image2��51   5   $��67��6   $��688,276,1,0q�6    $<��6/School%20Brain;component/Images/Logo-Brain-Studio.png��6C   $
��Fill=�6�   52      � 53   
   -   5        $��grid3��53      $��41��6   $	��821��6(   ���	#FF86B5E8$
��0,20q�6K    54      �� 55       57      �� 58      �� 59      .+�5;      �� -   5        $��cmdImprimer��5;   b   $��Imprimer��6   $��23��6(   $
��Left=�64   $��	14,12,0,0q�6O   $	��Top=�6u   $	��150��6�   6�   �� 5<      -   5        $��	cmdFermer��5<   q   $��Fermer��6   $��1��6&   $��23��66   $
��Left=�6B   $��
253,13,0,0q�6]   $	��Top=�6�   $	��146��6�   6�   5=      5>      5?      