   M S B A M L   `   `   `  ����  School Brain!    Ecole.UI.HistoriqueCaisseUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$.  (School Brain : Historique des operations��5   	   $	��744��6:   $	��867��6G    � WindowStartupLocation$ CenterScreen=�6S    � 
ResizeMode$ CanMinimize=�6x    � 	Resources 5      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value ��Name �� Path	 �� StringFormat
 �� RelativeSource �� AncestorType(  "System.Windows.Controls.DatePicker��%�   '        �� 5   
   "  6   .�5      ��  � �� 5      �� 5	      .R�5
      �� $ PART_TextBox��6"   ��5      �� $ SelectedDate�$	 
dd-MM-yyyy��
 �� "  5      5      5      5      .��5      � ���	#FFC8DDE2$��Center=�6"   $��Center=�6?   $	��838��6Z   $	��693��6f   .+�5   
   �� -   5        $��
rectangle1��5   <   $	��693��6   $
��Left=�6!   
���   �$	��Top=�6]   $	��826��6u   ���	#FF86B5E86�   �� 5   
   -   5        $��lblDateDebut��5   _   $��
Date debut��6   $��28��6%   $
��Left=�61   $��	19,26,0,0q�6L   $	��Top=�6s     5   
   -   5        $��dpiDateDebut��5   O   $��25��6   $
��Left=�6!   $��	94,27,0,0q�6<   $	��Top=�6c   $	��115��6{   6�   �� 5   
   -   5        $��
lblDateFin��5   ^   $��Date fin��6   $��28��6#   $
��Left=�6/   $��
215,26,0,0q�6J   $	��Top=�6p     5   
   -   5        $��
dpiDateFin��5   P   $��25��6   $
��Left=�6!   $��
265,27,0,0q�6<   $	��Top=�6b   $	��115��6z   6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��grdEtatCaisse��5         AutoGenerateColumns .  $	��398��5   /    $� IsVirtualizing .  $
��Left=�6j   $��	27,70,0,0q�6�      CanUserAddRows .  $	��Top=�5   (   $	��785��6@   $   HorizontalScrollBarVisibility$ Visible=�6L   "   VerticalScrollBarVisibility$ Visible=�6t      CanUserResizeColumns .     CanUserSortColumns .     CanUserReorderColumns .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5   �   ��6i   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$	 N°��6@      
IsReadOnly . -   5        	grdCodeOP��   Width$ 50 5   <   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   %   �� *numero+5        5      $ Motif��5       -   5        grdMotif�� . $	 200 5    J    5   %   �� *motif+ �� Mode$ TwoWay=�5!        5"      $ Entrée��5#      -	   5        	grdEntree�� . $	 120 5#   L    5"   %   �� *entree+$ TwoWay=�5$        5%      $ Sortie��5&      -
   5        	grdSortie�� . $	 120 5&   L    5%   %   �� *sortie+$ TwoWay=�5'        5(      $
 Date��5)      -   5        grdDate�� . $	 100 5)   H    5(   %   �� *date+$ TwoWay=�5*        5+      $ Concerne��5,      -   5        grdConcerne�� . $	 160 5,   Q    5+   %   �� *
concerne+$ TwoWay=�5-      5/      � 51   
   -   5        $��grid1��51   K   $	��140��6   $
��Left=�6   $��
10,490,0,0q�67   $	��Top=�6X   $	��802��6p   ���	#FF86B5E8 � RowDefinitions 52      �� 53      �� 54      �� 55      �� 56      �� 57       � ColumnDefinitions 59      �� 5:      �� 5;      �� 5<      .+�5>      �� -   5        $��
lblEntrees��5>   b   $��Entrées sur la periode��6   $
��Left=�65   $��66,1,0,0q�6P   $	��133��6t   $��2��6�   �� 5?      -   5        $��
lblSorties��5?   \   $��Sorties sur la periode��6   $��28��65   $
��Left=�6A   $	��Top=�6n   $	��127��6�   $��71,1,0,0q�6�   $��1��6�   $��2��6�   $��2��6�   �� 5@      -   5        $��txtEntreePeriode��5@   O   $��27��6   $
��Left=�6"   $��46,0,0,0q�6=   $	��Top=�6g   $	��181��6   $
��Bold"�6�   $��3��6�   $��Times New Roman)�6�   $��14(�6�   �� 5A      -   5        $��txtSortiePeriode��5A   _   $��1��6   $��27��6&   $
��Left=�62   $��38,0,0,0q�6M   $	��Top=�6w   $	��201��6�   $
��Bold"�6�   $��3��6�   $��Times New Roman)�6�   $��14(�6�   �� 5B      -   5        $��lblSolde��5B   Z   $��Solde sur la periode��6   $��28��63   $
��Left=�6?   $	��Top=�6j   $	��155��6�   $��2��6�   $��64,1,0,0q�6�   $��2��6�   $��2��6�   �� 5C      -   5        $��txtSoldePeriode��5C   Z   $��2��6   $��2��6&   $��28��63   $
��Left=�6?   $	��Top=�6q   $	��184��6�   $��Normal"�6�   $��	44,27,0,0q�6�   $��Times New Roman)�6�   $��14(�6�   $��2��6�   �� 5D      -   5        $��label1��5D   j   $��Solde��6   $��1��6$   $��28��61   $
��Left=�6=   $��0,23,0,0q�6X   $	��Top=�6x   $	��155��6�   �� 5E      -   5        $��label2��5E   j   $��Total des sorties��6   $��28��60   $
��Left=�6<   $��	71,-1,0,0q�6W   $	��Top=�6x   $��1��6�   �� 5F      -   5        $��label3��5F   i   $��Total des entrées��6   $��28��60   $
��Left=�6<   $��72,1,0,0q�6W   $	��Top=�6w   $��2��6�   �� 5G      -   5        $��label5��5G   n   $��Solde��6   $��2��6$   $��28��64   $
��Left=�6@   $��	113,0,0,0q�6[   $	��Top=�6|   �� 5H      -   5        $��txtTotalEntree��5H   P   $��1��6   $
��Left=�6#   $��46,0,0,0q�6>   $	��181��6f   $��Times New Roman)�6r   $��14(�6�   �� 5I      -   5        $��txtTotalSortie��5I   l   $��1��6   $��1��6&   $��27��63   $
��Left=�6?   $��37,1,0,0q�6Z   $	��Top=�6�   $	��202��6�   $��Times New Roman)�6�   $��14(�6�   �� 5J      -   5        $��txtTotalSolde��5J   R   $��2��6   $��1��6&   $��26��63   $��	45,2,39,0q�6?   $	��Top=�6g   $��Times New Roman)�6   $��14(�6�   5K      �� 5L   
   -   5        $��lblMotif��5L   [   $��Motif��6   $��28��6    $
��Left=�6,   $��
407,27,0,0q�6G   $	��Top=�6k   �� 5M   
   -   5        $��txtMotif��5M   M   $��23��6   $
��Left=�6   $��
475,26,0,0q�69   $	��Top=�6]   $	��120��6u   6�   �� 5N   
   -   5        $��cmdImprimer��5N   K   $��Imprimer��6   $��33��6$   $
��Left=�60   $	��Top=�6^   $	��155��6v   $��
10,652,0,0q�6�   �� 5O   
   -   5        $��
cmdQuitter��5O   I   $��Fermer��6   $��33��6"   $
��Left=�6.   $	��Top=�6[   $	��155��6s   $��657,652,0,0q�6�   5P      5Q      