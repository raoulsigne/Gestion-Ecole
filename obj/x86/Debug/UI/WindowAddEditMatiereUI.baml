   M S B A M L   `   `   `  ����  School Brain%    Ecole.UI.WindowAddEditMatiereUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$1  +School brain : Creer / Editer des Matières��5   	   $	��618��6<   $	��628��6I   ���	#FFD0DAE5 � 
ResizeMode$ CanMinimize=�6l   .��5	      � $	��605��6   ���	#FFC8DDE2$	��580��6.    � RowDefinitions 5
   
   �� 5      $
��597* �6   �� 5      $	��33* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��
-2,3,0,359q�6.   .+�5      �� -   5        $��	groupBox1��5   G   $��Informations obligatoires��6   $	��155��6:   $	��380��6X   $��15,15q�6d   .��5      � -   5        $��grid1��5   $   $	��107��6   $	��342��61   ���	#FF86B5E8 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      $
��124* �6+   �� 5      $
��218* �6+   .+�5      �� -   5        $��txtCode��5   V   $��25��6   $
��Left=�6*   $��0,3,0,0q�6E   $	��Top=�6e   $	��184��6}   $��1��6�   �� 5      -   5        $��txtNom��5   E   $��22��6   $
��Left=�6*   $	��Top=�6S   $	��184��6k   $��1��6w   $��1��6�   $��0,6,0,0q�6�   �� 5      -   5        $��lblCode��5   h   $��Code (*)��6   $��28��6/   $
��Left=�6;   $��0,-1,0,0q�6V   $	��Top=�6w   $��97��6�   �� 5      -   5        $��lblNom��5   U   $��Nom (*)��6   $��28��6.   $
��Left=�6:   $	��Top=�6c   $��95��6{   $��1��6�   �� 5      -   5        $��lblName��5   R   $
��Name��6   $��23��6+   $
��Left=�67   $	��Top=�6a   $��2��6y   $��97��6�   �� 5       -   5        $��lblAnnee��5    W   $��
Année (*)��6   $��24��60   $
��Left=�6<   $	��Top=�6g   $��3��6   $��95��6�   �� 5!      -	   5        $��txtName��5!   V   $��20��6   $
��Left=�6*   $��0,4,0,0q�6E   $	��Top=�6e   $	��184��6}   $��1��6�   $��2��6�   �� 5"      -
   5        $��txtAnneeScolaire��5"   V   $��21��6   $
��Left=�6*   $��0,2,0,0q�6E   $	��Top=�6n   $��69��6�   $��1��6�   $��3��6�   6�   �� 5#      -   5        $��txtAnnee��5#   O   {�.  $��21��60   $��	93,2,56,0q�6<   $	��Top=�6_   $��1��6w   $��3��6�   6�   �� 5$      -   5        $��label1��5$   ~   $��/��6   $��1��6(   $��3��68   $��28��6E   $
��Left=�6Q   $��73,0,0,0q�6l   $	��Top=�6�   $��20��6�   5%      5&      � 5'      -   5        $��grid2��5'       $	��106��6   $	��118��6-   $��10q�69   ���	#FF86B5E8 5(      �� 5)      �� 5*       5,      �� 5-      $	��36* �6'   �� 5.      $��8* �6'   �� 5/      $	��74* �6'   .+�51      �� -   5        $��cmdEnregistrer��51   c   $��Valider��6   $��25��6+   $
��Left=�67   $��6,8,0,0q�6R   $	��Top=�6y   $	��106��6�   $��3��6�   �� 52      -   5        $��
cmdAnnuler��52   7   $��Annuler��6   $��24��6+   $	��106��6I   $��3��6n   $��	7,11,5,18q�6�   $��1��6�   53      54      &   System.Windows.Controls.DataGrid  55   
   -   5        $��grdListeMatiere��55   }      AutoGenerateColumns .     
IsReadOnly . $	��318��6A   $
��Left=�6N   $��
13,199,0,0q�6i   $	��Top=�6�   $	��577��6�    $� IsVirtualizing .  ��6�   �� *ListeMatieres+   Columns 56      0  *System.Windows.Controls.DataGridTextColumn  57      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 code��6A   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 80 6O   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *	codeMat+6O     58      $		 Nom��6@   $	
 186 6M    6%   �� *nomMat+6M     59      $
	 Name��6A   $	
 186 6O    6%   �� *	nameMat+6O     5:      $	 Année��6?   $	
 115 6N    6%   �� *annee+6N   5<      � 5=   
   -   5        $
��grid��5=      $��40��6   ���	#FF86B5E8$��	0,540,0,0q�6>   $��2��6Q    5>      �� 5?      $	��20* �6     5A      �� 5B      �� 5C      �� 5D      .+�5F      �� -   5        $��cmdImprimer��5F   j   $��Imprimer la liste��6   $��26��61   $
��Left=�6=   $��13,9,0,0q�6X   $	��Top=�6}   $	��160��6�   6�   �� 5G      -   5        $��	cmdFermer��5G   _   $��Fermer��6   $��26��6&   $
��Left=�62   $��28,9,0,0q�6M   $	��Top=�6p   $	��160��6�   $��2��6�   6�   5H      5I      5J      