   M S B A M L   `   `   `  ����  School Brain    Ecole.UI.JournalCaisseUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$&   School Brain : Etat de la caisse��5   	   $	��649��62   $	��867��6?    � WindowStartupLocation$ CenterScreen=�6K    � 
ResizeMode$ CanMinimize=�6p    � 	Resources 5      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value ��Name �� Path	 �� StringFormat
 �� RelativeSource �� AncestorType(  "System.Windows.Controls.DatePicker��%�   '        �� 5   
   "  6   .�5      ��  � �� 5	      �� 5
      .R�5      �� $ PART_TextBox��6"   ��5      �� $ SelectedDate�$	 
yyyy-MM-dd��
 �� "  5      5      5      5      .��5      � ���	#FFC8DDE2$��Center=�6"   $��Center=�6?   $	��843��6Z   $	��650��6f   .+�5   
   �� -   5        $��
rectangle2��5   ^   ���	#FF86B5E8$��69��6%   $
��Left=�61   $��5,23,0,0q�6L   
���   �$	��Top=�6   $	��830��6�   �� 5   
   -   5        $��
rectangle1��5   <   $	��466��6   $
��Left=�6!   
���   �$	��Top=�6]   $	��830��6u   ���	#FF86B5E8$��5,98,0,0q�6�   �� 5   
   -   5        $��lblOperation��5   `   $��Utilisateur��6   $��28��6&   $
��Left=�62   $��	20,29,0,0q�6M   $	��Top=�6t   �� 5   
   -   5        $��cmbLogin��5   M   $��23��6   $
��Left=�6   $��	86,31,0,0q�6:   $	��Top=�6]   $	��110��6u   6�   �� 5   
   -   5        $��lblDateDebut��5   `   $��
Date debut��6   $��28��6%   $
��Left=�61   $��
204,30,0,0q�6L   $	��Top=�6t     5   
   -   5        $��
dpiDateFin��5   P   $��25��6   $
��Left=�6!   $��
451,32,0,0q�6<   $	��Top=�6b   $	��115��6z   6�     5   
   -   5        $��dpiDateDebut��5   P   $��25��6   $
��Left=�6!   $��
274,32,0,0q�6<   $	��Top=�6d   $	��115��6|   6�   �� 5   
   -   5        $��
lblDateFin��5   ^   $��Date fin��6   $��28��6#   $
��Left=�6/   $��
396,31,0,0q�6J   $	��Top=�6p   &   System.Windows.Controls.DataGrid  5   
   -	   5        $��
grdJournal��5         AutoGenerateColumns .  $	��449��5   /   $
��Left=�5      $��
12,107,0,0q�6&      CanUserAddRows .   $� IsVirtualizing .  $	��Top=�5      $	��815��65   $   HorizontalScrollBarVisibility$ Visible=�6A   "   VerticalScrollBarVisibility$ Visible=�6i      CanUserResizeColumns .     CanUserSortColumns .     CanUserReorderColumns .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6�   ��6�   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$	 Num��6=      
IsReadOnly . -
   5        	grdColNum��   Width$ 40 5!   <   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5    %   �� *num+5"        5#      $ Utilisateur��6?    . -   5        grdColLogin��$ 80 5$   >    5#   %   �� *login+5%        5&      $ Description��5'      -   5        	grdColNom�� . $	 130 5'   Q    5&   %   �� *nom+ �� Mode$ TwoWay=�5(        5)      $ Action��5*      -   5        grdColAction�� . $	 375 5*   O    5)   %   �� *action+$ TwoWay=�5+        5,      $ Date ��5-      -   5        
grdColDate�� . $ 90 5-   L    5,   %   �� *date+$ TwoWay=�5.        5/      $ Heure ��50      -   5        
grdColHeur�� . $ 90 50   M    5/   %   �� *heure+$ TwoWay=�51      54      �� 55   
   -   5        $��lblMotif��55   \   $��Filtre��6   $��28��6!   $
��Left=�6-   $��
574,30,0,0q�6H   $	��Top=�6l   �� 56   
   -   5        $��	txtFiltre��56   M   $��23��6   $
��Left=�6   $��
622,31,0,0q�69   $	��Top=�6^   $	��205��6v   6�   �� 57   
   -   5        $��stackPanel1��57   f   ���	#FF86B5E8$��55��6,   $
��Left=�68   $��	5,570,0,0q�6S    �� Orientation$ 
Horizontal=�6y   $	��Top=�6�   $	��830��6�   .+�58      �� -   5        $��cmdImprimer��58   O   $��Imprimer��6   $��25��6(   $
��Left=�64   $	��Top=�6b   $	��155��6z   $��	70,15,0,0q�6�   6�   �� 59      -   5        $��
cmdAnnuler��59   N   $��Annuler��6   $��25��6'   $
��Left=�63   $	��Top=�6`   $	��155��6x   $��	80,15,0,0q�6�   �� 5:      -   5        $��
cmdQuitter��5:   M   $��Fermer��6   $��25��6&   $
��Left=�62   $	��Top=�6_   $	��155��6w   $��
100,15,0,0q�6�   5;      �� 5<   
   -   5        $��lblNom��5<   U   $�� ��6   $��29��6   $
��Left=�6'   $��	88,61,0,0q�6B   $	��Top=�6c   $	��480��6{   5=      5>      