   M S B A M L   `   `   `  ����  School Brain    Ecole.UI.BilanFinancierUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$.  (School Brain : Historique des operations��5   	   $	��700��6:   $	��828��6G    � WindowStartupLocation$ CenterScreen=�6S    � 
ResizeMode$ CanMinimize=�6x    � 	Resources 5      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value ��Name �� Path	 �� StringFormat
 �� RelativeSource �� AncestorType(  "System.Windows.Controls.DatePicker��%�   '        �� 5   
   "  6   .�5      ��  � �� 5      �� 5	      .R�5
      �� $ PART_TextBox��6"   ��5      �� $ SelectedDate�$	 
yyyy-MM-dd��
 �� "  5      5      5      5      .��5      � ���	#FFC8DDE2$��Center=�6"   $��Center=�6?   $	��807��6Z   $	��653��6f   .+�5   
   �� -   5        $��
rectangle1��5   <   $	��639��6   $
��Left=�6!   
���   �$	��Top=�6]   $	��782��6u   ���	#FF86B5E8$��12,0,0,0q�6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��grdBilanFinancer��5         AutoGenerateColumns .  $	��398��5   /    $� IsVirtualizing .  $
��Left=�6j   $��	27,70,0,0q�6�      CanUserAddRows .  $	��Top=�5   +   $	��754��6C   $   HorizontalScrollBarVisibility$ Visible=�6O   "   VerticalScrollBarVisibility$ Visible=�6w      CanUserResizeColumns .     CanUserSortColumns .     CanUserReorderColumns .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5   �   ��6i   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$	 N°��6@      
IsReadOnly . -   5        	grdNumero��   Width$ 50 5   <   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   %   �� *numero+5        5      $ 
Prestation��5      -   5        grdPrestation�� . $	 200 5   T    5   %   �� *
prestation+ �� Mode$ TwoWay=�5        5      $ A Payer��5      -   5        	grdSortie�� . $	 120 5   M    5   %   �� *	SAPayer+$ TwoWay=�5         5!      $ Payé��5"      -   5        grdPaye�� . $	 120 5"   H    5!   %   �� *Spaye+$ TwoWay=�5#        5$      $ Remise��5%      -   5        	grdRemise�� . $	 120 5%   M    5$   %   �� *	Sremise+$ TwoWay=�5&        5'      $ Reste à payer��5(      -   5        grdReste�� . $	 120 5(   S    5'   %   �� *Sreste+$ TwoWay=�5)      5+      � 5-   
   -	   5        $��grid1��5-   J   $��88��6   $
��Left=�6   $��
27,490,0,0q�66   $	��Top=�6W   $	��754��6o   ���	#FF86B5E8 � RowDefinitions 5.      �� 5/      �� 50       � ColumnDefinitions 52      �� 53      $
��181* �6#   �� 54      $
��211* �6#   �� 55      $
��213* �6#   �� 56      $
��178* �6#   .+�58      �� -
   5        $��
lblEntrees��58   c   $��Total du reste à payer��6   $
��Left=�65   $��	50,1,0,17q�6P   $	��155��6u   $��1��6�   $��2��6�   $��14(�6�   �� 59      -   5        $��txtResteAPayer��59   O   $��27��6   $
��Left=�6"   $��12,1,0,0q�6=   $	��Top=�6e   $	��152��6}   $��Normal"�6�   $��1��6�   $��Times New Roman)�6�   $��18(�6�   $��3��6�   ���	#FFE517176�   �� 5:      -   5        $��label2��5:   g   $��Total déjà payé��6   $��28��6.   $
��Left=�6:   $��54,0,0,0q�6U   $	��Top=�6u   $��1��6�   $��14(�6�   $	��120��6�   �� 5;      -   5        $��label3��5;   e   $��Total à payer��6   $��28��6,   $
��Left=�68   $��54,6,0,0q�6S   $	��Top=�6s   $��14(�6�   $	��120��6�   �� 5<      -   5        $��label5��5<   y   $��Total des remises��6   $��2��60   $��28��6@   $
��Left=�6L   $��49,6,0,0q�6g   $	��Top=�6�   $��14(�6�   $	��133��6�   �� 5=      -   5        $��txtTotalAPayer��5=   1   $
��Left=�6   $	��181��6G   $��Times New Roman)�6S   $��18(�6p   $��1��6~   $��0,6,0,10q�6�   $
��Bold"�6�   ���	#FF106C896�   �� 5>      -   5        $��txtTotalDejaPaye��5>   Z   $��1��6   $��1��6&   $��27��63   $
��Left=�6?   $	��Top=�6r   $	��181��6�   $��Times New Roman)�6�   $��18(�6�   $��0,1,0,0q�6�   $��Normal"�6�   ���	#FF129A126�   �� 5?      -   5        $��txtTotalRemise��5?   9   $��3��6   $��	12,6,7,10q�6&   $��Times New Roman)�6O   $��18(�6l   $
��Bold"�6z   5@      �� 5A   
   -   5        $��cmdImprimer��5A   K   $��Imprimer��6   $��33��6$   $
��Left=�60   $	��Top=�6^   $	��155��6v   $��
27,592,0,0q�6�   6�   �� 5B   
   -   5        $��
cmdQuitter��5B   I   $��Fermer��6   $��33��6"   $
��Left=�6.   $	��Top=�6[   $	��155��6s   $��626,592,0,0q�6   6�   �� 5C   
   -   5        $��lbltitre��5C   h   $��BILAN FINANCIER��6   $��28��6*   $��Center=�66   $��301,0,320,0q�6S   $	��Top=�6x   $��Center=�6�   $��Center=�6�   $��18(�6�   $��Times New Roman)�6�   $	��186��6  �� 5D   
   -   5        $��lblAnnee��5D   �   $��Renseigner l'année :��6   $��28��6/   $
��Left=�6;   $��Center=�6V   $��
241,32,0,0q�6z   $	��Top=�6�   $	��141��6�   �� 5E   
   -   5        $��txtAnnee��5E   _   $��23��6   $
��Left=�6   {�.  $��
454,36,0,0q�6K   $	��Top=�6o   $��76��6�   �� 5F   
   -   5        $��txtAnneeScolaire��5F   M   $��23��6   $
��Left=�6   $��
388,36,0,0q�69   $	��Top=�6e   $��60��6}   6�   5G      5H      