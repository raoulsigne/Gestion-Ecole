   M S B A M L   `   `   `  ����  School Brain    Ecole.UI.EtatVersementUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$'  !School Brain : BILAN DE VERSEMENT��5   	   $	��643��63   $
��1029��6@    � WindowStartupLocation$ CenterScreen=�6M    � 
ResizeMode$ CanMinimize=�6r    � 	Resources 5      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value ��Name �� Path	 �� StringFormat
 �� RelativeSource �� AncestorType(  "System.Windows.Controls.DatePicker��%�   '        �� 5   
   "  6   .�5      ��  � �� 5      �� 5	      .R�5
      �� $ PART_TextBox��6"   ��5      �� $ SelectedDate�$	 
dd-MM-yyyy��
 �� "  5      5      5      5      .��5      � ���	#FFC8DDE2$��Center=�6"   $��Center=�6?   $
��1006��6Z   $	��606��6g    � RowDefinitions 5   
   �� 5      $
��646* �6   �� 5      $	��47* �6   .+�5   
   �� -   5        $��
rectangle1��5   <   $	��605��6   $
��Left=�6!   
���   �$	��Top=�6]   $	��994��6u   ���	#FF86B5E8$��2��6�   �� 5   
   -   5        $��lblDateDebut��5   _   $��
Date debut��6   $��28��6%   $
��Left=�61   $��	19,26,0,0q�6L   $	��Top=�6s     5   
   -   5        $��dpiDateDebut��5   O   $��25��6   $
��Left=�6!   $��	94,27,0,0q�6<   $	��Top=�6c   $	��115��6{   6�   �� 5   
   -   5        $��
lblDateFin��5   ^   $��Date fin��6   $��28��6#   $
��Left=�6/   $��
215,26,0,0q�6J   $	��Top=�6p     5   
   -   5        $��
dpiDateFin��5   P   $��25��6   $
��Left=�6!   $��
265,27,0,0q�6<   $	��Top=�6b   $	��115��6z   6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��grdEtatCaisse��5         AutoGenerateColumns .  $	��398��5   /    $� IsVirtualizing .  $
��Left=�6j   $��	27,70,0,0q�6�      CanUserAddRows .  $	��Top=�5   (   $	��955��6@   $   HorizontalScrollBarVisibility$ Visible=�6L   "   VerticalScrollBarVisibility$ Visible=�6t      CanUserResizeColumns .     CanUserSortColumns .     CanUserReorderColumns .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5   �   ��6i   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$ Motif��5!      -   5        
grdLibelle��   
IsReadOnly .    Width$	 250 5!   L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5    %   �� *	libelle+ �� Mode$ TwoWay=�5"        5#      $ Montant��5$      -   5        
grdMontant�� . $	 150 5$   N    5#   %   �� *
smontant+$ TwoWay=�5%        5&      $
 Date��5'      -	   5        grdDate�� . $	 120 5'   H    5&   %   �� *date+$ TwoWay=�5(        5)      $ Concerne��5*      -
   5        
grdNom�� . $	 300 5*   L    5)   %   �� *nom+$ TwoWay=�5+        5,      $ Classe��5-      -   5        	grdClasse�� . $	 200 5-   M    5,   %   �� *classe+$ TwoWay=�5.      50      � 52   
   -   5        $��grid1��52   J   $��34��6   $
��Left=�6   $��
10,495,0,0q�66   $	��Top=�6W   $	��370��6o   ���	#FF86B5E8 53      �� 54       � ColumnDefinitions 56      �� 57      $
��149* �6#   �� 58      $
��221* �6#   .+�5:      �� -   5        $��label3��5:   i   $��Total des entrées��6   $��28��60   $
��Left=�6<   $��17,0,0,0q�6W   $	��Top=�6w   �� 5;      -   5        $��txtTotalEntree��5;   O   $��26��6   $
��Left=�6"   $��23,2,0,0q�6=   $	��Top=�6e   $	��178��6}   $��1��6�   $
��Bold"�6�   5<      �� 5=   
   -   5        $��lblMotif��5=   [   $��Motif��6   $��28��6    $
��Left=�6,   $��
408,24,0,0q�6G   $	��Top=�6k   �� 5>   
   -   5        $��txtMotif��5>   M   $��23��6   $
��Left=�6   $��
453,26,0,0q�69   $	��Top=�6]   $	��120��6u   6�   �� 5?   
   -   5        $��cmdImprimer��5?   K   $��Imprimer��6   $��23��6$   $
��Left=�60   $	��Top=�6^   $	��155��6v   $��
27,557,0,0q�6�   $��2��6�   �� 5@   
   -   5        $��
cmdQuitter��5@   I   $��Fermer��6   $��23��6"   $
��Left=�6.   $	��Top=�6[   $	��155��6s   $��641,557,0,0q�6�   $��2��6�   5A      5B      