   M S B A M L   `   `   `  ����  School Brain     Ecole.UI.EleveDuneClasseUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$4  .School Brain : Liste des élèves d'une classe��5   	   $	��625��6>   $
��1028��6K    � WindowStartupLocation$ CenterScreen=�6X    � 
ResizeMode$ CanMinimize=�6}   .��5      �� -   5        $��stackPanel1��5      $	��586��6   $
��1007��61   ���	#FFC8DDE2.+�5   
   � $	��144��6   .+�5      �� -   5        $��	groupBox1��5   G   $��Informations obligatoires��6   $	��130��6:   $	��609��6X   $
��Left=�6d   $��	15,10,0,0q�6   .��5      � -   5        $��grid1��5   #   $��77��6   $	��581��60   ���	#FF86B5E8 � RowDefinitions 5	      �� 5
      �� 5       � ColumnDefinitions 5      �� 5      $
��114* �6+   �� 5      $
��202* �6+   �� 5      $
��265* �6+   .+�5      �� -   5        $��lblAnneeScolaire��5   \   $��Année scolaire��6   $��28��65   $
��Left=�6A   $	��Top=�6t   $	��114��6�   $��1��6�   �� 5      -   5        $��	lblClasse��5   T   $��Classe��6   $��28��6-   $
��Left=�69   $	��Top=�6e   $	��114��6}   �� 5      -   5        $��	cmbClasse��5   h   $��1��6   $��23��6/   $
��Left=�6;   $��21,6,0,0q�6V   $	��Top=�6y   $	��169��6�   6�   �� 5      -   5        $��txtAnneeScolaire��5   t   $��1��6   $��1��6.   $��23��6;   $
��Left=�6G   $��21,4,0,0q�6b   $	��Top=�6�   $��73��6�   6�   �� 5      -   5        $��lblNomClasse��5   p   $�� ��6   $��2��6'   $��28��67   $
��Left=�6C   $��14,1,0,0q�6^   $	��Top=�6�   $	��237��6�   �� 5      -	   5        $��txtAnnee��5   j   {�.  $��23��60   $
��Left=�6<   $��	100,4,0,0q�6W   $	��Top=�6z   $��90��6�   $��1��6�   $��1��6�   5      5      �� 5      -
   5        $��image2��5   (   $��
862,0,0,79q�6   $<��6/School%20Brain;component/Images/Logo-Brain-Studio.png��66   $
��Fill=�6v   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��grdListe��5   <      AutoGenerateColumns .  $	��343��6/   $	��964��6L   "   VerticalScrollBarVisibility$ Visible=�6X      CanUserAddRows .  $��20q�5   *    $� IsVirtualizing .  	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$	
 N°��6@      
IsReadOnly . -   5        	grdNumero��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 5    <   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   %   �� *numero+5!        5"      $
 	Matricule��6C    . -   5        grdMatricule��$	 100 5#   ?    5"   %   �� *	matricule+5$      4  .System.Windows.Controls.DataGridTemplateColumn  5%      $	 240 6)      HeaderTemplate 5&      �� 5'      .R�5(      ��  �� Orientation$ 
Horizontal=�6)   .+�5)   "   �� $	��Nom��6(   $��25��66   $
��20,0q�6B   6R   �� 5*   "   $��NameSearchBox��6*   -   5        $	��100��5*   ]   ��6?   �� *FilterString+6i   5+      5,         CellTemplate 5.      �� 5/      .R�50      �� ��6(   �� *nom+51      53        54      $
 Date de naissance��55      -   5        grdDateNaissance�� . $	 120 55   ^    54   %   �� *datenaissance+56        57      $
 Téléphone��58      -   5        grdTelephone�� . $	 150 58   R    57   %   �� *	telephone+59        5:      $
 Téléphone d'un parent��5;      -   5        grdTelparent�� . $	 140 5;   ^    5:   %   �� *	telparent+5<        5=      $
 Adresse��5>      -   5        
grdAdresse�� . $	 190 5>   N    5=   %   �� *	adresse+5?      5A      � 5B   
   -   5        $��grid3��5B      $��41��6   $
��1003��6(   ���	#FF86B5E8$
��0,20q�6L    5C      �� 5D       5F      �� 5G      �� 5H      .+�5J      �� -   5        $��cmdImprimer��5J   b   $��Imprimer��6   $��23��6(   $
��Left=�64   $��	20,12,0,0q�6O   $	��Top=�6u   $	��150��6�   6�   �� 5K      -   5        $��	cmdFermer��5K   q   $��Fermer��6   $��1��6&   $��23��66   $
��Left=�6B   $��
336,12,0,0q�6]   $	��Top=�6�   $	��146��6�   6�   5L      5M      5N      