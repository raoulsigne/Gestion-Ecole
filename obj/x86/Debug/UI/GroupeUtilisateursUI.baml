   M S B A M L   `   `   `  ����  School Brain#    Ecole.UI.GroupeUtilisateursUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$7  1School Brain : Gestion des groupes d'utilisateurs��5   	   $	��474��6C   $	��559��6P    � WindowStartupLocation$ CenterScreen=�6\    � 
ResizeMode$ CanMinimize=�6�   .��5      �� -   5        $��stackPanel1��5      $	��431��6   $	��526��61   ���	#FFC8DDE2.+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5        $��	groupBox1��5   C   $��Informations requises��6   $	��112��66   $	��380��6T   $
��Left=�6`   $��15,15q�6{   .��5      � -   5        $��grid1��5   #   $��69��6   $	��342��60   ���	#FF86B5E8 � ColumnDefinitions 5	      �� 5
      $
��134* �6+   �� 5      $
��231* �6+   .+�5      �� -   5        $��lblRole��5   c   $��Rôle��6   $��28��6+   $
��Left=�67   $��7,8,0,0q�6R   $	��Top=�6r   $	��114��6�   �� 5      -   5        $��lblDescription��5   j   $��Description��6   $��28��62   $
��Left=�6>   $��6,0,0,1q�6Y   $��Bottom=�6�   $	��114��6�   �� 5      -   5        $��txtRole��5   f   $��1��6   $��23��6.   $
��Left=�6:   $��1,8,0,0q�6U   $	��Top=�6u   $	��179��6�   �� 5      -   5        $��txtDescription��5   g   $��1��6   $��23��6.   $
��Left=�6:   $��0,40,0,0q�6U   $	��Top=�6}   $	��179��6�   5      5      � 5      -   5        $��grid2��5       $	��135��6   $��85��6-   $��10,0,0,0q�68    � RowDefinitions 5      �� 5      �� 5      �� 5       5      �� 5      .+�5      �� -	   5        $��cmdEnregistrer��5   h   $��Enregistrer��6   $��23��6/   $
��Left=�6;   $��4,38,0,0q�6V   $	��Top=�6~   $��75��6�   $��2��6�   �� 5      -
   5        $��
cmdAnnuler��5   q   $��Annuler��6   $��1��6+   $��23��68   $
��Left=�6D   $��6,35,0,0q�6_   $	��Top=�6�   $��75��6�   $��2��6�   5      5      �� 5    
   $��Liste des groupes��6   .��5!      &   System.Windows.Controls.DataGrid  -   5        $��grdListe��5!   @      AutoGenerateColumns .  $	��200��63   $	��472��6P   $��10q�6\   "   VerticalScrollBarVisibility$ Visible=�6h    $� IsVirtualizing .  	   CanUserAddRows	 .  
   
IsReadOnly
 . ��5"   <   ��    Columns 5#      0  *System.Windows.Controls.DataGridTextColumn  5$      ,  &System.Windows.Controls.DataGridColumn   Header$ Rôles��6>      
IsReadOnly . -   5        grdCode��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 130 5%   :   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5$   %   �� *role+5&        5'      $ Descriptions��6E    . -   5        
grdNom��$	 300 5(   9    5'   %   �� *description+5)      5+      5,      � 5-   
   -   5        $
��grid��5-      $��40��6   $	��527��6'   $��0,10,0,0q�63   ���	#FF86B5E8 5.      �� 5/       51      �� 52      �� 53      �� 54      .+�56      �� -   5        $��cmdImprimer��56   j   $��Imprimer la liste��6   $��23��61   $
��Left=�6=   $��9,11,0,0q�6X   $	��Top=�6}   $	��138��6�   $��2��6�   �� 57      -   5        $��	cmdFermer��57   `   $��Fermer��6   $��23��6&   $
��Left=�62   $��	27,11,0,0q�6M   $	��Top=�6q   $	��145��6�   6�   58      59      5:      