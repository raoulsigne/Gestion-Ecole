   M S B A M L   `   `   `  ����  School Brain$    Ecole.UI.WindowAddSetArticleUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$9  3School brain : Définir un set (package) d'articles��5   	   $	��498��6D   $	��745��6Q    � WindowStartupLocation$ CenterScreen=�6]   ���	#FFD0DAE5 � 
ResizeMode$ CanMinimize=�6�   .��5	      � $	��722��6   ���	#FFC8DDE2$��Center=�6.   $��Center=�6K    � RowDefinitions 5
   
   �� 5      $	��402 �6   �� 5      $��57 �6   .+�5   
   �� -   5        $��	groupBox1��5   6   $��Informations obligatoires��6   $��22,13,308,217q�6G   $	��172��6^   .��5      � -   5        $��grid1��5       $	��128��6   $	��342��6-   ���	#FF86B5E8 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      $
��124* �6'   �� 5      $
��218* �6'   .+�5      �� -   5        $��
lblCodeSet��5   r   $��Code du Set Article (*)��6   $��28��6:   $
��Left=�6F   $��0,6,0,0q�6a   $	��Top=�6�   $��2��6�   $��2��6�   $	��133��6�   �� 5      -   5        $��
txtCodeSet��5   S   $��23��6   $
��Left=�6&   $��23,6,0,0q�6A   $	��Top=�6e   $	��167��6}   $��1��6�   �� 5      -   5        $��	lblNomSet��5   E   $��Nom du Set Article (*)��6   $��31��69   $	��Top=�6V   $��1��6n   $��2��6{   $��	0,0,209,0q�6�   �� 5      -   5        $��	txtNomSet��5   T   $��23��6   $��Right=�6&   $��0,4,27,0q�6B   $	��Top=�6e   $	��167��6}   $��1��6�   $��1��6�   �� 5      -   5        $��lblAnnee��5   S   $��
Année (*)��6   $��28��6,   $
��Left=�68   $	��Top=�6c   $	��112��6{   $��2��6�   �� 5      -   5        $��txtAnneeScolaire��5   S   $��21��6   $
��Left=�6&   $��23,7,0,0q�6A   $	��Top=�6k   $��69��6�   $��1��6�   $��2��6�   6�   �� 5       -	   5        $��label1��5    z   $��/��6   $��1��6$   $��2��64   $��28��6A   $
��Left=�6M   $��98,4,0,0q�6h   $	��Top=�6�   $��20��6�   �� 5!      -
   5        $��txtAnnee��5!   f   {�.  $��23��6,   $
��Left=�68   $��	118,5,0,0q�6S   $	��Top=�6v   $��73��6�   $��1��6�   $��2��6�   6�   �� 5"      -   5        $��
lblMontant��5"   f   $��Montant (*)��6   $��28��6.   $
��Left=�6:   $��0,3,0,0q�6U   $	��Top=�6x   $	��112��6�   $��3��6�   �� 5#      -   5        $��
txtMontant��5#   S   $��26��6   $
��Left=�6&   $��23,3,0,0q�6A   $	��Top=�6e   $	��167��6}   $��1��6�   $��3��6�   6�   5$      5%      � 5&   
   -   5        $��
gridAction��5&      $��40��6   $��2,17,0,0q�6-   ���	#FF86B5E8$��1��6V    5'      �� 5(       5*      �� 5+      �� 5,      �� 5-      .+�5/      �� -   5        $��
cmdValider��5/   `   $��Valider��6   $��32��6'   $
��Left=�63   $��20,2,0,0q�6N   $	��Top=�6r   $	��131��6�   6�   �� 50      -   5        $��
cmdAnnuler��50   `   $��Annuler��6   $��32��6'   $
��Left=�63   $��60,6,0,0q�6N   $	��Top=�6r   $	��138��6�   $��2��6�   51      �� 52   
   -   5        $��cmdSimpleFlecheDroite��52   \   $��>��6   $��27��6    $
��Left=�6,   $��330,239,0,0q�6G   $	��Top=�6y   $��48��6�   6�   �� 53   
   -   5        $��cmdSimpleFlecheGauche��53   \   $��<��6   $��29��6    $
��Left=�6,   $��330,272,0,0q�6G   $	��Top=�6y   $��48��6�   6�   �� 54   
   -   5        $��cmdDoubleFlecheDroite��54   K   $��>>��6   $��29��6$   $
��Left=�60   $	��Top=�6h   $��48��6�   $��330,327,0,0q�6�   �� 55   
   -   5        $��cmdDoubleFlecheGau��55   K   $��<<��6   $��28��6$   $
��Left=�60   $	��Top=�6e   $��48��6}   $��330,362,0,0q�6�   �� 56   
   -   5        $��	groupBox2��56   T   $��Articles disponibles��6   $	��194��61   $��24,208,398,0q�6>   .��57      &   System.Windows.Controls.DataGrid  -   5        $��grdListeArticle1��57   �      AutoGenerateColumns .     CanUserAddRows .  $	��165��6J      
IsReadOnly . $	��283��6�   ��6i   �� *ListeArticles1+   Columns 58      0  *System.Windows.Controls.DataGridTextColumn  59      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Code��6I   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 90 6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *codeArticle+6W     5:      $	 Designation��6I   $	
 180 6^    6)   �� *designation+6^   5<      5=      �� 5>   
   -   5        $��	groupBox3��5>   O   $��Articles du set��6   $	��194��6,   $��383,208,12,0q�69   .��5?        -   5        $��grdListeArticle2��5?   �    .   .  $	��166��6J    .  $	��284��6�   ��6j   �� *ListeArticles2+ 5@        5A      $
	 Code��6I      
IsReadOnly . $
 90 6i    6)   �� *codeArticle+6i     5B      $	 Designation��6I    . $	
 120 6p    6)   �� *designation+6p     5C      $	 	Quantité��6F    .  $
 60 6k    6)   �� *
quantite+6k   5E      5F      5G      5H      