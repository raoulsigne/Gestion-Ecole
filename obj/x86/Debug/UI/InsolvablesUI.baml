   M S B A M L   `   `   `  ����  School Brain    Ecole.UI.InsolvablesUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$)  #School Brain : Etat des Insolvables��5   	   $	��723��65   $	��962��6B    � WindowStartupLocation$ CenterScreen=�6N    � 
ResizeMode$ CanMinimize=�6s    � 	Resources 5      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value ��Name �� Path	 �� StringFormat
 �� RelativeSource �� AncestorType(  "System.Windows.Controls.DatePicker��%�   '        �� 5   
   "  6   .�5      ��  � �� 5	      �� 5
      .R�5      �� $ PART_TextBox��6"   ��5      �� $ SelectedDate�$	 
yyyy-MM-dd��
 �� "  5      5      5      5      .��5      � ���	#FFC8DDE2$��Center=�6"   $��Center=�6?   $	��941��6Z   $	��682��6f   .+�5   
   �� -   5        $��
rectangle1��5   <   $	��678��6   $
��Left=�6!   
���   �$	��Top=�6]   $	��936��6u   ���	#FF86B5E86�   �� 5   
   -   5        $��lblOperation��5   _   $��	Situation��6   $��28��6$   $
��Left=�60   $��
198,27,0,0q�6K   $	��Top=�6s   �� 5   
   -   5        $��cmbObservation��5   N   $��23��6   $
��Left=�6   $��
261,29,0,0q�6:   $	��Top=�6d   $	��110��6|   6�   �� 5   
   -   5        $��lblDateDebut��5   Z   $
��Date��6   $��28��6   $
��Left=�6+   $��
628,29,0,0q�6F   $	��Top=�6n   &   System.Windows.Controls.DataGrid  5   
   -   5        $��grdInsolvable��5         AutoGenerateColumns .  $	��558��5   /    $� IsVirtualizing .  $
��Left=�6j   $��5,70,0,0q�6�      CanUserAddRows .  $	��Top=�5   (   $	��924��6@   $   HorizontalScrollBarVisibility$ Visible=�6L   "   VerticalScrollBarVisibility$ Visible=�6t      CanUserResizeColumns .     CanUserSortColumns .     CanUserReorderColumns .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5   �   ��6i   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$	 Num��6@      
IsReadOnly . -   5        
grdNum��   Width$ 50 5   9   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   %   �� *numero+5        5      $ Noms et Prénoms��5       -   5        
grdNom�� . $	 240 5    R    5   %   �� *nom+ �� Mode$ TwoWay=�5!        5"      $ 	Matricule��5#      -   5        grdMatricule�� . $ 80 5#   R    5"   %   �� *	matricule+$ TwoWay=�5$        5%      $ 
Catégorie��5&      -	   5        
grdCat�� . $ 70 5&   L    5%   %   �� *	categorie+$ TwoWay=�5'        5(      $ A Payer��5)      -
   5        	grdApayer�� . $ 90 5)   N    5(   %   �� *	SaPayer+$ TwoWay=�5*        5+      $ Payé��5,      -   5        grdPaye�� . $ 90 5,   I    5+   %   �� *Spaye+$ TwoWay=�5-        5.      $ Remise��5/      -   5        	grdRemise�� . $ 90 5/   M    5.   %   �� *	Sremise+$ TwoWay=�50        51      $ Reste à Payer��52      -   5        grdResteAPayer�� . $ 90 52   Y    51   %   �� *SresteApayer+$ TwoWay=�53        54      $ 	Situation��55      -   5        grdObservation�� . $ 90 55   U    54   %   �� *observation+$ TwoWay=�56      58      �� 5:   
   -   5        $��lblMotif��5:   [   $��Année��6   $��28��6    $
��Left=�6,   $��
387,29,0,0q�6G   $	��Top=�6k   �� 5;   
   -   5        $��txtAnnee��5;   N   $��23��6   $��Right=�6   $��
0,31,438,0q�6:   $	��Top=�6^   $��71��6v   6�   �� 5<   
   -   5        $��cmdImprimer��5<   K   $��Imprimer��6   $��31��6$   $
��Left=�60   $	��Top=�6^   $	��155��6v   $��256,634,0,0q�6�   6�   �� 5=   
   -   5        $��	cmdFermer��5=   I   $��Fermer��6   $��31��6"   $
��Left=�6.   $	��Top=�6Z   $	��155��6r   $��525,634,0,0q�6~   6�   �� 5>   
   -   5        $��label6��5>   [   $��Classe��6   $��28��6!   $
��Left=�6-   $��	30,27,0,0q�6H   $	��Top=�6i   �� 5?   
   -   5        $��	cmbClasse��5?   M   $��23��6   $
��Left=�6   $��	73,29,0,0q�6:   $	��Top=�6^   $	��110��6v   6�   �� 5@   
   -   5        $��txtAnneeScolaire��5@   N   $��23��6   $��Right=�6   $��
0,31,331,0q�6:   $	��Top=�6f   $	��101��6~   {�.  6�     5A   
   -   5        $��dpiDate��5A   P   $��25��6   $
��Left=�6!   $��
672,29,0,0q�6<   $	��Top=�6_   $	��115��6w   6�   �� 5B   
   -   5        $��cmdAfficher��5B   _   $��Afficher��6   $��31��6$   $
��Left=�60   $��
793,26,0,0q�6K   $	��Top=�6r   $	��132��6�   6�   5C      5D      